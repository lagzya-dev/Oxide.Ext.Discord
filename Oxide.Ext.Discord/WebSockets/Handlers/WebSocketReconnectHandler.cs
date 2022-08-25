using System;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.WebSockets.Handlers
{
    /// <summary>
    /// Handles reconnecting to the web socket
    /// </summary>
    public class WebSocketReconnectHandler
    {
        private readonly BotClient _client;
        private readonly DiscordWebSocket _webSocket;
        private readonly ILogger _logger;
        private int _reconnectRetries;
        private CancellationTokenSource _source;

        internal bool AttemptGatewayUpdate => _reconnectRetries >= 3;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="webSocket"></param>
        /// <param name="logger"></param>
        public WebSocketReconnectHandler(BotClient client, DiscordWebSocket webSocket, ILogger logger)
        {
            _client = client;
            _webSocket = webSocket;
            _logger = logger;
        }

        /// <summary>
        /// Starts the reconnect process
        /// </summary>
        public async Task StartReconnect()
        {
            if (!_client.Initialized)
            {
                _logger.Debug("Skipping reconnect. BotClient is not Initialized");
                return;
            }

            if (!_webSocket.Handler.IsDisconnected() && !_webSocket.Handler.IsDisconnecting())
            {
                _logger.Debug("Skipping reconnect. Websocket is not Disconnected or Disconnecting");
                return;
            }

            CancelReconnect();
            _source = new CancellationTokenSource();
            CancellationToken token = _source.Token;

            try
            {
                _webSocket.Handler.SocketState = SocketState.PendingReconnect;
                _reconnectRetries++;
                int delay = GetReconnectDelay();
                _logger.Warning("Attempting to reconnect to Discord. Retry: #{0} Delay: {1}", _reconnectRetries, delay);

                await Task.Delay(delay, token).ConfigureAwait(false);
                Connect();
            }
            catch (TaskCanceledException) { }
            catch (OperationCanceledException) { }
            finally
            {
                _source.Dispose();
                _source = null;
            }
        }

        private void Connect()
        {
            if (_webSocket.Handler.IsConnected() || _webSocket.Handler.IsConnecting())
            {
                _logger.Debug("Skipping Connect. Socket is: {0}", _webSocket.Handler.SocketState);
                return;
            }
            
            _webSocket.Connect();
        }
        
        /// <summary>
        /// Cancels the reconnect timer
        /// </summary>
        public void CancelReconnect()
        {
            _source?.Cancel();
            _source?.Dispose();
            _source = null;
        }

        /// <summary>
        /// Called when the websocket received a ready event from discord
        /// </summary>
        public void OnWebsocketReady()
        {
            _reconnectRetries = 0;
        }

        /// <summary>
        /// Called when the bot is shutting down
        /// </summary>
        public void OnSocketShutdown()
        {
            CancelReconnect();
        }

        private int GetReconnectDelay()
        {
            if (_reconnectRetries == 0) return 1000 / 60;
            if (_reconnectRetries <= 3) return 1 * 1000;
            if (_reconnectRetries <= 25) return 15 * 1000;
            return 60 * 1000;
        }
    }
}