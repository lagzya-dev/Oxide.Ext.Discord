using System;
using System.Collections.Generic;
using System.Threading;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.RateLimits;
using Oxide.Ext.Discord.Rest.Requests;

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
        internal string BucketId;
        
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
        internal DateTimeOffset ResetAt = DateTimeOffset.UtcNow;

        internal bool IsKnowBucket;

        internal readonly RestRateLimit RateLimit;

        private readonly ILogger _logger;
        private readonly RestHandler _rest;
        private readonly object _syncRoot = new object();
        private readonly List<RequestHandler> _requests = new List<RequestHandler>();
        private readonly BucketHandler _handler;

        /// <summary>
        /// Creates a new bucket for the given <see cref="RestHandler"/>
        /// </summary>
        /// <param name="bucketId">ID of the bucket. If not a known bucket then will be part of the route. If know bucket will be the Discord bucket ID</param>
        /// <param name="rest">The handler that owns this Bucket</param>
        /// <param name="logger">Logger for this bucket</param>
        public Bucket(string bucketId, RestHandler rest, ILogger logger)
        {
            BucketId = bucketId;
            _rest = rest;
            RateLimit = rest.RateLimit;
            _logger = logger;
            _handler = new BucketHandler(this, logger);
        }
        
        /// <summary>
        /// Queues a new request for the buck
        /// </summary>
        /// <param name="request">Request to be queued</param>
        public void QueueRequest(BaseRequest request)
        {
            request.OnRequestQueued(this);
            RequestHandler handler = RequestHandler.CreateRequestHandler(request);
            lock (_syncRoot)
            {
                _requests.Add(handler);
            }
            _handler.OnRequestQueued();
        }
        
        /// <summary>
        /// Merges the request to it's place in line by it's ID
        /// </summary>
        /// <param name="mergeHandler">Request to be merged</param>
        private void MergeRequest(RequestHandler mergeHandler)
        {
            lock (_syncRoot)
            {
                for (int i = 0; i < _requests.Count; i++)
                {
                    RequestHandler handler = _requests[0];
                    if (handler.Request.RequestId > mergeHandler.Request.RequestId)
                    {
                        _requests.Insert(i, mergeHandler);
                        handler.Request.OnRequestQueued(this);
                        return;
                    }
                }
                
                //Request is older than the current requests. Add it to the end.
                _requests.Add(mergeHandler);
            }
        }

        /// <summary>
        /// Removes the request from the queue.
        /// Either the request completed successfully or there was an error and failed to succeed after 3 attempts
        /// </summary>
        /// <param name="handler">Request to remove</param>
        private void DequeueRequest(RequestHandler handler)
        {
            lock (_syncRoot)
            {
                _requests.Remove(handler);
            }
        }
        
        internal int RequestCount()
        {
            lock (_syncRoot)
            {
                return _requests.Count;
            }
        }
        
        internal RequestHandler GetRequest(int index)
        {
            lock (_syncRoot)
            {
                return _requests[index];
            }
        }

        internal void Merge(Bucket data)
        {
            int count = data.RequestCount();
            for (int index = 0; index < count; index++)
            {
                RequestHandler handler = data.GetRequest(0);
                MergeRequest(handler);
                data.DequeueRequest(handler);
            }
        }

        internal void OnRequestCompleted(RequestHandler handler, RequestResponse response)
        {
            BaseRequest request = handler.Request;
            RateLimitResponse rateLimit = response.RateLimit;
            request.Client.Logger.Debug("Bucket.OnRequestCompleted Plugin: {0} Method: {1} Route: {2} Internal Bucket Id: {3} Limit: {4} Remaining: {5} Reset: {7} Time: {7} Bucket: {8}", 
                request.Client.PluginName, request.Method, request.Route, BucketId, Limit, Remaining, ResetAt, TimeHelpers.TimeSinceEpoch(), rateLimit?.BucketId);
            
            if (rateLimit != null)
            {
                if (rateLimit.HitGlobalRateLimit)
                {
                    RateLimit.ReachedRateLimit(rateLimit.ResetAt);
                    return;
                }
                
                if (rateLimit.ResetAt > ResetAt)
                {
                    Limit = rateLimit.BucketLimit;
                    Remaining = rateLimit.BucketRemaining;
                    ResetAt = rateLimit.ResetAt;
                }
                else
                {
                    Interlocked.Exchange(ref Remaining, Math.Min(Remaining, rateLimit.BucketRemaining));
                }
            }

            DequeueRequest(handler);
            handler.Dispose();
            
            if (!IsKnowBucket && rateLimit != null && !string.IsNullOrEmpty(rateLimit.BucketId))
            {
                _rest.UpgradeToKnownBucket(this, rateLimit.BucketId);
            }
        }
        
        internal void AbortClientRequests(DiscordClient client)
        {
            lock (_syncRoot)
            {
                for (int index = _requests.Count - 1; index >= 0; index--)
                {
                    RequestHandler handler = _requests[index];
                    if (handler.Request.Client != client)
                    {
                        continue;
                    }
                    
                    if (handler.InProgress)
                    {
                        handler.Abort();
                    }
                        
                    _requests.RemoveAt(index);
                }
            }
        }

        internal void OnBucketCompleted()
        {
            if (!IsKnowBucket)
            {
                _rest.RemoveBucket(this);
            }
        }

        internal void FireRequest()
        {
            RateLimit.FiredRequest();
        }

        internal void Shutdown()
        {
            _handler.Shutdown();
        }
    }
}