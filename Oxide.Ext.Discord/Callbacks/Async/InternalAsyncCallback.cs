using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    internal class InternalAsyncCallback : BaseDiscordAsyncCallback
    {
        internal static InternalAsyncCallback Create()
        {
            return DiscordPool.Get<InternalAsyncCallback>();
        }

        public override void InvokeSuccess()
        {
            InvokeSuccessInternal();
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}