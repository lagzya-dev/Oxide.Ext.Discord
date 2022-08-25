using System;
using System.Net.Http;
using System.Threading.Tasks;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Callbacks.Api;
using Oxide.Ext.Discord.Callbacks.Api.Entities;
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
        /// <param name="httpClient"><see cref="HttpClient"/> for the request</param>
        /// <param name="method">HTTP web method</param>
        /// <param name="route">Route for the request</param>
        /// <param name="data">Data being passed into the request. Null if no data is passed</param>
        /// <param name="onSuccess">Callback when the web request finishes successfully</param>
        /// <param name="onError">Callback when the web request fails to complete successfully and encounters an error</param>
        /// <param name="callback">Completed callback for the request</param>
        /// <returns>A <see cref="Request{T}"/></returns>
        public static Request<T> CreateRequest(DiscordClient client, HttpClient httpClient, RequestMethod method, string route, object data, Action<T> onSuccess, Action<RequestError> onError, BaseApiCompletedCallback callback)
        {
            Request<T> request = DiscordPool.Get<Request<T>>();
            request.Init(client, httpClient, method, route, data, onSuccess, onError, callback);
            return request;
        }
        
        /// <summary>
        /// Initializes the Request
        /// </summary>
        private void Init(DiscordClient client, HttpClient httpClient, RequestMethod method, string route, object data, Action<T> onSuccess, Action<RequestError> onError, BaseApiCompletedCallback callback)
        {
            base.Init(client, httpClient, method, route, data, onError, callback);
            OnSuccess = onSuccess;
        }

        ///<inheritdoc/>
        protected override async Task OnRequestSuccess(RequestResponse response)
        {
            if (OnSuccess != null)
            {
                ApiSuccessCallback<T> callback = DiscordPool.Get<ApiSuccessCallback<T>>();
                await callback.Init(this, response).ConfigureAwait(false);
                callback.Run();
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