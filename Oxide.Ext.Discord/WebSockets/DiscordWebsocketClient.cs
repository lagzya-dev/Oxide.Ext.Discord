using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.WebSockets
{
    public class DiscordWebsocketClient : IDisposable
    {
        public readonly Snowflake WebsocketId;
        public SocketState SocketState { get; private set; }
        
        public CancellationToken Token => _source.Token;
        public WebSocketState WebSocketState => _socket.State;
        public bool IsCancelRequested => _source.IsCancellationRequested;

        private readonly ClientWebSocket _socket = new ClientWebSocket();
        private readonly CancellationTokenSource _source = new CancellationTokenSource();
        private readonly ILogger _logger;
        
        private bool _isDisposed;
        private bool _socketClosed;

        public DiscordWebsocketClient(ILogger logger)
        {
            _logger = logger;
            WebsocketId = SnowflakeIdGenerator.Generate();
            SocketState = SocketState.Connecting;
            _socket.Options.KeepAliveInterval = TimeSpan.Zero;
        }

        private void SetSocketState(SocketState state)
        {
            if (SocketState > state)
            {
                throw new Exception($"Trying to set SocketState to a lower state {state} < {SocketState}");
            }

            SocketState = state;
        }

        public async Task ConnectAsync(Uri uri)
        {
            await _socket.ConnectAsync(uri, Token).ConfigureAwait(false);
            SetSocketState(SocketState.Connected);
        }

        public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer) => _socket.ReceiveAsync(buffer, Token);

        public Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage) => _socket.SendAsync(buffer, messageType, endOfMessage, Token);

        public async Task CloseSocket(WebSocketCloseStatus status, string reason)
        {
            SetSocketState(SocketState.Disconnecting);

            if (_socket.State == WebSocketState.CloseReceived)
            {
                _logger.Debug("Closing Socket Output for ID: {0}", WebsocketId);
                await _socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, Token).ConfigureAwait(false);
            }
            else
            {
                _logger.Debug("Closing Socket for ID: {0}", WebsocketId);
                await _socket.CloseAsync(status, reason, Token).ConfigureAwait(false);
            }

            _socketClosed = true;
            _logger.Debug("{0} Socket Closed Successfully", WebsocketId);
        }

        public void Dispose()
        {
            _logger.Debug("Disposing Client {0}", WebsocketId);
            if (_isDisposed)
            {
                _logger.Debug("{0} already disposed", WebsocketId);
                return;
            }

            _isDisposed = true;
            
            SetSocketState(SocketState.Disconnected);
            if (Token.CanBeCanceled)
            {
                _logger.Debug("{0} Cancel Token", WebsocketId);
                _source.Cancel();
            }

            _logger.Debug("{0} Socket Closed: {1}", WebsocketId, _socketClosed);
            if (!_socketClosed)
            {
                _logger.Debug("{0} Dispose Socket", WebsocketId);
                _socket.Dispose();
            }

            _logger.Debug("{0} Dispose Source", WebsocketId);
            _source.Dispose();
            _logger.Debug("{0} Dispose Complete", WebsocketId);
        }
    }
}