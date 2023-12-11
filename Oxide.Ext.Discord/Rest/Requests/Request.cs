using System.Net.Http;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Types.Pooling;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Represents a Request that does not return data
    /// </summary>
    public class Request : BaseRequest
    {
        private IPendingPromise _promise;

        /// <summary>
        /// Creates a REST API request that returns type of T from the response
        /// </summary>
        /// <param name="pluginPool"><see cref="DiscordPluginPool"/> for the request</param>
        /// <param name="client">Client making the request</param>
        /// <param name="httpClient"><see cref="HttpClient"/> for the request</param>
        /// <param name="method">HTTP web method</param>
        /// <param name="route">Route for the request</param>
        /// <param name="data">Data being passed into the request. Null if no data is passed</param>
        /// <param name="promise">Promise for the request</param>
        /// <returns>A <see cref="Request{T}"/></returns>
        public static Request CreateRequest(DiscordPluginPool pluginPool, DiscordClient client, HttpClient httpClient, RequestMethod method, string route, object data, IPendingPromise promise)
        {
            Request request = pluginPool.Get<Request>();
            request.Init(client, httpClient, method, route, data);
            request._promise = promise;
            return request;
        }

        ///<inheritdoc/>
        protected override void OnRequestSuccess(RequestResponse response)
        {
            _promise.Resolve();
        }

        ///<inheritdoc/>
        protected override void OnRequestError(RequestResponse response)
        {
            _promise.Reject(response.Error);
            _promise.Finally(response.Error.LogError);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _promise = null;
        }
    }
}