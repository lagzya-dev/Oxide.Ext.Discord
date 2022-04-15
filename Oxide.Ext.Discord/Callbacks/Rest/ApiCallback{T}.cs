using System;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Rest
{
    internal class ApiCallback<T> : BaseApiCallback
    {
        private Action<T> _onSuccess;
        private T _data;

        public void Init(Request request, Action<T> onSuccess, T data, DiscordClient client)
        {
            base.Init(request, client);
            _onSuccess = onSuccess;
            _data = data;
        }

        protected override void HandleCallback()
        {
            if (!Client.IsConnected())
            {
                return;
            }

            try
            {
                _onSuccess.Invoke(_data);
            }
            catch (Exception ex)
            {
                Client.Logger.Exception("An exception occured during _onSuccess callback for request: [{0}] {1}", Request.Method, Request.RequestUrl, ex);
            }
            finally
            {
                DiscordPool.Free(this);
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
            _data = default(T);
        }
    }
}