using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Rest;
using Oxide.Ext.Discord.WebSockets;
using Oxide.Plugins;

namespace Oxide.Ext.Discord
{
    /// <summary>
    /// Represents a bot that is connected to discord
    /// </summary>
    public class BotClient
    {
        /// <summary>
        /// List of active bots by bot API key
        /// </summary>
        public static readonly Hash<string, BotClient> ActiveBots = new Hash<string, BotClient>();

        /// <summary>
        /// If the connection is initialized and not disconnected
        /// </summary>
        public bool Initialized;

        /// <summary>
        /// The settings for this bot of all the combined clients
        /// </summary>
        public readonly DiscordSettings Settings;

        /// <summary>
        /// All the servers that this bot is in
        /// </summary>
        public readonly Hash<Snowflake, Guild> Servers = new Hash<Snowflake, Guild>();

        /// <summary>
        /// All the direct messages that we have seen by channel Id
        /// </summary>
        public readonly Hash<Snowflake, Channel> DirectMessagesByChannelId = new Hash<Snowflake, Channel>();

        /// <summary>
        /// All the direct messages that we have seen by User ID
        /// </summary>
        public readonly Hash<Snowflake, Channel> DirectMessagesByUserId = new Hash<Snowflake, Channel>();

        /// <summary>
        /// Application reference for this bot
        /// </summary>
        public Application Application { get; internal set; }

        /// <summary>
        /// Bot User
        /// </summary>
        public DiscordUser Bot { get; internal set; }

        /// <summary>
        /// Rest handler for all discord API calls
        /// </summary>
        public RestHandler Rest { get; private set; }
        
        internal readonly ILogger Logger;
        
        internal GatewayReadyEvent ReadyData;
        internal readonly List<Snowflake> MembersLoaded = new List<Snowflake>();
        internal readonly List<DiscordClient> ClientsPendingConnection = new List<DiscordClient>();

        internal Socket WebSocket;

        /// <summary>
        /// List of all clients that are using this bot client
        /// </summary>
        private readonly List<DiscordClient> _clients = new List<DiscordClient>();

        /// <summary>
        /// Creates a new bot client for the given settings
        /// </summary>
        /// <param name="settings"></param>
        public BotClient(DiscordSettings settings)
        {
            Settings = new DiscordSettings
            {
                ApiToken = settings.ApiToken,
                LogLevel = settings.LogLevel,
                Intents = settings.Intents
            };

            Logger = new Logger(Settings.LogLevel);
            
            Initialized = true;
            
            Rest = new RestHandler(this, Logger);
            WebSocket = new Socket(this, Logger);
        }

        /// <summary>
        /// Gets or creates a new bot client for the given discord client
        /// </summary>
        /// <param name="client">Client to use for creating / loading the bot client</param>
        /// <returns>Bot client that is created or already exists</returns>
        public static BotClient GetOrCreate(DiscordClient client)
        {
            BotClient bot = ActiveBots[client.Settings.ApiToken];
            if (bot == null)
            {
                DiscordExtension.GlobalLogger.Debug($"{nameof(BotClient)}.{nameof(GetOrCreate)} Creating new BotClient");
                bot = new BotClient(client.Settings);
                ActiveBots[client.Settings.ApiToken] = bot;
            }

            bot.AddClient(client);
            DiscordExtension.GlobalLogger.Debug($"{nameof(BotClient)}.{nameof(GetOrCreate)} Adding plugin client {client.Owner.Name} to bot {bot.Bot?.GetFullUserName}");
            return bot;
        }

        /// <summary>
        /// Connects the websocket to discord. Should only be called if the websocket is disconnected
        /// </summary>
        public void ConnectWebSocket()
        {
            if (Initialized)
            {
                Logger.Debug($"{nameof(BotClient)}.{nameof(ConnectWebSocket)} Connecting to websocket");
                WebSocket.Connect();
            }
        }

        /// <summary>
        /// Close the websocket with discord
        /// </summary>
        /// <param name="attemptReconnect">Should we attempt to reconnect to discord after closing</param>
        /// <param name="attemptResume">Should we attempt to resume the previous session</param>
        public void DisconnectWebsocket(bool attemptReconnect = false, bool attemptResume = false)
        {
            if (Initialized)
            {
                WebSocket.Disconnect(attemptReconnect, attemptResume);
            }
        }

        /// <summary>
        /// Called when bot client is no longer used by any client and can be shutdown.
        /// </summary>
        internal void ShutdownBot()
        {
            Logger.Debug($"{nameof(BotClient)}.{nameof(ShutdownBot)} Shutting down the bot");
            ActiveBots.Remove(Settings.ApiToken);
            Initialized = false;
            WebSocket?.Shutdown();
            WebSocket = null;
            Rest?.Shutdown();
            Rest = null;
            ReadyData = null;
        }

        /// <summary>
        /// Add a client to this bot client
        /// </summary>
        /// <param name="client">Client to add to the bot</param>
        public void AddClient(DiscordClient client)
        {
            if (Settings.ApiToken != client.Settings.ApiToken)
            {
                throw new Exception("Failed to add client to bot client as ApiTokens do not match");
            }

            _clients.RemoveAll(c => c == client);
            _clients.Add(client);
            
            Logger.Debug($"{nameof(BotClient)}.{nameof(AddClient)} Add client for plugin {client.Owner.Title}");
            
            if (_clients.Count == 1)
            {
                Logger.Debug($"{nameof(BotClient)}.{nameof(AddClient)} Clients.Count == 1 connecting bot");
                ConnectWebSocket();
            }
            else
            {
                if (client.Settings.LogLevel < Settings.LogLevel)
                {
                    UpdateLogLevel(client.Settings.LogLevel);
                }
                
                if (ReadyData != null)
                {
                    ClientsPendingConnection.Add(client);
                }

                GatewayIntents intents = Settings.Intents | client.Settings.Intents;
                
                //Our intents have changed. Disconnect websocket and reconnect with new intents.
                if (intents != Settings.Intents)
                {
                    Settings.Intents = intents;
                    DisconnectWebsocket(true);
                    return;
                }
                
                if (WebSocket.IsConnected())
                {
                    ProcessPendingClients();
                }
            }
        }

        internal void ProcessPendingClients()
        {
            foreach (DiscordClient client in ClientsPendingConnection)
            {
                if (ReadyData != null)
                {
                    ReadyData.Guilds = Servers.Values.ToList();
                    client.CallHook(DiscordHooks.OnDiscordGatewayReady, ReadyData, true);
                }

                foreach (Guild guild in Servers.Values)
                {
                    if (guild.IsAvailable)
                    {
                        client.CallHook(DiscordHooks.OnDiscordGuildCreated, guild, true);
                    }

                    if (MembersLoaded.Contains(guild.Id))
                    {
                        client.CallHook(DiscordHooks.OnDiscordGuildMembersLoaded, guild, true);
                    }
                }
            }
            
            _clients.Clear();
        }

        /// <summary>
        /// Remove a client from the bot client
        /// If not clients are left bot will shutdown
        /// </summary>
        /// <param name="client">Client to remove from bot client</param>
        public void RemoveClient(DiscordClient client)
        {
            _clients.Remove(client);
            Logger.Debug($"{nameof(BotClient)}.{nameof(RemoveClient)} Client Removed");
            if (_clients.Count == 0)
            {
                ShutdownBot();
                Logger.Debug($"{nameof(BotClient)}.{nameof(RemoveClient)} Bot count 0 shutting down bot");
                return;
            }

            LogLevel level = _clients.Min(c => c.Settings.LogLevel);
            if (level > Settings.LogLevel)
            {
                UpdateLogLevel(level);
            }

            GatewayIntents intents = GatewayIntents.None;
            foreach (DiscordClient exitingClients in _clients)
            {
                intents |= exitingClients.Settings.Intents;
            }

            //Our intents have changed. Disconnect websocket and reconnect with new intents.
            if (intents != Settings.Intents)
            {
                Settings.Intents = intents;
                DisconnectWebsocket(true);
            }
        }

        private void UpdateLogLevel(LogLevel level)
        {
            Logger.UpdateLogLevel(level);
            Logger.Debug($"{nameof(BotClient)}.{nameof(UpdateLogLevel)} Updating log level from:{Settings.LogLevel} to: {level}");
            Settings.LogLevel = level;
        }

        /// <summary>
        /// Call a hook on all clients using this bot
        /// </summary>
        /// <param name="hookName">Hook to call</param>
        /// <param name="args">Args for the hook</param>
        public void CallHook(string hookName, params object[] args)
        {
            foreach (DiscordClient client in _clients)
            {
                client.CallHook(hookName, args);
            }
        }

        #region Websocket Commands
        /// <summary>
        /// Used to Identify the bot with discord
        /// </summary>
        internal void Identify()
        {
            // Sent immediately after connecting. Opcode 2: Identify
            // Ref: https://discordapp.com/developers/docs/topics/gateway#identifying

            if (!Initialized)
            {
                return;
            }

            IdentifyCommand identify = new IdentifyCommand
            {
                Token = Settings.ApiToken,
                Properties = new Properties
                {
                    OS = "Oxide.Ext.Discord",
                    Browser = "Oxide.Ext.Discord",
                    Device = "Oxide.Ext.Discord"
                },
                Intents = Settings.Intents,
                Compress = false,
                LargeThreshold = 50,
                Shard = new List<int> {0, 1}
            };

            WebSocket.Send(GatewayCommandCode.Identify, identify);
        }

        /// <summary>
        /// Used to resume the current session with discord
        /// </summary>
        internal void Resume(string sessionId, int sequence)
        {
            if (!Initialized)
            {
                return;
            }

            ResumeSessionCommand resume = new ResumeSessionCommand
            {
                Sequence = sequence,
                SessionId = sessionId,
                Token = Settings.ApiToken
            };

            WebSocket.Send(GatewayCommandCode.Resume, resume);
        }

        /// <summary>
        /// Sends a heartbeat to Discord
        /// </summary>
        public void SendHeartbeat(int sequence)
        {
            WebSocket.Send(GatewayCommandCode.Heartbeat, sequence);
        }

        /// <summary>
        /// Used to request guild members from discord for a specific guild
        /// </summary>
        /// <param name="request">Request for guild members</param>
        public void RequestGuildMembers(GuildMembersRequestCommand request)
        {
            if (!Initialized)
            {
                return;
            }

            WebSocket.Send(GatewayCommandCode.RequestGuildMembers, request);
        }

        /// <summary>
        /// Used to update the voice state for the bot
        /// </summary>
        /// <param name="voiceState"></param>
        public void UpdateVoiceState(UpdateVoiceStatusCommand voiceState)
        {
            if (!Initialized)
            {
                return;
            }

            WebSocket.Send(GatewayCommandCode.VoiceStateUpdate, voiceState);
        }

        /// <summary>
        /// Used to update the bots status in discord
        /// </summary>
        /// <param name="statusUpdate"></param>
        public void UpdateStatus(UpdateStatusCommand statusUpdate)
        {
            if (!Initialized)
            {
                return;
            }

            WebSocket.Send(GatewayCommandCode.StatusUpdate, statusUpdate);
        }
        #endregion

        #region Entity Helpers
        /// <summary>
        /// Returns a guild for the specific ID
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <returns>Guild with the specified ID</returns>
        public Guild GetGuild(Snowflake? guildId)
        {
            if (guildId.HasValue && guildId.Value.IsValid())
            {
                return Servers[guildId.Value];
            }

            return null;
        }

        /// <summary>
        /// Returns the channel for the given channel ID.
        /// If guild ID is null it will search for a direct message channel
        /// If guild ID is not null it will search for a guild channel
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public Channel GetChannel(Snowflake channelId, Snowflake? guildId)
        {
            return guildId.HasValue ? GetGuild(guildId)?.Channels[channelId] : DirectMessagesByChannelId[channelId];
        }

        /// <summary>
        /// Adds a guild to the list of servers a bot is in
        /// </summary>
        /// <param name="guild"></param>
        public void AddGuild(Guild guild)
        {
            Servers[guild.Id] = guild;
        }

        /// <summary>
        /// Adds a guild if it does not exist or updates the guild with
        /// </summary>
        /// <param name="guild"></param>
        public void AddGuildOrUpdate(Guild guild)
        {
            Guild existing = Servers[guild.Id];
            if (existing != null)
            {
                Logger.Verbose($"{nameof(BotClient)}.{nameof(AddGuildOrUpdate)} Updating Existing Guild {guild.Id}");
                existing.Update(guild);
            }
            else
            {
                Logger.Verbose($"{nameof(BotClient)}.{nameof(AddGuildOrUpdate)} Adding new Guild {guild.Id}");
                Servers[guild.Id] = guild;
            }
        }

        /// <summary>
        /// Removes guild from the list of servers a bot is in
        /// </summary>
        /// <param name="guildId">Guild to remove from bot</param>
        internal void RemoveGuild(Snowflake guildId)
        {
            Servers.Remove(guildId);
            MembersLoaded.Remove(guildId);
        }

        /// <summary>
        /// Adds a Direct Message Channel to the bot cache
        /// </summary>
        /// <param name="channel">Channel to be added</param>
        public void AddDirectChannel(Channel channel)
        {
            if (channel.Type != ChannelType.Dm)
            {
                Logger.Warning($"{nameof(BotClient)}.{nameof(AddDirectChannel)} Tried to add non DM channel");
                return;
            }
            
            Logger.Verbose($"{nameof(BotClient)}.{nameof(AddDirectChannel)} Adding New Channel {channel.Id}");
            DirectMessagesByChannelId[channel.Id] = channel;
            Snowflake? toId = channel.Recipients.Values.FirstOrDefault(r => !(r.Bot ?? false))?.Id;
            if (toId.HasValue && channel.Recipients.Count == 2)
            {
                DirectMessagesByUserId[toId.Value] = channel;
            }
        }

        /// <summary>
        /// Removes a direct message channel if it exists
        /// </summary>
        /// <param name="id">ID of the channel to remove</param>
        public void RemoveDirectMessageChannel(Snowflake id)
        {
            Channel existing = DirectMessagesByChannelId[id];
            if (existing != null)
            {
                DirectMessagesByChannelId.Remove(id);
                DirectMessagesByUserId.RemoveAll(c => c.Id == id);
            }
        }
        #endregion

        #region Discord Command Helpers
        internal bool IsPluginRegistered(Plugin plugin)
        {
            return _clients.Any(t => t.RegisteredForHooks.Contains(plugin));
        }
        #endregion
    }
}