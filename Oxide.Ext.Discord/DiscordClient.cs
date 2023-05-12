using System;
using System.Text.RegularExpressions;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities.Applications;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;
using Oxide.Ext.Discord.Libraries.AppCommands;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord
{
    /// <summary>
    /// Represents the object a plugin uses to connects to discord
    /// </summary>
    public class DiscordClient
    {
        private static readonly Regex TokenValidator = new Regex(@"^[\w-]+\.[\w-]+\.[\w-]+$", RegexOptions.Compiled);

        /// <summary>
        /// Which plugin is the owner of this client
        /// </summary>
        public Plugin Plugin { get; private set; }

        /// <summary>
        /// The name of the plugin used as an ID
        /// </summary>
        public readonly string PluginId;
        
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
        internal DiscordSettings Settings { get; private set; }

        internal ILogger Logger;

        /// <summary>
        /// Constructor for a discord client
        /// </summary>
        /// <param name="plugin">Plugin that will own this discord client</param>
        internal DiscordClient(Plugin plugin)
        {
            Plugin = plugin;
            PluginId = plugin.Id();
            PluginName = Plugin.FullName();
            PluginExt.OnPluginLoaded(plugin);
            BaseDiscordLibrary.ProcessPluginLoaded(plugin);
            plugin.Call(DiscordExtHooks.OnDiscordClientCreated);
        }
        
        /// <summary>
        /// Starts a connection to discord with the given apiKey and intents
        /// </summary>
        /// <param name="apiKey">API key for the connecting bot</param>
        /// <param name="intents">Intents the bot needs in order to function</param>
        public void Connect(string apiKey, GatewayIntents intents)
        {
            DiscordSettings settings = new DiscordSettings
            {
                ApiToken = apiKey,
                LogLevel = DiscordLogLevel.Info,
                Intents = intents
            };
            
            Connect(settings);
        }
        
        /// <summary>
        /// Starts a connection to discord with the given discord settings
        /// </summary>
        /// <param name="settings">Discord connection settings</param>
        public void Connect(DiscordSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            Logger = DiscordLoggerFactory.Instance.CreateExtensionLogger(settings.LogLevel);
            
            if (string.IsNullOrEmpty(Settings.ApiToken))
            {
                Logger.Error("API Token is null or empty!");
                return;
            }

            if (!TokenValidator.IsMatch(Settings.ApiToken))
            {
                Logger.Warning("API Token does not appear to be a valid discord bot token: {0} for plugin {1}. Please confirm you are using the correct bot token. If the token is correct and this message is showing please let the Discord Extension Developers know.", Settings.GetHiddenToken(), PluginName);
            }

            if (!string.IsNullOrEmpty(DiscordExtension.TestVersion))
            {
                Logger.Warning("Using Discord Test Version: {0}", DiscordExtension.FullExtensionVersion);
            }

            if (settings.HasIntents(GatewayIntents.GuildMessages) && !settings.HasIntents(GatewayIntents.MessageContent))
            {
                settings.Intents |= GatewayIntents.MessageContent;
                Logger.Warning("Plugin {0} is using GatewayIntent.GuildMessages and did not specify GatewayIntents.MessageContent", Plugin.FullName());
            }
            
            Logger.Debug($"{nameof(DiscordClient)}.{nameof(Connect)} AddDiscordClient for {{0}}", Plugin.FullName());
            
            BotClientFactory.Instance.InitializeBotClient(this);
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

        internal void OnBotAdded(BotClient bot)
        {
            Bot = bot;
        }

        internal void RegisterApplicationCommands(DiscordApplication application)
        {
            DiscordAppCommand.Instance.RegisterApplicationCommands(application, Plugin);
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
