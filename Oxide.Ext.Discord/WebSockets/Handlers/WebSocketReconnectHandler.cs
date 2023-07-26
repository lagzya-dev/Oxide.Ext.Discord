using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.WebSockets.Handlers
{
    /// <summary>
    /// Handles reconnecting to the web socket
    /// </summary>
    internal class WebSocketReconnectHandler
    {
        internal readonly DiscordWebSocket WebSocket;
        private readonly BotClient _client;
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
            StringBuilder debug = DiscordPool.Internal.GetStringBuilder();
            try
            {
                debug.Append("A");
                if (_source == null)
                {
                    debug.Append("B");
                    return;
                }

                debug.Append("C");
                if (!_source.IsCancellationRequested)
                {
                    debug.Append("D");
                    _source.Cancel();
                }

                debug.Append("E");
                //This can be null here. No idea why.
                _source?.Dispose();
                debug.Append("F");
            }
            catch (Exception ex)
            {
                _logger.Exception($"{nameof(WebSocketReconnectHandler)}.{nameof(CancelReconnect)} An error occured. Websocket: {{0}} Source: {{1}} Debug: \n{{2}}", WebSocket == null, _source == null, debug.ToString(), ex);
                throw;
            }
            finally
            {
                DiscordPool.Internal.FreeStringBuilder(debug);
            }
        }

        /// <summary>
        /// Called when the websocket received a ready event from discord
        /// </summary>
        public void OnWebsocketReady() => _reconnectRetries = 0;

        /// <summary>
        /// Called when the bot is shutting down
        /// </summary>
        public void OnSocketShutdown() => CancelReconnect();

        private int GetReconnectDelay()
        {
            if (_reconnectRetries == 0) return 1000 / 60;
            if (_reconnectRetries <= 3) return 1 * 1000;
            if (_reconnectRetries <= 25) return 15 * 1000;
            return 60 * 1000;
        }
    }
}