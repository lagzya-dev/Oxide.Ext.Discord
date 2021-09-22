using System.Collections.Generic;
using System.Timers;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.RateLimits;

namespace Oxide.Ext.Discord.WebSockets.Handlers
{
    /// <summary>
    /// Handles command queueing when the websocket is down
    /// </summary>
    public class SocketCommandHandler
    {
        private readonly Socket _webSocket;
        private readonly ILogger _logger;
        private readonly List<CommandPayload> _pendingCommands = new List<CommandPayload>();
        private readonly WebsocketRateLimit _rateLimit = new WebsocketRateLimit();
        private readonly Timer _rateLimitTimer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webSocket">Websocket to handle commands for</param>
        /// <param name="logger">Logger for this handler</param>
        public SocketCommandHandler(Socket webSocket, ILogger logger)
        {
            _webSocket = webSocket;
            _logger = logger;
            
            _rateLimitTimer = new Timer(1000);
            _rateLimitTimer.AutoReset = false;
            _rateLimitTimer.Elapsed += RateLimitElapsed;
        }

        /// <summary>
        /// Enqueue a payload to be sent over the websocket
        /// If the websocket is connected it will be sent immediately
        /// If the websocket is not connected it will be queued until it is back online
        /// </summary>
        /// <param name="command">Command to send over the websocket</param>
        public void Enqueue(CommandPayload command)
        {
            if (_logger.IsLogging(DiscordLogLevel.Debug))
            {
                _logger.Debug($"{nameof(SocketCommandHandler)}.{nameof(Enqueue)} Queuing command {command.OpCode.ToString()}");
            }
            
            if (_webSocket.IsConnected())
            {
                _pendingCommands.Add(command);
                SendCommands();
                return;
            }

            if (command.OpCode == GatewayCommandCode.PresenceUpdate || command.OpCode == GatewayCommandCode.VoiceStateUpdate)
            {
                _pendingCommands.RemoveAll(p => p.OpCode == GatewayCommandCode.PresenceUpdate || p.OpCode == GatewayCommandCode.VoiceStateUpdate);
            }
            
            _pendingCommands.Add(command);
        }

        internal void OnSocketConnected()
        {
            _logger.Debug($"{nameof(SocketCommandHandler)}.{nameof(OnSocketConnected)} Socket Connected. Sending queued commands.");
            SendCommands();
        }

        private void RateLimitElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Debug($"{nameof(SocketCommandHandler)}.{nameof(RateLimitElapsed)} Rate Limit has elapsed. Send Queued Commands");
            _rateLimitTimer.Stop();
            SendCommands();
        }
        
        private void SendCommands()
        {
            while (_pendingCommands.Count != 0)
            {
                if (_rateLimit.HasReachedRateLimit)
                {
                    if (!_rateLimitTimer.Enabled)
                    {
                        _rateLimitTimer.Interval = _rateLimit.NextReset;
                        _rateLimitTimer.Stop();
                        _rateLimitTimer.Start();
                        _logger.Debug($"{nameof(SocketCommandHandler)}.{nameof(SendCommands)} Rate Limit Hit! Retrying in {_rateLimit.NextReset.ToString()} seconds");
                    }

                    return;
                }
                
                CommandPayload payload = _pendingCommands[0];
                _pendingCommands.RemoveAt(0);

                if (_logger.IsLogging(DiscordLogLevel.Debug))
                {
                    _logger.Debug($"{nameof(SocketCommandHandler)}.{nameof(SendCommands)} Sending Command {payload.OpCode.ToString()}");
                }

                _webSocket.Send(payload);
                _rateLimit.FiredRequest();
            }
        }
    }
}