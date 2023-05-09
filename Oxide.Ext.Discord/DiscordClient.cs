using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Entities.Gatway.Commands;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Hooks;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;
using Oxide.Ext.Discord.Libraries.AppCommands;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord
{
    /// <summary>
    /// Represents the object a plugin uses to connects to discord
    /// </summary>
    public class DiscordClient
    {
        internal static readonly Hash<string, DiscordClient> Clients = new Hash<string, DiscordClient>();

        private static readonly Regex TokenValidator = new Regex(@"^[\w-]+\.[\w-]+\.[\w-]+$", RegexOptions.Compiled);
        private static readonly Type ClientAttribute = typeof(DiscordClientAttribute);

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

        private FieldInfo _clientField;

        /// <summary>
        /// Constructor for a discord client
        /// </summary>
        /// <param name="plugin">Plugin that will own this discord client</param>
        public DiscordClient(Plugin plugin)
        {
            Plugin = plugin;
            PluginId = plugin.Id();
            PluginName = Plugin.FullName();
            Clients[PluginId] = this;
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
            Logger = DiscordLoggerFactory.Instance.GetExtensionLogger(settings.LogLevel);
            
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
            
            BotClient.AddDiscordClient(this);
            DiscordHook.CallGlobalHook(DiscordExtHooks.OnDiscordClientConnected, Plugin, this);
        }

        /// <summary>
        /// Disconnects this client from discord
        /// </summary>
        public void Disconnect()
        {
            DiscordHook.CallGlobalHook(DiscordExtHooks.OnDiscordClientDisconnected, Plugin, this);
            Bot?.Rest.OnClientClosed(this);
            Bot?.RemoveClient(this);
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
            if (bot.Application != null)
            {
                DiscordAppCommand.Instance.RegisterApplicationCommands(bot.Application, Plugin);
            }
        }

        internal void OnBotRemoved()
        {
            Bot = null;
        }

        /// <summary>
        /// Subscribe the owner plugin to the given hook
        /// </summary>
        /// <param name="hook">Hook to subscribe to</param>
        public void SubscribeHook(string hook)
        {
            Bot?.Hooks.SubscribeHook(this, hook);
        }
        
        /// <summary>
        /// Unsubscribe the owner plugin to the given hook
        /// </summary>
        /// <param name="hook">Hook to unsubscribe to</param>
        public void UnsubscribeHook(string hook)
        {
            Bot?.Hooks.UnsubscribeHook(Plugin, hook);
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
        
        #region Plugin Handling
        /// <summary>
        /// Sets the client field on the plugin.
        /// This should only be used if you need the client in the Init or Loaded hooks
        /// The client field will automatically be set on the plugin before the OnDiscordClientCreated or OnServerInitialized hooks
        /// </summary>
        /// <param name="plugin">Plugin to get client for</param>
        /// <returns>Discord client for the plugin</returns>
        public static void CreateClient(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            OnPluginAdded(plugin);
        }

        /// <summary>
        /// Gets the client for the given plugin
        /// </summary>
        /// <param name="plugin">Plugin to get client for</param>
        /// <returns>Discord client for the plugin</returns>
        public static DiscordClient GetClient(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            return GetClient(plugin.Id());
        }

        /// <summary>
        /// Gets the client for the given plugin name
        /// </summary>
        /// <param name="pluginName">Plugin Name to get client for</param>
        /// <returns>Discord client for the plugin name</returns>
        public static DiscordClient GetClient(string pluginName)
        {
            if (pluginName == null) throw new ArgumentNullException(nameof(pluginName));
            return Clients[pluginName];
        }
        
        internal static void OnPluginAdded(Plugin plugin)
        {
            if (!plugin.IsCorePlugin)
            {
                OnPluginLoadedInternal(plugin);
            }
        }
        
        internal static void OnPluginLoadedInternal(Plugin plugin)
        {
            DiscordPluginCache.Instance.OnPluginLoaded(plugin);
            OnPluginRemoved(plugin);
            
#pragma warning disable CS0184
            if (!plugin is IDiscordPlugin)
#pragma warning restore CS0184
            {
                return;
            }
            
            foreach (FieldInfo field in plugin.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                if (field.GetCustomAttributes(ClientAttribute, true).Length != 0)
                {
                    DiscordClient client = Clients[plugin.Id()];
                    if (client == null)
                    {
                        DiscordExtension.GlobalLogger.Debug($"{nameof(DiscordClient)}.{nameof(OnPluginAdded)} Creating DiscordClient for plugin {{0}}", plugin.FullName());
                        client = new DiscordClient(plugin)
                        {
                            _clientField = field
                        };
                    }

                    field.SetValue(plugin, client);
                    PluginExt.OnPluginLoaded(plugin);
                    BaseDiscordLibrary.ProcessPluginLoaded(plugin);
                    plugin.Call(DiscordExtHooks.OnDiscordClientCreated);
                    break;
                }
            }
        }

        internal static void OnPluginRemoved(Plugin plugin)
        {
            if (plugin.IsCorePlugin)
            {
                return;
            }
            
            if (DiscordExtension.IsShuttingDown)
            {
                return;
            }

            DiscordClient client = Clients[plugin.Id()];
            if (client != null)
            {
                CloseClient(client);
                BaseDiscordLibrary.ProcessPluginUnloaded(plugin);
            }

            PluginExt.OnPluginUnloaded(plugin);
            DiscordPluginCache.Instance.OnPluginUnloaded(plugin);
            DiscordLoggerFactory.Instance.OnPluginUnloaded(plugin);
        }

        internal static void CloseClient(DiscordClient client)
        {
            if (client == null)
            {
                return;
            }

            try
            {
                client.Disconnect();

                DiscordExtension.GlobalLogger.Debug($"{nameof(DiscordClient)}.{nameof(CloseClient)} Closing DiscordClient for plugin {{0}}", client.PluginName);
                if (client.Plugin != null && client.Plugin.IsLoaded && client._clientField != null)
                {
                    client._clientField.SetValue(client.Plugin, null);
                }
            }
            catch (Exception ex)
            {
                client.Logger.Exception($"Failed to close the {nameof(DiscordClient)} for {{0}}", client.PluginName, ex);
            }
            finally
            {
                Clients.Remove(client.PluginId);
                client.Plugin = null;
            }
        }
        #endregion
    }
}
