using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Hooks
{
    internal class DiscordHookCache
    {
        private readonly Hash<string, List<Plugin>> _hookCache = new Hash<string, List<Plugin>>();
        private readonly ILogger _logger;

        internal DiscordHookCache(ILogger logger) 
        {
            _logger = logger;
        }

        internal void AddPlugin(Plugin plugin)
        {
            Type pluginType = plugin.GetType();
            foreach (MethodInfo method in pluginType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (method.DeclaringType != pluginType)
                {
                    continue;
                }

                string name = method.Name;
                if (method.IsPublic)
                {
                    object[] attributes = method.GetCustomAttributes(typeof(HookMethodAttribute), false);
                    if (attributes.Length == 0)
                    {
                        continue;
                    }
                    
                    name = ((HookMethodAttribute)attributes[0]).Name;
                }

                SubscribeHook(plugin, name);
            }
        }
        
        internal void SubscribeHook(Plugin plugin, string hook)
        {
            if (!DiscordExtHooks.AllHooks.Contains(hook))
            {
                return;
            }
                
            List<Plugin> hooks = _hookCache[hook];
            if (hooks == null)
            {
                hooks = new List<Plugin>();
                _hookCache[hook] = hooks;
            }

            if (!hooks.Contains(plugin))
            {
                hooks.Add(plugin);
            }
        }

        internal void RemovePlugin(Plugin plugin)
        {
            List<string> hooksToRemove = DiscordPool.GetList<string>();
            foreach (KeyValuePair<string, List<Plugin>> cache in _hookCache)
            {
                if (cache.Value.Remove(plugin) && cache.Value.Count == 0)
                {
                    hooksToRemove.Add(cache.Key);
                }
            }

            for (int index = 0; index < hooksToRemove.Count; index++)
            {
                string hook = hooksToRemove[index];
                _hookCache.Remove(hook);
            }
            
            DiscordPool.FreeList(ref hooksToRemove);
        }
        
        internal void UnsubscribeHook(Plugin plugin, string hook)
        {
            if (!TryGetHook(hook, out List<Plugin> plugins) || !plugins.Remove(plugin))
            {
                return;
            }

            if (plugins.Count == 0)
            {
                _hookCache.Remove(hook);
            }
        }

        internal bool TryGetHook(string hook, out List<Plugin> plugins)
        {
            return _hookCache.TryGetValue(hook, out plugins);
        }
    }
}