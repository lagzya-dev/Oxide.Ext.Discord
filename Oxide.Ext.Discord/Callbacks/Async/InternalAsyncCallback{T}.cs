using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    internal class InternalAsyncCallback<T> : BaseDiscordAsyncCallback<T>
    {
        internal static InternalAsyncCallback<T> Create()
        {
            return DiscordPool.Get<InternalAsyncCallback<T>>();
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