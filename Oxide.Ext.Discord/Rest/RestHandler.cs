using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Promise;
using Oxide.Ext.Discord.RateLimits;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Rest
{
    /// <summary>
    /// Represents a REST handler for a bot
    /// </summary>
    public class RestHandler : IDebugLoggable
    {
        /// <summary>
        /// <see cref="HttpClient"/> for API Requests
        /// </summary>
        public readonly HttpClient Client;

        /// <summary>
        /// Global Rate Limit for the bot
        /// </summary>
        public readonly RestRateLimit RateLimit;
        
        /// <summary>
        /// Buckets with Routes we don't know the Hash of yet
        /// </summary>
        public readonly ConcurrentDictionary<BucketId, Bucket> Buckets = new ConcurrentDictionary<BucketId, Bucket>();

        /// <summary>
        /// Route to Bucket ID
        /// </summary>
        public readonly ConcurrentDictionary<BucketId, BucketId> RouteToBucketId = new ConcurrentDictionary<BucketId, BucketId>();

        /// <summary>
        /// The authorization header value
        /// </summary>
        //internal readonly string AuthHeader;
        
        private readonly ILogger _logger;
        
        /// <summary>
        /// Creates a new REST handler for a bot client
        /// </summary>
        /// <param name="client">Client the request is for</param>
        /// <param name="logger">Logger from the client</param>
        public RestHandler(BotClient client, ILogger logger)
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = false
            };
            Client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(15),
                BaseAddress = new Uri(DiscordEndpoints.Rest.ApiUrl)
            };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", client.Settings.ApiToken);
            Client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add( StringWithQualityHeaderValue.Parse("gzip"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("deflate"));
            Client.DefaultRequestHeaders.Add("user-agent", $"DiscordBot (https://github.com/Kirollos/Oxide.Ext.Discord, v{DiscordExtension.FullExtensionVersion})");
            _logger = logger;
            RateLimit = new RestRateLimit(logger);
        }

        public IDiscordPromise<TResult> Get<TResult>(DiscordClient client, string url) => CreateRequest<TResult>(client, url, RequestMethod.GET);
        public IDiscordPromise Post(DiscordClient client, string url, object data) => CreateRequest(client, url, RequestMethod.POST, data);
        public IDiscordPromise<TResult> Post<TResult>(DiscordClient client, string url, object data) => CreateRequest<TResult>(client, url, RequestMethod.POST, data);
        public IDiscordPromise Put(DiscordClient client, string url, object data) => CreateRequest(client, url, RequestMethod.PUT, data);
        public IDiscordPromise<TResult> Put<TResult>(DiscordClient client, string url, object data) => CreateRequest<TResult>(client, url, RequestMethod.PUT, data);
        public IDiscordPromise Patch(DiscordClient client, string url, object data) => CreateRequest(client, url, RequestMethod.PATCH, data);
        public IDiscordPromise<TResult> Patch<TResult>(DiscordClient client, string url, object data) => CreateRequest<TResult>(client, url, RequestMethod.PATCH, data);
        public IDiscordPromise Delete(DiscordClient client, string url) => CreateRequest(client, url, RequestMethod.DELETE);
        public IDiscordPromise<TResult> Delete<TResult>(DiscordClient client, string url) => CreateRequest<TResult>(client, url, RequestMethod.DELETE);

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        private IDiscordPromise CreateRequest(DiscordClient client, string url, RequestMethod method, object data = null)
        {
            if (data is IDiscordValidation validate)
            {
                validate.Validate();
            }
            
            Request request = Request.CreateRequest(DiscordPool.Internal, client, Client, method, url, data);
            StartRequest(request);
            return request.Promise;
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <typeparam name="T">The type that is expected to be returned</typeparam>
        private IDiscordPromise<T> CreateRequest<T>(DiscordClient client, string url, RequestMethod method, object data = null)
        {
            if (data is IDiscordValidation validate)
            {
                validate.Validate();
            }

            Request<T> request = Request<T>.CreateRequest(DiscordPool.Internal, client, Client, method, url, data);
            StartRequest(request);
            return request.Promise;
        }

        /// <summary>
        /// Starts the request
        /// </summary>
        /// <param name="request">Request to be started</param>
        public void StartRequest(Request request)
        {
            _logger.Debug($"{nameof(RestHandler)}.{nameof(StartRequest)} Method: {{0}} Route: {{1}}", request.Method, request.Route);
            RequestHandler.StartRequest(this, request);
        }
        
        /// <summary>
        /// Queues the request for the bucket
        /// </summary>
        public Bucket QueueBucket(RequestHandler handler, Request request)
        {
            BucketId bucketId = BucketIdFactory.Instance.GenerateId(request.Method, request.Route);
            _logger.Debug("RestHandler Queuing Bucket for {0} bucket {1}",  request.Route, bucketId);
            Bucket bucket = GetBucket(bucketId);
            bucket.QueueRequest(handler);
            return bucket;
        }

        internal void UpgradeToKnownBucket(Bucket bucket, BucketId newBucketId)
        {
            _logger.Debug("RestHandler Upgrading To Known Bucket for Old ID: {0} New ID: {1}", bucket.Id, newBucketId);
            RouteToBucketId[bucket.Id] = newBucketId;

            if (Buckets.TryGetValue(newBucketId, out Bucket existing))
            {
                existing.Merge(bucket);
                bucket.Dispose();
                return;
            }

            Buckets.TryRemove(bucket.Id, out Bucket _);
            bucket.Id = newBucketId;
            bucket.IsKnownBucket = true;
            Buckets[newBucketId] = bucket;
        }

        internal void RemoveBucket(Bucket bucket)
        {
            Buckets.TryRemove(bucket.Id, out Bucket _);
            bucket.Dispose();
        }

        /// <summary>
        /// Returns the bucket with the given ID
        /// </summary>
        /// <param name="bucketId"></param>
        /// <returns></returns>
        public Bucket GetBucket(BucketId bucketId)
        {
            if (RouteToBucketId.ContainsKey(bucketId))
            {
                bucketId = RouteToBucketId[bucketId];
            }

            if (!Buckets.TryGetValue(bucketId, out Bucket bucket))
            {
                bucket = DiscordPool.Internal.Get<Bucket>();
                bucket.Init(bucketId, this, _logger);
                Buckets[bucketId] = bucket;
            }

            return bucket;
        }

        internal void OnClientClosed(DiscordClient client)
        {
            foreach (KeyValuePair<BucketId, Bucket> bucket in Buckets)
            {
                bucket.Value.AbortClientRequests(client);
            }
        }

        /// <summary>
        /// Shutdown the REST handler
        /// </summary>
        public void Shutdown()
        {
            foreach (KeyValuePair<BucketId, Bucket> bucket in Buckets)
            {
                bucket.Value.Dispose();
            }
            
            RouteToBucketId.Clear();
            Buckets.Clear();
            RateLimit.Shutdown();
        }

        public void LogDebug(DebugLogger logger)
        {
            logger.AppendList("Buckets", Buckets.Values);
        }
    }
}
