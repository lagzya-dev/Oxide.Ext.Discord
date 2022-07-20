using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Exceptions.Entities.Websocket;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Interfaces.WebSockets;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.WebSockets.Handlers
{
    /// <summary>
    /// Handles the web socket connection and events
    /// </summary>
    public class WebSocketHandler
    {
        private readonly IWebSocketEventHandler _handler;
        
        private ClientWebSocket _socket;
        private CancellationTokenSource _source;
        private CancellationToken _token;
        internal Snowflake WebsocketId;
        
        private readonly ILogger _logger;

        private readonly ArraySegment<byte> _receiveBuffer;
        private readonly byte[] _sendBuffer;
        private readonly Encoding _encoding;
        private readonly int _maxSendChars;

        //private readonly ZlibDecompressorHandler _decompressor;
        
        private const int SendChunkSize = 1024;
        private const int ReceiveChunkSize = 1024;

        internal SocketState SocketState = SocketState.Disconnected;

        private Thread _thread;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handler">Handles for web socket events</param>
        /// <param name="logger"></param>
        public WebSocketHandler(IWebSocketEventHandler handler, ILogger logger)
        {
            _handler = handler;
            _logger = logger;
            _encoding = Encoding.UTF8;
            //_decompressor = new ZlibDecompressorHandler(_encoding, logger);
            _receiveBuffer = WebSocket.CreateClientBuffer(ReceiveChunkSize, SendChunkSize);
            _sendBuffer = new byte[SendChunkSize];
            //_receivedBuffer = new byte[ReceiveChunkSize * 4];
            _maxSendChars = _encoding.GetMaxCharCount(SendChunkSize);
        }

        /// <summary>
        /// Connects to the websocket at the given URL
        /// </summary>
        /// <param name="url"></param>
        /// <exception cref="WebsocketException"></exception>
        public void Connect(string url)
        {
            if (IsConnected() || IsConnecting())
            {
                throw new WebsocketException("Socket is already running. Please disconnect before attempting to connect.");
            }

            _thread?.Abort();
            
            WebsocketId = SnowflakeIdGenerator.Generate();
            SocketState = SocketState.Connecting;
            _source?.Dispose();
            _source = new CancellationTokenSource();
            _token = _source.Token;
            _socket = new ClientWebSocket();
            _socket.Options.KeepAliveInterval = TimeSpan.Zero;

            _thread = new Thread(RunInternal)
            {
                IsBackground = true
            };
            _thread.Start(url);
        }

        private async void RunInternal(object url)
        {
            await RunWebsocket((string)url);
        }
        
        private async Task RunWebsocket(string url)
        {
            Snowflake id = WebsocketId;
            try
            {
                _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(Connect)} Connecting Websocket To: {{0}}", url);
                await _socket.ConnectAsync(new Uri(url), _token);
                SocketState = SocketState.Connected;
                await _handler.SocketOpened(id);
                _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(Connect)} Websocket Connected To: {{0}}", url);
                await ReceiveHandler();
                if (IsConnected())
                {
                    SocketState = SocketState.Disconnecting;
                }
                _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(Connect)} Websocket Completed");
                _socket.Dispose();
                _socket = null;
            }
            catch (TaskCanceledException) { }
            catch (OperationCanceledException) { }
            catch (WebsocketException ex)
            {
                await _handler.SocketErrored(id, ex);
            }
            catch (Exception ex)
            {
                await _handler.SocketErrored(id, ex);
                _logger.Exception("A Unhandled Websocket Error Occured", ex);
            }
            finally
            {
                SocketState = SocketState.Disconnected;
            }
        }

        private async Task ReceiveHandler()
        {
            Snowflake id = WebsocketId;
            MemoryStream input = new MemoryStream();
            StreamReader reader = new StreamReader(input, _encoding);
            byte[] array = _receiveBuffer.Array ?? throw new ArgumentNullException();
            
            _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(ReceiveHandler)} Start Receive");
            while (!_token.IsCancellationRequested && (_socket.State == WebSocketState.Open || _socket.State == WebSocketState.CloseSent))
            {
                WebSocketReceiveResult result = await _socket.ReceiveAsync(_receiveBuffer, _token);
                if (_token.IsCancellationRequested)
                {
                    return;
                }

                await input.WriteAsync(array, 0, result.Count, _token);

                if (result.EndOfMessage)
                {
                    try
                    {
                        input.Position = 0;
                        string message = await reader.ReadToEndAsync();
                        _logger.Debug("Got Type: {0} Message: {1}", result.MessageType, message);
                        await ProcessReceivedMessage(id, result, message);
                    }
                    catch (Exception ex)
                    {
                        _logger.Exception("An error occured processing websocket message.", ex);
                    }
                    finally
                    {
                        input.SetLength(0);
                    }
                }
            }
        }

        private async Task ProcessReceivedMessage(Snowflake id, WebSocketReceiveResult result, string message)
        {
            _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(ProcessReceivedMessage)} Processing Receive Message For: {{0}}", result.MessageType);

            if (_socket.State == WebSocketState.CloseReceived && result.MessageType == WebSocketMessageType.Close)
            {
                if (!IsDisconnected() && !IsDisconnecting())
                {
                    int closeCode = result.CloseStatus.HasValue ? (int)result.CloseStatus : 1000;
                    SocketState = SocketState.Disconnecting;
                    _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(ProcessReceivedMessage)} Invoke On Close Code: {{0}} Message: {{1}}", closeCode, message);
                    await _handler.SocketClosed(id, closeCode, message);
                    await _socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", _token);
                    SocketState = SocketState.Disconnected;
                }

                return;
            }

            _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(ProcessReceivedMessage)} Invoke On Message: {{0}}", message);
            await _handler.SocketMessage(id, message);
        }

        private async Task<bool> SendInternalAsync(string message)
        {
            int charIndex = 0;
            while (!_source.IsCancellationRequested)
            {
                int charAmount = Math.Min(charIndex + _maxSendChars, message.Length);
                int byteSize = _encoding.GetBytes(message, charIndex, charAmount, _sendBuffer, 0);
                charIndex += charAmount;
                bool endOfMessage = charIndex == message.Length;
                //_logger.Debug($"{nameof(WebsocketHandler)}.{nameof(SendMessage)} Sending Message Char Amount: {{0}} ({{1}}/{{2}}) Bytes: {{3}} End Of Message: {{4}}", charAmount, charIndex, message.Length, byteSize, endOfMessage);
                await _socket.SendAsync(new ArraySegment<byte>(_sendBuffer, 0, byteSize), WebSocketMessageType.Text, endOfMessage, _token);
                if (endOfMessage)
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Sends the string message over the web socket
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public async Task<bool> SendAsync(string message)
        {
            if (_socket == null || _socket.State != WebSocketState.Open)
            {
                return false;
            }
            
            _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(SendAsync)} Sending Message: {{0}}", message);
            return await SendInternalAsync(message);
        }

        /// <summary>
        /// Disconnects from the websocket with the given code and reason
        /// </summary>
        /// <param name="code">Code to close with</param>
        /// <param name="reason">Reason for the close</param>
        /// <returns></returns>
        public Task Disconnect(int code, string reason)
        {
            return Disconnect((WebSocketCloseStatus)code, reason);
        }
        
        /// <summary>
        /// Disconnects from the websocket with the given code and reason
        /// </summary>
        /// <param name="status">Status to close with</param>
        /// <param name="reason">Reason for the close</param>
        /// <returns></returns>
        public async Task Disconnect(WebSocketCloseStatus status, string reason)
        {
            try
            {
                if(_socket != null && IsConnected() && !_source.IsCancellationRequested)
                {
                    Snowflake id = WebsocketId;
                    SocketState = SocketState.Disconnecting;
                    await _socket.CloseAsync(status, reason, _token);
                    await _handler.SocketClosed(id, (int)status, reason);
                    SocketState = SocketState.Disconnected;
                    _socket = null;
                }
            }
            catch (Exception ex)
            {
                _logger.Exception("An error occured closing the socket", ex);
            }
            finally
            {
                _source.Cancel();
            }
        }

        /// <summary>
        /// Returns if the websocket is in the open state
        /// </summary>
        /// <returns>Returns if the websocket is in open state</returns>
        public bool IsConnected()
        {
            return SocketState == SocketState.Connected;
        }

        /// <summary>
        /// Returns if the websocket is in the connecting state
        /// </summary>
        /// <returns>Returns if the websocket is in connecting state</returns>
        public bool IsConnecting()
        {
            return SocketState == SocketState.Connecting;
        }
        
        /// <summary>
        /// Returns if the socket is waiting to reconnect
        /// </summary>
        /// <returns>Returns if the socket is waiting to reconnect</returns>
        public bool IsPendingReconnect()
        {
            return SocketState == SocketState.PendingReconnect;
        }
        
        /// <summary>
        /// Returns if the websocket is null or is currently closing / closed
        /// </summary>
        /// <returns>Returns if the websocket is null or is currently closing / closed</returns>
        public bool IsDisconnecting()
        {
            return SocketState == SocketState.Disconnecting;
        }

        /// <summary>
        /// Returns if the websocket is null or is currently closing / closed
        /// </summary>
        /// <returns>Returns if the websocket is null or is currently closing / closed</returns>
        public bool IsDisconnected()
        {
            return SocketState == SocketState.Disconnected;
        }
    }
}