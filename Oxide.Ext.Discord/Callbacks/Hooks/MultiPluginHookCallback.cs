using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Hooks
{
    internal class MultiPluginHookCallback : BaseHookCallback
    {
        private List<Plugin> _plugins;

        public static void Start(List<Plugin> plugins, string hook, object[] args)
        {
            MultiPluginHookCallback callback = DiscordPool.Get<MultiPluginHookCallback>();
            callback.Init(hook, args);
            callback._plugins = plugins;
            callback.Run();
        }

        protected override void HandleCallback()
        {
            for (int index = 0; index < _plugins.Count; index++)
            {
                Plugin plugin = _plugins[index];
                plugin.CallHook(Hook, Args);
            }
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            _plugins = null;
        }
    }
}