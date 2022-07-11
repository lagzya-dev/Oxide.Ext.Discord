using System;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal abstract class BaseApiCallback : BaseCallback
    {
        private DiscordClient _client;
        private Snowflake _requestId;
        private RequestMethod _method;
        private string _route;

        protected void Init(BaseRequest request)
        {
            _client = request.Client;
            _requestId = request.Id;
            _method = request.Method;
            _route = request.Route;
        }

        protected sealed override void HandleCallback()
        {
            if (_client.IsConnected())
            {
                try
                {
                    HandleApiCallback();
                }
                catch (Exception ex)
                {
                    _client.Bot.Logger.Exception("An exception occured during callback for request Type: {0} ID: {1} [{2}] {3}", GetType(), _requestId, _method, _route, ex);
                }
                finally
                {
                    OnCallbackCompleted();
                }
            }
        }

        protected abstract void HandleApiCallback();

        protected virtual void OnCallbackCompleted()
        {
            
        }

        protected override void EnterPool()
        {
            _client = null;
            _requestId = default(Snowflake);
            _method = default(RequestMethod);
            _route = null;
        }
    }
}