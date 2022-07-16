using System.Timers;
using Oxide.Core;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.WebSockets.Handlers
{
    public class WebsocketReconnectHandler
    {
        private readonly BotClient _client;
        private readonly DiscordWebSocket _webSocket;
        private readonly ILogger _logger;
        private int _reconnectRetries;
        private readonly Timer _reconnectTimer;
        
        internal bool AttemptGatewayUpdate => _reconnectRetries >= 3;
        
        public WebsocketReconnectHandler(BotClient client, DiscordWebSocket webSocket, ILogger logger)
        {
            _client = client;
            _webSocket = webSocket;
            _logger = logger;

            _reconnectTimer = new Timer();
            _reconnectTimer.Elapsed += ReconnectWebsocket;
            _reconnectTimer.AutoReset = false;
        }

        public void StartReconnect()
        {
            if (!_client.Initialized)
            {
                return;
            }

            if (!_webSocket.Handler.IsDisconnected())
            {
                return;
            }

            _webSocket.Handler.SocketState = SocketState.PendingReconnect;
            
            //If we haven't had any errors reconnect to the gateway
            if (_reconnectRetries == 0)
            {
                _reconnectRetries++;
                Interface.Oxide.NextTick(Connect);
                return;
            }

            _reconnectTimer.Interval = GetReconnectDelay();
            _reconnectTimer.Start();

            _logger.Warning("Attempting to reconnect to Discord... [Retry={0}]", _reconnectRetries);
            _reconnectRetries++;
        }
        
        private void ReconnectWebsocket(object sender, ElapsedEventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            _webSocket.Connect();
        }
        
        public void CancelReconnect()
        {
            _reconnectTimer.Stop();
        }

        public void OnWebsocketReady()
        {
            _reconnectRetries = 0;
        }

        public void OnSocketShutdown()
        {
            _reconnectTimer.Stop();
            _reconnectTimer.Dispose();
        }

        private double GetReconnectDelay()
        {
            if (_reconnectRetries <= 3) return 1 * 1000;
            if (_reconnectRetries <= 25) return 15 * 1000;
            return 60 * 1000;
        }
    }
}