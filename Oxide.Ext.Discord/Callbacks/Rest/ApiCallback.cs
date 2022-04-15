using System;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Rest
{
    internal class ApiCallback : BaseApiCallback
    {
        private Action _onSuccess;

        public void Init(Request request, Action onCompleted, DiscordClient client)
        {
            base.Init(request, client);
            _onSuccess = onCompleted;
        }

        protected override void HandleCallback()
        {
            if (!Client.IsConnected())
            {
                return;
            }

            try
            {
                _onSuccess.Invoke();
            }
            catch (Exception ex)
            {
                Client.Logger.Exception("An exception occured during Success callback for request: [{0}] {1}", Request.Method, Request.RequestUrl, ex);
            }
        }

        protected override void HandleCleanup()
        {
            DiscordPool.Free(ref Request);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            _onSuccess = null;
        }
    }
}