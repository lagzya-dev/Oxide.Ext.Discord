using System;
using System.Net.Http;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Callbacks.Api;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Represents a REST API request that returns no data
    /// </summary>
    public class Request : BaseRequest
    {
        /// <summary>
        /// Callback to call if the request completed successfully
        /// </summary>
        internal Action OnSuccess;

        /// <summary>
        /// Creates a REST API request that returns type of T from the response
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="method">HTTP web method</param>
        /// <param name="route">Route for the request</param>
        /// <param name="data">Data being passed into the request. Null if no data is passed</param>
        /// <param name="onSuccess">Callback when the web request finishes successfully</param>
        /// <param name="onError">Callback when the web request fails to complete successfully and encounters an error</param>
        /// <returns>A <see cref="Request"/></returns>
        public static Request CreateRequest(DiscordClient client, HttpClient httpClient, RequestMethod method, string route, object data, Action onSuccess, Action<RequestError> onError)
        {
            Request request = DiscordPool.Get<Request>();
            request.Init(client, httpClient, method, route, data, onSuccess, onError);
            return request;
        }
        
        /// <summary>
        /// Initializes the Request
        /// </summary>
        private void Init(DiscordClient client, HttpClient httpClient, RequestMethod method, string route, object data, Action onSuccess, Action<RequestError> onError)
        {
            base.Init(client, httpClient, method, route, data, onError);
            OnSuccess = onSuccess;
        }

        ///<inheritdoc/>
        protected override void OnRequestSuccess(RequestResponse response)
        {
            if (OnSuccess != null)
            {
                ApiSuccessCallback callback = DiscordPool.Get<ApiSuccessCallback>();
                callback.Init(this);
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