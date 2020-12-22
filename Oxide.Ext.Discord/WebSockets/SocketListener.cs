using System;
using System.Linq;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxide.Ext.Discord.DiscordEvents;
using Oxide.Ext.Discord.DiscordObjects;
using Oxide.Ext.Discord.Exceptions;
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
        }

        public void SocketClosed(object sender, CloseEventArgs e)
        {
            if (e.Code == 4004)
            {
                _logger.LogError("Given Bot token is invalid!");
                throw new APIKeyException();
            }

            _logger.LogDebug($"Discord WebSocket closed. Code: {e.Code}, reason: {e.Reason}");

            if (_client.requestReconnect)
            {
                _client.requestReconnect = false;
                _client.ConnectToWebSocket();
                return;
            }

            if (e.Code == 4006)
            {
                _webSocket.ShouldAttemptResume = false;
                _logger.LogWarning("Discord session no longer valid... Reconnecting...");
                _client.REST.Shutdown(); // Clean up buckets
                _client.ConnectToWebSocket();
                _client.CallHook("DiscordSocket_WebSocketClosed", null, e.Reason, e.Code, e.WasClean);
                return;
            }

            if (!e.WasClean)
            {
                _logger.LogWarning($"Discord connection closed uncleanly: code {e.Code}, Reason: {e.Reason}");

                if(Retries >= 5)
                {
                    _logger.LogError("Exceeded number of retries... Attempting in 15 seconds.");
                    Timer reconnecttimer = new Timer() { Interval = 15000f, AutoReset = false };
                    reconnecttimer.Elapsed += (object a, ElapsedEventArgs b) =>
                    {
                        if (_client == null) return;
                        Retries = 0;
                        _logger.LogWarning("Attempting to reconnect to Discord...");
                        _client.REST.Shutdown(); // Clean up buckets
                        _client.ConnectToWebSocket();
                    };
                    reconnecttimer.Start();
                    return;
                }
                Retries++;

                _logger.LogWarning("Attempting to reconnect to Discord...");
                _client.REST.Shutdown(); // Clean up buckets
                _client.ConnectToWebSocket();
            }
            else
            {
                Discord.CloseClient(_client);
            }
            
            _client.CallHook("DiscordSocket_WebSocketClosed", null, e.Reason, e.Code, e.WasClean);
        }

        public void SocketErrored(object sender, ErrorEventArgs e)
        {
            if (e.Exception is APIKeyException)
                return;
            if(e.Exception is NoURLException)
            {
                _logger.LogError("Error: WebSocketUrl not present! Retrying..");
                _client.ConnectToWebsocketUrl();
                return;
            }
            
            _logger.LogWarning($"An error has occured: Response: {e.Message}\n{e.Exception}");

            _client.CallHook("DiscordSocket_WebSocketErrored", null, e.Exception, e.Message);

            if (_client == null) return;
            if (Retries > 0) return; // Retry timer is already triggered
            _logger.LogWarning("Attempting to reconnect to Discord...");
            _client.REST.Shutdown(); // Clean up buckets
            _client.ConnectToWebSocket();
        }

        public void SocketMessage(object sender, MessageEventArgs e)
        {
            RPayload payload = JsonConvert.DeserializeObject<RPayload>(e.Data);

            if (payload.Sequence.HasValue)
            {
                _client.Sequence = payload.Sequence.Value;
            }

            _logger.LogDebug($"Recieved socket message, OpCode: {payload.OpCode}");

            switch (payload.OpCode)
            {
                // Dispatch (dispatches an event)
                case OpCodes.Dispatch:
                {
                    _logger.LogDebug($"Recieved OpCode 0, event: {payload.EventName}");

                    // Listed here: https://discordapp.com/developers/docs/topics/gateway#commands-and-events-gateway-events
                    switch (payload.EventName)
                    {
                        case "READY":
                        {
                            /*
                            Moved to DiscordClient.Initialized -> Not at all cases will READY be called.
                            _client.UpdatePluginReference();
                            _client.CallHook("DiscordSocket_Initialized");
                            */

                            Ready ready = payload.EventData.ToObject<Ready>();

                            if (ready.Guilds.Count != 0)
                            {
                                _logger.LogWarning($"Your bot was found in {ready.Guilds.Count} Guilds!");
                            }

                            if (ready.Guilds.Count == 0)
                            {
                                _logger.LogDebug("Ready event but no Guilds sent.");
                            }

                            _client.DiscordServers = ready.Guilds;
                            _client.SessionID = ready.SessionID;
                            
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
                            Channel channelCreate = payload.EventData.ToObject<Channel>();
                            if (channelCreate.type == ChannelType.DM || channelCreate.type == ChannelType.GROUP_DM)
                                _client.DMs.Add(channelCreate);
                            else
                                _client.GetGuild(channelCreate.guild_id).channels.Add(channelCreate);
                            _client.CallHook("Discord_ChannelCreate", null, channelCreate);
                            break;
                        }

                        case "CHANNEL_UPDATE":
                        {
                            Channel channelUpdated = payload.EventData.ToObject<Channel>();
                            Channel channelPrevious = (channelUpdated.type == ChannelType.DM || channelUpdated.type == ChannelType.GROUP_DM)
                                ? _client.DMs?.FirstOrDefault(x => x.id == channelUpdated.id)
                                : _client.GetGuild(channelUpdated.guild_id).channels.FirstOrDefault(x => x.id == channelUpdated.id);

                            if (channelPrevious != null)
                            {
                                if (channelUpdated.type == ChannelType.DM || channelUpdated.type == ChannelType.GROUP_DM)
                                    _client.DMs.Remove(channelPrevious);
                                else
                                    _client.GetGuild(channelUpdated.guild_id).channels.Remove(channelPrevious);
                            }

                            if (channelUpdated.type == ChannelType.DM || channelUpdated.type == ChannelType.GROUP_DM)
                                _client.DMs.Add(channelUpdated);
                            else
                                _client.GetGuild(channelUpdated.guild_id).channels.Add(channelUpdated);

                            _client.CallHook("Discord_ChannelUpdate", null, channelUpdated, channelPrevious);
                            break;
                        }

                        case "CHANNEL_DELETE":
                        {
                            Channel channelDelete = payload.EventData.ToObject<Channel>();
                            
                            _client.GetGuild(channelDelete.guild_id).channels.RemoveAll(c => c.id == channelDelete.id);

                            _client.CallHook("Discord_ChannelDelete", null, channelDelete);
                            break;
                        }

                        case "CHANNEL_PINS_UPDATE":
                        {
                            ChannelPinsUpdate channelPinsUpdate = payload.EventData.ToObject<ChannelPinsUpdate>();
                            _client.CallHook("Discord_ChannelPinsUpdate", null, channelPinsUpdate);
                            break;
                        }

                        // NOTE: Some elements of Guild object is only sent with GUILD_CREATE
                        case "GUILD_CREATE":
                        {
                            Guild guildCreate = payload.EventData.ToObject<Guild>();
                            string g_id = guildCreate.id;
                            bool g_unavail = guildCreate.unavailable ?? false;
                            if(_client.GetGuild(g_id) == null)
                            {
                                _client.DiscordServers.Add(guildCreate);
                                _logger.LogDebug($"Guild ID ({g_id}) added to list.");
                            }
                            else if(g_unavail == false && (_client.GetGuild(g_id)?.unavailable ?? false) == true)
                            {
                                _client.UpdateGuild(g_id, guildCreate);
                                _logger.LogDebug($"Guild ID ({g_id}) updated to list.");
                            }
                            _client.CallHook("Discord_GuildCreate", null, guildCreate);
                            break;
                        }

                        case "GUILD_UPDATE":
                        {
                            Guild guildUpdate = payload.EventData.ToObject<Guild>();
                            //_client.UpdateGuild(guildUpdate.id, guildUpdate); // <-- DON'T REPLACE GUILD REFERENCE!!!!
                            _client.GetGuild(guildUpdate.id).Update(guildUpdate);
                            _client.CallHook("Discord_GuildUpdate", null, guildUpdate);
                            break;
                        }

                        case "GUILD_DELETE":
                        {
                            Guild guildDelete = payload.EventData.ToObject<Guild>();
                            if(guildDelete.unavailable ?? false == true) // outage
                            {
                                _logger.LogDebug($"Guild ID {guildDelete.id} outage!");
                                _client.UpdateGuild(guildDelete.id, guildDelete);
                            }
                            else
                            {
                                _logger.LogDebug($"Guild ID {guildDelete.id} removed from list");
                                _client.DiscordServers.Remove(_client.GetGuild(guildDelete.id)); // guildDelete may not be same reference
                            }
                            _client.CallHook("Discord_GuildDelete", null, guildDelete);
                            break;
                        }

                        case "GUILD_BAN_ADD":
                        {
                            User bannedUser = payload.EventData.ToObject<BanObject>().user;
                            _client.CallHook("Discord_GuildBanAdd", null, bannedUser);
                            break;
                        }

                        case "GUILD_BAN_REMOVE":
                        {
                            User unbannedUser = payload.EventData.ToObject<BanObject>().user;
                            _client.CallHook("Discord_GuildBanRemove", null, unbannedUser);
                            break;
                        }

                        case "GUILD_EMOJIS_UPDATE":
                        {
                            GuildEmojisUpdate guildEmojisUpdate = payload.EventData.ToObject<GuildEmojisUpdate>();
                            _client.CallHook("Discord_GuildEmojisUpdate", null, guildEmojisUpdate);
                            break;
                        }

                        case "GUILD_INTEGRATIONS_UPDATE":
                        {
                            GuildIntergrationsUpdate guildIntergrationsUpdate = payload.EventData.ToObject<GuildIntergrationsUpdate>();
                            _client.CallHook("Discord_GuildIntergrationsUpdate", null, guildIntergrationsUpdate);
                            break;
                        }

                        case "GUILD_MEMBER_ADD":
                        {
                            GuildMemberAdd memberAdded = payload.EventData.ToObject<GuildMemberAdd>();
                            GuildMember guildMember = memberAdded as GuildMember;

                            _client.GetGuild(memberAdded.guild_id)?.members.Add(guildMember);

                            _client.CallHook("Discord_MemberAdded", null, guildMember);
                            break;
                        }

                        case "GUILD_MEMBER_REMOVE":
                        {
                            GuildMemberRemove memberRemoved = payload.EventData.ToObject<GuildMemberRemove>();

                            GuildMember member = _client.GetGuild(memberRemoved.guild_id)?.members.FirstOrDefault(x => x.user.id == memberRemoved.user.id);
                            if (member != null)
                            {
                                _client.GetGuild(memberRemoved.guild_id)?.members.Remove(member);
                            }

                            _client.CallHook("Discord_MemberRemoved", null, member);
                            break;
                        }

                        case "GUILD_MEMBER_UPDATE":
                        {
                            GuildMemberUpdate memberUpdated = payload.EventData.ToObject<GuildMemberUpdate>();

                            GuildMember newMember = _client.GetGuild(memberUpdated.guild_id)?.members.FirstOrDefault(x => x.user.id == memberUpdated.user.id);
                            if (newMember == null)
                            {
                                _logger.LogWarning($"Tried to update a user who doesn't exist.  Guild ID: {memberUpdated.guild_id} Member ID: {memberUpdated.user.id}");
                                break;
                            }
                            
                            GuildMember oldMember = JObject.FromObject(newMember).ToObject<GuildMember>(); // lazy way to copy the object
                            if (memberUpdated.user != null)
                                newMember.user = memberUpdated.user;
                            if (memberUpdated.nick != null)
                                newMember.nick = memberUpdated.nick;
                            if (memberUpdated.roles != null)
                                newMember.roles = memberUpdated.roles;

                            _client.CallHook("Discord_GuildMemberUpdate", null, memberUpdated, oldMember);
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
                            GuildRoleCreate guildRoleCreate = payload.EventData.ToObject<GuildRoleCreate>();

                            _client.GetGuild(guildRoleCreate.guild_id)?.roles.Add(guildRoleCreate.role);

                            _client.CallHook("Discord_GuildRoleCreate", null, guildRoleCreate.role);
                            break;
                        }

                        case "GUILD_ROLE_UPDATE":
                        {
                            GuildRoleUpdate guildRoleUpdate = payload.EventData.ToObject<GuildRoleUpdate>();
                            Role newRole = guildRoleUpdate.role;

                            Role oldRole = _client.GetGuild(guildRoleUpdate.guild_id).roles.FirstOrDefault(x => x.id == newRole.id);
                            if (oldRole != null)
                            {
                                _client.GetGuild(guildRoleUpdate.guild_id).roles.Remove(oldRole);
                            }

                            _client.GetGuild(guildRoleUpdate.guild_id).roles.Add(newRole);

                            _client.CallHook("Discord_GuildRoleUpdate", null, newRole, oldRole);
                            break;
                        }

                        case "GUILD_ROLE_DELETE":
                        {
                            GuildRoleDelete guildRoleDelete = payload.EventData.ToObject<GuildRoleDelete>();

                            Role deletedRole = _client.GetGuild(guildRoleDelete.guild_id)?.roles.FirstOrDefault(x => x.id == guildRoleDelete.role_id);
                            if (deletedRole != null)
                            {
                                _client.GetGuild(guildRoleDelete.guild_id).roles.Remove(deletedRole);
                            }

                            _client.CallHook("Discord_GuildRoleDelete", null, deletedRole);
                            break;
                        }

                        case "MESSAGE_CREATE":
                        {
                            Message messageCreate = payload.EventData.ToObject<Message>();
                            Channel c;
                            if (messageCreate.guild_id != null)
                                c = _client.GetGuild(messageCreate.guild_id)?.channels.FirstOrDefault(x => x.id == messageCreate.channel_id);
                            else
                                c = _client.DMs.FirstOrDefault(x => x.id == messageCreate.channel_id);
                            if(c != null)
                                c.last_message_id = messageCreate.id;
                            _client.CallHook("Discord_MessageCreate", null, messageCreate);
                            break;
                        }

                        case "MESSAGE_UPDATE":
                        {
                            Message messageUpdate = payload.EventData.ToObject<Message>();
                            _client.CallHook("Discord_MessageUpdate", null, messageUpdate);
                            break;
                        }

                        case "MESSAGE_DELETE":
                        {
                            MessageDelete messageDelete = payload.EventData.ToObject<MessageDelete>();
                            _client.CallHook("Discord_MessageDelete", null, messageDelete);
                            break;
                        }

                        case "MESSAGE_DELETE_BULK":
                        {
                            MessageDeleteBulk messageDeleteBulk = payload.EventData.ToObject<MessageDeleteBulk>();
                            _client.CallHook("Discord_MessageDeleteBulk", null, messageDeleteBulk);
                            break;
                        }

                        case "MESSAGE_REACTION_ADD":
                        {
                            MessageReactionUpdate messageReactionAdd = payload.EventData.ToObject<MessageReactionUpdate>();
                            _client.CallHook("Discord_MessageReactionAdd", null, messageReactionAdd);
                            break;
                        }

                        case "MESSAGE_REACTION_REMOVE":
                        {
                            MessageReactionUpdate messageReactionRemove = payload.EventData.ToObject<MessageReactionUpdate>();
                            _client.CallHook("Discord_MessageReactionRemove", null, messageReactionRemove);
                            break;
                        }

                        case "MESSAGE_REACTION_REMOVE_ALL":
                        {
                            MessageReactionRemoveAll messageReactionRemoveAll = payload.EventData.ToObject<MessageReactionRemoveAll>();
                            _client.CallHook("Discord_MessageReactionRemoveAll", null, messageReactionRemoveAll);
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

                            User updatedPresence = presenceUpdate?.user;

                            if (updatedPresence != null)
                            {
                                var updatedMember = _client.GetGuild(presenceUpdate.guild_id)?.members.FirstOrDefault(x => x.user.id == updatedPresence.id);

                                if (updatedMember != null)
                                {
                                    //updatedMember.user = updatedPresence;
                                    updatedMember.user.Update(updatedPresence);
                                }
                            }

                            _client.CallHook("Discord_PresenceUpdate", null, updatedPresence);
                            break;
                        }

                        // Bots should ignore this
                        case "PRESENCES_REPLACE":
                            break;

                        case "TYPING_START":
                        {
                            TypingStart typingStart = payload.EventData.ToObject<TypingStart>();
                            _client.CallHook("Discord_TypingStart", null, typingStart);
                            break;
                        }

                        case "USER_UPDATE":
                        {
                            User userUpdate = payload.EventData.ToObject<User>();

                            //GuildMember memberUpdate = _client.DiscordServer.members.FirstOrDefault(x => x.user.id == userUpdate.id);

                            //memberUpdate.user = userUpdate;

                            var guilds = _client.DiscordServers.Where(x => x.members.FirstOrDefault(y => y.user.id == userUpdate.id) != null).ToList();
                            foreach(Guild g in guilds)
                            {
                                GuildMember memberUpdate = g.members.FirstOrDefault(x => x.user.id == userUpdate.id);
                                if (memberUpdate != null)
                                {
                                    memberUpdate.user = userUpdate;
                                }
                            }

                            _client.CallHook("Discord_UserUpdate", null, userUpdate);
                            break;
                        }

                        case "VOICE_STATE_UPDATE":
                        {
                            VoiceState voiceStateUpdate = payload.EventData.ToObject<VoiceState>();
                            _client.CallHook("Discord_VoiceStateUpdate", null, voiceStateUpdate);
                            break;
                        }

                        case "VOICE_SERVER_UPDATE":
                        {
                            VoiceServerUpdate voiceServerUpdate = payload.EventData.ToObject<VoiceServerUpdate>();
                            _client.CallHook("Discord_VoiceServerUpdate", null, voiceServerUpdate);
                            break;
                        }

                        case "WEBHOOKS_UPDATE":
                        {
                            WebhooksUpdate webhooksUpdate = payload.EventData.ToObject<WebhooksUpdate>();
                            _client.CallHook("Discord_WebhooksUpdate", null, webhooksUpdate);
                            break;
                        }

                        case "INVITE_CREATE":
                        {
                            InviteCreated invitecreatedUpdate = payload.EventData.ToObject<InviteCreated>();
                            _client.CallHook("Discord_InviteCreated", null, invitecreatedUpdate);
                            break;
                        }

                        case "INVITE_DELETE":
                        {
                            InviteDeleted invitedeletedUpdate = payload.EventData.ToObject<InviteDeleted>();
                            _client.CallHook("Discord_InviteDeleted", null, invitedeletedUpdate);
                            break;
                        }

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
                case OpCodes.Heartbeat:
                {
                    _logger.LogInfo("Manually sent heartbeat (received opcode 1)");
                    _client.SendHeartbeat();
                    break;
                }

                // Reconnect (used to tell clients to reconnect to the gateway)
                // we should immediately reconnect here
                case OpCodes.Reconnect:
                {
                    _logger.LogInfo("Reconnect has been called (opcode 7)! Reconnecting...");
                    _webSocket.ShouldAttemptResume = true; // attempt resume opcode
                    _client.requestReconnect = true;
                    _webSocket.ReconnectRequested(); //If we disconnect normally our session becomes invalid per: https://discord.com/developers/docs/topics/gateway#resuming
                    break;
                }

                // Invalid Session (used to notify client they have an invalid session ID)
                case OpCodes.InvalidSession:
                {
                    _logger.LogWarning("Invalid Session ID opcode recieved!");
                    _client.requestReconnect = true;
                    _webSocket.ShouldAttemptResume = false;
                    _webSocket.Disconnect(false);
                    break;
                }

                // Hello (sent immediately after connecting, contains heartbeat and server debug information)
                case OpCodes.Hello:
                {
                    Hello hello = payload.EventData.ToObject<Hello>();
                    _client.CreateHeartbeat(hello.HeartbeatInterval);
                    // Client should now perform identification
                    //_client.Identify();
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
                case OpCodes.HeartbeatACK:
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
