using System;
using System.Threading;
using Oxide.Ext.Discord.Callbacks.Rest;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Extensions;
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
        /// Buckets with Routes we don't know the Hash of yet
        /// </summary>
        public readonly Hash<string, Bucket> Buckets = new Hash<string, Bucket>();

        /// <summary>
        /// Route to Bucket Hash
        /// </summary>
        public readonly Hash<string, string> RouteToHash = new Hash<string, string>();

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
        }

        internal bool UpgradeToKnownBucket(Bucket bucket, string newBucketId)
        {
            lock (_bucketSyncObject)
            {
                RouteToHash[bucket.BucketId] = newBucketId;
                
                Bucket existing = Buckets[newBucketId];
                if (existing != null)
                {
                    existing.Merge(bucket);
                    bucket.Close();
                    return false;
                }

                bucket.BucketId = newBucketId;
                return true;
            }
        }

        internal void RemoveBucket(Bucket bucket)
        {
            lock (_bucketSyncObject)
            {
                Buckets.Remove(bucket.BucketId);
            }
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
                if (RouteToHash.ContainsKey(bucketId))
                {
                    bucketId = RouteToHash[bucketId];
                }
                
                Bucket bucket = Buckets[bucketId];
                if (bucket == null)
                {
                    Buckets[bucketId] = new Bucket(this, bucketId, _logger);
                }

                return bucket;
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
                }
                RouteToHash.Clear();
                Buckets.Clear();
            }
            
            RateLimit.Shutdown();
        }
    }
}
