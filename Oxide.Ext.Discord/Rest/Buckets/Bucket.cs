using System;
using System.Collections.Generic;
using System.Threading;
using Oxide.Ext.Discord.Callbacks.ThreadPool;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
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
        //internal bool IsShutdown;

        private readonly RestRateLimit _rateLimit;
        private readonly ILogger _logger;
        private readonly RestHandler _rest;
        private readonly object _requestSync = new object();
        internal readonly AdjustableSemaphore Semaphore = new AdjustableSemaphore(1);
        internal readonly ThreadSafeList<RequestHandler> Requests = new ThreadSafeList<RequestHandler>();

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
        /// Discord of the Thread Safe List
        /// </summary>
        ~Bucket()
        {
            Requests.Dispose();
        }
        
        /// <summary>
        /// Queues a new request for the buck
        /// </summary>
        /// <param name="request">Request to be queued</param>
        public void QueueRequest(BaseRequest request)
        {
            request.OnRequestQueued(this);
            RequestHandler handler = RequestHandler.CreateRequestHandler(request);
            
            Requests.Add(handler);

            if (ResetAt < DateTimeOffset.UtcNow)
            {
                Remaining = Limit;
            }
            
            _logger.Debug("Queued Request Bucket ID: {0} Request ID: {1} Requests: {2}", Id, request.Id, request.Method, request.Route, Requests.Count);
            
            RestRequestHandler callback = RestRequestHandler.CreateRequestCallback(handler, _logger);
            ThreadPool.QueueUserWorkItem(callback.Callback);
        }
        
        /// <summary>
        /// Merges the request to it's place in line by it's ID
        /// </summary>
        /// <param name="mergeHandler">Request to be merged</param>
        private void MergeRequest(RequestHandler mergeHandler)
        {
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestHandler handler = Requests[0];
                if (handler.Request.Id > mergeHandler.Request.Id)
                {
                    Requests.Insert(i, mergeHandler);
                    handler.Request.OnRequestQueued(this);
                    return;
                }
            }
                
            //Request is older than the current requests. Add it to the end.
            Requests.Add(mergeHandler);
        }

        /// <summary>
        /// Removes the request from the queue.
        /// Either the request completed successfully or there was an error and failed to succeed after 3 attempts
        /// </summary>
        /// <param name="handler">Request to remove</param>
        private void DequeueRequest(RequestHandler handler)
        {
            _logger.Debug($"{nameof(Bucket)}.{nameof(DequeueRequest)} Bucket ID: {{0}} Request ID: {{1}} Removed: {{2}}", Id, handler.Request.Id, Requests.Remove(handler));
        }

        internal void Merge(Bucket data)
        {
            ThreadSafeList<RequestHandler> requests = data.Requests;
            for (int index = 0; index < requests.Count; index++)
            {
                RequestHandler handler = requests[0];
                MergeRequest(handler);
                data.DequeueRequest(handler);
            }
        }

        internal void WaitUntilBucketAvailable(RequestHandler handler)
        {
            BaseRequest request = handler.Request;
            DiscordClient client = request.Client;

            lock (_requestSync)
            {
                while (true)
                {
                    DateTimeOffset resetAt;
                    if (_rateLimit.HasReachedRateLimit)
                    {
                        resetAt = _rateLimit.NextReset();
                        _logger.Debug($"{nameof(Bucket)}.{nameof(WaitUntilBucketAvailable)} {{0}} ID: {{1}} Can't Start Request Due to Global Rate Limit Method: {{2}} Url: {{3}} Waiting For: {{4}} Seconds", client.PluginName, request.Id, request.Method, request.RequestUrl, (resetAt - DateTimeOffset.UtcNow).TotalSeconds);
                        ThreadExt.SleepUntil(resetAt);
                        continue;
                    }

                    resetAt = ResetAt;
                    if ((Limit == 0 || Remaining <= 0) && resetAt > DateTimeOffset.UtcNow)
                    {
                        _logger.Debug($"{nameof(Bucket)}.{nameof(WaitUntilBucketAvailable)} {{0}} ID: {{1}} Can't Start Request Due to Bucket Rate Limit Method: {{2}} Url: {{3}} Limit: {{4}} Remaining: {{5}} Waiting For: {{6}} Seconds", client.PluginName, request.Id, request.Method, request.RequestUrl, Limit, Remaining, (resetAt - DateTimeOffset.UtcNow).TotalSeconds);
                        ThreadExt.SleepUntil(resetAt);
                        continue;
                    }

                    _logger.Debug($"{nameof(Bucket)}.{nameof(WaitUntilBucketAvailable)} {{0}} ID: {{1}} Can Start Request: Bucket: {{2}}/{{3}} Reset In: {{4}}", client.PluginName, request.Id, Remaining, Limit, (resetAt - DateTimeOffset.UtcNow).TotalSeconds);
                    break;
                }

                OnRequestStarted(handler);
            }
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

            DequeueRequest(handler);
            
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
            for (int index = Requests.Count - 1; index >= 0; index--)
            {
                RequestHandler handler = Requests[index];
                if (handler.Request.Client != client)
                {
                    continue;
                }
                    
                if (handler.Request.Status == RequestStatus.InProgress)
                {
                    handler.Abort();
                }
                        
                Requests.RemoveAt(index);
            }
            
            OnBucketCompleted();
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
            for (int index = Requests.Count - 1; index >= 0; index--)
            {
                RequestHandler handler = Requests[index];
                handler.Abort();
                Requests.RemoveAt(index);
            }
            
            Semaphore.AllowAllThrough();
        }
    }
}