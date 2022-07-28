using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Data.Users;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Entities.Gatway.Events;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Exceptions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Hooks;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
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
        /// All the servers that this bot is in
        /// </summary>
        public readonly Hash<Snowflake, DiscordGuild> Servers = new Hash<Snowflake, DiscordGuild>();

        /// <summary>
        /// All the direct messages that we have seen by channel Id
        /// </summary>
        public readonly Hash<Snowflake, DiscordChannel> DirectMessagesByChannelId = new Hash<Snowflake, DiscordChannel>();

        /// <summary>
        /// All the direct messages that we have seen by User ID
        /// </summary>
        public readonly Hash<Snowflake, DiscordChannel> DirectMessagesByUserId = new Hash<Snowflake, DiscordChannel>();

        /// <summary>
        /// If the connection is initialized and not disconnected
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Application reference for this bot
        /// </summary>
        public DiscordApplication Application { get; private set; }

        /// <summary>
        /// Bot User
        /// </summary>
        public DiscordUser BotUser { get; private set; }

        /// <summary>
        /// Rest handler for all discord API calls
        /// </summary>
        public RestHandler Rest { get; private set; }
        
        internal readonly DiscordHook Hooks;
        internal readonly ILogger Logger;
        
        /// <summary>
        /// The settings for this bot of all the combined clients
        /// </summary>
        internal readonly DiscordSettings Settings;
        
        /// <summary>
        /// Discord Extension JSON Serialization settings
        /// </summary>
        internal readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        internal readonly JsonSerializer JsonSerializer;

        internal DiscordWebSocket WebSocket;

        private readonly List<DiscordClient> _clients = new List<DiscordClient>();

        /// <summary>
        /// List of all clients that are using this bot client
        /// </summary>
        public readonly IReadOnlyList<DiscordClient> Clients;

        private GatewayReadyEvent _readyData;

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

            Logger = new DiscordLogger(Settings.LogLevel);
            
            Initialized = true;

            JsonSerializer = JsonSerializer.Create(JsonSettings);
            JsonSerializer.Formatting = Formatting.None;

            Hooks = new DiscordHook(Logger);
            Rest = new RestHandler(this, Logger);
            WebSocket = new DiscordWebSocket(this, Logger);
            
            Clients = new ReadOnlyCollection<DiscordClient>(_clients);
        }

        /// <summary>
        /// Gets or creates a new bot client for the given discord client
        /// </summary>
        /// <param name="client">Client to use for creating / loading the bot client</param>
        /// <returns>Bot client that is created or already exists</returns>
        public static void AddDiscordClient(DiscordClient client)
        {
            try
            {
                BotClient bot = ActiveBots[client.Settings.ApiToken];
                if (bot == null)
                {
                    DiscordExtension.GlobalLogger.Debug($"{nameof(BotClient)}.{nameof(AddDiscordClient)} Creating new BotClient");
                    bot = new BotClient(client.Settings);
                    ActiveBots[client.Settings.ApiToken] = bot;
                }

                bot.AddClient(client);
                DiscordExtension.GlobalLogger.Debug($"{nameof(BotClient)}.{nameof(AddDiscordClient)} Adding {{0}} client to bot {{1}}", client.Plugin.Name, bot.BotUser?.GetFullUserName);
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception($"{nameof(BotClient)}.{nameof(AddDiscordClient)} An error occured adding {{0}} client", client.PluginName, ex);
            }
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
        /// <param name="reconnect">Should we attempt to reconnect to discord after closing</param>
        /// <param name="resume">Should we attempt to resume the previous session</param>
        public void DisconnectWebsocket(bool reconnect = false, bool resume = false)
        {
            if (Initialized)
            {
                WebSocket.Disconnect(reconnect, resume);
            }
        }

        internal void ResetWebSocket()
        {
            WebSocket?.Shutdown();
            WebSocket = new DiscordWebSocket(this, Logger);
            WebSocket.Connect();
        }
        
        internal void ResetRestApi()
        {
            Rest?.Shutdown();
            Rest = new RestHandler(this, Logger);
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
            _readyData = null;
            DiscordExtension.DiscordAppCommand.OnBotShutdown(this);
        }

        /// <summary>
        /// Add a client to this bot client
        /// </summary>
        /// <param name="client">Client to add to the bot</param>
        public void AddClient(DiscordClient client)
        {
            TokenMismatchException.ThrowIfMismatchedToken(client, Settings);

            _clients.RemoveAll(c => c == client);
            _clients.Add(client);
            client.OnBotAdded(this);
            Hooks.AddPlugin(client.Plugin);
            
            Logger.Debug($"{nameof(BotClient)}.{nameof(AddClient)} Add client for plugin {{0}}", client.Plugin.Title);
            
            if (_clients.Count == 1)
            {
                Logger.Debug($"{nameof(BotClient)}.{nameof(AddClient)} Clients.Count == 1 connecting bot");
                ConnectWebSocket();
                return;
            }
            
            if (client.Settings.LogLevel < Settings.LogLevel)
            {
                UpdateLogLevel(client.Settings.LogLevel);
            }

            GatewayIntents intents = Settings.Intents | client.Settings.Intents;
                
            //Our intents have changed. Disconnect websocket and reconnect with new intents.
            if (intents != Settings.Intents)
            {
                Settings.Intents = intents;
                if (WebSocket.Intents != Settings.Intents && WebSocket.Handler.IsConnected())
                {
                    Logger.Debug("New intents have been requested for the a connected bot. Reconnecting with updated intents.");
                    DisconnectWebsocket(true);
                }
            }
                
            if (_readyData != null)
            {
                _readyData.Guilds = Servers;
                DiscordHook.CallPluginHook(client.Plugin, DiscordExtHooks.OnDiscordGatewayReady, _readyData);

                foreach (DiscordGuild guild in Servers.Values)
                {
                    if (guild.IsAvailable)
                    {
                        DiscordHook.CallPluginHook(client.Plugin, DiscordExtHooks.OnDiscordGuildCreated, guild);
                    }

                    if (guild.HasLoadedAllMembers)
                    {
                        DiscordHook.CallPluginHook(client.Plugin, DiscordExtHooks.OnDiscordGuildMembersLoaded, guild);
                    }
                }
            }
        }

        /// <summary>
        /// Remove a client from the bot client
        /// If not clients are left bot will shutdown
        /// </summary>
        /// <param name="client">Client to remove from bot client</param>
        public void RemoveClient(DiscordClient client)
        {
            client.OnBotRemoved();
            _clients.Remove(client);
            Hooks.RemovePlugin(client.Plugin);
            Logger.Debug($"{nameof(BotClient)}.{nameof(RemoveClient)} {{0}} Client Removed", client.PluginName);
            if (_clients.Count == 0)
            {
                ShutdownBot();
                Logger.Debug($"{nameof(BotClient)}.{nameof(RemoveClient)} Bot count 0 shutting down bot");
                return;
            }

            DiscordLogLevel level = DiscordLogLevel.Off;
            for (int index = 0; index < _clients.Count; index++)
            {
                DiscordClient remainingClient = _clients[index];
                if (remainingClient.Settings.LogLevel < level)
                {
                    level = remainingClient.Settings.LogLevel;
                }
            }

            if (level > Settings.LogLevel)
            {
                UpdateLogLevel(level);
            }
            
            GatewayIntents intents = GatewayIntents.None;
            for (int index = 0; index < _clients.Count; index++)
            {
                DiscordClient exitingClients = _clients[index];
                intents |= exitingClients.Settings.Intents;
            }

            //Update Intents so the next reconnect we supply the correct GatewayIntents for connected plugins
            Settings.Intents = intents;
        }

        /// <summary>
        /// Returns the list of plugins for this bot
        /// </summary>
        /// <returns></returns>
        public string GetClientPluginList()
        {
            StringBuilder sb = DiscordPool.GetStringBuilder();
            for (int index = 0; index < _clients.Count; index++)
            {
                DiscordClient client = _clients[index];
                sb.Append('[');
                sb.Append(client.PluginName);
                sb.Append(']');
                if (index + 1 != _clients.Count)
                {
                    sb.Append(",");
                }
            }

            return DiscordPool.ToStringAndFreeStringBuilder(ref sb);
        }

        private void UpdateLogLevel(DiscordLogLevel level)
        {
            Logger.UpdateLogLevel(level);
            Logger.Debug($"{nameof(BotClient)}.{nameof(UpdateLogLevel)} Updating log level from: {{0}} to: {{1}}", Settings.LogLevel, level);
            Settings.LogLevel = level;
        }

        internal void OnClientReady(GatewayReadyEvent ready)
        {
            Application = ready.Application;
            BotUser = ready.User;
            
            if (_readyData == null)
            {
                Hooks.CallHook(DiscordExtHooks.OnDiscordGatewayReady, ready);
                if (DiscordUsersData.Instance.Bots.TryGetValue(ready.User.Id, out BotData botData))
                {
                    foreach (UserData userData in botData.Users.Values)
                    {
                        DiscordChannel channel = userData.CreateDmChannel();
                        DirectMessagesByChannelId[channel.Id] = channel;
                        DirectMessagesByUserId[userData.UserId] = channel;
                        channel.UserData = userData;
                        userData.ClearBlockIfExpired();
                    }
                }
            }
            
            _readyData = ready;
            _readyData.Guilds = Servers;

            if (Settings.Intents != WebSocket.Intents)
            {
                DisconnectWebsocket(true);
            }
        }
        
        /// <summary>
        /// Returns the first client connected to this bot.
        /// Only use for Gateway API call
        /// </summary>
        /// <returns></returns>
        internal DiscordClient GetFirstClient()
        {
            return _clients.Count != 0 ? _clients[0] : null;
        }

        /// <summary>
        /// Sends a websocket command
        /// </summary>
        /// <param name="client">Client sending the command</param>
        /// <param name="opCode"><see cref="GatewayCommandCode"/> OP Code for the command</param>
        /// <param name="data">Command Payload</param>
        public void SendWebSocketCommand(DiscordClient client, GatewayCommandCode opCode, object data)
        {
            if (Initialized)
            {
                WebSocket.Send(client, opCode, data);
            }
        }

        #region Entity Helpers
        /// <summary>
        /// Returns a guild for the specific ID
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <returns>Guild with the specified ID</returns>
        public DiscordGuild GetGuild(Snowflake? guildId)
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
        public DiscordChannel GetChannel(Snowflake channelId, Snowflake? guildId)
        {
            return guildId.HasValue ? GetGuild(guildId)?.Channels?[channelId] : DirectMessagesByChannelId[channelId];
        }

        /// <summary>
        /// Adds a guild to the list of servers a bot is in
        /// </summary>
        /// <param name="guild"></param>
        public void AddGuild(DiscordGuild guild)
        {
            Servers[guild.Id] = guild;
        }

        /// <summary>
        /// Adds a guild if it does not exist or updates the guild with
        /// </summary>
        /// <param name="guild"></param>
        public void AddGuildOrUpdate(DiscordGuild guild)
        {
            DiscordGuild existing = Servers[guild.Id];
            if (existing != null)
            {
                Logger.Verbose($"{nameof(BotClient)}.{nameof(AddGuildOrUpdate)} Updating Existing Guild {{0}}", guild.Id);
                existing.Update(guild);
            }
            else
            {
                Logger.Verbose($"{nameof(BotClient)}.{nameof(AddGuildOrUpdate)} Adding new Guild {{0}}", guild.Id);
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
        }

        /// <summary>
        /// Adds a Direct Message Channel to the bot cache
        /// </summary>
        /// <param name="channel">Channel to be added</param>
        public void AddDirectChannel(DiscordChannel channel)
        {
            if (channel.Type != ChannelType.Dm)
            {
                Logger.Warning($"{nameof(BotClient)}.{nameof(AddDirectChannel)} Tried to add a non DM channel");
                return;
            }
            
            Logger.Verbose($"{nameof(BotClient)}.{nameof(AddDirectChannel)} Adding New Channel {{0}}", channel.Id);
            DirectMessagesByChannelId[channel.Id] = channel;

            BotData data = DiscordUsersData.Instance.GetBotData(BotUser.Id);

            foreach (DiscordUser recipient in channel.Recipients.Values)
            {
                if (!recipient.Bot.HasValue || !recipient.Bot.Value)
                {
                    DirectMessagesByUserId[recipient.Id] = channel;

                    UserData userData = data.GetUserData(recipient.Id);
                    channel.UserData = userData;
                    if (userData.DmChannelId != channel.Id)
                    {
                        userData.DmChannelId = channel.Id;
                        DiscordUsersData.Instance.OnDataChanged();
                    }
                }
            }
        }

        /// <summary>
        /// Removes a direct message channel if it exists
        /// </summary>
        /// <param name="id">ID of the channel to remove</param>
        public void RemoveDirectMessageChannel(Snowflake id)
        {
            DiscordChannel existing = DirectMessagesByChannelId[id];
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
            for (int index = 0; index < _clients.Count; index++)
            {
                DiscordClient client = _clients[index];
                if (client.Plugin == plugin)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}