using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Cache
{
    public static class DiscordPluginCache
    {
        private static readonly CSharpPluginLoader PluginLoader = Interface.Oxide.GetPluginLoaders().OfType<CSharpPluginLoader>().FirstOrDefault();

        private static readonly List<string> LoadablePlugins = new List<string>();
        private static readonly List<string> LoadedPlugins = new List<string>();
        private static DateTime _lastUpdated = DateTime.MinValue;

        public static IReadOnlyList<string> GetLoadedPlugins()
        {
            if (LoadedPlugins.Count != 0)
            {
                return LoadedPlugins;
            }

            LoadedPlugins.AddRange(Interface.Oxide.RootPluginManager.GetPlugins().Select(p => p.Name).OrderBy(p => p));
            return LoadedPlugins;
        }
        
        public static IReadOnlyList<string> GetLoadablePlugins()
        {
            if (_lastUpdated + TimeSpan.FromMinutes(1) < DateTime.UtcNow)
            {
                return LoadablePlugins;
            }
            
            LoadablePlugins.Clear();
            LoadablePlugins.AddRange(PluginLoader.ScanDirectory(Interface.Oxide.PluginDirectory).Except(GetLoadedPlugins()).OrderBy(p => p));
            _lastUpdated = DateTime.UtcNow;
            return LoadablePlugins;
        }

        internal static void OnPluginLoaded(Plugin plugin)
        {
            LoadedPlugins.Clear();
        }

        internal static void OnPluginUnloaded(Plugin plugin)
        {
            LoadedPlugins.Remove(plugin.Name);
        }
    }
}