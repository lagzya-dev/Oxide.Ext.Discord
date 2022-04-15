using System;
using Oxide.Core;
using Oxide.Ext.Discord.Callbacks.Rest;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Handles request that return data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Request<T> : Request
    {
        private Action<T> _onSuccess;

        /// <summary>
        /// Initializes a new request
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="method">HTTP method to call</param>
        /// <param name="route">Route to call on the API</param>
        /// <param name="data">Data for the request</param>
        /// <param name="authHeader">Authorization Header</param>
        /// <param name="onSuccess">Callback once the request completes successfully</param>
        /// <param name="onError">Callback when the request errors</param>
        public void Init(DiscordClient client, RequestMethod method, string route, object data, string authHeader, Action<T> onSuccess, Action<RestError> onError)
        {
            Init(client, method, route, data, authHeader, onError);
            _onSuccess = onSuccess;
        }

        /// <inheritdoc/>
        protected override void InvokeSuccess()
        {
            if (_onSuccess == null)
            {
                return;
            }
            
            T data = Response.ParseData<T>();
            ApiCallback<T> callback = DiscordPool.Get<ApiCallback<T>>();
            callback.Init(this, _onSuccess, data, Client);
            Interface.Oxide.NextTick(callback.Callback);
        }

        /// <inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            _onSuccess = null;
        }
    }
}