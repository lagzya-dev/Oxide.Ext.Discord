using System;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.WebSockets.Handlers
{
    /// <summary>
    /// Handles reconnecting to the web socket
    /// </summary>
    internal class WebSocketReconnectHandler
    {
        private readonly BotClient _client;
        internal readonly DiscordWebSocket WebSocket;
        private readonly ILogger _logger;
        private int _reconnectRetries;
        private CancellationTokenSource _source;

        public bool IsPendingReconnect { get; private set; }
        
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
            WebSocket = webSocket;
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

            if (!WebSocket.IsDisconnected() && !WebSocket.IsDisconnecting())
            {
                _logger.Debug("Skipping reconnect. Websocket is not Disconnected or Disconnecting");
                return;
            }
            
            CancelReconnect();
            _source = new CancellationTokenSource();

            try
            {
                int delay = GetReconnectDelay();

                if (_reconnectRetries == 0)
                {
                    _logger.Info("Reconnecting to Discord.");
                }
                else
                {
                    _logger.Info("Reconnecting to Discord. Retry: #{0} Delay: {1}ms", _reconnectRetries, delay);
                }
                
                _reconnectRetries++;
                
                await Task.Delay(delay, _source.Token).ConfigureAwait(false);
                IsPendingReconnect = false;
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
            if (WebSocket.IsConnected() || WebSocket.IsConnecting())
            {
                _logger.Debug("Skipping Connect. Socket is: {0}", WebSocket.Handler.SocketState);
                return;
            }
            
            WebSocket.Connect();
        }
        
        /// <summary>
        /// Cancels the reconnect timer
        /// </summary>
        public void CancelReconnect()
        {
            if (_source == null)
            {
                return;
            }
            
            if (!_source.IsCancellationRequested)
            {
                _source.Cancel();
            }
            
            _source.Dispose();
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