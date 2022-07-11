using System;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal class ApiSuccessCallback : BaseApiCallback
    {
        private Action _onSuccess;
        
        public void Init(Request request)
        {
            base.Init(request);
            _onSuccess = request.OnSuccess;
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