using System;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    internal class InternalAsyncCallback<T> : BaseAsyncCallback<T>
    {
        internal static InternalAsyncCallback<T> Create()
        {
            return DiscordPool.Get<InternalAsyncCallback<T>>();
        }

        public IDiscordAsyncCallback<T> OnSuccess(Action<T> complete)
        {
            Success.Add(complete);
            return this;
        }

        public override void InvokeSuccess(T data)
        {
            Data = data;
            InvokeSuccessInternal();
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}