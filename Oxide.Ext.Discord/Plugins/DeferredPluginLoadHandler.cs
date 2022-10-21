using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Plugins
{
    internal class DeferredPluginLoadHandler
    {
        private readonly List<Plugin> _queue;
        private readonly object _sync = new object();

        public DeferredPluginLoadHandler()
        {
            _queue = new List<Plugin>(DiscordPluginCache.Instance.GetLoadablePlugins().Count);
            Task.Run(LoadPlugins);
        }

        public void AddPlugin(Plugin plugin)
        {
            DiscordExtension.GlobalLogger.Debug("Adding plugin for deferred load: {0}", plugin.Id());
            lock (_sync)
            {
                _queue.RemoveAll(p => !p.IsLoaded || p.Id() == plugin.Id());
                _queue.Add(plugin);
            }
        }
        
        public async Task LoadPlugins()
        {
            try
            {
                while (CanLoadPlugins())
                {
                    lock (_sync)
                    {
                        if (_queue.Count != 0)
                        {
                            Plugin plugin = _queue[0];
                            DiscordExtension.GlobalLogger.Debug("Deferred loading plugin: {0}", plugin.Id());
                            DiscordClient.OnPluginLoadedInternal(plugin);
                            _queue.RemoveAt(0);
                            continue;
                        }
                    }
                    
                    await Task.Delay(250).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("An error occured during deferred plugin load.", ex);
            }
            finally
            {
                DiscordClient.OnDeferredLoadedCompleted();
            }
        }
        
        private bool CanLoadPlugins()
        {
            lock (_sync)
            {
                DiscordExtensionCore core = DiscordExtensionCore.Instance;
                return  core == null || !core.IsServerLoaded || _queue.Count != 0;
            }
        }
    }
}