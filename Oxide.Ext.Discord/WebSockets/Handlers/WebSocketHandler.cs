using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Exceptions.Entities.Websocket;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Interfaces.WebSockets;
using Oxide.Ext.Discord.Json.Serialization;
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
        private readonly AutoResetEvent _sendLock = new AutoResetEvent(true);

        //private readonly ZlibDecompressorHandler _decompressor;
        
        private const int SendChunkSize = 1024;
        private const int ReceiveChunkSize = 1024;

        internal SocketState SocketState = SocketState.Disconnected;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handler">Handles for web socket events</param>
        /// <param name="logger"></param>
        public WebSocketHandler(IWebSocketEventHandler handler, ILogger logger)
        {
            _handler = handler;
            _logger = logger;
            _receiveBuffer = WebSocket.CreateClientBuffer(ReceiveChunkSize, SendChunkSize);
            _sendBuffer = new byte[SendChunkSize];
        }

        /// <summary>
        /// Connects to the websocket at the given URL
        /// </summary>
        /// <param name="url"></param>
        /// <exception cref="DiscordWebSocketException"></exception>
        public void Connect(string url)
        {
            if (IsConnected() || IsConnecting())
            {
                throw new DiscordWebSocketException("Socket is already running. Please disconnect before attempting to connect.");
            }

            WebsocketId = SnowflakeIdGenerator.Generate();
            SocketState = SocketState.Connecting;
            
            CancelToken();
            _source = new CancellationTokenSource();
            _token = _source.Token;
            _socket = new ClientWebSocket();
            _socket.Options.KeepAliveInterval = TimeSpan.Zero;

            Task.Factory.StartNew(RunInternal, url, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private async void RunInternal(object url)
        {
            await RunWebsocket((string)url).ConfigureAwait(false);
        }
        
        private async Task RunWebsocket(string url)
        {
            Snowflake id = WebsocketId;
            CancellationToken token = _token;
            try
            {
                _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(Connect)} Connecting Websocket To: {{0}}", url);
                await _socket.ConnectAsync(new Uri(url), token).ConfigureAwait(false);
                SocketState = SocketState.Connected;
                await _handler.SocketOpened(id).ConfigureAwait(false);
                _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(Connect)} Websocket Connected");
                await ReceiveHandlerAsync(id, _socket, token).ConfigureAwait(false);
                if (IsConnected())
                {
                    SocketState = SocketState.Disconnecting;
                }
                _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(Connect)} Websocket Completed");
                DisposeSocket();
            }
            catch (TaskCanceledException)
            {
                DisposeSocket();
            }
            catch (OperationCanceledException)
            {
                DisposeSocket();
            }
            catch (WebSocketException ex)
            {
                DisposeSocket();
                if (ex.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
                {
                    _logger.Verbose("Disconnected Socket Because: {0} Error Code: {1} HResult:{2}\n{3}", ex.WebSocketErrorCode, ex.ErrorCode, ex.HResult, ex);
                    await _handler.SocketClosed(id, WebSocketCloseStatus.NormalClosure, string.Empty).ConfigureAwait(false);
                }
                else
                {
                    _logger.Debug("Disconnected Socket Because: {0} Error Code: {1} HResult:{2}\n{3}", ex.WebSocketErrorCode, ex.ErrorCode, ex.HResult, ex);
                    await _handler.SocketErrored(id, ex).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                DisposeSocket();
                await _handler.SocketErrored(id, ex).ConfigureAwait(false);
                _logger.Exception("A Unhandled Websocket Error Occured", ex);
            }
        }

        private async Task ReceiveHandlerAsync(Snowflake id, ClientWebSocket socket, CancellationToken token)
        {
            DiscordJsonReader reader = new DiscordJsonReader();
            MemoryStream input = reader.Stream;
            byte[] array = _receiveBuffer.Array ?? throw new ArgumentNullException();
            
            _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(ReceiveHandlerAsync)} Start Receive");
            while (!token.IsCancellationRequested && (socket.State == WebSocketState.Open || socket.State == WebSocketState.CloseSent))
            {
                WebSocketReceiveResult result = await socket.ReceiveAsync(_receiveBuffer, token).ConfigureAwait(false);
                if (token.IsCancellationRequested)
                {
                    return;
                }

                await input.WriteAsync(array, 0, result.Count, token).ConfigureAwait(false);

                if (result.EndOfMessage)
                {
                    try
                    {
                        input.Position = 0;
                        await ProcessReceivedMessageAsync(id, socket, result, reader).ConfigureAwait(false);
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

        private async Task ProcessReceivedMessageAsync(Snowflake id, ClientWebSocket socket, WebSocketReceiveResult result, DiscordJsonReader reader)
        {
            _logger.Verbose($"{nameof(WebSocketHandler)}.{nameof(ProcessReceivedMessageAsync)} Processing Receive Message For: {{0}} State: {{1}} Type: {{2}} Size: {{3}}", id, socket.State, result.MessageType, reader.Stream.Length);

            if (socket.State == WebSocketState.CloseReceived && result.MessageType == WebSocketMessageType.Close)
            {
                if (!IsDisconnected() && !IsDisconnecting())
                {
                    WebSocketCloseStatus closeStatus = result.CloseStatus ?? WebSocketCloseStatus.NormalClosure;
                    await DisconnectInternal(id, closeStatus, "Websocket closed by Discord", true).ConfigureAwait(false);
                }

                return;
            }

            //_logger.Debug($"{nameof(WebSocketHandler)}.{nameof(ProcessReceivedMessage)} Invoke On Message: {{0}}", message);
            await _handler.SocketMessage(id, reader).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the string message over the web socket
        /// </summary>
        /// <param name="stream">Stream to send</param>
        public async Task<bool> SendAsync(MemoryStream stream)
        {
            if (_socket == null || _socket.State != WebSocketState.Open || stream.Length == 0)
            {
                return false;
            }

            _sendLock.WaitOne();
            try
            {
                return await SendInternalAsync(stream).ConfigureAwait(false);
            }
            finally
            {
                _sendLock.Set();
            }
        }
        
        private async Task<bool> SendInternalAsync(MemoryStream stream)
        {
            stream.Position = 0;
            int readIndex = 0;
            while (!_source.IsCancellationRequested)
            {
                int read = await stream.ReadAsync(_sendBuffer, 0, _sendBuffer.Length, _token).ConfigureAwait(false);
                readIndex += read;
                bool endOfMessage = readIndex == stream.Length;
                //_logger.Debug($"{nameof(WebSocketHandler)}.{nameof(SendInternalAsync)} Sending Message Amount: {{0}} ({{1}}/{{2}}) Message: {{3}} - {{4}} End Of Message: {{5}}", read, readIndex, stream.Length, g, g.Length, endOfMessage);
                await _socket.SendAsync(new ArraySegment<byte>(_sendBuffer, 0, read), WebSocketMessageType.Text, endOfMessage, _token).ConfigureAwait(false);
                if (endOfMessage)
                {
                    return true;
                }
            }

            return false;
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
        /// <param name="status"><see cref="WebSocketCloseStatus"/>to close with</param>
        /// <param name="reason">Reason for the close</param>
        /// <returns></returns>
        public Task Disconnect(WebSocketCloseStatus status, string reason)
        {
            return DisconnectInternal(WebsocketId, status, reason, false);
        }

        /// <summary>
        /// Disconnects from the websocket with the given code and reason
        /// </summary>
        /// <param name="id">ID of the websocket</param>
        /// <param name="status">Status to close with</param>
        /// <param name="reason">Reason for the close</param>
        /// <param name="closeReceived">If we received a close from the websocket</param>
        /// <returns></returns>
        private async Task DisconnectInternal(Snowflake id, WebSocketCloseStatus status, string reason, bool closeReceived)
        {
            try
            {
                _logger.Debug($"{nameof(WebSocketHandler)}.{nameof(Disconnect)} Status: {{0}} Reason: {{1}} Close Received: {{2}} Socket Is Disposed: {{3}} Is Connected {{4}} Cancel Requested {{5}}",
                    status, reason, closeReceived, _socket != null, IsConnected(), _source.IsCancellationRequested);

                if (id.IsValid() && id != WebsocketId)
                {
                    return;
                }

                if (_socket != null && IsConnected() && !_source.IsCancellationRequested)
                {
                    SocketState = SocketState.Disconnecting;
                    WebsocketId = default(Snowflake);

                    if (closeReceived)
                    {
                        await _socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, _token).ConfigureAwait(false);
                    }
                    else
                    {
                        await _socket.CloseAsync(status, reason, _token).ConfigureAwait(false);
                    }

                    DisposeSocket();
                    await _handler.SocketClosed(id, status, reason).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception("An error occured closing the socket", ex);
            }
        }

        private void DisposeSocket()
        {
            SocketState = SocketState.Disconnected;
            CancelToken();
            _socket?.Dispose();
            _socket = null;
        }

        private void CancelToken()
        {
            if (_source == null)
            {
                return;
            }
            
            if (_source.Token.CanBeCanceled)
            {
                _source.Cancel();    
            }
                
            _source.Dispose();
            _source = null;
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