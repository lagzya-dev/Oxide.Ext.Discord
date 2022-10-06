using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Singleton;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Cache
{
    public class DiscordPluginCache : Singleton<DiscordPluginCache>
    {
        private readonly CSharpPluginLoader _pluginLoader = Interface.Oxide.GetPluginLoaders().OfType<CSharpPluginLoader>().FirstOrDefault();

        private readonly List<string> _loadablePlugins = new List<string>();
        private readonly List<string> _loadedPlugins = new List<string>();
        private DateTime _lastUpdated = DateTime.MinValue;

        public IReadOnlyList<string> GetLoadedPlugins()
        {
            if (_loadedPlugins.Count != 0)
            {
                return _loadedPlugins;
            }

            _loadedPlugins.AddRange(Interface.Oxide.RootPluginManager.GetPlugins().Select(p => p.Name).OrderBy(p => p));
            return _loadedPlugins;
        }
        
        public IReadOnlyList<string> GetLoadablePlugins()
        {
            if (_lastUpdated + TimeSpan.FromMinutes(1) < DateTime.UtcNow)
            {
                return _loadablePlugins;
            }
            
            _loadablePlugins.Clear();
            _loadablePlugins.AddRange(_pluginLoader.ScanDirectory(Interface.Oxide.PluginDirectory).Except(GetLoadedPlugins()).OrderBy(p => p));
            _lastUpdated = DateTime.UtcNow;
            return _loadablePlugins;
        }

        internal void OnPluginLoaded(Plugin plugin)
        {
            _loadedPlugins.Clear();
        }

        internal void OnPluginUnloaded(Plugin plugin)
        {
            _loadedPlugins.Remove(plugin.Name);
        }
    }
}