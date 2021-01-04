using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Roles;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Entities.Voice;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using WebSocketSharp;

namespace Oxide.Ext.Discord.WebSockets
{
    public class SocketListener
    {
        private readonly DiscordClient _client;

        private readonly Socket _webSocket;

        internal int Retries;

        private readonly ILogger _logger;

        private Timer _reconnectTimer;

        public SocketListener(DiscordClient client, Socket socket)
        {
            _client = client;
            _webSocket = socket;
            _logger = new Logger<SocketListener>(client.Settings.LogLevel);
        }

        public void DisconnectWebsocket(bool shouldReconnect, bool shouldResume, bool discordRequestedReconnect = false)
        {
            _client.requestReconnect = shouldReconnect;
            _webSocket.ShouldAttemptResume = shouldResume;

            if (discordRequestedReconnect)
            {
                _webSocket.ReconnectRequested();
            }
            else
            {
                _webSocket.Disconnect();
            }
        }

        public void SocketOpened(object sender, EventArgs e)
        {
            _logger.LogWarning("Discord socket opened!");
            _client.CallHook("DiscordSocket_WebSocketOpened");
            Retries = 0;
        }

        public void SocketClosed(object sender, CloseEventArgs e)
        {
            _logger.LogDebug($"Discord WebSocket closed. Code: {e.Code}, reason: {e.Reason}");
            
            if (_client.requestReconnect)
            {
                _client.requestReconnect = false;
                _client.ConnectToWebSocket();
                return;
            }
            
            _client.CallHook("DiscordSocket_WebSocketClosed", null, e.Reason, e.Code, e.WasClean);
            
            if (HandleDiscordClosedSocket(e.Code, e.Reason))
            {
                return;
            }

            if (!e.WasClean)
            {
                _logger.LogWarning($"Discord connection closed uncleanly: code {e.Code}, Reason: {e.Reason}");
            }

            if (!_client.Disconnected)
            {
                if (Retries < 3)
                {
                    _logger.LogWarning("Attempting to reconnect to Discord...");
                    _client.ConnectToWebSocket();
                }
                else
                {
                    if (_reconnectTimer != null && _reconnectTimer.Enabled)
                    {
                        return;
                    }

                    _reconnectTimer = new Timer
                    {
                        Interval = 15000f,
                        AutoReset = false
                    };
                    _reconnectTimer.Elapsed += (_, __) =>
                    {
                        _logger.LogWarning("Attempting to reconnect to Discord...");
                        _client.ConnectToWebSocket();
                    };
                }
                
                Retries++;
            }
        }

        private bool HandleDiscordClosedSocket(int code, string reason)
        {
            if (!code.ToString().TryParse(out SocketCloseCode closeCode))
            {
                return false;
            }

            bool reconnect = false;

            switch (closeCode)
            {
                case SocketCloseCode.UnknownError: 
                    _logger.LogError("Discord had an unknown error. Reconnecting.");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.UnknownOpcode: 
                    _logger.LogError($"Unknown gateway opcode sent: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.DecodeError: 
                    _logger.LogError($"Invalid gateway payload sent: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.NotAuthenticated: 
                    _logger.LogError($"Tried to send a payload before identifying: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.AuthenticationFailed: 
                    _logger.LogError($"The given bot token is invalid. Please enter a valid token: {reason}");
                    break;
                
                case SocketCloseCode.AlreadyAuthenticated: 
                    _logger.LogError($"The bot has already authenticated. Please don't identify more than once.: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.InvalidSequence: 
                    _logger.LogError($"Invalid resume sequence. Doing full reconnect.: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.RateLimited: 
                    _logger.LogError($"You're being rate limited. Please slow down how quickly you're sending requests: {reason}");
                    break;
                
                case SocketCloseCode.SessionTimedOut: 
                    _logger.LogError($"Session has timed out. Starting a new one: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.InvalidShard: 
                    _logger.LogError($"Invalid shared has been specified: {reason}");
                    reconnect = true;
                    break;
                
                case SocketCloseCode.ShardingRequired: 
                    _logger.LogError($"Bot is in too many guilds. You must shard your bot: {reason}");
                    break;
                
                case SocketCloseCode.InvalidApiVersion: 
                    _logger.LogError("Gateway is using invalid API version. Please contact Discord Extension Devs immediately!");
                    break;
                
                case SocketCloseCode.InvalidIntents: 
                    _logger.LogError("Invalid intent(s) specified for the gateway. Please check that you're using valid intents in the connect.");
                    break;
                
                case SocketCloseCode.DisallowedIntent:
                    _logger.LogError("The plugin is asking for an intent you have not granted your bot. Please go to your bot and enable the privileged gateway intents: https://support.discord.com/hc/en-us/articles/360040720412-Bot-Verification-and-Data-Whitelisting#privileged-intent-whitelisting");
                    break;
                
                default:
                    return false;
            }

            if (reconnect)
            {
                _webSocket.ShouldAttemptResume = false;
                _client.ConnectToWebSocket();
            }
            
            return true;
        }

        public void SocketErrored(object sender, ErrorEventArgs e)
        {
            _logger.LogError($"An error has occured in the websocket: {e.Message}\n{e.Exception}");
            _client.CallHook("DiscordSocket_WebSocketErrored", null, e.Exception, e.Message);

            _logger.LogWarning("Attempting to reconnect to Discord...");
            DisconnectWebsocket(true, false);
        }

        public void SocketMessage(object sender, MessageEventArgs e)
        {
            RPayload payload = JsonConvert.DeserializeObject<RPayload>(e.Data);

            if (payload.Sequence.HasValue)
            {
                _client.Sequence = payload.Sequence.Value;
            }

            _logger.LogDebug($"Received socket message, OpCode: {payload.OpCode}");

            switch (payload.OpCode)
            {
                // Dispatch (dispatches an event)
                case ReceiveOpCode.Dispatch:
                {
                    _logger.LogDebug($"Received OpCode 0, event: {payload.EventName}");

                    // Listed here: https://discordapp.com/developers/docs/topics/gateway#commands-and-events-gateway-events
                    switch (payload.EventName)
                    {
                        case "READY":
                        {
                            Ready ready = payload.EventData.ToObject<Ready>();
                            if (ready.Guilds.Count != 0)
                            {
                                _logger.LogWarning($"Your bot was found in {ready.Guilds.Count} Guilds!");
                            }

                            if (ready.Guilds.Count == 0)
                            {
                                _logger.LogDebug("Ready event but no Guilds sent.");
                            }

                            _client.DiscordServers = ready.Guilds.ToHash(key => key.Id);
                            _client.SessionID = ready.SessionId;
                            _client.Application = ready.Application;
                            
                            _client.CallHook("Discord_Ready", null, ready);
                            break;
                        }

                        case "RESUMED":
                        {
                            Resumed resumed = payload.EventData.ToObject<Resumed>();
                            _logger.LogWarning("Session resumed!");
                            _client.CallHook("Discord_Resumed", null, resumed);
                            break;
                        }

                        case "CHANNEL_CREATE":
                        {
                            Channel channel = payload.EventData.ToObject<Channel>();
                            _client.GetGuild(channel.GuildId).Channels.Add(channel);
                            _client.CallHook("Discord_ChannelCreate", null, channel);
                            break;
                        }

                        case "CHANNEL_UPDATE":
                        {
                            Channel update = payload.EventData.ToObject<Channel>();
                            Channel previous = _client.GetGuild(update.GuildId).Channels.FirstOrDefault(x => x.Id == update.Id);

                            if (previous != null)
                            {
                                _client.GetGuild(update.GuildId).Channels.Remove(previous);
                            }

                            _client.GetGuild(update.GuildId).Channels.Add(update);

                            _client.CallHook("Discord_ChannelUpdate", null, update, previous);
                            break;
                        }

                        case "CHANNEL_DELETE":
                        {
                            Channel channel = payload.EventData.ToObject<Channel>();
                            
                            _client.GetGuild(channel.GuildId).Channels.RemoveAll(c => c.Id == channel.Id);

                            _client.CallHook("Discord_ChannelDelete", null, channel);
                            break;
                        }

                        case "CHANNEL_PINS_UPDATE":
                        {
                            ChannelPinsUpdate pins = payload.EventData.ToObject<ChannelPinsUpdate>();
                            _client.CallHook("Discord_ChannelPinsUpdate", null, pins);
                            break;
                        }

                        // NOTE: Some elements of Guild object is only sent with GUILD_CREATE
                        case "GUILD_CREATE":
                        {
                            Guild guild = payload.EventData.ToObject<Guild>();
                            string id = guild.Id;
                            bool unavailable = guild.Unavailable ?? false;
                            if(_client.GetGuild(id) == null)
                            {
                                _client.AddGuild(guild);
                                _logger.LogDebug($"Guild ID ({id}) added to list.");
                            }
                            else if(unavailable == false && (_client.GetGuild(id)?.Unavailable ?? false))
                            {
                                _client.AddGuild(guild);
                                _logger.LogDebug($"Guild ID ({id}) updated to list.");
                            }
                            _client.CallHook("Discord_GuildCreate", null, guild);
                            break;
                        }

                        case "GUILD_UPDATE":
                        {
                            Guild guildUpdate = payload.EventData.ToObject<Guild>();
                            _client.GetGuild(guildUpdate.Id).Update(guildUpdate);
                            _client.CallHook("Discord_GuildUpdate", null, guildUpdate);
                            break;
                        }

                        case "GUILD_DELETE":
                        {
                            Guild guildDelete = payload.EventData.ToObject<Guild>();
                            if(guildDelete.Unavailable ?? false) // outage
                            {
                                _logger.LogDebug($"Guild ID {guildDelete.Id} outage!");
                                _client.AddGuild(guildDelete);
                            }
                            else
                            {
                                _logger.LogDebug($"Guild ID {guildDelete.Id} removed from list");
                                _client.DiscordServers.Remove(guildDelete.Id); // guildDelete may not be same reference
                            }
                            _client.CallHook("Discord_GuildDelete", null, guildDelete);
                            break;
                        }

                        case "GUILD_BAN_ADD":
                        {
                            DiscordUser user = payload.EventData.ToObject<GuildBanEvent>().User;
                            _client.CallHook("Discord_GuildBanAdd", null, user);
                            break;
                        }

                        case "GUILD_BAN_REMOVE":
                        {
                            DiscordUser user = payload.EventData.ToObject<GuildBanEvent>().User;
                            _client.CallHook("Discord_GuildBanRemove", null, user);
                            break;
                        }

                        case "GUILD_EMOJIS_UPDATE":
                        {
                            GuildEmojisUpdate emojis = payload.EventData.ToObject<GuildEmojisUpdate>();
                            _client.CallHook("Discord_GuildEmojisUpdate", null, emojis);
                            break;
                        }

                        case "GUILD_INTEGRATIONS_UPDATE":
                        {
                            GuildIntergrationsUpdate integration = payload.EventData.ToObject<GuildIntergrationsUpdate>();
                            _client.CallHook("Discord_GuildIntergrationsUpdate", null, integration);
                            _client.CallHook("Discord_GuildIntegrationsUpdate", null, integration);
                            break;
                        }

                        case "GUILD_MEMBER_ADD":
                        {
                            GuildMemberAdd memberAdded = payload.EventData.ToObject<GuildMemberAdd>();

                            _client.GetGuild(memberAdded.GuildId)?.Members.Add(memberAdded);

                            _client.CallHook("Discord_MemberAdded", null, memberAdded);
                            break;
                        }

                        case "GUILD_MEMBER_REMOVE":
                        {
                            GuildMemberRemove memberRemoved = payload.EventData.ToObject<GuildMemberRemove>();

                            GuildMember member = _client.GetGuild(memberRemoved.GuildId)?.Members.FirstOrDefault(x => x.User.Id == memberRemoved.User.Id);
                            if (member != null)
                            {
                                _client.GetGuild(memberRemoved.GuildId)?.Members.Remove(member);
                            }

                            _client.CallHook("Discord_MemberRemoved", null, member);
                            break;
                        }

                        case "GUILD_MEMBER_UPDATE":
                        {
                            GuildMemberUpdate memberUpdated = payload.EventData.ToObject<GuildMemberUpdate>();

                            GuildMember newMember = _client.GetGuild(memberUpdated.GuildId)?.Members.FirstOrDefault(x => x.User.Id == memberUpdated.User.Id);
                            if (newMember != null)
                            {
                                GuildMember oldMember = JObject.FromObject(newMember).ToObject<GuildMember>(); // lazy way to copy the object
                                if (memberUpdated.User != null)
                                    newMember.User = memberUpdated.User;
                                if (memberUpdated.Nick != null)
                                    newMember.Nick = memberUpdated.Nick;
                                if (memberUpdated.Roles != null)
                                    newMember.Roles = memberUpdated.Roles;
                                _client.CallHook("Discord_GuildMemberUpdate", null, memberUpdated, oldMember);
                            }

                            break;
                        }

                        case "GUILD_MEMBERS_CHUNK":
                        {
                            GuildMembersChunk guildMembersChunk = payload.EventData.ToObject<GuildMembersChunk>();
                            _client.CallHook("Discord_GuildMembersChunk", null, guildMembersChunk);
                            break;
                        }

                        case "GUILD_ROLE_CREATE":
                        {
                            GuildRoleCreate role = payload.EventData.ToObject<GuildRoleCreate>();

                            _client.GetGuild(role.GuildId)?.Roles.Add(role.Role);

                            _client.CallHook("Discord_GuildRoleCreate", null, role.Role);
                            break;
                        }

                        case "GUILD_ROLE_UPDATE":
                        {
                            GuildRoleUpdate update = payload.EventData.ToObject<GuildRoleUpdate>();
                            Role updatedRole = update.Role;

                            Role oldRole = _client.GetGuild(update.GuildId).Roles.FirstOrDefault(x => x.Id == updatedRole.Id);
                            if (oldRole != null)
                            {
                                _client.GetGuild(update.GuildId).Roles.Remove(oldRole);
                            }

                            _client.GetGuild(update.GuildId).Roles.Add(updatedRole);

                            _client.CallHook("Discord_GuildRoleUpdate", null, updatedRole, oldRole);
                            break;
                        }

                        case "GUILD_ROLE_DELETE":
                        {
                            GuildRoleDelete guildRoleDelete = payload.EventData.ToObject<GuildRoleDelete>();

                            Role deletedRole = _client.GetGuild(guildRoleDelete.GuildId)?.Roles.FirstOrDefault(x => x.Id == guildRoleDelete.RoleId);
                            if (deletedRole != null)
                            {
                                _client.GetGuild(guildRoleDelete.GuildId).Roles.Remove(deletedRole);
                            }

                            _client.CallHook("Discord_GuildRoleDelete", null, deletedRole);
                            break;
                        }

                        case "MESSAGE_CREATE":
                        {
                            Message message = payload.EventData.ToObject<Message>();
                            if (message.GuildId != null)
                            {
                                Channel channel = _client.GetGuild(message.GuildId)?.Channels.FirstOrDefault(x => x.Id == message.ChannelId);
                                if (channel != null)
                                {
                                    channel.LastMessageId = message.Id;
                                }
                            }
                            else
                            {
                                Channel channel = _client.DMs[message.ChannelId];
                                if (channel == null)
                                {
                                    Channel.GetChannel(_client, message.ChannelId, dmChannel =>
                                    {
                                        _client.DMs[dmChannel.Id] = dmChannel;
                                        dmChannel.LastMessageId = message.Id;
                                    });
                                }
                                else
                                {
                                    channel.LastMessageId = message.Id;
                                }
                            }

                            _client.CallHook("Discord_MessageCreate", null, message);
                            break;
                        }

                        case "MESSAGE_UPDATE":
                        {
                            Message message = payload.EventData.ToObject<Message>();
                            _client.CallHook("Discord_MessageUpdate", null, message);
                            break;
                        }

                        case "MESSAGE_DELETE":
                        {
                            MessageDelete message = payload.EventData.ToObject<MessageDelete>();
                            _client.CallHook("Discord_MessageDelete", null, message);
                            break;
                        }

                        case "MESSAGE_DELETE_BULK":
                        {
                            MessageDeleteBulk bulkDelete = payload.EventData.ToObject<MessageDeleteBulk>();
                            _client.CallHook("Discord_MessageDeleteBulk", null, bulkDelete);
                            break;
                        }

                        case "MESSAGE_REACTION_ADD":
                        {
                            MessageReactionAdd reaction = payload.EventData.ToObject<MessageReactionAdd>();
                            _client.CallHook("Discord_MessageReactionAdd", null, reaction);
                            break;
                        }

                        case "MESSAGE_REACTION_REMOVE":
                        {
                            MessageReactionRemove reaction = payload.EventData.ToObject<MessageReactionRemove>();
                            _client.CallHook("Discord_MessageReactionRemove", null, reaction);
                            break;
                        }

                        case "MESSAGE_REACTION_REMOVE_ALL":
                        {
                            MessageReactionRemoveAll reaction = payload.EventData.ToObject<MessageReactionRemoveAll>();
                            _client.CallHook("Discord_MessageReactionRemoveAll", null, reaction);
                            break;
                        }

                        /*
                         * From Discord API docs:
                         * The user object within this event can be partial, the only field which must be sent is the id field, everything else is optional.
                         * Along with this limitation, no fields are required, and the types of the fields are not validated.
                         * Your client should expect any combination of fields and types within this event.
                        */

                        case "PRESENCE_UPDATE":
                        {
                            PresenceUpdate presenceUpdate = payload.EventData.ToObject<PresenceUpdate>();

                            DiscordUser updatedPresence = presenceUpdate?.User;

                            if (updatedPresence != null)
                            {
                                GuildMember updatedMember = _client.GetGuild(presenceUpdate.GuildId)?.Members.FirstOrDefault(x => x.User.Id == updatedPresence.Id);
                                updatedMember?.User.Update(updatedPresence);
                            }

                            _client.CallHook("Discord_PresenceUpdate", null, updatedPresence);
                            break;
                        }

                        // Bots should ignore this
                        case "PRESENCES_REPLACE":
                            break;

                        case "TYPING_START":
                        {
                            TypingStart typing = payload.EventData.ToObject<TypingStart>();
                            _client.CallHook("Discord_TypingStart", null, typing);
                            break;
                        }

                        case "USER_UPDATE":
                        {
                            DiscordUser user = payload.EventData.ToObject<DiscordUser>();
                            
                            List<Guild> guilds = _client.DiscordServers.Values.Where(x => x.Members.FirstOrDefault(y => y.User.Id == user.Id) != null).ToList();
                            foreach(Guild g in guilds)
                            {
                                GuildMember memberUpdate = g.Members.FirstOrDefault(x => x.User.Id == user.Id);
                                if (memberUpdate != null)
                                {
                                    memberUpdate.User = user;
                                }
                            }

                            _client.CallHook("Discord_UserUpdate", null, user);
                            break;
                        }

                        case "VOICE_STATE_UPDATE":
                        {
                            VoiceState voice = payload.EventData.ToObject<VoiceState>();
                            _client.CallHook("Discord_VoiceStateUpdate", null, voice);
                            break;
                        }

                        case "VOICE_SERVER_UPDATE":
                        {
                            VoiceServerUpdate voice = payload.EventData.ToObject<VoiceServerUpdate>();
                            _client.CallHook("Discord_VoiceServerUpdate", null, voice);
                            break;
                        }

                        case "WEBHOOKS_UPDATE":
                        {
                            WebhooksUpdate webhook = payload.EventData.ToObject<WebhooksUpdate>();
                            _client.CallHook("Discord_WebhooksUpdate", null, webhook);
                            break;
                        }

                        case "INVITE_CREATE":
                        {
                            InviteCreated invite = payload.EventData.ToObject<InviteCreated>();
                            _client.CallHook("Discord_InviteCreated", null, invite);
                            break;
                        }

                        case "INVITE_DELETE":
                        {
                            InviteDeleted invite = payload.EventData.ToObject<InviteDeleted>();
                            _client.CallHook("Discord_InviteDeleted", null, invite);
                            break;
                        }
                        
                        case "INTEGRATION_CREATE":
                        case "INTEGRATION_UPDATE":
                        case "INTEGRATION_DELETE":
                        case "INTERACTION_CREATE":
                            break;

                        default:
                        {
                            _client.CallHook("Discord_UnhandledEvent", null, payload);
                            _logger.LogWarning($"Unhandled event: {payload.EventName}");
                            break;
                        }
                    }

                    break;
                }

                // Heartbeat
                // https://discordapp.com/developers/docs/topics/gateway#gateway-heartbeat
                case ReceiveOpCode.Heartbeat:
                {
                    _logger.LogInfo("Manually sent heartbeat (received opcode 1)");
                    _client.SendHeartbeat();
                    break;
                }

                // Reconnect (used to tell clients to reconnect to the gateway)
                // we should immediately reconnect here
                case ReceiveOpCode.Reconnect:
                {
                    _logger.LogInfo("Reconnect has been called (opcode 7)! Reconnecting...");
                    _webSocket.ShouldAttemptResume = true; // attempt resume opcode
                    _client.requestReconnect = true;
                    _webSocket.ReconnectRequested(); //If we disconnect normally our session becomes invalid per: https://discord.com/developers/docs/topics/gateway#resuming
                    break;
                }

                // Invalid Session (used to notify client they have an invalid session ID)
                case ReceiveOpCode.InvalidSession:
                {
                    _logger.LogWarning("Invalid Session ID opcode received!");
                    DisconnectWebsocket(true, payload.EventData.ToObject<bool>());
                    break;
                }

                // Hello (sent immediately after connecting, contains heartbeat and server debug information)
                case ReceiveOpCode.Hello:
                {
                    Hello hello = payload.EventData.ToObject<Hello>();
                    _client.CreateHeartbeat(hello.HeartbeatInterval);
                    
                    // Client should now perform identification
                    if (_webSocket.ShouldAttemptResume)
                    {
                        _logger.LogWarning("Attempting resume opcode...");
                        _client.Resume();
                    }
                    else
                    {
                        _client.Identify();
                        _webSocket.ShouldAttemptResume = true;
                    }
                    break;
                }

                // Heartbeat ACK (sent immediately following a client heartbeat
                // that was received)
                // (See 'zombied or failed connections')
                case ReceiveOpCode.HeartbeatAcknowledge:
                {
                    _client.HeartbeatACK = true;
                    break;
                }

                default:
                {
                    _logger.LogInfo($"Unhandled OP code: code {payload.OpCode}");
                    break;
                }
            }
        }
    }
}
