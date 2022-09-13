using System;
using Oxide.Core;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    public class DiscordAsyncCallback : BaseDiscordAsyncCallback
    {
        private readonly Action _successCallback;
        
        public DiscordAsyncCallback()
        {
            _successCallback = InvokeSuccessInternal;
        }
        
        internal static DiscordAsyncCallback Create()
        {
            return DiscordPool.Get<DiscordAsyncCallback>();
        }

        public override void InvokeSuccess()
        {
            Interface.Oxide.NextTick(_successCallback);
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}