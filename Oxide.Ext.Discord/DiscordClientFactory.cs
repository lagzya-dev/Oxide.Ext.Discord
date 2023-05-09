using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord
{
    internal static class DiscordClientFactory
    {
        private static readonly Hash<string, DiscordClient> Clients = new Hash<string, DiscordClient>();
        internal static IEnumerable<DiscordClient> GetClients => Clients.Values;

        #region Plugin Handling
        /// <summary>
        /// Sets the client field on the plugin.
        /// This should only be used if you need the client in the Init or Loaded hooks
        /// The client field will automatically be set on the plugin before the OnDiscordClientCreated or OnServerInitialized hooks
        /// </summary>
        /// <param name="plugin">Plugin to get client for</param>
        /// <returns>Discord client for the plugin</returns>
        public static DiscordClient CreateClient(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            DiscordPluginCache.Instance.OnPluginLoaded(plugin);
            OnPluginRemoved(plugin);

            // ReSharper disable once SuspiciousTypeConversion.Global
            if (!(plugin is IDiscordPlugin discordPlugin))
            {
                return null;
            }

            DiscordClient client = Clients[plugin.Id()];
            if (client == null)
            {
                DiscordExtension.GlobalLogger.Debug($"{nameof(DiscordClient)}.{nameof(OnPluginAdded)} Creating DiscordClient for plugin {{0}}", plugin.FullName());
                client = new DiscordClient(plugin);
                Clients[plugin.Id()] = client;
            }

            discordPlugin.Client = client;
            return client;
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
                CreateClient(plugin);
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
                client.CloseClient();
                BaseDiscordLibrary.ProcessPluginUnloaded(plugin);
            }

            PluginExt.OnPluginUnloaded(plugin);
            DiscordPluginCache.Instance.OnPluginUnloaded(plugin);
            DiscordLoggerFactory.Instance.OnPluginUnloaded(plugin);
        }

        internal static void RemoveClient(DiscordClient client)
        {
            Clients.Remove(client.PluginId);
        }
        #endregion
    }
}