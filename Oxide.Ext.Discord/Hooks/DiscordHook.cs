using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Hooks;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

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
        internal void AddPlugin(Plugin plugin)
        {
            _cache.AddPlugin(plugin);
        }

        internal void RemovePlugin(Plugin plugin)
        {
            _cache.RemovePlugin(plugin);
        }

        internal void SubscribeHook(Plugin plugin, string hook)
        {
            _cache.SubscribeHook(plugin, hook);
        }
        
        internal void UnsubscribeHook(Plugin plugin, string hook)
        {
            _cache.UnsubscribeHook(plugin, hook);
        }
        #endregion

        #region Hooks
        internal void CallHook(string hookName)
        {
            if (!_cache.TryGetHook(hookName, out List<Plugin> plugins) || plugins.Count == 0)
            {
                return;
            }
            
            object[] args = ArrayPool.Get(0);
            CallHookInternal(plugins, hookName, args);
        }
        
        internal void CallHook(string hookName, object arg0)
        {
            if (!_cache.TryGetHook(hookName, out List<Plugin> plugins) || plugins.Count == 0)
            {
                return;
            }
            
            object[] args = ArrayPool.Get(1);
            args[0] = arg0;
            CallHookInternal(plugins, hookName, args);
        }
        
        internal void CallHook(string hookName, object arg0, object arg1)
        {
            if (!_cache.TryGetHook(hookName, out List<Plugin> plugins) || plugins.Count == 0)
            {
                return;
            }
            
            object[] args = ArrayPool.Get(2);
            args[0] = arg0;
            args[1] = arg1;
            CallHookInternal(plugins, hookName, args);
        }
        
        internal void CallHook(string hookName, object arg0, object arg1, object arg2)
        {
            if (!_cache.TryGetHook(hookName, out List<Plugin> plugins) || plugins.Count == 0)
            {
                return;
            }
            
            object[] args = ArrayPool.Get(3);
            args[0] = arg0;
            args[1] = arg1;
            args[2] = arg2;
            CallHookInternal(plugins, hookName, args);
        }
        
        internal void CallHook(string hookName, object arg0, object arg1, object arg2, object arg3)
        {
            if (!_cache.TryGetHook(hookName, out List<Plugin> plugins) || plugins.Count == 0)
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
        #endregion

        #region Static Hooks
        internal static void CallGlobalHook(string name)
        {
            object[] args = ArrayPool.Get(0);
            Interface.Oxide.CallHook(name, args);
            ArrayPool.Free(args);
        }
        
        internal static void CallGlobalHook(string name, object arg0)
        {
            object[] args = ArrayPool.Get(1);
            args[0] = arg0;
            Interface.Oxide.CallHook(name, args);
            ArrayPool.Free(args);
        }
        
        internal static void CallGlobalHook(string name, object arg0, object arg1)
        {
            object[] args = ArrayPool.Get(2);
            args[0] = arg0;
            args[1] = arg1;
            Interface.Oxide.CallHook(name, args);
            ArrayPool.Free(args);
        }
        
        internal static void CallGlobalHook(string name, object arg0, object arg1, object arg2)
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
        
        internal static void CallPluginHook(Plugin plugin, string name, object arg0)
        {
            object[] args = ArrayPool.Get(1);
            args[0] = arg0;
            CallHookInternal(plugin, name, args);
        }
        
        internal static void CallPluginHook(Plugin plugin, string name, object arg0, object arg1)
        {
            object[] args = ArrayPool.Get(2);
            args[0] = arg0;
            args[1] = arg1;
            CallHookInternal(plugin, name, args);
        }
        
        internal static void CallPluginHook(Plugin plugin, string name, object arg0, object arg1, object arg2)
        {
            object[] args = ArrayPool.Get(3);
            args[0] = arg0;
            args[1] = arg1;
            args[2] = arg2;
            CallHookInternal(plugin, name, args);
        }
        
        internal static void CallPluginHook(Plugin plugin, string name, object arg0, object arg1, object arg2, object arg3)
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
        private static void CallHookInternal(Plugin plugin, string hookName, object[] args)
        {
            SinglePluginHookCallback call = DiscordPool.Get<SinglePluginHookCallback>();
            call.Init(plugin, hookName, args);
            Interface.Oxide.NextTick(call.Callback);
        }
        
        private static void CallHookInternal(List<Plugin> plugins, string hookName, object[] args)
        {
            MultiPluginHookCallback call = DiscordPool.Get<MultiPluginHookCallback>();
            call.Init(plugins, hookName, args);
            Interface.Oxide.NextTick(call.Callback);
        }
        #endregion
    }
}