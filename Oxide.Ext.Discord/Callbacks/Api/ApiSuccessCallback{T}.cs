using System;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal class ApiSuccessCallback<T> : BaseApiCallback
    {
        private Action<T> _onSuccess;
        private T _data;

        public static void Start(Request<T> request, T data)
        {
            ApiSuccessCallback<T> callback = DiscordPool.Get<ApiSuccessCallback<T>>();
            callback.Init(request, data);
            callback.Run();
        }

        private void Init(Request<T> request, T data)
        {
            Init(request);
            _onSuccess = request.OnSuccess;
            _data = data;
        }

        protected override void HandleApiCallback()
        {
            _onSuccess.Invoke(_data);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            _data = default(T);
            _onSuccess = null;
        }
    }
}