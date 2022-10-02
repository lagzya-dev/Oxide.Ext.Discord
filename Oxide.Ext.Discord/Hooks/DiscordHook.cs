using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Hooks;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Hooks
{
    internal class DiscordHook
    {
        private readonly DiscordHookCache _cache;
        private readonly ILogger _logger;

        internal DiscordHook(ILogger logger) 
        {
            _logger = logger;
            _cache = new DiscordHookCache(logger);
        }

        #region Plugin Handling
        internal void AddPlugin(DiscordClient client)
        {
            _cache.AddPlugin(client);
        }

        internal void RemovePlugin(Plugin plugin)
        {
            _cache.RemovePlugin(plugin);
        }

        internal void SubscribeHook(DiscordClient client, string hook)
        {
            _cache.SubscribeHook(client, hook);
        }
        
        internal void UnsubscribeHook(Plugin plugin, string hook)
        {
            _cache.UnsubscribeHook(plugin, hook);
        }
        #endregion

        #region Hooks
        internal void CallHook(string hookName)
        {
            if (!CanCallHook(hookName, out List<Plugin> plugins))
            {
                return;
            }
            
            object[] args = ArrayPool.Get(0);
            CallHookInternal(plugins, hookName, args);
        }

        internal void CallHook<T0>(string hookName, T0 arg0)
        {
            if (!CanCallHook(hookName, out List<Plugin> plugins))
            {
                return;
            }
            
            object[] args = ArrayPool.Get(1);
            args[0] = arg0;
            CallHookInternal(plugins, hookName, args);
        }
        
        internal void CallHook<T0, T1>(string hookName, T0 arg0, T1 arg1)
        {
            if (!CanCallHook(hookName, out List<Plugin> plugins))
            {
                return;
            }
            
            object[] args = ArrayPool.Get(2);
            args[0] = arg0;
            args[1] = arg1;
            CallHookInternal(plugins, hookName, args);
        }
        
        internal void CallHook<T0, T1, T2>(string hookName, T0 arg0, T1 arg1, T2 arg2)
        {
            if (!CanCallHook(hookName, out List<Plugin> plugins))
            {
                return;
            }
            
            object[] args = ArrayPool.Get(3);
            args[0] = arg0;
            args[1] = arg1;
            args[2] = arg2;
            CallHookInternal(plugins, hookName, args);
        }
        
        internal void CallHook<T0, T1, T2, T3>(string hookName, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            if (!CanCallHook(hookName, out List<Plugin> plugins))
            {
                return;
            }
            
            object[] args = ArrayPool.Get(4);
            args[0] = arg0;
            args[1] = arg1;
            args[2] = arg2;
            args[3] = arg3;
            CallHookInternal(plugins, hookName, args);
        }
        
        internal void CallHook<T0, T1, T2, T3, T4>(string hookName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!CanCallHook(hookName, out List<Plugin> plugins))
            {
                return;
            }
            
            object[] args = ArrayPool.Get(5);
            args[0] = arg0;
            args[1] = arg1;
            args[2] = arg2;
            args[3] = arg3;
            args[4] = arg4;
            CallHookInternal(plugins, hookName, args);
        }
        #endregion

        #region Static Hooks
        internal static void CallGlobalHook(string name)
        {
            object[] args = ArrayPool.Get(0);
            Interface.Oxide.CallHook(name, args);
            ArrayPool.Free(args);
        }
        
        internal static void CallGlobalHook<T0>(string name, T0 arg0)
        {
            object[] args = ArrayPool.Get(1);
            args[0] = arg0;
            Interface.Oxide.CallHook(name, args);
            ArrayPool.Free(args);
        }
        
        internal static void CallGlobalHook<T0, T1>(string name, T0 arg0, T1 arg1)
        {
            object[] args = ArrayPool.Get(2);
            args[0] = arg0;
            args[1] = arg1;
            Interface.Oxide.CallHook(name, args);
            ArrayPool.Free(args);
        }
        
        internal static void CallGlobalHook<T0, T1, T2>(string name, T0 arg0, T1 arg1, T2 arg2)
        {
            object[] args = ArrayPool.Get(3);
            args[0] = arg0;
            args[1] = arg1;
            args[2] = arg2;
            Interface.Oxide.CallHook(name, args);
            ArrayPool.Free(args);
        }
        
        internal static void CallPluginHook(Plugin plugin, string name)
        {
            object[] args = ArrayPool.Get(0);
            CallHookInternal(plugin, name, args);
        }
        
        internal static void CallPluginHook<T0>(Plugin plugin, string name, T0 arg0)
        {
            object[] args = ArrayPool.Get(1);
            args[0] = arg0;
            CallHookInternal(plugin, name, args);
        }
        
        internal static void CallPluginHook<T0, T1>(Plugin plugin, string name, T0 arg0, T1 arg1)
        {
            object[] args = ArrayPool.Get(2);
            args[0] = arg0;
            args[1] = arg1;
            CallHookInternal(plugin, name, args);
        }
        
        internal static void CallPluginHook<T0, T1, T2>(Plugin plugin, string name, T0 arg0, T1 arg1, T2 arg2)
        {
            object[] args = ArrayPool.Get(3);
            args[0] = arg0;
            args[1] = arg1;
            args[2] = arg2;
            CallHookInternal(plugin, name, args);
        }
        
        internal static void CallPluginHook<T0, T1, T2, T3>(Plugin plugin, string name, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            object[] args = ArrayPool.Get(4);
            args[0] = arg0;
            args[1] = arg1;
            args[2] = arg2;
            args[3] = arg3;
            CallHookInternal(plugin, name, args);
        }
        #endregion

        #region Internal Handling
        private bool CanCallHook(string hookName, out List<Plugin> plugins)
        {
            _logger.Verbose("Hook Called: {0}", hookName);
            return _cache.TryGetHook(hookName, out plugins) && plugins.Count != 0;
        }
        
        private static void CallHookInternal(Plugin plugin, string hookName, object[] args)
        {
            PluginHookCallback.Start(plugin, hookName, args);
        }
        
        private static void CallHookInternal(List<Plugin> plugins, string hookName, object[] args)
        {
            PluginHookCallback.Start(plugins, hookName, args);
        }
        #endregion
    }
}