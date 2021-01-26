using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Attributes;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord
{
    public class DiscordClient
    {
        internal static Hash<string, DiscordClient> Clients { get; } = new Hash<string, DiscordClient>();
        
        public Plugin Owner { get; }
        public List<Plugin> RegisteredForHooks { get; } = new List<Plugin>();
        public BotClient Bot { get; private set; }
        public DiscordSettings Settings { get; private set; } = new DiscordSettings();
        
        private ILogger _logger;

        public DiscordClient(Plugin plugin)
        {
            Owner = plugin;
        }
        
        public void Connect(string apiKey, BotIntents intents)
        {
            DiscordSettings settings = new DiscordSettings
            {
                ApiToken = apiKey,
                LogLevel = LogLevel.Warning,
                Intents = intents
            };
            
            Connect(settings);
        }
        
        public void Connect(DiscordSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = new Logger(settings.LogLevel);
            
            if (string.IsNullOrEmpty(Settings.ApiToken))
            {
                _logger.Error("API Token is null or empty!");
                return;
            }

            if (!string.IsNullOrEmpty(DiscordExtension.TestVersion))
            {
                _logger.Warning($"Using Discord Test Version: {DiscordExtension.GetExtensionVersion}");
            }
            
            _logger.Debug($"{nameof(DiscordClient)}.{nameof(Connect)} GetOrCreate bot for {Owner.Name}");

            Bot = BotClient.GetOrCreate(this);

            RegisterPluginForHooks(Owner);
            Interface.Call("Discord_ClientConnect", Owner, this);
        }
        
        public void Disconnect()
        {
            Interface.Call("Discord_ClientDisconnect", Owner, this);
            Bot?.RemoveClient(this);
        }

        public void RegisterPluginForHooks(Plugin plugin)
        {
            RegisteredForHooks.RemoveAll(p => p.Name == plugin.Name);
            RegisteredForHooks.Add(plugin);
        }

        public void CloseClient()
        {
            Bot.RemoveClient(this);
        }

        public void CallHook(string hookName, params object[] args)
        {
            //Run from next tick so we can be sure it's ran on the main thread.
            Interface.Oxide.NextTick(() =>
            {
                foreach (Plugin plugin in RegisteredForHooks)
                {
                    plugin.CallHook(hookName, args);
                }
            });
        }

        #region Plugin Handling
        public static DiscordClient GetClient(Plugin plugin) => GetClient(plugin?.Name);

        public static DiscordClient GetClient(string pluginName)
        {
            return Clients[pluginName];
        }
        
        internal static void OnPluginAdded(Plugin plugin)
        {
            foreach (FieldInfo field in plugin.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                if (field.GetCustomAttributes(typeof(DiscordClientAttribute), true).Any())
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
                    if (field.GetCustomAttributes(typeof(DiscordClientAttribute), true).Any())
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
