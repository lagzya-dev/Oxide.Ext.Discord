using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Oxide.Ext.Discord.Callbacks.Rest;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.RateLimits;
using Oxide.Ext.Discord.Rest.Requests;
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
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Error callback if an error occurs</param>
        public void DoRequest(DiscordClient client, string url, RequestMethod method, object data, Action callback, Action<RestError> error)
        {
            if (data is IDiscordValidation validate)
            {
                validate.Validate();
            }
            
            Request request = DiscordPool.Get<Request>();
            request.Init(client, method, url, data, _authorization, callback, error);
            QueueRequest(request);
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Error callback if an error occurs</param>
        /// <typeparam name="T">The type that is expected to be returned</typeparam>
        public void DoRequest<T>(DiscordClient client, string url, RequestMethod method, object data, Action<T> callback, Action<RestError> error)
        {
            if (data is IDiscordValidation validate)
            {
                validate.Validate();
            }
            
            Request<T> request = DiscordPool.Get<Request<T>>();
            request.Init(client, method, url, data, _authorization, callback, error);
            QueueRequest(request);
        }

        /// <summary>
        /// Queues the request using the thread pool so we don't block the main thread with any locks
        /// </summary>
        /// <param name="request"></param>
        public void QueueRequest(Request request)
        {
            _logger.Debug("RestHandler Queuing Request for {0}", request.Route);
            QueueRequestCallback queue = QueueRequestCallback.Create(request, this);
            ThreadPool.QueueUserWorkItem(queue.Callback);
        }
        
        /// <summary>
        /// Queues the request for the bucket
        /// </summary>
        public void QueueBucket(Request request)
        {
            string bucketId = BucketIdGenerator.GetBucketId(request.Method, request.Route);
            _logger.Debug("RestHandler Queuing Bucket for {0} bucket {1}", request.Route, bucketId);
            Bucket bucket = GetBucket(bucketId);
            bucket.QueueRequest(request);
            CleanupExpired();
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
                    bucket = DiscordPool.Get<Bucket>();
                    bucket.Init(this, bucketId, _logger);
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
                Buckets.RemoveAll(b => b.ShouldCleanup(), DiscordPool.Free);
            }
        }

        internal void OnClientClosed(DiscordClient client)
        {
            lock (_bucketSyncObject)
            {
                foreach (Bucket bucket in Buckets.Values)
                {
                    bucket.AbortClientRequests(client);
                }
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
                    DiscordPool.Free(bucket);
                }
                Buckets.Clear();
            }
            
            RateLimit.Shutdown();
        }
    }
}
