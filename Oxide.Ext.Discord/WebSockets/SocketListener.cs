using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.DiscordEvents;
using Oxide.Ext.Discord.DiscordObjects;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Gateway;
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

        public SocketListener(DiscordClient client, Socket socket)
        {
            _client = client;
            _webSocket = socket;
            _logger = new Logger<SocketListener>(client.Settings.LogLevel);
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
            
            _webSocket.DisposeSocket();
            
            if (_webSocket.RequestReconnect)
            {
                _webSocket.RequestReconnect = false;
                _client.ConnectWebSocket();
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
                _webSocket.StartReconnectTimer(Retries < 3 ? 1f : 15f, () =>
                {
                    _logger.LogWarning($"Attempting to reconnect to Discord... [Retry={Retries + 1}]");
                    if (Retries < 8)
                    {
                        _client.ConnectWebSocket();
                    }
                    else
                    {
                        //If more than 8 tries something could be wrong on discords end. Try and fetch the websocket url
                        _client.UpdateGatewayUrl(_client.ConnectWebSocket);
                    }
                    Retries++;
                });
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
                _client.ConnectWebSocket();
            }
            
            return true;
        }

        public void SocketErrored(object sender, ErrorEventArgs e)
        {
            _logger.LogError($"An error has occured in the websocket: {e.Message}\n{e.Exception}");
            _client.CallHook("DiscordSocket_WebSocketErrored", null, e.Exception, e.Message);

            _logger.LogWarning("Attempting to reconnect to Discord...");
            _webSocket.Disconnect(true, false);
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
                    _logger.LogDebug($"Received OpCode: Dispatch, event: {payload.EventName}");

                    // Listed here: https://discordapp.com/developers/docs/topics/gateway#commands-and-events-gateway-events
                    switch (payload.EventName)
                    {
                        case "READY":
                        {
                            Ready ready = payload.EventData.ToObject<Ready>();
                            _client.DiscordServers = ready.Guilds;
                            _client.SessionID = ready.SessionID;
                            _logger.LogWarning($"Your bot was found in {ready.Guilds.Count} Guilds!");
                            _client.CallHook("Discord_Ready", null, ready);
                            break;
                        }

                        case "RESUMED":
                        {
                            Resumed resumed = payload.EventData.ToObject<Resumed>();
                            _logger.LogWarning("Session resumed successfully!");
                            _client.CallHook("Discord_Resumed", null, resumed);
                            break;
                        }

                        case "CHANNEL_CREATE":
                        {
                            Channel channel = payload.EventData.ToObject<Channel>();
                            if (channel.type == ChannelType.DM || channel.type == ChannelType.GROUP_DM)
                            {
                                _client.DMs.Add(channel);
                            }
                            else
                            {
                                _client.GetGuild(channel.guild_id)?.channels.Add(channel);
                            }
                            _client.CallHook("Discord_ChannelCreate", null, channel);
                            break;
                        }

                        case "CHANNEL_UPDATE":
                        {
                            Channel update = payload.EventData.ToObject<Channel>();
                            Channel previous = null;
                            if (update.type == ChannelType.DM || update.type == ChannelType.GROUP_DM)
                            {
                                previous = _client.DMs.FirstOrDefault(c => c.id == update.id);
                                _client.DMs.Remove(previous);
                                _client.DMs.Add(update);
                            }
                            else
                            {
                                Guild guild = _client.GetGuild(update.guild_id);
                                if (guild != null)
                                {
                                    previous = guild.channels.FirstOrDefault(c => c.id == update.id);
                                    guild.channels.Remove(previous);
                                    guild.channels.Add(update);
                                }
                            }

                            _client.CallHook("Discord_ChannelUpdate", null, update, previous);
                            break;
                        }

                        case "CHANNEL_DELETE":
                        {
                            Channel channel = payload.EventData.ToObject<Channel>();
                            _client.GetGuild(channel.guild_id)?.channels.RemoveAll(c => c.id == channel.id);
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
                            bool unavailable = guild.unavailable ?? false;
                            Guild existing = _client.GetGuild(guild.id);
                            if(existing == null)
                            {
                                _client.DiscordServers.Add(guild);
                                _logger.LogDebug($"{nameof(SocketListener)}.{nameof(SocketMessage)} GUILD_CREATE: Guild not found adding to list.");
                            }
                            else if(!unavailable && (existing.unavailable ?? false))
                            {
                                _client.UpdateGuild(guild.id, guild);
                                _logger.LogDebug($"{nameof(SocketListener)}.{nameof(SocketMessage)} GUILD_CREATE: Guild found but was unavailable. Updating guild in list.");
                            }
                            
                            _client.CallHook("Discord_GuildCreate", null, guild);
                            break;
                        }

                        case "GUILD_UPDATE":
                        {
                            Guild guild = payload.EventData.ToObject<Guild>();
                            _client.GetGuild(guild.id)?.Update(guild);
                            _logger.LogDebug($"{nameof(SocketListener)}.{nameof(SocketMessage)} GUILD_UPDATE: Guild was Updated.");
                            _client.CallHook("Discord_GuildUpdate", null, guild);
                            break;
                        }

                        case "GUILD_DELETE":
                        {
                            Guild guild = payload.EventData.ToObject<Guild>();
                            if(guild.unavailable ?? false) // There is an outage with Discord
                            {
                                _logger.LogDebug($"{nameof(SocketListener)}.{nameof(SocketMessage)} GUILD_DELETE: There is an outage with the guild.");
                                _client.UpdateGuild(guild.id, guild);
                            }
                            else
                            {
                                _logger.LogDebug($"{nameof(SocketListener)}.{nameof(SocketMessage)} GUILD_DELETE: User was removed from the guild.");
                                _client.RemoveGuild(guild.id);
                            }
                            _client.CallHook("Discord_GuildDelete", null, guild);
                            break;
                        }

                        case "GUILD_BAN_ADD":
                        {
                            //TODO: Pass Guild ID of the banned user
                            User user = payload.EventData.ToObject<BanObject>().user;
                            _client.CallHook("Discord_GuildBanAdd", null, user);
                            break;
                        }

                        case "GUILD_BAN_REMOVE":
                        {
                            //TODO: Pass Guild ID of the banned user
                            User user = payload.EventData.ToObject<BanObject>().user;
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
                            _client.CallHook("Discord_GuildIntergrationsUpdate", null, integration); //TODO: Remove Deprecated Hook Call
                            _client.CallHook("Discord_GuildIntegrationsUpdate", null, integration);
                            break;
                        }

                        case "GUILD_MEMBER_ADD":
                        {
                            GuildMemberAdd member = payload.EventData.ToObject<GuildMemberAdd>();
                            _client.GetGuild(member.guild_id)?.members.Add(member);
                            _client.CallHook("Discord_MemberAdded", null, member);
                            break;
                        }

                        case "GUILD_MEMBER_REMOVE":
                        {
                            GuildMemberRemove remove = payload.EventData.ToObject<GuildMemberRemove>();
                            Guild guild = _client.GetGuild(remove.guild_id);
                            if (guild != null)
                            {
                                GuildMember member = guild.members.FirstOrDefault(x => x.user.id == remove.user.id);
                                if (member != null)
                                {
                                    guild.members.Remove(member);
                                }
                                
                                _client.CallHook("Discord_MemberRemoved", null, member);
                            }
                            
                            break;
                        }

                        case "GUILD_MEMBER_UPDATE":
                        {
                            GuildMemberUpdate update = payload.EventData.ToObject<GuildMemberUpdate>();
                            Guild guild = _client.GetGuild(update.guild_id);
                            GuildMember member = guild?.members.FirstOrDefault(x => x.user.id == update.user.id);
                            if (member != null)
                            {
                                GuildMember oldMember = JObject.FromObject(member).ToObject<GuildMember>(); // lazy way to copy the object
                                if (update.user != null)
                                    member.user = update.user;
                                if (update.nick != null)
                                    member.nick = update.nick;
                                if (update.roles != null)
                                    member.roles = update.roles;
                                _client.CallHook("Discord_GuildMemberUpdate", null, update, oldMember);
                            }
                            
                            break;
                        }

                        case "GUILD_MEMBERS_CHUNK":
                        {
                            GuildMembersChunk chunk = payload.EventData.ToObject<GuildMembersChunk>();
                            _client.CallHook("Discord_GuildMembersChunk", null, chunk);
                            break;
                        }

                        case "GUILD_ROLE_CREATE":
                        {
                            GuildRoleCreate role = payload.EventData.ToObject<GuildRoleCreate>();
                            _client.GetGuild(role.guild_id)?.roles.Add(role.role);
                            _client.CallHook("Discord_GuildRoleCreate", null, role.role);
                            break;
                        }

                        case "GUILD_ROLE_UPDATE":
                        {
                            GuildRoleUpdate update = payload.EventData.ToObject<GuildRoleUpdate>();
                            Role updatedRole = update.role;
                            Guild guild = _client.GetGuild(update.guild_id);
                            if (guild != null)
                            {
                                Role oldRole = guild.roles.FirstOrDefault(x => x.id == updatedRole.id);
                                if (oldRole != null)
                                {
                                    guild.roles.Remove(oldRole);
                                }

                                guild.roles.Add(updatedRole);

                                _client.CallHook("Discord_GuildRoleUpdate", null, updatedRole, oldRole);
                            }
                            
                            break;
                        }

                        case "GUILD_ROLE_DELETE":
                        {
                            GuildRoleDelete delete = payload.EventData.ToObject<GuildRoleDelete>();
                            Guild guild = _client.GetGuild(delete.guild_id);
                            Role role = guild?.roles.FirstOrDefault(x => x.id == delete.role_id);
                            if (role != null)
                            {
                                guild.roles.Remove(role);
                                _client.CallHook("Discord_GuildRoleDelete", null, role);
                            }

                            break;
                        }

                        case "MESSAGE_CREATE":
                        {
                            Message message = payload.EventData.ToObject<Message>();
                            Channel channel = message.guild_id != null ? 
                                _client.GetGuild(message.guild_id)?.channels.FirstOrDefault(x => x.id == message.channel_id) : 
                                _client.DMs.FirstOrDefault(x => x.id == message.channel_id);
                            
                            if (channel != null)
                            {
                                channel.last_message_id = message.id;
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
                            MessageReactionUpdate reaction = payload.EventData.ToObject<MessageReactionUpdate>();
                            _client.CallHook("Discord_MessageReactionAdd", null, reaction);
                            break;
                        }

                        case "MESSAGE_REACTION_REMOVE":
                        {
                            MessageReactionUpdate reaction = payload.EventData.ToObject<MessageReactionUpdate>();
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
                            PresenceUpdate update = payload.EventData.ToObject<PresenceUpdate>();

                            User updateUser = update?.user;
                            if (updateUser != null)
                            {
                                GuildMember member = _client.GetGuild(update.guild_id)?.members.FirstOrDefault(x => x.user.id == updateUser.id);
                                member?.user.Update(updateUser);
                            }

                            _client.CallHook("Discord_PresenceUpdate", null, updateUser);
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
                            User user = payload.EventData.ToObject<User>();
                            
                            foreach(Guild g in _client.DiscordServers)
                            {
                                GuildMember memberUpdate = g.members.FirstOrDefault(x => x.user.id == user.id);
                                if (memberUpdate != null)
                                {
                                    memberUpdate.user = user;
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
                    //If we disconnect normally our session becomes invalid per: https://discord.com/developers/docs/topics/gateway#resuming
                    _webSocket.Disconnect(true, true, true);
                    break;
                }

                // Invalid Session (used to notify client they have an invalid session ID)
                case ReceiveOpCode.InvalidSession:
                {
                    _logger.LogWarning("Invalid Session ID opcode received!");
                    _webSocket.Disconnect(true, payload.TokenData?.ToObject<bool>() ?? false);
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
                    _client.HeartbeatAcknowledged = true;
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
