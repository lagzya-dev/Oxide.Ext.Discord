using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Emojis;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Roles;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Entities.Voice;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.WebSockets.Handlers;
using Oxide.Plugins;
using WebSocketSharp;
using LogLevel = Oxide.Ext.Discord.Logging.LogLevel;

namespace Oxide.Ext.Discord.WebSockets
{
    /// <summary>
    /// Represents a listens for socket events
    /// </summary>
    public class SocketListener
    {
        /// <summary>
        /// The current session ID for the connected bot
        /// </summary>
        internal string SessionId;
        
        /// <summary>
        /// The current sequence number for the websocket
        /// </summary>
        internal int Sequence;

        private readonly BotClient _client;
        private readonly Socket _webSocket;
        private readonly ILogger _logger;
        private HeartbeatHandler _heartbeat;

        /// <summary>
        /// Creates a new socket listener
        /// </summary>
        /// <param name="client">Client this listener is for</param>
        /// <param name="socket">Socket this listener is for</param>
        /// <param name="logger">Logger for the client</param>
        public SocketListener(BotClient client, Socket socket, ILogger logger)
        {
            _client = client;
            _webSocket = socket;
            _logger = logger;
            _heartbeat = new HeartbeatHandler(_client, _webSocket, this, _logger);
        }

        /// <summary>
        /// Shutdown the SocketListener
        /// </summary>
        public void Shutdown()
        {
            _heartbeat?.DestroyHeartbeat();
            _heartbeat = null;
        }

        #region Socket Events
        /// <summary>
        /// Called when a socket is open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SocketOpened(object sender, EventArgs e)
        {
            _logger.Info("Discord socket opened!");
            _client.CallHook(DiscordHooks.OnDiscordWebsocketOpened);
            _webSocket.SocketState = SocketState.Connected;
        }

        /// <summary>
        /// Called when a socket is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SocketClosed(object sender, CloseEventArgs e)
        {
            //If the socket close came from the extension then this will be true
            if (sender is WebSocket socket && !_webSocket.IsCurrentSocket(socket))
            {
                _logger.Debug($"{nameof(SocketListener)}.{nameof(SocketClosed)} Socket closed event for non matching socket. Code: {e.Code}, reason: {e.Reason}");
                return;
            }
            
            if (e.Code == 1000 || e.Code == 4199)
            {
                _logger.Debug($"{nameof(SocketListener)}.{nameof(SocketClosed)} Discord WebSocket closed. Code: {e.Code}, reason: {e.Reason}");
            }
            else
            {
                _logger.Warning($"Discord WebSocket closed with abnormal close code. Code: {e.Code}, reason: {e.Reason}");
            }
            
            _client.CallHook(DiscordHooks.OnDiscordWebsocketClosed, e.Reason, e.Code, e.WasClean);
            _webSocket.SocketState = SocketState.Disconnected;
            _webSocket.DisposeSocket();

            if (!_client.Initialized)
            {
                return;
            }
            
            if (_webSocket.RequestedReconnect)
            {
                _webSocket.RequestedReconnect = false;
                _webSocket.Reconnect();
                return;
            }

            if (HandleDiscordClosedSocket(e.Code, e.Reason))
            {
                return;
            }

            _webSocket.Reconnect();
        }

        /// <summary>
        /// Parse out the closing reason if discord closed the socket
        /// </summary>
        /// <param name="code">Socket close code</param>
        /// <param name="reason">Socket close reason</param>
        /// <returns>True if discord closed the socket with one of it's close codes</returns>
        private bool HandleDiscordClosedSocket(int code, string reason)
        {
            if (!code.TryParse(out SocketCloseCode closeCode))
            {
                if(code >= 4000 && code < 5000)
                {
                    closeCode = SocketCloseCode.UnknownCloseCode;
                }
                else
                {
                    return false;
                }
            }

            bool reconnect = false;
            switch (closeCode)
            {
                case SocketCloseCode.UnknownError: 
                    _logger.Error("Discord had an unknown error. Reconnecting.");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.UnknownOpcode: 
                    _logger.Error($"Unknown gateway opcode sent: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.DecodeError: 
                    _logger.Error($"Invalid gateway payload sent: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.NotAuthenticated: 
                    _logger.Error($"Tried to send a payload before identifying: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.AuthenticationFailed: 
                    _logger.Error($"The given bot token is invalid. Please enter a valid token: {reason}");
                    break;
                
                case SocketCloseCode.AlreadyAuthenticated: 
                    _logger.Error($"The bot has already authenticated. Please don't identify more than once.: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.InvalidSequence: 
                    _logger.Error($"Invalid resume sequence. Doing full reconnect.: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.RateLimited: 
                    _logger.Error($"You're being rate limited. Please slow down how quickly you're sending requests: {reason}");
                    break;
                
                case SocketCloseCode.SessionTimedOut: 
                    _logger.Error($"Session has timed out. Starting a new one: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.InvalidShard: 
                    _logger.Error($"Invalid shared has been specified: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.ShardingRequired: 
                    _logger.Error($"Bot is in too many guilds. You must shard your bot: {reason}");
                    break;
                
                case SocketCloseCode.InvalidApiVersion: 
                    _logger.Error("Gateway is using invalid API version. Please contact Discord Extension Devs immediately!");
                    break;
                
                case SocketCloseCode.InvalidIntents: 
                    _logger.Error("Invalid intent(s) specified for the gateway. Please check that you're using valid intents in the connect.");
                    break;
                
                case SocketCloseCode.DisallowedIntent:
                    _logger.Error("The plugin is asking for an intent you have not granted your bot. Please go to your bot and enable the privileged gateway intents: https://support.discord.com/hc/en-us/articles/360040720412-Bot-Verification-and-Data-Whitelisting#privileged-intent-whitelisting");
                    break;
                
                case SocketCloseCode.UnknownCloseCode:
                    _logger.Error($"Discord has closed the gateway with a code we do not recognize. Code: {code}. Please Contact Discord Extension Authors.");
                    break;
                
                default:
                    return false;
            }

            if (reconnect)
            {
                _webSocket.ShouldAttemptResume = false;
                _webSocket.Reconnect();
            }
            
            return true;
        }

        /// <summary>
        /// Called when an error occurs on a socket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SocketErrored(object sender, ErrorEventArgs e)
        {
            if (sender is WebSocket socket && !_webSocket.IsCurrentSocket(socket))
            {
                return;
            }
            
            _client.CallHook(DiscordHooks.OnDiscordWebsocketErrored, e.Exception, e.Message);
            _logger.Exception("An error has occured in the websocket. Attempting to reconnect to discord.", e.Exception);
            _webSocket.Disconnect(true, false);
        }

        /// <summary>
        /// Called when a socket receives a message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SocketMessage(object sender, MessageEventArgs e)
        {
            EventPayload payload = JsonConvert.DeserializeObject<EventPayload>(e.Data);
            if (payload.Sequence.HasValue)
            {
                Sequence = payload.Sequence.Value;
            }

            if (_logger.IsLogging(LogLevel.Verbose))
            {
                _logger.Debug($"Received socket message, OpCode: {payload.OpCode}\nContent:\n{e.Data}");
            }
            else
            {
                _logger.Debug($"Received socket message, OpCode: {payload.OpCode}");
            }

            try
            {
                switch (payload.OpCode)
                {
                    // Dispatch (dispatches an event)
                    case GatewayEventCode.Dispatch:
                        HandleDispatch(payload);
                        break;

                    // Heartbeat
                    // https://discordapp.com/developers/docs/topics/gateway#gateway-heartbeat
                    case GatewayEventCode.Heartbeat:
                        HandleHeartbeat(payload);
                        break;

                    // Reconnect (used to tell clients to reconnect to the gateway)
                    // we should immediately reconnect here
                    case GatewayEventCode.Reconnect:
                        HandleReconnect(payload);
                        break;

                    // Invalid Session (used to notify client they have an invalid session ID)
                    case GatewayEventCode.InvalidSession:
                        HandleInvalidSession(payload);
                        break;

                    // Hello (sent immediately after connecting, contains heartbeat and server debug information)
                    case GatewayEventCode.Hello:
                        HandleHello(payload);
                        break;

                    // Heartbeat ACK (sent immediately following a client heartbeat
                    // that was received)
                    // (See 'zombied or failed connections')
                    case GatewayEventCode.HeartbeatAcknowledge:
                        HandleHeartbeatAcknowledge(payload);
                        break;

                    default:
                        UnhandledOpCode(payload);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Exception($"{nameof(SocketListener)}.{nameof(SocketMessage)} Exception Occured. Please give error message below to Discord Extension Authors:\n", ex);
            }
        }
        #endregion

        #region Discord Events
        private void HandleDispatch(EventPayload payload)
        {
            _logger.Debug($"Received OpCode: Dispatch, event: {payload.EventName}");

            // Listed here: https://discordapp.com/developers/docs/topics/gateway#commands-and-events-gateway-events
            switch (payload.EventName)
            {
                case "READY":
                    HandleDispatchReady(payload);
                    break;

                case "RESUMED":
                    HandleDispatchResumed(payload);
                    break;

                case "CHANNEL_CREATE":
                    HandleDispatchChannelCreate(payload);
                    break;

                case "CHANNEL_UPDATE":
                    HandleDispatchChannelUpdate(payload);
                    break;

                case "CHANNEL_DELETE":
                    HandleDispatchChannelDelete(payload);
                    break;

                case "CHANNEL_PINS_UPDATE":
                    HandleDispatchChannelPinUpdate(payload);
                    break;

                case "GUILD_CREATE":
                    HandleDispatchGuildCreate(payload);
                    break;

                case "GUILD_UPDATE":
                    HandleDispatchGuildUpdate(payload);
                    break;

                case "GUILD_DELETE":
                    HandleDispatchGuildDelete(payload);
                    break;

                case "GUILD_BAN_ADD":
                    HandleDispatchGuildBanAdd(payload);
                    break;

                case "GUILD_BAN_REMOVE":
                    HandleDispatchGuildBanRemove(payload);
                    break;

                case "GUILD_EMOJIS_UPDATE":
                    HandleDispatchGuildEmojisUpdate(payload);
                    break;

                case "GUILD_INTEGRATIONS_UPDATE":
                    HandleDispatchGuildIntegrationsUpdate(payload);
                    break;

                case "GUILD_MEMBER_ADD":
                    HandleDispatchGuildMemberAdd(payload);
                    break;

                case "GUILD_MEMBER_REMOVE":
                    HandleDispatchGuildMemberRemove(payload);
                    break;

                case "GUILD_MEMBER_UPDATE":
                    HandleDispatchGuildMemberUpdate(payload);
                    break;

                case "GUILD_MEMBERS_CHUNK":
                    HandleDispatchGuildMembersChunk(payload);
                    break;

                case "GUILD_ROLE_CREATE":
                    HandleDispatchGuildRoleCreate(payload);
                    break;

                case "GUILD_ROLE_UPDATE":
                    HandleDispatchGuildRoleUpdate(payload);
                    break;

                case "GUILD_ROLE_DELETE":
                    HandleDispatchGuildRoleDelete(payload);
                    break;
                
                case "INTEGRATION_CREATE":
                    HandleDispatchIntegrationCreate(payload);
                    break;
                
                case "INTEGRATION_UPDATE":
                    HandleDispatchIntegrationUpdate(payload);
                    break;
                
                case "INTEGRATION_DELETE":
                    HandleDispatchIntegrationDelete(payload);
                    break;    

                case "MESSAGE_CREATE":
                    HandleDispatchMessageCreate(payload);
                    break;

                case "MESSAGE_UPDATE":
                    HandleDispatchMessageUpdate(payload);
                    break;

                case "MESSAGE_DELETE":
                    HandleDispatchMessageDelete(payload);
                    break;

                case "MESSAGE_DELETE_BULK":
                    HandleDispatchMessageDeleteBulk(payload);
                    break;

                case "MESSAGE_REACTION_ADD":
                    HandleDispatchMessageReactionAdd(payload);
                    break;

                case "MESSAGE_REACTION_REMOVE":
                    HandleDispatchMessageReactionRemove(payload);
                    break;

                case "MESSAGE_REACTION_REMOVE_ALL":
                    HandleDispatchMessageReactionRemoveAll(payload);
                    break;
                
                case "MESSAGE_REACTION_REMOVE_EMOJI":
                    HandleDispatchMessageReactionRemoveEmoji(payload);
                    break;

                case "PRESENCE_UPDATE":
                    HandleDispatchPresenceUpdate(payload);
                    break;

                // Bots should ignore this
                case "PRESENCES_REPLACE":
                    break;

                case "TYPING_START":
                    HandleDispatchTypingStart(payload);
                    break;

                case "USER_UPDATE":
                    HandleDispatchUserUpdate(payload);
                    break;

                case "VOICE_STATE_UPDATE":
                    HandleDispatchVoiceStateUpdate(payload);
                    break;

                case "VOICE_SERVER_UPDATE":
                    HandleDispatchVoiceServerUpdate(payload);
                    break;

                case "WEBHOOKS_UPDATE":
                    HandleDispatchWebhooksUpdate(payload);
                    break;

                case "INVITE_CREATE":
                    HandleDispatchInviteCreate(payload);
                    break;

                case "INVITE_DELETE":
                    HandleDispatchInviteDelete(payload);
                    break;
                
                case "INTERACTION_CREATE":
                    HandleDispatchInteractionCreate(payload);
                    break;

                case "APPLICATION_COMMAND_CREATE":
                    HandleDispatchApplicationCommandCreate(payload);
                    break;                
                
                case "APPLICATION_COMMAND_UPDATE":
                    HandleDispatchApplicationCommandUpdate(payload);
                    break;                
                
                case "APPLICATION_COMMAND_DELETE":
                    HandleDispatchApplicationCommandDelete(payload);
                    break;                
                
                case "GUILD_JOIN_REQUEST_DELETE":
                    HandleGuildJoinRequestDelete(payload);
                    break;
                
                default:
                    HandleDispatchUnhandledEvent(payload);
                    break;
            }
        }

        //https://discord.com/developers/docs/topics/gateway#ready
        private void HandleDispatchReady(EventPayload payload)
        {
            GatewayReadyEvent ready = payload.EventData.ToObject<GatewayReadyEvent>();
            foreach (Guild guild in ready.Guilds.Values)
            {
                _client.AddGuildOrUpdate(guild);
            }
            SessionId = ready.SessionId;
            _client.Application = ready.Application;
            _client.Bot = ready.User;
            _webSocket.ReconnectRetries = 0;
            _logger.Info($"Your bot was found in {ready.Guilds.Count} Guilds!");

            if (_client.ReadyData == null)
            {
                _client.ReadyData = ready;
                _client.CallHook(DiscordHooks.OnDiscordGatewayReady, ready);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#resumed`
        private void HandleDispatchResumed(EventPayload payload)
        {
            GatewayResumedEvent resumed = payload.EventData.ToObject<GatewayResumedEvent>();
            _logger.Info("Session resumed successfully!");
            _client.CallHook(DiscordHooks.OnDiscordGatewayResumed, resumed);
        }

        //https://discord.com/developers/docs/topics/gateway#channel-create
        private void HandleDispatchChannelCreate(EventPayload payload)
        {
            Channel channel = payload.EventData.ToObject<Channel>();
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchChannelCreate)}: ID: {channel.Id} Type: {channel.Type}. Guild ID: {channel.GuildId}");
            
            if (channel.Type == ChannelType.Dm || channel.Type == ChannelType.GroupDm)
            {
                _client.AddDirectChannel(channel);
                _client.CallHook(DiscordHooks.OnDiscordDirectChannelCreated, channel);
            }
            else
            {
                Guild guild = _client.GetGuild(channel.GuildId);
                if (guild != null && guild.IsAvailable)
                {
                    guild.Channels[channel.Id] = channel;
                    _client.CallHook(DiscordHooks.OnDiscordGuildChannelCreated, channel, guild);
                }
            }
        }

        //https://discord.com/developers/docs/topics/gateway#channel-update
        private void HandleDispatchChannelUpdate(EventPayload payload)
        {
            Channel update = payload.EventData.ToObject<Channel>();
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchChannelUpdate)} ID: {update.Id} Type: {update.Type} Guild ID: {update.GuildId}");
            
            if (update.Type == ChannelType.Dm || update.Type == ChannelType.GroupDm)
            {
                Channel channel = _client.GetChannel(update.Id, null);
                if (channel == null)
                {
                    _client.AddDirectChannel(update);
                    _client.CallHook(DiscordHooks.OnDiscordDirectChannelUpdated, update, null);
                }
                else
                {
                    Channel previous = channel.Update(update);
                    _client.CallHook(DiscordHooks.OnDiscordDirectChannelUpdated, channel, previous);
                }
            }
            else
            {
                Guild guild = _client.GetGuild(update.GuildId);
                if (guild != null && guild.IsAvailable)
                {
                    Channel channel = guild.Channels[update.Id];
                    if (channel == null)
                    {
                        guild.Channels[update.Id] = update;
                        _client.CallHook(DiscordHooks.OnDiscordGuildChannelUpdated, update, null, guild);
                    }
                    else
                    {
                        Channel previous = channel.Update(update);
                        _client.CallHook(DiscordHooks.OnDiscordGuildChannelUpdated, channel, previous, guild);
                    }
                }
            }
        }

        //https://discord.com/developers/docs/topics/gateway#channel-delete
        private void HandleDispatchChannelDelete(EventPayload payload)
        {
            Channel channel = payload.EventData.ToObject<Channel>();
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchChannelDelete)} ID: {channel.Id} Type: {channel.Type} Guild ID: {channel.GuildId}");
            Guild guild = _client.GetGuild(channel.GuildId);
            if (channel.Type == ChannelType.Dm || channel.Type == ChannelType.GroupDm)
            {
                _client.RemoveDirectMessageChannel(channel.Id);
                _client.CallHook(DiscordHooks.OnDiscordDirectChannelDeleted, channel);
            }
            else
            {
                guild.Channels.Remove(channel.Id);
                _client.CallHook(DiscordHooks.OnDiscordGuildChannelDeleted, channel, guild);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#channel-pins-update
        private void HandleDispatchChannelPinUpdate(EventPayload payload)
        {
            ChannelPinsUpdatedEvent pins = payload.EventData.ToObject<ChannelPinsUpdatedEvent>();
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchChannelPinUpdate)} Channel ID: {pins.GuildId} Guild ID: {pins.GuildId}");

            Guild guild = _client.GetGuild(pins.GuildId);
            Channel channel = _client.GetChannel(pins.ChannelId, pins.GuildId);
            if (pins.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildChannelPinsUpdated, pins, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectChannelPinsUpdated, pins, channel);
            }
        }

        // NOTE: Some elements of Guild object is only sent with GUILD_CREATE
        //https://discord.com/developers/docs/topics/gateway#guild-create
        private void HandleDispatchGuildCreate(EventPayload payload)
        {
            Guild guild = payload.EventData.ToObject<Guild>();
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildCreate)} Guild ID: {guild.Id} Name: {guild.Name}");
            
            Guild existing = _client.GetGuild(guild.Id);
            if (existing == null || !existing.IsAvailable && guild.IsAvailable)
            {
                _client.AddGuildOrUpdate(guild);
                existing = _client.GetGuild(guild.Id);
                _client.CallHook(DiscordHooks.OnDiscordGuildCreated, existing);
            }

            if (!existing.HasLoadedAllMembers)
            {
                _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildCreate)} Guild is now requesting all guild members.");
                //Request all guild members so we can be sure we have them all.
                _client.RequestGuildMembers(new GuildMembersRequestCommand
                {
                    Nonce = "DiscordExtension",
                    GuildId = guild.Id,
                });
            }
        }

        //https://discord.com/developers/docs/topics/gateway#guild-update
        private void HandleDispatchGuildUpdate(EventPayload payload)
        {
            Guild guild = payload.EventData.ToObject<Guild>();
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildUpdate)} Guild ID: {guild.Id}");
            
            Guild previous = _client.GetGuild(guild.Id)?.Update(guild);
            _client.CallHook(DiscordHooks.OnDiscordGuildUpdated, guild, previous);
        }

        //https://discord.com/developers/docs/topics/gateway#guild-delete
        private void HandleDispatchGuildDelete(EventPayload payload)
        {
            Guild guild = payload.EventData.ToObject<Guild>();
            if (guild.Unavailable ?? false) // There is an outage with Discord
            {
                _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildDelete)} There is an outage with the guild. Guild ID: {guild.Id}");
                Guild existing = _client.GetGuild(guild.Id);
                if (existing != null)
                {
                    existing.Unavailable = guild.Unavailable;
                }
                else
                {
                    _client.AddGuild(guild);
                }
                
                _client.CallHook(DiscordHooks.OnDiscordGuildUnavailable, existing ?? guild);
            }
            else
            {
                Guild existing = _client.GetGuild(guild.Id);
                _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildDelete)} Guild deleted or user removed from guild. Guild ID: {guild.Id} Name: {existing?.Name ?? guild.Name}");
                _client.RemoveGuild(guild.Id);
                _client.CallHook(DiscordHooks.OnDiscordGuildDeleted, existing ?? guild);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#guild-ban-add
        private void HandleDispatchGuildBanAdd(EventPayload payload)
        {
            GuildMemberBannedEvent ban = payload.EventData.ToObject<GuildMemberBannedEvent>();
            Guild guild = _client.GetGuild(ban.GuildId);
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildBanAdd)} User was banned from the guild. Guild ID: {ban.GuildId} Guild Name: {guild?.Name} User ID: {ban.User.Id} User Name: {ban.User.GetFullUserName}");
            _client.CallHook(DiscordHooks.OnDiscordGuildMemberBanned, ban, guild);
        }

        //https://discord.com/developers/docs/topics/gateway#guild-ban-remove
        private void HandleDispatchGuildBanRemove(EventPayload payload)
        {
            GuildMemberBannedEvent ban = payload.EventData.ToObject<GuildMemberBannedEvent>();
            Guild guild = _client.GetGuild(ban.GuildId);
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildBanRemove)} User was unbanned from the guild. Guild ID: {ban.GuildId} Guild Name: {guild?.Name} User ID: {ban.User.Id} User Name: {ban.User.GetFullUserName}");
            _client.CallHook(DiscordHooks.OnDiscordGuildMemberUnbanned, ban.User, ban.GuildId);
        }
        
        //https://discord.com/developers/docs/topics/gateway#guild-emojis-update
        private void HandleDispatchGuildEmojisUpdate(EventPayload payload)
        {
            GuildEmojisUpdatedEvent emojis = payload.EventData.ToObject<GuildEmojisUpdatedEvent>();
            Guild guild = _client.GetGuild(emojis.GuildId);
            if (guild != null)
            {
                _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildEmojisUpdate)} Guild ID: {emojis.GuildId} Guild Name: {guild.Name}");

                if (guild.IsAvailable)
                {
                    Hash<Snowflake, Emoji> previous = guild.Emojis.Copy();

                    List<Snowflake> removedEmojis = new List<Snowflake>();

                    foreach (Snowflake id in guild.Emojis.Keys)
                    {
                        if (!emojis.Emojis.ContainsKey(id))
                        {
                            removedEmojis.Add(id);
                        }
                    }

                    for (int index = 0; index < removedEmojis.Count; index++)
                    {
                        Snowflake id = removedEmojis[index];
                        guild.Emojis.Remove(id);
                    }

                    guild.Emojis.RemoveAll(e => e.EmojiId.HasValue && !emojis.Emojis.ContainsKey(e.EmojiId.Value));
                    
                    foreach (Emoji emoji in emojis.Emojis.Values)
                    {
                        Emoji existing = guild.Emojis[emojis.GuildId];
                        if (existing != null)
                        {
                            existing.Update(emoji);
                        }
                        else
                        {
                            guild.Emojis[emojis.GuildId] = emoji;
                        }
                    }

                    _client.CallHook(DiscordHooks.OnDiscordGuildEmojisUpdated, emojis, previous, guild);
                }
            }
        }

        //https://discord.com/developers/docs/topics/gateway#guild-integrations-update
        private void HandleDispatchGuildIntegrationsUpdate(EventPayload payload)
        {
            GuildIntegrationsUpdatedEvent integration = payload.EventData.ToObject<GuildIntegrationsUpdatedEvent>();
            Guild guild = _client.GetGuild(integration.GuildId);
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildIntegrationsUpdate)} Guild ID: {integration.GuildId} Guild Name: {guild?.Name}");
            _client.CallHook(DiscordHooks.OnDiscordGuildIntegrationsUpdated, integration, guild);
        }

        //https://discord.com/developers/docs/topics/gateway#guild-member-add
        private void HandleDispatchGuildMemberAdd(EventPayload payload)
        {
            GuildMemberAddedEvent member = payload.EventData.ToObject<GuildMemberAddedEvent>();
            Guild guild = _client.GetGuild(member.GuildId);
            if (guild != null && guild.IsAvailable)
            {
                guild.Members[member.User.Id] = member;
                _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildMemberAdd)} Guild ID: {member.GuildId} Guild Name: {guild.Name} User ID: {member.User.Id} User Name: {member.User.GetFullUserName}");
                _client.CallHook(DiscordHooks.OnDiscordGuildMemberAdded, member, guild);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#guild-member-remove
        private void HandleDispatchGuildMemberRemove(EventPayload payload)
        {
            GuildMemberRemovedEvent remove = payload.EventData.ToObject<GuildMemberRemovedEvent>();
            Guild guild = _client.GetGuild(remove.GuildId);
            if (guild != null && guild.IsAvailable)
            {
                GuildMember member = guild.Members[remove.User.Id];
                if (member != null)
                {
                    guild.Members.Remove(remove.User.Id);
                }

                _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildMemberRemove)} Guild ID: {remove.GuildId} Guild Name: {guild.Name} User ID: {member?.User.Id} User Name: {member?.User.GetFullUserName}");
                _client.CallHook(DiscordHooks.OnDiscordGuildMemberRemoved, remove, guild);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#guild-member-update
        private void HandleDispatchGuildMemberUpdate(EventPayload payload)
        {
            GuildMemberUpdatedEvent update = payload.EventData.ToObject<GuildMemberUpdatedEvent>();
            Guild guild = _client.GetGuild(update.GuildId);
            if (guild != null && guild.IsAvailable)
            {
                GuildMember member = guild.Members[update.User.Id];
                if (member != null)
                {
                    _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildMemberUpdate)} GUILD_MEMBER_UPDATE: Guild ID: {update.GuildId} User ID: {update.User.Id}");
                    GuildMember previous = member.Update(update);
                    _client.CallHook(DiscordHooks.OnDiscordGuildMemberUpdated, update, previous, guild);
                }
                else
                {
                    guild.Members[update.User.Id] = update;
                    _client.CallHook(DiscordHooks.OnDiscordGuildMemberUpdated, update, null, guild);
                }
            }
        }

        //https://discord.com/developers/docs/topics/gateway#guild-members-chunk
        private void HandleDispatchGuildMembersChunk(EventPayload payload)
        {
            GuildMembersChunkEvent chunk = payload.EventData.ToObject<GuildMembersChunkEvent>();

            Guild guild = _client.GetGuild(chunk.GuildId);

            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildMembersChunk)}: Guild ID: {chunk.GuildId} Guild Name: {guild?.Name} Nonce: {chunk.Nonce}");
            //Used to load all members in the discord server
            if (chunk.Nonce == "DiscordExtension")
            {
                if (guild != null && guild.IsAvailable)
                {
                    for (int index = 0; index < chunk.Members.Count; index++)
                    {
                        GuildMember member = chunk.Members[index];
                        if (!guild.Members.ContainsKey(member.User.Id))
                        {
                            guild.Members[member.User.Id] = member;
                        }
                    }
                    
                    //Once we've loaded all guild members call hook
                    if (chunk.ChunkIndex + 1 == chunk.ChunkCount)
                    {
                        guild.HasLoadedAllMembers = true;
                        _client.CallHook(DiscordHooks.OnDiscordGuildMembersLoaded, guild);
                    }
                }
                
                return;
            }

            _client.CallHook(DiscordHooks.OnDiscordGuildMembersChunk, chunk, guild);
        }

        //https://discord.com/developers/docs/topics/gateway#guild-role-create
        private void HandleDispatchGuildRoleCreate(EventPayload payload)
        {
            GuildRoleCreatedEvent role = payload.EventData.ToObject<GuildRoleCreatedEvent>();
            Guild guild = _client.GetGuild(role.GuildId);
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildRoleCreate)} Guild ID: {role.GuildId} Guild Name: {guild?.Name} Role ID: {role.Role.Id} Role Name: {role.Role.Name}");
            if (guild != null && guild.IsAvailable)
            {
                guild.Roles[role.Role.Id] = role.Role;
                _client.CallHook(DiscordHooks.OnDiscordGuildRoleCreated, role.Role, guild);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#guild-role-update
        private void HandleDispatchGuildRoleUpdate(EventPayload payload)
        {
            GuildRoleUpdatedEvent update = payload.EventData.ToObject<GuildRoleUpdatedEvent>();
            Role updatedRole = update.Role;
            Guild guild = _client.GetGuild(update.GuildId);
            
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildRoleUpdate)} Guild ID: {update.GuildId} Guild Name: {guild?.Name} Role ID: {update.Role.Id} Role Name: {update.Role.Name}");
            if (guild != null && guild.IsAvailable)
            {
                Role existing = guild.Roles[updatedRole.Id];
                if (existing != null)
                {
                    Role previous = existing.UpdateRole(updatedRole);
                    _client.CallHook(DiscordHooks.OnDiscordGuildRoleUpdated, existing, previous, guild);
                }
                else
                {
                    guild.Roles[updatedRole.Id] = updatedRole;
                    _client.CallHook(DiscordHooks.OnDiscordGuildRoleUpdated, updatedRole, null, guild);
                }
            }
        }

        //https://discord.com/developers/docs/topics/gateway#guild-role-delete
        private void HandleDispatchGuildRoleDelete(EventPayload payload)
        {
            GuildRoleDeletedEvent delete = payload.EventData.ToObject<GuildRoleDeletedEvent>();
            Guild guild = _client.GetGuild(delete.GuildId);
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchGuildRoleDelete)} Guild ID: {delete.GuildId} Guild Name: {guild?.Name} Role ID: {delete.RoleId}");
            if (guild != null && guild.IsAvailable)
            {
                Role role = guild.Roles[delete.RoleId];
                if (role != null)
                {
                    guild.Roles.Remove(delete.RoleId);
                    _client.CallHook(DiscordHooks.OnDiscordGuildRoleDeleted, role, guild);
                }
            }
        }
        
        //TODO: Add Link
        private void HandleDispatchIntegrationCreate(EventPayload payload)
        {
            IntegrationCreatedEvent integration = payload.EventData.ToObject<IntegrationCreatedEvent>();
            Guild guild = _client.GetGuild(integration.GuildId); 
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchInteractionCreate)} Guild ID: {integration.GuildId} Guild Name: {guild?.Name} Integration ID: {integration.Id}");
            _client.CallHook(DiscordHooks.OnDiscordGuildIntegrationCreated, integration, guild);
        }

        //TODO: Add Link
        private void HandleDispatchIntegrationUpdate(EventPayload payload)
        {
            IntegrationUpdatedEvent integration = payload.EventData.ToObject<IntegrationUpdatedEvent>();
            Guild guild = _client.GetGuild(integration.GuildId); 
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchIntegrationUpdate)} Guild ID: {integration.GuildId} Guild Name: {guild?.Name} Integration ID: {integration.Id}");
            _client.CallHook(DiscordHooks.OnDiscordGuildIntegrationUpdated, integration, guild);
        }

        //TODO: Add Link
        private void HandleDispatchIntegrationDelete(EventPayload payload)
        {
            IntegrationDeletedEvent integration = payload.EventData.ToObject<IntegrationDeletedEvent>();
            Guild guild = _client.GetGuild(integration.GuildId); 
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchIntegrationDelete)} Guild ID: {integration.GuildId} Guild Name: {guild?.Name} Integration ID: {integration.Id}");
            _client.CallHook(DiscordHooks.OnDiscordIntegrationDeleted, integration, guild);
        }

        //https://discord.com/developers/docs/topics/gateway#message-create
        private void HandleDispatchMessageCreate(EventPayload payload)
        {
            DiscordMessage message = payload.EventData.ToObject<DiscordMessage>();
            Guild guild = _client.GetGuild(message.GuildId);
            Channel channel = _client.GetChannel(message.ChannelId, message.GuildId);
            
            if (channel != null)
            {
                channel.LastMessageId = message.Id;
            }

            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageCreate)}: Guild ID: {message.GuildId} Channel ID: {message.ChannelId} Message ID: {message.Id}");
            
            if (!message.Author.Bot.HasValue || !message.Author.Bot.Value)
            {
                if(!string.IsNullOrEmpty(message.Content) && DiscordExtension.DiscordCommand.HasCommands() && DiscordExtension.DiscordConfig.Commands.CommandPrefixes.Contains(message.Content[0]))
                {
                    message.Content.TrimStart(DiscordExtension.DiscordConfig.Commands.CommandPrefixes).ParseCommand(out string command, out string[] args);
                    _logger.Debug($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageCreate)} Cmd: {command} Args: {string.Join(" ", args)}");
                    
                    if (message.GuildId.HasValue && message.GuildId.Value.IsValid() && DiscordExtension.DiscordCommand.HandleGuildCommand(_client, message, channel, command, args))
                    {
                        _logger.Debug($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageCreate)} Guild Handled Cmd: {command}");
                        return;
                    }

                    if (message.GuildId == null && DiscordExtension.DiscordCommand.HandleDirectMessageCommand(_client, message, channel, command, args))
                    {
                        _logger.Debug($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageCreate)} Direct Handled Cmd: {command}");
                        return;
                    }
                }

                if (DiscordExtension.DiscordSubscriptions.HasSubscriptions())
                {
                    DiscordExtension.DiscordSubscriptions.HandleMessage(message, channel);
                }
            }

            if (message.GuildId.HasValue && message.GuildId.Value.IsValid())
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildMessageCreated, message, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectMessageCreated, message, channel);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#message-update
        private void HandleDispatchMessageUpdate(EventPayload payload)
        {
            DiscordMessage message = payload.EventData.ToObject<DiscordMessage>();
            Guild guild = _client.GetGuild(message.GuildId);
            Channel channel = _client.GetChannel(message.ChannelId, message.GuildId);

            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageUpdate)} Guild ID: {message.GuildId} Guild Name: {guild?.Name}  Channel ID: {message.ChannelId} Channel Name: {channel?.Name} Message ID: {message.Id}");
            if (guild != null)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildMessageUpdated, message, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectMessageUpdated, message, channel);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#message-delete
        private void HandleDispatchMessageDelete(EventPayload payload)
        {
            MessageDeletedEvent message = payload.EventData.ToObject<MessageDeletedEvent>();
            Guild guild = _client.GetGuild(message.GuildId);
            Channel channel = _client.GetChannel(message.ChannelId, message.GuildId);
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageDelete)} Message ID: {message.Id} Channel ID: {message.ChannelId} Channel Name: {channel?.Name} Guild Id: {message.GuildId} Guild Name: {guild?.Name}");
            
            if (message.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildMessageDeleted, message, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectMessageDeleted, message, channel);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#message-delete-bulk
        private void HandleDispatchMessageDeleteBulk(EventPayload payload)
        {
            MessageBulkDeletedEvent bulkDelete = payload.EventData.ToObject<MessageBulkDeletedEvent>();
            Guild guild = _client.GetGuild(bulkDelete.GuildId);
            Channel channel = _client.GetChannel(bulkDelete.ChannelId, bulkDelete.GuildId);
            
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageDeleteBulk)} Channel ID: {bulkDelete.ChannelId} Channel Name: {channel?.Name} Guild ID: {bulkDelete.GuildId} Guild Name: {guild?.Name}");
            if (bulkDelete.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildMessagesBulkDeleted, bulkDelete.Ids, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectMessagesBulkDeleted, bulkDelete.Ids, channel, guild);
            }
            
        }
        //https://discord.com/developers/docs/topics/gateway#message-reaction-add
        private void HandleDispatchMessageReactionAdd(EventPayload payload)
        {
            MessageReactionAddedEvent reaction = payload.EventData.ToObject<MessageReactionAddedEvent>();
            Guild guild = _client.GetGuild(reaction.GuildId);
            Channel channel = _client.GetChannel(reaction.ChannelId, reaction.GuildId);
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageReactionAdd)} Emoji: {reaction.Emoji.Name} Channel ID: {reaction.ChannelId} Channel Name: {channel?.Name} Message ID: {reaction.MessageId} User ID: {reaction.UserId} Guild ID: {reaction.GuildId} Guild Name: {guild?.Name}");
            
            if (reaction.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildMessageReactionAdded, reaction, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectMessageReactionAdded, reaction, channel);
            } 
        }

        //https://discord.com/developers/docs/topics/gateway#message-reaction-remove
        private void HandleDispatchMessageReactionRemove(EventPayload payload)
        {
            MessageReactionRemovedEvent reaction = payload.EventData.ToObject<MessageReactionRemovedEvent>();
            Guild guild = _client.GetGuild(reaction.GuildId);
            Channel channel = _client.GetChannel(reaction.ChannelId, reaction.GuildId);
            
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageReactionRemove)} Emoji: {reaction.Emoji.Name} Channel ID: {reaction.ChannelId} Channel Name: {channel?.Name} Message ID: {reaction.MessageId} User ID: {reaction.UserId} Guild ID: {reaction.GuildId} Guild Name: {guild?.Name}");
            
            if (reaction.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildMessageReactionRemoved, reaction, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectMessageReactionRemoved, reaction, channel);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#message-reaction-remove-all
        private void HandleDispatchMessageReactionRemoveAll(EventPayload payload)
        {
            MessageReactionRemovedAllEvent reaction = payload.EventData.ToObject<MessageReactionRemovedAllEvent>();
            Guild guild = _client.GetGuild(reaction.GuildId);
            Channel channel = _client.GetChannel(reaction.ChannelId, reaction.GuildId);
            
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageReactionRemoveAll)} Channel ID: {reaction.ChannelId} Channel Name: {channel?.Name} Message ID: {reaction.MessageId} Guild ID: {reaction.GuildId} Guild Name: {guild?.Name}");
            
            if (reaction.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildMessageReactionRemovedAll, reaction, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectMessageReactionRemovedAll, reaction, channel);
            }
        }        
        
        //https://discord.com/developers/docs/topics/gateway#message-reaction-remove-emoji
        private void HandleDispatchMessageReactionRemoveEmoji(EventPayload payload)
        {
            MessageReactionRemovedAllEmojiEvent reaction = payload.EventData.ToObject<MessageReactionRemovedAllEmojiEvent>();
            Guild guild = _client.GetGuild(reaction.GuildId);
            Channel channel = _client.GetChannel(reaction.ChannelId, reaction.GuildId);
            
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchMessageReactionRemoveAll)} Emoji: {reaction.Emoji.Name} Channel ID: {reaction.ChannelId} Channel Name: {channel?.Name} Message ID: {reaction.MessageId} Guild ID: {reaction.GuildId} Guild Name: {guild?.Name}");
            
            if (reaction.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildMessageReactionEmojiRemoved, reaction, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectMessageReactionEmojiRemoved, reaction, channel);
            }
        }

        /// <summary>
        ///  * From Discord API docs:
        ///  * The user object within this event can be partial, the only field which must be sent is the id field, everything else is optional.
        ///  * Along with this limitation, no fields are required, and the types of the fields are not validated.
        ///  * Your client should expect any combination of fields and types within this event
        /// </summary>
        /// <param name="payload"></param>
        /// https://discord.com/developers/docs/topics/gateway#presence-update
        private void HandleDispatchPresenceUpdate(EventPayload payload)
        {
            PresenceUpdatedEvent update = payload.EventData.ToObject<PresenceUpdatedEvent>();
            DiscordUser updateUser = update.User;
            
            Guild guild = _client.GetGuild(update.GuildId);
            if (guild != null && guild.IsAvailable)
            {
                GuildMember member = guild.Members[updateUser.Id];
                if (member != null)
                {
                    DiscordUser previous = member.User.Update(updateUser);
                    _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchPresenceUpdate)} Guild ID: {update.GuildId} User ID: {update.User} Status: {update.Status}");
                    _client.CallHook(DiscordHooks.OnDiscordGuildMemberPresenceUpdated, member, guild);
                }
            }
        }

        //https://discord.com/developers/docs/topics/gateway#typing-start
        private void HandleDispatchTypingStart(EventPayload payload)
        {
            TypingStartedEvent typing = payload.EventData.ToObject<TypingStartedEvent>();
            Guild guild = _client.GetGuild(typing.GuildId);
            Channel channel = _client.GetChannel(typing.ChannelId, typing.GuildId);
            
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchTypingStart)} Channel ID: {typing.ChannelId} Channel Name: {channel?.Name} User ID: {typing.UserId} Guild ID: {typing.GuildId} Guild Name: {guild?.Name}");

            if (typing.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildTypingStarted, typing, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectTypingStarted, typing, channel);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#user-update
        private void HandleDispatchUserUpdate(EventPayload payload)
        {
            DiscordUser user = payload.EventData.ToObject<DiscordUser>();

            foreach (Guild guild in _client.Servers.Values)
            {
                if (guild.IsAvailable)
                {
                    GuildMember memberUpdate = guild.Members[user.Id];
                    memberUpdate?.User.Update(user);
                }
            }

            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchUserUpdate)} User ID: {user.Id}");
            _client.CallHook(DiscordHooks.OnDiscordUserUpdated, user);
        }

        //https://discord.com/developers/docs/topics/gateway#voice-state-update
        private void HandleDispatchVoiceStateUpdate(EventPayload payload)
        {
            VoiceState voice = payload.EventData.ToObject<VoiceState>();
            Guild guild = _client.GetGuild(voice.GuildId);
            Channel channel = _client.GetChannel(voice.ChannelId, voice.GuildId);

            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchVoiceStateUpdate)} Guild ID: {voice.GuildId} Guild Name: {guild?.Name} Channel ID: {voice.ChannelId} Channel Name: {channel?.Name} User ID: {voice.UserId}");
            
            if (voice.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildVoiceStateUpdated, voice, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectVoiceStateUpdated, voice, channel);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#voice-server-update
        private void HandleDispatchVoiceServerUpdate(EventPayload payload)
        {
            VoiceServerUpdatedEvent voice = payload.EventData.ToObject<VoiceServerUpdatedEvent>();
            Guild guild = _client.GetGuild(voice.GuildId);
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchVoiceServerUpdate)} Guild ID: {voice.GuildId} Guild Name: {guild?.Name}");
            _client.CallHook(DiscordHooks.OnDiscordGuildVoiceServerUpdated, voice, guild);
        }

        //https://discord.com/developers/docs/topics/gateway#webhooks-update
        private void HandleDispatchWebhooksUpdate(EventPayload payload)
        {
            WebhooksUpdatedEvent webhook = payload.EventData.ToObject<WebhooksUpdatedEvent>();
            Guild guild = _client.GetGuild(webhook.GuildId);
            Channel channel = _client.GetChannel(webhook.ChannelId, webhook.GuildId);
            
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchWebhooksUpdate)} Guild ID: {webhook.GuildId} Guild Name {guild?.Name} Channel ID: {webhook.ChannelId} Channel Name: {channel?.Name}");
            _client.CallHook(DiscordHooks.OnDiscordGuildWebhookUpdated, webhook, channel, guild);
        }

        //https://discord.com/developers/docs/topics/gateway#invite-create
        private void HandleDispatchInviteCreate(EventPayload payload)
        {
            InviteCreatedEvent invite = payload.EventData.ToObject<InviteCreatedEvent>();
            Guild guild = _client.GetGuild(invite.GuildId);
            Channel channel = _client.GetChannel(invite.ChannelId, invite.GuildId);

            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchInviteCreate)} Guild ID: {invite.GuildId} Guild Name: {guild?.Name} Channel ID: {invite.ChannelId} Channel Name: {channel?.Name} Code: {invite.Code}");
            if (invite.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildInviteCreated, invite, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectInviteCreated, invite, channel);
            }
        }

        //https://discord.com/developers/docs/topics/gateway#invite-delete
        private void HandleDispatchInviteDelete(EventPayload payload)
        {
            InviteDeletedEvent invite = payload.EventData.ToObject<InviteDeletedEvent>();
            
            Guild guild = _client.GetGuild(invite.GuildId);
            Channel channel = _client.GetChannel(invite.ChannelId, invite.GuildId);

            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchInviteDelete)} Guild ID: {invite.GuildId} Guild Name: {guild?.Name} Channel ID: {invite.ChannelId} Channel Name: {channel?.Name} Code: {invite.Code}");
            if (invite.GuildId.HasValue)
            {
                _client.CallHook(DiscordHooks.OnDiscordGuildInviteDeleted, invite, channel, guild);
            }
            else
            {
                _client.CallHook(DiscordHooks.OnDiscordDirectInviteDeleted, invite, channel);
            }
        }
        
        //https://discord.com/developers/docs/topics/gateway#interaction-create
        private void HandleDispatchInteractionCreate(EventPayload payload)
        {
            Interaction interaction = payload.EventData.ToObject<Interaction>();
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchInteractionCreate)} Guild ID: {interaction.GuildId} Channel ID: {interaction.ChannelId} Interaction ID: {interaction.Id} Interaction Token: {interaction.Token}");
            _client.CallHook(DiscordHooks.OnDiscordInteractionCreated, interaction);
        }

        //TODO: Add Link
        private void HandleDispatchApplicationCommandCreate(EventPayload payload)
        {
            ApplicationCommandEvent commandEvent = payload.EventData.ToObject<ApplicationCommandEvent>();
            Guild guild = _client.GetGuild(commandEvent.GuildId); 
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchApplicationCommandCreate)} Guild ID: {commandEvent.GuildId} Guild Name: {guild?.Name} Command ID: {commandEvent.Id}");
            _client.CallHook(DiscordHooks.OnDiscordApplicationCommandCreated, commandEvent, guild);
        }
        
        //TODO: Add Link
        private void HandleDispatchApplicationCommandUpdate(EventPayload payload)
        {
            ApplicationCommandEvent commandEvent = payload.EventData.ToObject<ApplicationCommandEvent>();
            Guild guild = _client.GetGuild(commandEvent.GuildId); 
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchApplicationCommandUpdate)} Guild ID: {commandEvent.GuildId} Guild Name: {guild?.Name} Command ID: {commandEvent.Id}");
            _client.CallHook(DiscordHooks.OnDiscordApplicationCommandUpdated, commandEvent, guild);
        }
        
        //TODO: Add Link
        private void HandleDispatchApplicationCommandDelete(EventPayload payload)
        {
            ApplicationCommandEvent commandEvent = payload.EventData.ToObject<ApplicationCommandEvent>();
            Guild guild = _client.GetGuild(commandEvent.GuildId); 
            _logger.Verbose($"{nameof(SocketListener)}.{nameof(HandleDispatchApplicationCommandDelete)} Guild ID: {commandEvent.GuildId} Guild Name: {guild?.Name} Command ID: {commandEvent.Id}");
            _client.CallHook(DiscordHooks.OnDiscordApplicationCommandDeleted, commandEvent, guild);
        }
        
        //TODO: Implement once docs are available
        private void HandleGuildJoinRequestDelete(EventPayload payload)
        {
            
        }

        private void HandleDispatchUnhandledEvent(EventPayload payload)
        {
            _logger.Warning($"Unhandled Dispatch Event: {payload.EventName}. Please contact Discord Extension authors.\n{JsonConvert.SerializeObject(payload)}");
            _client.CallHook(DiscordHooks.OnDiscordUnhandledCommand, payload);
        }

        //https://discord.com/developers/docs/topics/gateway#heartbeat
        private void HandleHeartbeat(EventPayload payload)
        {
            _logger.Debug("Manually sent heartbeat (received opcode 1)");
            SendHeartbeat();
        }

        //https://discord.com/developers/docs/topics/gateway#reconnect
        private void HandleReconnect(EventPayload payload)
        {
            _logger.Info("Reconnect has been called (opcode 7)! Reconnecting...");
            //If we disconnect normally our session becomes invalid per: https://discord.com/developers/docs/topics/gateway#resuming
            _webSocket.Disconnect(true, true, true);
        }

        //https://discord.com/developers/docs/topics/gateway#invalid-session
        private void HandleInvalidSession(EventPayload payload)
        {
            bool shouldResume = !string.IsNullOrEmpty(SessionId) && (payload.TokenData?.ToObject<bool>() ?? false);
            _logger.Warning($"Invalid Session ID opcode received! Attempting to reconnect. Should Resume? {shouldResume}");
            _webSocket.Disconnect(true, shouldResume);
        }

        //https://discord.com/developers/docs/topics/gateway#hello
        private void HandleHello(EventPayload payload)
        {
            GatewayHelloEvent hello = payload.EventData.ToObject<GatewayHelloEvent>();
            _heartbeat.SetupHeartbeat(hello.HeartbeatInterval);

            // Client should now perform identification
            if (_webSocket.ShouldAttemptResume && !string.IsNullOrEmpty(SessionId))
            {
                _logger.Info($"{nameof(SocketListener)}.{nameof(HandleHello)} Attempting to resume session with ID: {SessionId}");
                Resume(SessionId, Sequence);
            }
            else
            {
                _logger.Debug($"{nameof(SocketListener)}.{nameof(HandleHello)} Identifying bot with discord.");
                Identify();
                _webSocket.ShouldAttemptResume = true;
            }
        }

        //https://discord.com/developers/docs/topics/gateway#heartbeating
        private void HandleHeartbeatAcknowledge(EventPayload payload)
        {
            _heartbeat.HeartbeatAcknowledged = true;
        }

        private void UnhandledOpCode(EventPayload payload)
        {
            _logger.Warning($"Unhandled OP code: {payload.OpCode}. Please contact Discord Extension authors.");
        }
        #endregion

        #region Discord Commands
        /// <summary>
        /// Sends a heartbeat to Discord
        /// </summary>
        internal void SendHeartbeat()
        {
            _webSocket.Send(GatewayCommandCode.Heartbeat, Sequence);
        }

        /// <summary>
        /// Used to Identify the bot with discord
        /// </summary>
        internal void Identify()
        {
            // Sent immediately after connecting. Opcode 2: Identify
            // Ref: https://discordapp.com/developers/docs/topics/gateway#identifying

            if (!_client.Initialized)
            {
                return;
            }

            IdentifyCommand identify = new IdentifyCommand
            {
                Token = _client.Settings.ApiToken,
                Properties = new Properties
                {
                    OS = "Oxide.Ext.Discord",
                    Browser = "Oxide.Ext.Discord",
                    Device = "Oxide.Ext.Discord"
                },
                Intents = _client.Settings.Intents,
                Compress = false,
                LargeThreshold = 50,
                Shard = new List<int> {0, 1}
            };

            _webSocket.Send(GatewayCommandCode.Identify, identify);
        }

        /// <summary>
        /// Used to resume the current session with discord
        /// </summary>
        internal void Resume(string sessionId, int sequence)
        {
            if (!_client.Initialized)
            {
                return;
            }

            ResumeSessionCommand resume = new ResumeSessionCommand
            {
                Sequence = sequence,
                SessionId = sessionId,
                Token = _client.Settings.ApiToken
            };

            _webSocket.Send(GatewayCommandCode.Resume, resume);
        }
        #endregion
    }
}
