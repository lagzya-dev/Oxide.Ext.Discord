using System;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal class ApiSuccessCallback : BaseApiCallback
    {
        private Action _onSuccess;

        public static void Start(Request request)
        {
            ApiSuccessCallback callback = DiscordPool.Get<ApiSuccessCallback>();
            callback.Init(request);
            callback._onSuccess = request.OnSuccess;
            callback.Run();
        }

        protected override void HandleApiCallback()
        {
            _onSuccess.Invoke();
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            _onSuccess = null;
        }
    }
}