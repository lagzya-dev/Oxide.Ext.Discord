using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Exceptions.Entities.Websocket;
using Oxide.Ext.Discord.Interfaces.WebSockets;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.WebSockets
{
    public class WebsocketHandler
    {
        private readonly IWebSocketEventHandler _handler;
        
        private ClientWebSocket _socket;
        private CancellationTokenSource _source;
        private CancellationToken _token;
        
        private readonly ConcurrentQueue<string> _sendMessages = new ConcurrentQueue<string>();
        private readonly ILogger _logger;

        private readonly ArraySegment<byte> _receiveBuffer;
        private readonly byte[] _sendBuffer;
        private readonly Encoding _encoding;
        private readonly int _maxSendChars;
        private byte[] _receivedBuffer;
        
        private const int SendChunkSize = 1024;
        private const int ReceiveChunkSize = 1024;

        internal SocketState SocketState = SocketState.Disconnected;

        public WebsocketHandler(IWebSocketEventHandler handler, ILogger logger)
        {
            _handler = handler;
            _logger = logger;
            _encoding = Encoding.UTF8;
            _receiveBuffer = WebSocket.CreateClientBuffer(ReceiveChunkSize, SendChunkSize);
            _sendBuffer = new byte[SendChunkSize];
            _receivedBuffer = new byte[ReceiveChunkSize * 4];
            _maxSendChars = _encoding.GetMaxCharCount(SendChunkSize);
        }

        public void Connect(string url)
        {
            if (IsConnected() || IsConnecting())
            {
                throw new WebsocketException("Socket is already running. Please disconnect before attempting to connect.");
            }

            SocketState = SocketState.Connecting;
            _source = new CancellationTokenSource();
            _token = _source.Token;
            _socket = new ClientWebSocket();
            
            Task.Run(async () =>
            {
                await RunWebsocket(url);
            }, _token);
        }
        
        private async Task RunWebsocket(string url)
        {
            try
            {
                _logger.Debug($"{nameof(WebsocketHandler)}.{nameof(Connect)} Connecting Websocket To: {{0}}", url);
                await _socket.ConnectAsync(new Uri(url), _token);
                SocketState = SocketState.Connected;
                InvokeOnConnected();
                _logger.Debug($"{nameof(WebsocketHandler)}.{nameof(Connect)} Websocket Connected To: {{0}}", url);
                await Task.WhenAll(ReceiveHandler(), SendHandler());
                if (IsConnected())
                {
                    SocketState = SocketState.Disconnecting;
                }
                _logger.Debug($"{nameof(WebsocketHandler)}.{nameof(Connect)} Websocket Completed");
                _socket.Dispose();
                _socket = null;
            }
            catch (TaskCanceledException) { }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                InvokeOnError(ex);
                _logger.Exception("A Websocket Error Occured", ex);
            }
            finally
            {
                SocketState = SocketState.Disconnected;
            }
        }

        private async Task ReceiveHandler()
        {
            int receivedCount = 0;
            
            _logger.Debug($"{nameof(WebsocketHandler)}.{nameof(ReceiveHandler)} Start Receive");
            while (!_token.IsCancellationRequested && (_socket.State == WebSocketState.Open || _socket.State == WebSocketState.CloseSent))
            {
                WebSocketReceiveResult result = await _socket.ReceiveAsync(_receiveBuffer, _token);
                if (_token.IsCancellationRequested)
                {
                    continue;
                }

                if (receivedCount + result.Count > _receivedBuffer.Length)
                {
                    Array.Resize(ref _receivedBuffer, _receivedBuffer.Length * 2);
                }
                
                Buffer.BlockCopy(_receiveBuffer.Array, 0, _receivedBuffer, receivedCount, result.Count);

                receivedCount += result.Count;

                //_logger.Debug($"{nameof(WebsocketHandler)}.{nameof(ReceiveHandler)} Received Type: {{0}} Size: {{1}} End Of Message: {{2}}", result.MessageType, result.Count, result.EndOfMessage);
                
                if (result.EndOfMessage)
                {
                    string message = Encoding.UTF8.GetString(_receivedBuffer, 0, receivedCount);
                    await ProcessReceivedMessage(result, message);
                    receivedCount = 0;
                }
            }
        }

        private async Task ProcessReceivedMessage(WebSocketReceiveResult result, string message)
        {
            _logger.Debug($"{nameof(WebsocketHandler)}.{nameof(ProcessReceivedMessage)} Processing Receive Message For: {{0}}", result.MessageType);

            if (_socket.State == WebSocketState.CloseReceived && result.MessageType == WebSocketMessageType.Close)
            {
                if (!IsDisconnected() && !IsDisconnecting())
                {
                    int closeCode = result.CloseStatus.HasValue ? (int)result.CloseStatus : 1000;
                    SocketState = SocketState.Disconnecting;
                    _logger.Debug($"{nameof(WebsocketHandler)}.{nameof(ProcessReceivedMessage)} Invoke On Close Code: {{0}} Message: {{1}}", closeCode, message);
                    InvokeOnClose(closeCode, message);
                    await _socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", _token);
                    SocketState = SocketState.Disconnected;
                }

                return;
            }

            _logger.Debug($"{nameof(WebsocketHandler)}.{nameof(ProcessReceivedMessage)} Invoke On Message: {{0}}", message);
            InvokeOnMessage(message);
        }

        private async Task SendHandler()
        {
            _logger.Debug($"{nameof(WebsocketHandler)}.{nameof(SendHandler)} Start Send");
            while (!_token.IsCancellationRequested && _socket.State == WebSocketState.Open)
            {
                if (_sendMessages.Count == 0)
                {
                    await Task.Delay(25, _token);
                    continue;
                }

                if (_sendMessages.TryDequeue(out string message))
                {
                    _logger.Debug($"{nameof(WebsocketHandler)}.{nameof(SendHandler)} Sending Message: {{0}}", message);
                    await SendMessage(message);
                }
            }
        }
        
        private async Task SendMessage(string message)
        {
            int charIndex = 0;
            while (true)
            {
                int charAmount = Math.Min(charIndex + _maxSendChars, message.Length);
                int byteSize = _encoding.GetBytes(message, charIndex, charAmount, _sendBuffer, 0);
                charIndex += charAmount;
                bool endOfMessage = charIndex == message.Length;
                //_logger.Debug($"{nameof(WebsocketHandler)}.{nameof(SendMessage)} Sending Message Char Amount: {{0}} ({{1}}/{{2}}) Bytes: {{3}} End Of Message: {{4}}", charAmount, charIndex, message.Length, byteSize, endOfMessage);
                await _socket.SendAsync(new ArraySegment<byte>(_sendBuffer, 0, byteSize), WebSocketMessageType.Text, endOfMessage, _token);
                if (endOfMessage)
                {
                    break;
                }
            }
        }
        
        public void Send(string message)
        {
            _sendMessages.Enqueue(message);
        }

        public Task Close(int code, string reason)
        {
            return Close((WebSocketCloseStatus)code, reason);
        }
        
        public async Task Close(WebSocketCloseStatus status, string reason)
        {
            try
            {
                if(_socket != null && IsConnected() && !_source.IsCancellationRequested)
                {
                    SocketState = SocketState.Disconnecting;
                    await _socket.CloseAsync(status, reason, _token);
                    InvokeOnClose((int)status, reason);
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

        private void InvokeOnConnected()
        {
            if (_handler != null)
            {
                Task.Run(() => _handler.SocketOpened(), _token);
            }
        }
        
        private void InvokeOnMessage(string message)
        {
            if (_handler != null)
            {
                Task.Run(async () => await _handler.SocketMessage(message), _token);
            }
        }
        
        private void InvokeOnError(Exception ex)
        {
            if (_handler != null)
            {
                Task.Run(() => _handler.SocketErrored(ex), _token);
            }
        }
        
        private void InvokeOnClose(int code, string message)
        {
            if (_handler != null)
            {
                Task.Run(() => _handler.SocketClosed(code, message), _token);
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