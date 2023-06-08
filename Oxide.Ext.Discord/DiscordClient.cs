using System;
using System.Text.RegularExpressions;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Entities.Gateway;
using Oxide.Ext.Discord.Entities.Gateway.Commands;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;
using Oxide.Ext.Discord.Libraries.AppCommands;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;
using Oxide.Ext.Discord.Plugins.Setup;

namespace Oxide.Ext.Discord
{
    /// <summary>
    /// Represents the object a plugin uses to connects to discord
    /// </summary>
    public class DiscordClient
    {
        /// <summary>
        /// Which plugin is the owner of this client
        /// </summary>
        public Plugin Plugin { get; private set; }

        /// <summary>
        /// The ID of the plugin
        /// </summary>
        public readonly PluginId PluginId;
        
        /// <summary>
        /// The full plugin name including author and version
        /// </summary>
        public readonly string PluginName;
        
        /// <summary>
        /// The bot client that is unique to the Token used
        /// </summary>
        public BotClient Bot { get; private set; }
        
        /// <summary>
        /// Settings used to connect to discord and configure the extension
        /// </summary>
        internal BotConnection Connection { get; private set; }

        internal ILogger Logger;
        
        private PluginSetupData _data;

        /// <summary>
        /// Constructor for a discord client
        /// </summary>
        /// <param name="plugin">Plugin that will own this discord client</param>
        internal DiscordClient(Plugin plugin)
        {
            Plugin = plugin;
            PluginId = plugin.Id();
            PluginName = plugin.FullName();
            _data = new PluginSetupData(plugin);
            PluginExt.OnPluginLoaded(plugin);
            BaseDiscordLibrary.ProcessPluginLoaded(_data);
        }
        
        /// <summary>
        /// Starts a connection to discord with the given apiKey and intents
        /// </summary>
        /// <param name="apiKey">API key for the connecting bot</param>
        /// <param name="intents">Intents the bot needs in order to function</param>
        public void Connect(string apiKey, GatewayIntents intents) => Connect(new BotConnection(apiKey, intents));

        /// <summary>
        /// Starts a connection to discord with the given discord settings
        /// </summary>
        /// <param name="connection">Discord connection settings</param>
        public void Connect(BotConnection connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Logger = DiscordLoggerFactory.Instance.CreateExtensionLogger(connection.LogLevel);
            
            if (string.IsNullOrEmpty(Connection.ApiToken))
            {
                Logger.Error("API Token is null or empty!");
                return;
            }

            if (!string.IsNullOrEmpty(DiscordExtension.TestVersion))
            {
                Logger.Warning("Using Discord Test Version: {0}", DiscordExtension.FullExtensionVersion);
            }

            Logger.Debug($"{nameof(DiscordClient)}.{nameof(Connect)} AddDiscordClient for {{0}}", Plugin.FullName());

            Connection.Initialize(this);
            Bot = BotClientFactory.Instance.InitializeBotClient(this);
            DiscordAppCommand.Instance.RegisterApplicationCommands(_data, Connection);
            _data = null;
        }

        /// <summary>
        /// Disconnects this client from discord
        /// </summary>
        public void Disconnect()
        {
            Bot?.RemoveClient(this);
            Bot = null;
        }

        /// <summary>
        /// Returns if the client is connected to a bot and if the bot is initialized
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return Bot?.Initialized ?? false;
        }

        #region Websocket Commands
        /// <summary>
        /// Used to request guild members from discord for a specific guild
        /// </summary>
        /// <param name="request">Request for guild members</param>
        public void RequestGuildMembers(GuildMembersRequestCommand request)
        {
            Bot?.SendWebSocketCommand(this, GatewayCommandCode.RequestGuildMembers, request);
        }

        /// <summary>
        /// Used to update the voice state for the bot
        /// </summary>
        /// <param name="voiceState"></param>
        public void UpdateVoiceState(UpdateVoiceStatusCommand voiceState)
        {
            Bot?.SendWebSocketCommand(this, GatewayCommandCode.VoiceStateUpdate, voiceState);
        }

        /// <summary>
        /// Used to update the bots status in discord
        /// </summary>
        /// <param name="presenceUpdate"></param>
        public void UpdateStatus(UpdatePresenceCommand presenceUpdate)
        {
            Bot?.SendWebSocketCommand(this, GatewayCommandCode.PresenceUpdate, presenceUpdate);
        }
        #endregion
        
        internal void CloseClient()
        {
            try
            {
                DiscordExtension.GlobalLogger.Debug($"{nameof(DiscordClient)}.{nameof(CloseClient)} Closing DiscordClient for plugin {{0}}", PluginName);
                Disconnect();
            }
            catch (Exception ex)
            {
                Logger.Exception($"Failed to close the {nameof(DiscordClient)} for {{0}}", PluginName, ex);
            }
            finally
            {
                DiscordClientFactory.Instance.RemoveClient(this);
                
                // ReSharper disable once SuspiciousTypeConversion.Global
                if (Plugin != null && Plugin.IsLoaded && Plugin is IDiscordPlugin discordPlugin)
                {
                    discordPlugin.Client = null;
                }
                
                Plugin = null;
            }
        }
    }
}
