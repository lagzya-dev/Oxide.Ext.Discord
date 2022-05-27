using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Hooks;
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

        private static readonly Regex TokenValidator = new Regex(@"^[\w-]{24}\.[\w-]{6}\.[\w-]+$", RegexOptions.Compiled);

        /// <summary>
        /// Which plugin is the owner of this client
        /// </summary>
        public Plugin Plugin { get; private set; }

        /// <summary>
        /// The name of the plugin that this client was created for
        /// </summary>
        public readonly string PluginName;

        /// <summary>
        /// The bot client that is unique to the Token used
        /// </summary>
        public BotClient Bot { get; internal set; }
        
        /// <summary>
        /// Settings used to connect to discord and configure the extension
        /// </summary>
        internal DiscordSettings Settings { get; private set; }

        internal ILogger Logger;

        /// <summary>
        /// Constructor for a discord client
        /// </summary>
        /// <param name="plugin">Plugin that will own this discord client</param>
        public DiscordClient(Plugin plugin)
        {
            Plugin = plugin;
            PluginName = plugin.Name;
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
            Logger = new DiscordLogger(settings.LogLevel);
            
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

            if ((settings.Intents & GatewayIntents.GuildMessages) != 0 && (settings.Intents & GatewayIntents.MessageContentIntent) == 0)
            {
                settings.Intents |= GatewayIntents.MessageContentIntent;
                Logger.Warning("Plugin {0} is using GatewayIntent.GuildMessages and did not specify GatewayIntents.MessageContentIntent", Plugin.Name);
            }
            
            Logger.Debug($"{nameof(DiscordClient)}.{nameof(Connect)} AddDiscordClient for {{0}}", Plugin.Name);

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

        /// <summary>
        /// Subscribe the owner plugin to the given hook
        /// </summary>
        /// <param name="hook">Hook to subscribe to</param>
        public void SubscribeHook(string hook)
        {
            Bot?.Hooks.SubscribeHook(Plugin, hook);
        }
        
        /// <summary>
        /// Unsubscribe the owner plugin to the given hook
        /// </summary>
        /// <param name="hook">Hook to unsubscribe to</param>
        public void UnsubscribeHook(string hook)
        {
            Bot?.Hooks.UnsubscribeHook(Plugin, hook);
        }

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
            return GetClient(plugin.Name);
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
            OnPluginRemoved(plugin);
            
            foreach (FieldInfo field in plugin.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                if (field.GetCustomAttributes(typeof(DiscordClientAttribute), true).Length != 0)
                {
                    DiscordClient client = Clients[plugin.Name];
                    if (client == null)
                    {
                        DiscordExtension.GlobalLogger.Debug($"{nameof(DiscordClient)}.{nameof(OnPluginAdded)} Creating DiscordClient for plugin {{0}}", plugin.Name);
                        client = new DiscordClient(plugin);
                        Clients[plugin.Name] = client;
                    }
                    
                    field.SetValue(plugin, client);
                    plugin.Call(DiscordExtHooks.OnDiscordClientCreated);
                    break;
                }
            }
            
            DiscordExtension.DiscordCommand.ProcessPluginCommands(plugin);
        }

        internal static void OnPluginRemoved(Plugin plugin)
        {
            DiscordClient client = Clients[plugin.Name];
            if (client == null)
            {
                return;
            }

            CloseClient(client);

            DiscordExtension.DiscordLink.OnPluginUnloaded(plugin);
            DiscordExtension.DiscordCommand.OnPluginUnloaded(plugin);
            DiscordExtension.DiscordSubscriptions.OnPluginUnloaded(plugin);
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

                DiscordExtension.GlobalLogger.Debug($"{nameof(DiscordClient)}.{nameof(CloseClient)} Closing DiscordClient for plugin {{0}}", client.Plugin.Name);
                foreach (FieldInfo field in client.Plugin.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    if (field.GetCustomAttributes(typeof(DiscordClientAttribute), true).Length != 0)
                    {
                        field.SetValue(client.Plugin, null);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                client.Logger.Exception($"Failed to close the {nameof(DiscordClient)} for {{0}}", client.PluginName, ex);
            }
            finally
            {
                Clients.Remove(client.PluginName);
                client.Plugin = null;
            }
            
        }
        #endregion
    }
}
