using System;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Rest
{
    internal class ApiErrorCallback : BaseApiCallback
    {
        private Action<RestError> _onError;
        private RestError _error;

        public void Init(Request request, Action<RestError> onError, RestError error, DiscordClient client)
        {
            base.Init(request, client);
            _onError = onError;
            _error = error;
        }

        protected override void HandleCallback()
        {
            if (!Client.IsConnected())
            {
                return;
            }

            if (_onError == null)
            {
                _error.LogError();
                return;
            }

            try
            {
                _onError.Invoke(_error);
            }
            catch (Exception ex)
            {
                Client.Logger.Exception("An exception occured during Error callback for request: [{0}] {1}", Request.Method, Request.RequestUrl, ex);
            }
            finally
            {
                _error.LogError();
            }
        }

        protected override void HandleCleanup()
        {
            DiscordPool.Free(ref Request);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            _onError = null;
            _error = null;
        }
    }
}