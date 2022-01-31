using System;
using System.Text;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.RateLimits;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Rest
{
    /// <summary>
    /// Represents a REST handler for a bot
    /// </summary>
    public class RestHandler
    {
        /// <summary>
        /// Global Rate Limit for the bot
        /// </summary>
        public readonly RestRateLimit RateLimit = new RestRateLimit();
        
        /// <summary>
        /// The request buckets for the bot
        /// </summary>
        public readonly Hash<string, Bucket> Buckets = new Hash<string, Bucket>();

        /// <summary>
        /// Request Handler for the request
        /// </summary>
        public readonly RequestHandler RequestHandler;

        /// <summary>
        /// The authorization header value
        /// </summary>
        private readonly string _authorization;
        
        private readonly ILogger _logger;
        private readonly object _bucketSyncObject = new object();

        /// <summary>
        /// Creates a new REST handler for a bot client
        /// </summary>
        /// <param name="client">Client the request is for</param>
        /// <param name="logger">Logger from the client</param>
        public RestHandler(BotClient client, ILogger logger)
        {
            _authorization = $"Bot {client.Settings.ApiToken}";
            _logger = logger;
            RequestHandler = new RequestHandler(this, _logger);
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Error callback if an error occurs</param>
        public void DoRequest(string url, RequestMethod method, object data, Action callback, Action<RestError> error)
        {
            Request request = new Request(method, url, data, _authorization, callback, error, _logger);
            RequestHandler.QueueRequest(request);
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Error callback if an error occurs</param>
        /// <typeparam name="T">The type that is expected to be returned</typeparam>
        public void DoRequest<T>(string url, RequestMethod method, object data, Action<T> callback, Action<RestError> error)
        {
            Request<T> request = new Request<T>(method, url, data, _authorization, callback, error, _logger);
            RequestHandler.QueueRequest(request);
        }

        /// <summary>
        /// Returns the bucket with the given ID
        /// </summary>
        /// <param name="bucketId"></param>
        /// <returns></returns>
        public Bucket GetBucket(string bucketId)
        {
            lock (_bucketSyncObject)
            {
                Bucket bucket = Buckets[bucketId];
                if (bucket == null)
                {
                    bucket = new Bucket(this, bucketId, _logger);
                    Buckets[bucketId] = bucket;
                }

                return bucket;
            }
        }
        
        /// <summary>
        /// Removed buckets that are old and not being used
        /// </summary>
        public void CleanupExpired()
        {
            lock (_bucketSyncObject)
            {
                Buckets.RemoveAll(b => b.ShouldCleanup());
            }
        }

        /// <summary>
        /// Shutdown the REST handler
        /// </summary>
        public void Shutdown()
        {
            lock (_bucketSyncObject)
            {
                foreach (Bucket bucket in Buckets.Values)
                {
                    bucket.Close();
                }
                Buckets.Clear();
            }
            
            RateLimit.Shutdown();
        }
    }
}
