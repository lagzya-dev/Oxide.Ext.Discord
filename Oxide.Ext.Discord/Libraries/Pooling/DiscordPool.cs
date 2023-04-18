using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Pooling
{
    /// <summary>
    /// Discord Pool Library
    /// </summary>
    public class DiscordPool : BaseDiscordLibrary<DiscordPool>
    {
        private readonly Hash<Plugin, DiscordPluginPool> _pluginPools = new Hash<Plugin, DiscordPluginPool>();
        internal static DiscordPluginPool Internal;
        private readonly ILogger _logger;
        
        internal DiscordPool(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns an existing <see cref="DiscordPluginPool"/> for the given plugin or returns a new one
        /// </summary>
        /// <param name="plugin">The pool the plugin is for</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the plugin is null</exception>
        public DiscordPluginPool GetOrCreate(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            return CreatePoolInternal(plugin);
        }
        
        internal void CreateInternal(Plugin plugin)
        {
            Internal = CreatePoolInternal(plugin);
            Internal.SetSettings(new PoolSettings
            {
                HashPoolSize = 256,
                HashSetPoolSize = 128,
                MemoryStreamPoolSize = 256,
                PlaceholderDataPoolSize = 256,
                ListPoolSize = 128,
                ObjectPoolSize = 256,
                StringBuilderPoolSize = 128
            });
        }
        
        private DiscordPluginPool CreatePoolInternal(Plugin plugin)
        {
            DiscordPluginPool pool = _pluginPools[plugin];
            if (pool == null)
            {
                pool = new DiscordPluginPool(plugin);
                _pluginPools[plugin] = pool;
            }

            return pool;
        }
        
        ///<inheritdoc/>
        protected override void OnPluginLoaded(Plugin plugin) { }

        ///<inheritdoc/>
        protected override void OnPluginUnloaded(Plugin plugin)
        {
            DiscordPluginPool pool = _pluginPools[plugin];
            if (pool != null)
            {
                pool.OnPluginUnloaded(plugin);
                _pluginPools.Remove(plugin);
            }
        }

        internal void Clear()
        {
            foreach (DiscordPluginPool pool in _pluginPools.Values)
            {
                pool.Clear();
            }
        }
        
        internal void Wipe()
        {
            foreach (DiscordPluginPool pool in _pluginPools.Values)
            {
                pool.Wipe();
            }
        }
    }
}