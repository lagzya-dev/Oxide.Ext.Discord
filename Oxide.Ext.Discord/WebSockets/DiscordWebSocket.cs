using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.WebSockets.Handlers;

namespace Oxide.Ext.Discord.WebSockets
{
    /// <summary>
    /// Represents a websocket that connects to discord
    /// </summary>
    public class DiscordWebSocket
    {
        /// <summary>
        /// The current session ID for the connected bot
        /// </summary>
        private string _sessionId;
        
        /// <summary>
        /// If we should attempt to reconnect to discord on disconnect
        /// </summary>
        public bool ShouldReconnect;
        
        /// <summary>
        /// If we should attempt to resume our previous session after connecting
        /// </summary>
        public bool ShouldResume;

        /// <summary>
        /// The current sequence number for the websocket
        /// </summary>
        private int _sequence;

        /// <summary>
        /// If the bot has successfully connected to the websocket at least once
        /// </summary>
        public bool SocketHasConnected { get; private set; }

        internal GatewayIntents Intents { get; private set; }

        private readonly BotClient _client;
        internal readonly WebsocketHandler Handler;
        private readonly WebsocketEventHandler _listener;
        internal readonly WebsocketCommandHandler Commands;
        private readonly DiscordHeartbeatHandler _heartbeat;
        private readonly WebsocketReconnectHandler _reconnect;
        private readonly ILogger _logger;

        private bool _isShutdown;

        /// <summary>
        /// Socket used by the BotClient
        /// </summary>
        /// <param name="client">Client using the socket</param>
        /// <param name="logger">Logger for the bot client</param>
        public DiscordWebSocket(BotClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;

            _reconnect = new WebsocketReconnectHandler(client, this, logger);
            Commands = new WebsocketCommandHandler(client, this, logger);
            _heartbeat = new DiscordHeartbeatHandler(client, this, logger);
            _listener = new WebsocketEventHandler(client, this, logger);
            Handler = new WebsocketHandler(_listener, logger);
        }

        /// <summary>
        /// Connect to the websocket
        /// </summary>
        /// <exception cref="Exception">Thrown if the socket still exists. Must call disconnect before trying to connect</exception>
        public void Connect()
        {
            _logger.Debug($"{nameof(DiscordWebSocket)}.{nameof(Connect)} Start websocket connection");
            string url = Gateway.WebsocketUrl;
            
            //We haven't gotten the websocket url. Get url then attempt to connect.
            //There has been more than 3 tries to reconnect. Discord suggests trying to update gateway url.
            if (string.IsNullOrEmpty(url) || (_reconnect.AttemptGatewayUpdate && Gateway.LastUpdate + TimeSpan.FromMinutes(5) <= DateTime.UtcNow))
            {
                Gateway.UpdateGatewayUrl(_client, Connect, OnGatewayUrlUpdateFailed);
                return;
            }

            ShouldReconnect = false;
            ShouldResume = false;

            Intents = _client.Settings.Intents;

            Handler.Connect(url);
        }

        private void OnGatewayUrlUpdateFailed(RequestError error)
        {
            _logger.Warning("Failed to update gateway url. Attempting reconnect.");

            //Set as disconnected if we're in pending reconnect state we reconnect goes through.
            if (Handler.SocketState == SocketState.PendingReconnect)
            {
                Handler.SocketState = SocketState.Disconnected;
            }
            _reconnect.StartReconnect();
        }

        /// <summary>
        /// Disconnects the websocket from discord
        /// </summary>
        /// <param name="reconnect">Should we attempt to reconnect to discord after disconnecting</param>
        /// <param name="resume">Should we attempt to resume our previous session</param>
        /// <param name="requested">If discord requested that we reconnect to discord</param>
        public async void Disconnect(bool reconnect, bool resume, bool requested = false)
        {
            _logger.Debug($"{nameof(DiscordWebSocket)}.{nameof(Disconnect)} Disconnecting Web Socket. Socket State: {{0}} Reconnect: {{1}} Resume: {{2}} Requested {{3}}", Handler.SocketState, reconnect, resume, requested);
            ShouldReconnect = reconnect;
            ShouldResume = resume;
            _reconnect.CancelReconnect();

            if (!Handler.IsDisconnected() && !Handler.IsDisconnecting())
            {
                OnSocketDisconnected();

                if (requested)
                {
                    await Handler.Disconnect(4199, "Discord Requested Reconnect");
                }
                else
                {
                    await Handler.Disconnect(WebSocketCloseStatus.NormalClosure, string.Empty);
                }
            }

            ReconnectIfRequested();
        }

        /// <summary>
        /// Returns if the given websocket matches our current websocket.
        /// If socket is null we return false
        /// </summary>
        /// <param name="webSocketId">ID of the socket</param>
        /// <returns>True if current socket is not null and socket matches current socket; False otherwise.</returns>
        internal bool IsCurrentSocket(Snowflake webSocketId)
        {
            return Handler.WebsocketId == webSocketId;
        }

        /// <summary>
        /// Shutdowns the websocket completely. Used when bot is being shutdown
        /// </summary>
        public void Shutdown()
        {
            _isShutdown = true;
            Disconnect(false, false);
            _reconnect.OnSocketShutdown();
            _heartbeat.OnSocketShutdown();
            Commands.OnSocketShutdown();
        }

        /// <summary>
        /// Send a command to discord over the websocket
        /// </summary>
        /// <param name="client">Client sending the command</param>
        /// <param name="opCode">Command code to send</param>
        /// <param name="data">Data to send</param>
        public void Send(DiscordClient client, GatewayCommandCode opCode, object data)
        {
            WebSocketCommand command = WebSocketCommand.CreateCommand(client, opCode, data);
            Commands.Enqueue(command);
        }
        
        /// <summary>
        /// Send a command to discord over the websocket
        /// </summary>
        /// <param name="opCode">Command code to send</param>
        /// <param name="data">Data to send</param>
        private async Task SendImmediatelyAsync(GatewayCommandCode opCode, object data)
        {
            CommandPayload payload = CommandPayload.CreatePayload(opCode, data);
            if (!await SendAsync(payload))
            {
                _logger.Debug($"{nameof(DiscordWebSocket)}.{nameof(SendImmediatelyAsync)} Failed to send command! {{0}}", opCode);
            }
            payload.Dispose();
        }
        
        internal async Task<bool> SendAsync(CommandPayload payload)
        {
            if (Handler == null)
            {
                return false;
            }
            
            string payloadData = JsonConvert.SerializeObject(payload, _client.ClientSerializerSettings);
            _logger.Verbose($"{nameof(DiscordWebSocket)}.{nameof(SendAsync)} Payload: {{0}}", payloadData);

            await Handler.SendAsync(payloadData);
            return true;
        }

        /// <summary>
        /// Reconnected to the websocket is a reconnect is requested and we are not shutting down
        /// </summary>
        public void ReconnectIfRequested()
        {
            if (ShouldReconnect && !_isShutdown)
            {
                _logger.Debug($"{nameof(DiscordWebSocket)}.{nameof(Disconnect)} Attempting Reconnect");
                ShouldReconnect = false;
                _reconnect.StartReconnect();
            }
        }

        internal void OnSocketConnected()
        {
            _reconnect.CancelReconnect();
        }
        
        internal void OnSocketReady(string sessionId)
        {
            _sessionId = sessionId;
            SocketHasConnected = true;
            ShouldResume = true;
            _reconnect.OnWebsocketReady();
            Commands.OnWebSocketReady();
        }

        internal void OnSocketDisconnected()
        {
            Commands.OnSocketDisconnected();
        }

        internal void OnSequenceUpdate(int? sequence)
        {
            if (sequence.HasValue)
            {
                _sequence = sequence.Value;
            }
        }

        internal async Task OnDiscordHello(GatewayHelloEvent hello)
        {
            _logger.Debug($"{nameof(DiscordWebSocket)}.{nameof(OnDiscordHello)}");

            // Client should now perform identification
            if (ShouldResume && !string.IsNullOrEmpty(_sessionId))
            {
                await Resume();
            }
            else
            {
                await Identify();
            }
            
            _heartbeat.SetupHeartbeat(hello.HeartbeatInterval);
        }

        /// <summary>
        /// Used to Identify the bot with discord
        /// </summary>
        private async Task Identify()
        {
            // Sent immediately after connecting. Opcode 2: Identify
            // Ref: https://discord.com/developers/docs/topics/gateway#identifying
            if (!_client.Initialized)
            {
                return;
            }
            
            _logger.Debug($"{nameof(DiscordWebSocket)}.{nameof(Identify)} Identifying bot with discord.");

            IdentifyCommand identify = new IdentifyCommand
            {
                Token = _client.Settings.ApiToken,
                Properties = new ConnectionProperties(),
                Intents = _client.Settings.Intents,
                Compress = false,
                LargeThreshold = 50,
                Shard = new List<int> {0, 1}
            };

            await SendImmediatelyAsync(GatewayCommandCode.Identify, identify);
        }
        
        /// <summary>
        /// Used to resume the current session with discord
        /// </summary>
        private async Task Resume()
        {
            if (!_client.Initialized)
            {
                return;
            }

            ResumeSessionCommand resume = new ResumeSessionCommand
            {
                Sequence = _sequence,
                SessionId = _sessionId,
                Token = _client.Settings.ApiToken
            };
            
            _logger.Debug($"{nameof(DiscordWebSocket)}.{nameof(Resume)} Attempting to resume session with ID: {{0}} Sequence: {{1}}", _sessionId, _sequence);

            await SendImmediatelyAsync(GatewayCommandCode.Resume, resume);
        }
        
        internal void OnHeartbeatAcknowledge()
        {
            _heartbeat.OnHeartbeatAcknowledge();
        }
        
        /// <summary>
        /// Sends a heartbeat to Discord
        /// </summary>
        internal Task SendHeartbeat()
        {
            if (Handler.IsConnected())
            {
                return SendImmediatelyAsync(GatewayCommandCode.Heartbeat, _sequence);
            }
            
            return Task.CompletedTask;
        }
        
        /// <summary>
        /// Sends a heartbeat to Discord
        /// </summary>
        internal Task RequestAllGuildMembers(Snowflake guildId)
        {
            return SendImmediatelyAsync(GatewayCommandCode.Heartbeat, new GuildMembersRequestCommand
            {
                Nonce = "DiscordExtension",
                GuildId = guildId,
            });
        }

        internal void OnInvalidSession(bool resume)
        {
            bool shouldResume = !string.IsNullOrEmpty(_sessionId) && resume;
            _logger.Warning("Invalid Session ID opcode received! Attempting to reconnect. Should Resume? {0}", shouldResume);
            Disconnect(true, shouldResume);
        }

        internal void OnReconnectRequested()
        {
            _logger.Debug("Discord has requested a reconnect. Reconnecting...");
            //If we disconnect normally our session becomes invalid per: https://discord.com/developers/docs/topics/gateway#resuming
            Disconnect(true, true, true);
        }
    }
}