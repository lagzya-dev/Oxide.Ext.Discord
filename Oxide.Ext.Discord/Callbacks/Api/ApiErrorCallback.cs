using System;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal class ApiErrorCallback : BaseApiCallback
    {
        private Action<RequestError> _onError;
        private RequestError _error;

        public static void Start(BaseRequest request, RequestResponse response)
        {
            ApiErrorCallback callback = DiscordPool.Get<ApiErrorCallback>();
            callback.Init(request, response);
            callback.Run();
        }

        private void Init(BaseRequest request, RequestResponse response)
        {
            Init(request);
            _onError = request.OnError;
            _error = response.Error;
        }

        protected override void HandleApiCallback()
        {
            _onError?.Invoke(_error);
        }

        protected override void OnCallbackCompleted()
        {
            _error.LogError();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            _onError = null;
            _error = null;
        }
    }
}