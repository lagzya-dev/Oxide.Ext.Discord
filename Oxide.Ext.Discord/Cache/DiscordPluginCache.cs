using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Singleton;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// Represents a cache for Loaded and Loadable plugins
    /// </summary>
    public sealed class DiscordPluginCache : Singleton<DiscordPluginCache>
    {
        private readonly CSharpPluginLoader _pluginLoader = Interface.Oxide.GetPluginLoaders().OfType<CSharpPluginLoader>().FirstOrDefault();

        private readonly List<string> _loadablePlugins = new List<string>();
        private readonly List<string> _loadedPlugins = new List<string>();
        private DateTime _nextUpdate = DateTime.MinValue;

        private DiscordPluginCache() {}
        
        /// <summary>
        /// Returns a list of plugins loaded by oxide
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<string> GetLoadedPlugins()
        {
            if (_loadedPlugins.Count != 0)
            {
                return _loadedPlugins;
            }

            _loadedPlugins.AddRange(Interface.Oxide.RootPluginManager.GetPlugins().Select(p => p.Name).OrderBy(p => p));
            return _loadedPlugins;
        }
        
        /// <summary>
        /// Returns a list of plugins that can be loaded by oxide
        /// Already loaded plugins are excluded from the list
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<string> GetLoadablePlugins()
        {
            if (_nextUpdate < DateTime.UtcNow)
            {
                return _loadablePlugins;
            }
            
            _loadablePlugins.Clear();
            _loadablePlugins.AddRange(_pluginLoader.ScanDirectory(Interface.Oxide.PluginDirectory).Except(GetLoadedPlugins()).OrderBy(p => p));
            _nextUpdate = DateTime.UtcNow + TimeSpan.FromMinutes(1);
            return _loadablePlugins;
        }

        internal void OnPluginLoaded(Plugin plugin)
        {
            _loadablePlugins.Remove(plugin.Name);
            if (!_loadedPlugins.Contains(plugin.Name))
            {
                _loadedPlugins.Add(plugin.Name);
            }
        }

        internal void OnPluginUnloaded(Plugin plugin)
        {
            _loadedPlugins.Remove(plugin.Name);
        }
    }
}