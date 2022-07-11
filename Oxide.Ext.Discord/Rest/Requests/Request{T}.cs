using System;
using Oxide.Core;
using Oxide.Ext.Discord.Callbacks.Api;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Represents a REST API request that returns {T} data
    /// </summary>
    /// <typeparam name="T">Data to be returned</typeparam>
    public class Request<T> : BaseRequest
    {
        /// <summary>
        /// Callback to call if the request completed successfully
        /// </summary>
        internal Action<T> OnSuccess;

        /// <summary>
        /// Creates a REST API request that returns type of T from the response
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="method">HTTP web method</param>
        /// <param name="route">Route for the request</param>
        /// <param name="data">Data being passed into the request. Null if no data is passed</param>
        /// <param name="onSuccess">Callback when the web request finishes successfully</param>
        /// <param name="onError">Callback when the web request fails to complete successfully and encounters an error</param>
        /// <returns>A <see cref="Request{T}"/></returns>
        public static Request<T> CreateRequest(DiscordClient client, RequestMethod method, string route, object data, Action<T> onSuccess, Action<RequestError> onError)
        {
            Request<T> request = DiscordPool.Get<Request<T>>();
            request.Init(client, method, route, data, onSuccess, onError);
            return request;
        }
        
        /// <summary>
        /// Initializes the Request
        /// </summary>
        private void Init(DiscordClient client, RequestMethod method, string route, object data, Action<T> onSuccess, Action<RequestError> onError)
        {
            base.Init(client, method, route, data, onError);
            OnSuccess = onSuccess;
        }

        ///<inheritdoc/>
        protected override void OnRequestSuccess(RequestResponse response)
        {
            if (OnSuccess != null)
            {
                ApiSuccessCallback<T> callback = DiscordPool.Get<ApiSuccessCallback<T>>();
                callback.Init(this, response);
                Interface.Oxide.NextTick(callback.Callback);
            }
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            OnSuccess = null;
        }
    }
}