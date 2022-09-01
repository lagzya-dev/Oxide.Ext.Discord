using System;
using Oxide.Core;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    public class DiscordAsyncCallback<T> : BaseAsyncCallback<T>
    {
        private readonly Action _successCallback;
        
        public DiscordAsyncCallback()
        {
            _successCallback = InvokeSuccessInternal;
        }
        
        internal static DiscordAsyncCallback<T> Create()
        {
            return DiscordPool.Get<DiscordAsyncCallback<T>>();
        }

        public override void InvokeSuccess(T data)
        {
            Data = data;
            Interface.Oxide.NextTick(_successCallback);
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}