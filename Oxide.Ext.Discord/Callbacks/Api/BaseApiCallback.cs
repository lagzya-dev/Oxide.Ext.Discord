using System;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal abstract class BaseApiCallback<T> : BaseCallback where T : BaseRequest
    {
        protected DiscordClient Client;
        protected T Request;
        private RequestResponse _response;

        public virtual void Init(T request, RequestResponse response)
        {
            Client = request.Client;
            Request = request;
            _response = response;
        }

        protected sealed override void HandleCallback()
        {
            if (!Client.IsConnected())
            {
                return;
            }

            if (_response.Status == RequestCompletedStatus.Success)
            {
                OnSuccess();
                return;
            }
            
            OnError();
        }

        protected abstract void OnSuccess();

        private void OnError()
        {
            if (Request.OnError == null)
            {
                _response.Error.LogError();
                return;
            }

            try
            {
                Request.OnError.Invoke(_response.Error);
            }
            catch (Exception ex)
            {
                Client.Bot.Logger.Exception("An exception occured during Error callback for request: [{0}] {1}", Request.Method, Request.Route, ex);
            }
            finally
            {
                _response.Error.LogError();
            }
        }

        protected override void EnterPool()
        {
            Client = null;
            Request.Dispose();
            Request = null;
            _response.Dispose();
            _response = null;
        }
    }
}