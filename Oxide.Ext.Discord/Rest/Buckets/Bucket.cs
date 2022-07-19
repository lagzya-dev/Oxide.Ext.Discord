using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.RateLimits;
using Oxide.Ext.Discord.Rest.Requests;
using Oxide.Ext.Discord.Threading;

namespace Oxide.Ext.Discord.Rest.Buckets
{
    /// <summary>
    /// Contains bucket information for a REST API Bucket
    /// </summary>
    public class Bucket
    {
        /// <summary>
        /// The ID of this bucket which is based on the route
        /// </summary>
        internal string Id;
        
        /// <summary>
        /// The number of requests that can be made per rate limit reset
        /// </summary>
        internal int Limit = 1;

        /// <summary>
        /// How many requests are remaining before hitting the rate limit for the bucket
        /// </summary>
        internal int Remaining = 1;

        /// <summary>
        /// How long until the rate limit resets
        /// </summary>
        internal DateTimeOffset ResetAt = DateTimeOffset.MinValue;

        internal bool IsKnowBucket;

        private readonly RestRateLimit _rateLimit;
        private readonly ILogger _logger;
        private readonly RestHandler _rest;
        private readonly AutoResetEvent _requestSync = new AutoResetEvent(true);
        private readonly AutoResetEvent _completedSync = new AutoResetEvent(true);
        internal readonly AdjustableSemaphore Semaphore = new AdjustableSemaphore(1);
        internal readonly ConcurrentDictionary<Snowflake, RequestHandler> Requests = new ConcurrentDictionary<Snowflake, RequestHandler>();

        /// <summary>
        /// Creates a new bucket for the given <see cref="RestHandler"/>
        /// </summary>
        /// <param name="bucketId">ID of the bucket. If not a known bucket then will be part of the route. If know bucket will be the Discord bucket ID</param>
        /// <param name="rest">The handler that owns this Bucket</param>
        /// <param name="logger">Logger for this bucket</param>
        public Bucket(string bucketId, RestHandler rest, ILogger logger)
        {
            Id = bucketId;
            _rest = rest;
            _rateLimit = rest.RateLimit;
            _logger = logger;
            _logger.Debug($"{nameof(Bucket)}.Ctor Bucket Created: {{0}}", Id);
        }

        /// <summary>
        /// Queues a new request for the buck
        /// </summary>
        /// <param name="handler"><see cref="RequestHandler"/> to be queued</param>
        public void QueueRequest(RequestHandler handler)
        {
            BaseRequest request = handler.Request;
            request.Bucket?.DequeueRequest(handler);
            Requests[request.Id] = handler;
            request.Bucket = this;

            if (ResetAt < DateTimeOffset.UtcNow)
            {
                Remaining = Limit;
            }
            
            _logger.Debug("Queued Request Bucket ID: {0} Request ID: {1} Requests: {2}", Id, request.Id, request.Method, request.Route, Requests.Count);
        }

        private void DequeueRequest(RequestHandler handler)
        {
            Requests.TryRemove(handler.Request.Id, out RequestHandler _);
        }
        
        internal void Merge(Bucket data)
        {
            List<RequestHandler> handlers = DiscordPool.GetList<RequestHandler>();
            handlers.AddRange(data.Requests.Values);

            foreach (RequestHandler handler in handlers)
            {
                QueueRequest(handler);
            }
            
            DiscordPool.FreeList(ref handlers);
            
            data.Requests.Clear();
        }

        internal async Task WaitUntilBucketAvailable(RequestHandler handler, CancellationToken token)
        {
            BaseRequest request = handler.Request;
            DiscordClient client = request.Client;

            _requestSync.WaitOne();
            while (true)
            {
                if (_rateLimit.HasReachedRateLimit)
                {
                    DateTimeOffset resetAt = _rateLimit.NextReset();
                    _logger.Debug($"{nameof(Bucket)}.{nameof(WaitUntilBucketAvailable)} Plugin: {{0}} Bucket ID: {{1}} Request ID: {{2}} Can't Start Request Due to Global Rate Limit Method: {{3}} Url: {{4}} Waiting For: {{5}} Seconds", client.PluginName, Id, request.Id, request.Method, request.Route, (resetAt - DateTimeOffset.UtcNow).TotalSeconds);
                    if (resetAt > DateTimeOffset.UtcNow)
                    {
                        await Task.Delay(resetAt - DateTimeOffset.UtcNow, token);
                    }
                        
                    continue;
                }
                
                if ((Limit == 0 || Remaining <= 0) && ResetAt > DateTimeOffset.UtcNow)
                {
                    _logger.Debug($"{nameof(Bucket)}.{nameof(WaitUntilBucketAvailable)} Plugin: {{0}} Bucket ID: {{1}} Request ID: {{2}} Can't Start Request Due to Bucket Rate Limit Method: {{3}} Url: {{4}} Limit: {{5}} Remaining: {{6}} Waiting For: {{7}} Seconds", client.PluginName, Id, request.Id, request.Method, request.Route, Limit, Remaining, (ResetAt - DateTimeOffset.UtcNow).TotalSeconds);
                    if (ResetAt > DateTimeOffset.UtcNow)
                    {
                        await Task.Delay(ResetAt - DateTimeOffset.UtcNow, token);
                    }
                    continue;
                }

                _logger.Debug($"{nameof(Bucket)}.{nameof(WaitUntilBucketAvailable)} Plugin: {{0}} Bucket ID: {{1}} Request ID: {{2}} Can Start Request: Bucket: {{3}}/{{4}} Reset In: {{5}}", client.PluginName, Id, request.Id, Remaining, Limit, (ResetAt - DateTimeOffset.UtcNow).TotalSeconds);
                break;
            }
            _requestSync.Set();
        }
        
        internal void OnRequestStarted(RequestHandler handler)
        {
            Interlocked.Decrement(ref Remaining);
            _rateLimit.FiredRequest();
            _logger.Debug($"{nameof(Bucket)}.{nameof(OnRequestStarted)} ID: {{0}} Has Started Bucket {{1}}/{{2}}", handler.Request.Id, Remaining, Limit);
        }

        internal void OnRequestCompleted(RequestHandler handler, RequestResponse response)
        {
            RateLimitResponse rateLimit = response.RateLimit;

            if (!Requests.TryRemove(handler.Request.Id, out RequestHandler _))
            {
                _logger.Warning("Failed to remove request from bucket!!! Bucket ID: {0} Request ID: {1} Method: {2} Route: {3} Status: {4}", Id, handler.Request.Id, handler.Request.Method, handler.Request.Route, handler.Request.Status);
            }

            try
            {
                _completedSync.WaitOne();

                if (!IsKnowBucket && rateLimit != null && !string.IsNullOrEmpty(rateLimit.BucketId))
                {
                    _rest.UpgradeToKnownBucket(this, rateLimit.BucketId);
                    if (!IsKnowBucket)
                    {
                        Semaphore.AllowAllThrough();
                    }
                }
                
                if (Requests.Count == 0)
                {
                    OnBucketCompleted();
                }
            }
            finally
            {
                _completedSync.Set();
            }
        }
        
        internal void UpdateRateLimits(RequestHandler handler, RequestResponse response)
        {
            BaseRequest request = handler.Request;
            RateLimitResponse rateLimit = response.RateLimit;
            
            if (rateLimit == null)
            {
                return;
            }
            
            if (rateLimit.IsGlobalRateLimit)
            {
                _rateLimit.ReachedRateLimit(rateLimit.ResetAt);
            }
            
            if (rateLimit.ResetAt > ResetAt)
            {
                Limit = rateLimit.Limit;
                Interlocked.Exchange(ref Remaining, rateLimit.Remaining);
                ResetAt = rateLimit.ResetAt;
            }
            else
            {
                Interlocked.Exchange(ref Remaining, Math.Min(Remaining, rateLimit.Remaining));
            }

            if (Semaphore.MaximumCount != rateLimit.Limit)
            {
                Semaphore.MaximumCount = Math.Max(rateLimit.Limit, 1);
            }
            
            request.Client.Logger.Debug($"{nameof(Bucket)}.{nameof(UpdateRateLimits)} Bucket ID: {{0}} Scope: {{1}} Request ID: {{2}} Limit: {{3}} Remaining: {{4}} Reset: {{5}} Reset In: {{6}}s Rate Limit Bucket ID: {{7}}", Id, rateLimit.Scope, request.Id, Limit, Remaining, ResetAt, (ResetAt - DateTimeOffset.UtcNow).TotalSeconds , rateLimit.BucketId);
        }

        internal void AbortClientRequests(DiscordClient client)
        {
            foreach (RequestHandler handler in Requests.Values)
            {
                if (handler.Request.Client != client)
                {
                    continue;
                }
                    
                if (handler.Request.Status == RequestStatus.InProgress)
                {
                    handler.Abort();
                }
            }
        }

        private void OnBucketCompleted()
        {
            if (!IsKnowBucket)
            {
                _logger.Debug($"{nameof(Bucket)}.{nameof(OnBucketCompleted)} Bucket Completed: {{0}}", Id);
                _rest.RemoveBucket(this);
            }
        }

        internal void Shutdown()
        {
            _logger.Debug($"{nameof(Bucket)}.{nameof(Shutdown)} Shutting down bucket ID: {{0}}", Id);
            foreach (RequestHandler request in Requests.Values)
            {
                request.Abort();
            }
            
            Requests.Clear();
            Semaphore.AllowAllThrough();
        }
    }
}