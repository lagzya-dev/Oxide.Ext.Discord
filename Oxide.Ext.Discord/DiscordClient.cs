using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;
using Oxide.Ext.Discord.Entities.Gatway;
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
        
        /// <summary>
        /// Which plugin is the owner of this client
        /// </summary>
        public Plugin Owner { get; }
        
        /// <summary>
        /// List of plugins that are registered to receive hook calls for this client
        /// </summary>
        public List<Plugin> RegisteredForHooks { get; } = new List<Plugin>();
        
        /// <summary>
        /// The bot client that is unique to the Token used
        /// </summary>
        public BotClient Bot { get; private set; }
        
        /// <summary>
        /// Settings used to connect to discord and configure the extension
        /// </summary>
        public DiscordSettings Settings { get; private set; }
        
        internal ILogger Logger;
        
        private readonly Hash<Plugin, Event.Callback<Plugin, PluginManager>> _pluginRemovedFromManager = new Hash<Plugin, Event.Callback<Plugin, PluginManager>>();

        /// <summary>
        /// Constructor for a discord client
        /// </summary>
        /// <param name="plugin">Plugin that will own this discord client</param>
        public DiscordClient(Plugin plugin)
        {
            Owner = plugin;
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
                LogLevel = LogLevel.Warning,
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
            Logger = new Logger(settings.LogLevel);
            
            if (string.IsNullOrEmpty(Settings.ApiToken))
            {
                Logger.Error("API Token is null or empty!");
                return;
            }

            if (!string.IsNullOrEmpty(DiscordExtension.TestVersion))
            {
                Logger.Warning($"Using Discord Test Version: {DiscordExtension.GetExtensionVersion}");
            }
            
            Logger.Debug($"{nameof(DiscordClient)}.{nameof(Connect)} GetOrCreate bot for {Owner.Name}");

            Bot = BotClient.GetOrCreate(this);

            RegisterPluginForHooks(Owner);
            Interface.Call("Discord_ClientConnect", Owner, this);
        }
        
        /// <summary>
        /// Disconnects this client from discord
        /// </summary>
        public void Disconnect()
        {
            Interface.Call("Discord_ClientDisconnect", Owner, this);
            Bot?.RemoveClient(this);
        }

        /// <summary>
        /// Registers a plugin to receive hook calls for this client
        /// </summary>
        /// <param name="plugin"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPluginForHooks(Plugin plugin)
        {
            if (plugin == null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }
            
            RegisteredForHooks.RemoveAll(p => p.Name == plugin.Name);
            RegisteredForHooks.Add(plugin);

            if (plugin != Owner && !_pluginRemovedFromManager.ContainsKey(plugin))
            {
                _pluginRemovedFromManager[plugin] = plugin.OnRemovedFromManager.Add(RemovePluginFromHooks);
            }
        }

        /// <summary>
        /// Remove a plugin from hooks
        /// </summary>
        /// <param name="plugin">Plugin to be removed from hooks</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemovePluginFromHooks(Plugin plugin)
        {
            if (plugin == null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }
            
            RemovePluginFromHooks(plugin, null);
        }

        private void RemovePluginFromHooks(Plugin plugin, PluginManager manager)
        {
            RegisteredForHooks.RemoveAll(p => p.Name == plugin.Name);
            if (_pluginRemovedFromManager.TryGetValue(plugin, out Event.Callback<Plugin, PluginManager> callback))
            {
                callback.Remove();
                _pluginRemovedFromManager.Remove(plugin);
            }
        }

        /// <summary>
        /// Call a hook for all plugins registered to receive hook calls for this client
        /// </summary>
        /// <param name="hookName"></param>
        /// <param name="args"></param>
        public void CallHook(string hookName, params object[] args)
        {
            //Run from next tick so we can be sure it's ran on the main thread.
            Interface.Oxide.NextTick(() =>
            {
                for (int index = 0; index < RegisteredForHooks.Count; index++)
                {
                    Plugin plugin = RegisteredForHooks[index];
                    plugin.CallHook(hookName, args);
                }
            });
        }
        
        /// <summary>
        /// Call a hook for all plugins registered to receive hook calls for this client
        /// </summary>
        /// <param name="hookName"></param>
        /// <param name="args"></param>
        public static void GlobalCallHook(string hookName, params object[] args)
        {
            foreach (DiscordClient client in Clients.Values)
            {
                client.CallHook(hookName, args);
            }
        }

        #region Plugin Handling
        /// <summary>
        /// Gets the client for the given plugin
        /// </summary>
        /// <param name="plugin">Plugin to get client for</param>
        /// <returns>Discord client for the plugin</returns>
        public static DiscordClient GetClient(Plugin plugin) => GetClient(plugin?.Name);

        /// <summary>
        /// Gets the client for the given plugin name
        /// </summary>
        /// <param name="pluginName">Plugin Name to get client for</param>
        /// <returns>Discord client for the plugin name</returns>
        public static DiscordClient GetClient(string pluginName)
        {
            return Clients[pluginName];
        }
        
        internal static void OnPluginAdded(Plugin plugin)
        {
            foreach (FieldInfo field in plugin.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (field.GetCustomAttributes(typeof(DiscordClientAttribute), true).Length != 0)
                {
                    DiscordClient client = Clients[plugin.Name];
                    if (client == null)
                    {
                        DiscordExtension.GlobalLogger.Debug($"{nameof(DiscordClient)}.{nameof(OnPluginAdded)} Creating DiscordClient for plugin {plugin.Name}");
                        client = new DiscordClient(plugin);
                        Clients[plugin.Name] = client;
                    }
                    
                    field.SetValue(plugin, client);
                    break;
                }
            }
            
            DiscordExtension.DiscordCommand.ProcessPluginCommands(plugin);
        }

        internal static void OnPluginRemoved(Plugin plugin)
        {
            DiscordClient client = Clients[plugin.Name];
            if (client?.Settings == null)
            {
                return;
            }

            if (!client.Settings.CloseOnUnload)
            {
                return;
            }
            
            CloseClient(client);
        }

        internal static void CloseClient(DiscordClient client)
        {
            if (client != null)
            {
                client.Disconnect();
                DiscordExtension.GlobalLogger.Debug($"{nameof(DiscordClient)}.{nameof(CloseClient)} Closing DiscordClient for plugin {client.Owner.Name}");
                foreach (FieldInfo field in client.Owner.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    if (field.GetCustomAttributes(typeof(DiscordClientAttribute), true).Length != 0)
                    {
                        field.SetValue(client.Owner, null);
                        break;
                    }
                }

                Clients.Remove(client.Owner.Name);
            }
        }
        #endregion
    }
}
