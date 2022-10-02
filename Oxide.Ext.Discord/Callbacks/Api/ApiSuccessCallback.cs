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
            callback.Run();
        }

        private void Init(Request request)
        {
            Init(request);
            _onSuccess = request.OnSuccess;
        }

        protected override void HandleApiCallback()
        {
            _onSuccess.Invoke();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            _onSuccess = null;
        }
    }
}