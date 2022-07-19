using System;
using System.Collections.Concurrent;
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
        private readonly ConcurrentQueue<CommandPayload> _pendingCommands = new ConcurrentQueue<CommandPayload>();
        private readonly WebsocketRateLimit _rateLimit = new WebsocketRateLimit();
        private readonly AutoResetEvent _online = new AutoResetEvent(false);
        private readonly AutoResetEvent _commands = new AutoResetEvent(false);
        private readonly CancellationTokenSource _source;
        private readonly CancellationToken _token;
        private bool _isSocketReady;
        private Thread _thread;

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

            _thread = new Thread(RunInternal)
            {
                IsBackground = true
            };
            _thread.Start();
        }

        private async void RunInternal()
        {
            try
            {
                await SendCommandsInternal();
            }
            catch (ThreadAbortException) { }
            catch (TaskCanceledException) { }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger.Exception($"Unhandled exception in {nameof(WebsocketCommandHandler)}.{nameof(RunInternal)}", ex);
            }
        }

        private async Task SendCommandsInternal()
        {
            while (_source != null && !_source.IsCancellationRequested)
            {
                try
                {
                    _online.WaitOne();
                    _commands.WaitOne();

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
                    
                    if (!_pendingCommands.IsEmpty)
                    {
                        _commands.Set();
                    }
                }
            }
        }

        private CommandPayload GetNextCommand()
        {
            if (_isSocketReady && _pendingCommands.TryPeek(out CommandPayload command))
            {
                return command;
            }

            return null;
        }

        private void RemoveCommand(CommandPayload command)
        {
            _pendingCommands.TryDequeue(out CommandPayload _);
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
            _pendingCommands.Enqueue(command);
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
            while (!_pendingCommands.IsEmpty)
            {
                _pendingCommands.TryDequeue(out CommandPayload _);
            }
            _isSocketReady = false;
        }

        internal void OnSocketShutdown()
        {
            _source.Cancel();
            _thread?.Abort();
        }

        internal IReadOnlyCollection<CommandPayload> GetPendingCommands()
        {
            return _pendingCommands;
        }
    }
}