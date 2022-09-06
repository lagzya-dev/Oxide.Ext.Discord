using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Hooks
{
    internal class SinglePluginHookCallback : BaseHookCallback
    {
        private Plugin _plugin;
        
        public static void Start(Plugin plugin, string hook, object[] args)
        {
            SinglePluginHookCallback callback = DiscordPool.Get<SinglePluginHookCallback>();
            callback.Init(plugin, hook, args);
            callback.Run();
        }

        private void Init(Plugin plugin, string hook, object[] args)
        {
            Init(hook, args);
            _plugin = plugin;
        }

        protected override void HandleCallback()
        {
            _plugin.CallHook(Hook, Args);
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        protected override void EnterPool()
        {
            base.EnterPool();
            _plugin = null;
        }
    }
}