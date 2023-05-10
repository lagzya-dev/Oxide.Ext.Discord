using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Pooling.Pools;

namespace Oxide.Ext.Discord.Callbacks.Hooks
{
    internal class PluginHookCallback : BaseNextTickCallback
    {
        private string _hook;
        private object[] _args;
        private Plugin _plugin;
        private List<Plugin> _plugins;

        public static void Start(Plugin plugin, string hook, object[] args)
        {
            PluginHookCallback callback = DiscordPool.Internal.Get<PluginHookCallback>();
            callback.Init(plugin, hook, args);
            callback.Run();
        }
        
        public static void Start(List<Plugin> plugins, string hook, object[] args)
        {
            PluginHookCallback callback = DiscordPool.Internal.Get<PluginHookCallback>();
            callback.Init(plugins, hook, args);
            callback.Run();
        }
        
        private void Init(Plugin plugin, string hook, object[] args)
        {
            Init(hook, args);
            _plugin = plugin;
        }
        
        private void Init(List<Plugin> plugins, string hook, object[] args)
        {
            Init(hook, args);
            _plugins = plugins;
        }
        
        private void Init(string hook, object[] args)
        {
            _hook = hook;
            _args = args;
        }

        protected override void HandleCallback()
        {
            if (_plugin != null && _plugin.IsLoaded)
            {
                _plugin.CallHook(_hook, _args);
                return;
            }
            
            for (int index = 0; index < _plugins.Count; index++)
            {
                Plugin plugin = _plugins[index];
                if (plugin.IsLoaded)
                {
                    plugin.CallHook(_hook, _args);
                }
            }
            
            ArrayPool<object>.Instance.Free(ref _args);
        }

        protected override void EnterPool()
        {
            _hook = null;
            _args = null;
            _plugin = null;
            _plugins = null;
        }
    }
}