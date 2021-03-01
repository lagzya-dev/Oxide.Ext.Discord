using System.Timers;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.WebSockets
{
    /// <summary>
    /// Handles the heartbeating for the websocket connection
    /// </summary>
    public class HeartbeatHandler
    {
        /// <summary>
        /// Discord Acknowledged our heartbeat successfully 
        /// </summary>
        public bool HeartbeatAcknowledged;
        
        
        private readonly BotClient _client;
        private readonly SocketListener _listener;
        private readonly ILogger _logger;
        private Timer _heartbeatTimer;

        /// <summary>
        /// Constructor for Heartbeat Handler
        /// </summary>
        /// <param name="client">Client for the handler</param>
        /// <param name="listener">Socket Listener for the client</param>
        /// <param name="logger">Logger for the bot</param>
        public HeartbeatHandler(BotClient client, SocketListener listener, ILogger logger)
        {
            _client = client;
            _listener = listener;
            _logger = logger;
        }
        
        #region Heartbeat
        /// <summary>
        /// Setup a heartbeat for this bot with the given interval
        /// </summary>
        /// <param name="heartbeatInterval"></param>
        internal void SetupHeartbeat(float heartbeatInterval)
        {
            if (_heartbeatTimer != null)
            {
                _logger.Debug($"{nameof(HeartbeatHandler)}.{nameof(SetupHeartbeat)} Previous heartbeat timer exists.");
                DestroyHeartbeat();
            }

            HeartbeatAcknowledged = true;
            _heartbeatTimer = new Timer(heartbeatInterval);
            _heartbeatTimer.Elapsed += HeartbeatElapsed;
            _heartbeatTimer.Start();
            _logger.Debug($"{nameof(HeartbeatHandler)}.{nameof(SetupHeartbeat)} Creating heartbeat with interval {heartbeatInterval}ms.");
            _client.CallHook(DiscordHooks.OnDiscordSetupHeartbeat, heartbeatInterval);
        }

        /// <summary>
        /// Destroy the heartbeat on this bot
        /// </summary>
        public void DestroyHeartbeat()
        {
            if(_heartbeatTimer != null)
            {
                _logger.Debug($"{nameof(HeartbeatHandler)}.{nameof(DestroyHeartbeat)} Destroy Heartbeat");
                _heartbeatTimer.Dispose();
                _heartbeatTimer = null;
            }
        }

        private void HeartbeatElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Debug($"{nameof(HeartbeatHandler)}.{nameof(HeartbeatElapsed)} heartbeat Elapsed");
            if (!_client.WebSocket.IsAlive() && !_client.WebSocket.IsConnecting())
            {
                if (!_client.WebSocket.IsReconnectTimerActive())
                {
                    _logger.Debug($"{nameof(HeartbeatHandler)}.{nameof(HeartbeatElapsed)} Websocket is offline and is NOT connecting. Start reconnect timer.");
                    _client.WebSocket.StartReconnectTimer(1f, () => _client.UpdateGatewayUrl(_client.ConnectWebSocket));
                }
                else
                {
                    _logger.Debug($"{nameof(HeartbeatHandler)}.{nameof(HeartbeatElapsed)} Websocket is offline and is waiting to connect.");
                }

                return;
            }
            
            if(!HeartbeatAcknowledged)
            {
                //Discord did not acknowledge our last sent heartbeat. This is a zombie connection we should reconnect.
                if (_client.WebSocket.IsAlive())
                {
                    _client.WebSocket.Disconnect(true, true, true);
                }
                else if (!_client.WebSocket.IsReconnectTimerActive())
                {
                    _logger.Debug($"{nameof(HeartbeatHandler)}.{nameof(SendHeartbeat)} Heartbeat Elapsed and bot is not online or connecting.");
                    _client.WebSocket.StartReconnectTimer(1f, _client.ConnectWebSocket);
                }
                else
                {
                    _logger.Debug($"{nameof(HeartbeatHandler)}.{nameof(SendHeartbeat)} Heartbeat Elapsed and bot is not online but is waiting to connect.");
                }
                
                return;
            }

            SendHeartbeat();
        }
        
        /// <summary>
        /// Sends a heartbeat to discord.
        /// If the previous heartbeat wasn't acknowledged then we will attempt to reconnect
        /// </summary>
        public void SendHeartbeat()
        {
            HeartbeatAcknowledged = false;
            _listener.SendHeartbeat();
            _client.CallHook(DiscordHooks.OnDiscordHeartbeatSent);
            _logger.Debug($"{nameof(HeartbeatHandler)}.{nameof(SendHeartbeat)} Heartbeat sent - {_heartbeatTimer.Interval}ms interval.");
        }
        #endregion
    }
}