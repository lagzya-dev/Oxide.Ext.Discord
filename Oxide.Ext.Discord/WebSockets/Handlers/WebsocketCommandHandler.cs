using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.RateLimits;
using Oxide.Ext.Discord.Threading;

namespace Oxide.Ext.Discord.WebSockets.Handlers
{
    /// <summary>
    /// Handles command queueing when the websocket is down
    /// </summary>
    public class WebsocketCommandHandler
    {
        private readonly BotClient _client;
        private readonly DiscordWebSocket _webSocket;
        private readonly ILogger _logger;
        private readonly ThreadSafeList<CommandPayload> _pendingCommands = new ThreadSafeList<CommandPayload>();
        private readonly WebsocketRateLimit _rateLimit = new WebsocketRateLimit();
        private readonly AutoResetEvent _online = new AutoResetEvent(false);
        private readonly AutoResetEvent _commands = new AutoResetEvent(false);
        private readonly CancellationTokenSource _source;
        private readonly CancellationToken _token;
        private bool _isSocketReady;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Bot Client for socket commands</param>
        /// <param name="webSocket">Websocket to handle commands for</param>
        /// <param name="logger">Logger for this handler</param>
        public WebsocketCommandHandler(BotClient client, DiscordWebSocket webSocket, ILogger logger)
        {
            _client = client;
            _webSocket = webSocket;
            _logger = logger;
            
            _source = new CancellationTokenSource();
            _token = _source.Token;

            Task.Run(async () => await SendCommandsInternal());
        }

        private async Task SendCommandsInternal()
        {
            while (!_source.IsCancellationRequested)
            {
                try
                {
                    _logger.Debug("Waiting for online");
                    _online.WaitOne();
                    _logger.Debug("Waiting for commands");
                    _commands.WaitOne();
                    _logger.Debug("Sending command");

                    if (_rateLimit.HasReachedRateLimit)
                    {
                        DateTimeOffset reset = _rateLimit.NextReset();
                        if (reset > DateTimeOffset.UtcNow)
                        {
                            await Task.Delay(reset - DateTimeOffset.UtcNow, _token);
                        }
                    
                        continue;
                    }

                    CommandPayload command = GetNextCommand();
                    if (command == null)
                    {
                        continue;
                    }
                    
                    _logger.Debug($"{nameof(WebsocketCommandHandler)}.{nameof(SendCommandsInternal)} Sending Command {{0}}", command.OpCode);

                    _rateLimit.FiredRequest();
                    
                    if (await _webSocket.Send(command))
                    {
                        RemoveCommand(command);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Exception("An error occured sending commands", ex);
                }
                finally
                {
                    if (_isSocketReady)
                    {
                        _online.Set();
                    }
                    
                    if (_pendingCommands.Count != 0)
                    {
                        _commands.Set();
                    }
                }
            }
        }

        private CommandPayload GetNextCommand()
        {
            if (_isSocketReady)
            {
                return _pendingCommands.Count != 0 ? _pendingCommands[0] : null;
            }

            return null;
        }

        private void RemoveCommand(CommandPayload command)
        {
            _pendingCommands.Remove(command);
            command.Dispose();
        }

        /// <summary>
        /// Enqueue a payload to be sent over the websocket
        /// If the websocket is connected it will be sent immediately
        /// If the websocket is not connected it will be queued until it is back online
        /// </summary>
        /// <param name="command">Command to send over the websocket</param>
        public void Enqueue(CommandPayload command)
        {
            _logger.Debug($"{nameof(WebsocketCommandHandler)}.{nameof(Enqueue)} Queuing command {{0}}", command.OpCode);

            if (command.OpCode == GatewayCommandCode.PresenceUpdate)
            {
                RemoveByType(GatewayCommandCode.PresenceUpdate);
            }
            else if (command.OpCode == GatewayCommandCode.VoiceStateUpdate)
            {
                RemoveByType(GatewayCommandCode.VoiceStateUpdate);
            }

            _pendingCommands.Add(command);
            _commands.Set();
        }
        internal void OnWebSocketReady()
        {
            _logger.Debug($"{nameof(WebsocketCommandHandler)}.{nameof(OnWebSocketReady)} Socket Connected. Sending queued commands.");
            _isSocketReady = true;
            _online.Set();
        }

        internal void OnSocketDisconnected()
        {
            _logger.Debug($"{nameof(WebsocketCommandHandler)}.{nameof(OnSocketDisconnected)} Socket Disconnected. Queuing Commands.");
            _online.Reset();
            _pendingCommands.Clear();
            _isSocketReady = false;
        }
        
        private void RemoveByType(GatewayCommandCode code)
        {
            for (int index = _pendingCommands.Count - 1; index >= 0; index--)
            {
                CommandPayload command = _pendingCommands[index];
                if (command.OpCode == code)
                {
                    _pendingCommands.RemoveAt(index);
                    command.Dispose();
                }
            }
        }

        internal void OnSocketShutdown()
        {
            _source.Cancel();
        }

        internal IList<CommandPayload> GetPendingCommands()
        {
            return _pendingCommands;
        }
    }
}