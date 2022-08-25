using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Hooks
{
    internal class SinglePluginHookCallback : BaseHookCallback
    {
        private Plugin _plugin;
        
        public static SinglePluginHookCallback CreateCallback(Plugin plugin, string hook, object[] args)
        {
            SinglePluginHookCallback callback = DiscordPool.Get<SinglePluginHookCallback>();
            callback.Init(plugin, hook, args);
            return callback;
        }
        
        private void Init(Plugin plugin, string hook, object[] args)
        {
            base.Init(hook, args);
            _plugin = plugin;
        }
        
        protected override void HandleCallback()
        {
            _plugin.CallHook(Hook, Args);
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            DiscordPool.Free(this);
        }
        
        protected override void EnterPool()
        {
            base.EnterPool();
            _plugin = null;
        }
    }
}