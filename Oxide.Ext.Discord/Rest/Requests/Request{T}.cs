using System;
using System.Net.Http;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Callbacks.Api;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Represents a REST API request that returns {T} data
    /// </summary>
    /// <typeparam name="T">Data to be returned</typeparam>
    public class Request<T> : Request
    {
        /// <summary>
        /// Callback to call if the request completed successfully
        /// </summary>
        internal new IDiscordPromise<T> Promise;

        /// <summary>
        /// Creates a REST API request that returns type of T from the response
        /// </summary>
        /// <param name="pluginPool"><see cref="DiscordPluginPool"/> for the request</param>
        /// <param name="client">Client making the request</param>
        /// <param name="httpClient"><see cref="HttpClient"/> for the request</param>
        /// <param name="method">HTTP web method</param>
        /// <param name="route">Route for the request</param>
        /// <param name="data">Data being passed into the request. Null if no data is passed</param>
        /// <returns>A <see cref="Request{T}"/></returns>
        public static Request<T> CreateRequest(DiscordPluginPool pluginPool, DiscordClient client, HttpClient httpClient, RequestMethod method, string route, object data)
        {
            Request<T> request = pluginPool.Get<Request<T>>();
            request.Init(client, httpClient, method, route, data);
            return request;
        }
        
        /// <summary>
        /// Initializes the Request
        /// </summary>
        private void Init(DiscordClient client, HttpClient httpClient, RequestMethod method, string route, object data)
        {
            Promise = DiscordPromise<T>.Create();
            base.Init(client, httpClient, method, route, data, Promise);
        }

        ///<inheritdoc/>
        protected override void OnRequestSuccess(RequestResponse response)
        {
            try
            {
                T data = JsonConvert.DeserializeObject<T>(response.Content, Client.Bot.JsonSettings);
                Promise.Resolve(data);
            }
            catch (Exception ex)
            {
                Logger.Exception("An error occured deserializing JSON response. Method: {0} Route: {1}\nResponse:\n{2}", Method, Route, response.Content, ex);
            }
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            Promise = null;
        }
    }
}