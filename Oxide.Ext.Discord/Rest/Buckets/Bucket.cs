using System;
using System.Collections.Generic;
using System.Threading;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Rest
{
    /// <summary>
    /// Represents a discord API bucket for a group of requests
    /// </summary>
    public class Bucket
    {
        /// <summary>
        /// The ID of this bucket which is based on the route
        /// </summary>
        public string BucketId { get; internal set; }
        
        /// <summary>
        /// The number of requests that can be made per rate limit reset
        /// </summary>
        public int RateLimitTotalRequests;

        /// <summary>
        /// How many requests are remaining before hitting the rate limit for the bucket
        /// </summary>
        public int RateLimitRemaining;

        /// <summary>
        /// How long until the rate limit resets
        /// </summary>
        public double RateLimitReset;

        /// <summary>
        /// How long to wait before retrying request since there was a web exception
        /// </summary>
        public double ErrorDelayUntil;

        /// <summary>
        /// Rest Handler for the bucket
        /// </summary>
        public readonly RestHandler Handler;

        private readonly ILogger _logger;
        private readonly List<Request> _requests;
        private readonly object _syncRoot = new object();

        private readonly ThreadStart _threadStart;
        private Thread _thread;
        private bool _isKnowBucket;

        /// <summary>
        /// Creates a new bucket for the given rest handler and bucket ID
        /// </summary>
        /// <param name="handler">Rest Handler for the bucket</param>
        /// <param name="bucketId">ID of the bucket</param>
        /// <param name="logger">Logger for the client</param>
        public Bucket(RestHandler handler, string bucketId, ILogger logger)
        {
            Handler = handler;
            BucketId = bucketId;
            _logger = logger;
            _requests = new List<Request>();
            _threadStart = RunThread;
            _logger.Debug("New Bucket Created with id: {0}", bucketId);
        }

        /// <summary>
        /// Close the bucket and abort the thread
        /// </summary>
        public void Close()
        {
            _thread?.Abort();
            _thread = null;
        }

        /// <summary>
        /// Queues a new request for the buck
        /// </summary>
        /// <param name="request">Request to be queued</param>
        public void QueueRequest(Request request)
        {
            _logger.Debug("Bucket {0} Queuing request {1}", BucketId, request.Route);
            request.Bucket = this;
            lock (_syncRoot)
            {
                _requests.Add(request);
                if (_thread == null || !_thread.IsAlive)
                {
                    _thread = new Thread(_threadStart) {IsBackground = true};
                    _thread.Start();
                }
                else
                {
                    _thread.Interrupt();
                }
            }
        }

        /// <summary>
        /// Removes the request from the queue.
        /// Either the request completed successfully or there was an error and failed to succeed after 3 attempts
        /// </summary>
        /// <param name="request">Request to remove</param>
        public void DequeueRequest(Request request)
        {
            lock (_syncRoot)
            {
                _requests.Remove(request);
            }
        }

        private void RunThread()
        {
            try
            {
                while (RequestCount() > 0)
                {
                    FireRequests();
                }
                SleepBucketFor((int)(RateLimitReset - Time.TimeSinceEpoch()) + 1);
            }
            catch (ThreadInterruptedException)
            {
                RunThread();
            }
            catch (ThreadAbortException)
            {
                _logger.Debug("Bucket thread has been aborted.");
            }
            catch (Exception ex)
            {
                _logger.Exception("An exception occured for bucket {0}", BucketId, ex);
            }

            if (!_isKnowBucket)
            {
                Handler.RemoveBucket(this);
            }
        }

        private void FireRequests()
        {
            double timeSince = Time.TimeSinceEpoch();
            if (Handler.RateLimit.HasReachedRateLimit)
            {
                int resetIn = (int)(Handler.RateLimit.NextReset * 1000) + 1;
                _logger.Debug("Global Rate limit hit. Sleeping until Reset: {0}ms", resetIn);
                SleepBucketFor(resetIn);
                return;
            }
            
            if (RateLimitTotalRequests != 0 && RateLimitRemaining == 0 && RateLimitReset > timeSince)
            {
                int resetIn = (int)((RateLimitReset - timeSince) * 1000) + 1;
                _logger.Debug("Bucket Rate limit hit. Sleeping until Reset: {0}ms", resetIn);
                SleepBucketFor(resetIn);
                return;
            }

            if (ErrorDelayUntil > timeSince)
            {
                int resetIn = (int)((ErrorDelayUntil - timeSince) * 1000) + 1;
                _logger.Debug("Web request error occured delaying next send until: {0}ms", resetIn);
                SleepBucketFor(resetIn);
                return;
            }

            Handler.RateLimit.FiredRequest();
            GetRequest(0).Fire();
        }

        private int RequestCount()
        {
            lock (_syncRoot)
            {
                return _requests.Count;
            }
        }

        private Request GetRequest(int index)
        {
            lock (_syncRoot)
            {
                return _requests[index];
            }
        }

        private static void SleepBucketFor(int delay)
        {
            if (delay > 0)
            {
                Thread.Sleep(delay);
            }
        }
        
        internal void AbortClientRequests(DiscordClient client)
        {
            lock (_syncRoot)
            {
                for (int index = _requests.Count - 1; index >= 0; index--)
                {
                    Request request = _requests[index];
                    if (request.Client != client)
                    {
                        continue;
                    }
                    
                    if (request.InProgress)
                    {
                        request.Abort();
                    }
                        
                    _requests.RemoveAt(index);
                }
            }
        }

        internal void UpdateFromRequest(int bucketLimit, int bucketRemaining, double resetTime, string bucketId)
        {
            RateLimitTotalRequests = bucketLimit;
            RateLimitRemaining = bucketRemaining;
            resetTime = Time.TimeSinceEpoch() + resetTime;
            if (resetTime > RateLimitReset)
            {
                RateLimitReset = resetTime;
            }
            
            if (!_isKnowBucket && !string.IsNullOrEmpty(bucketId) && Handler.UpgradeToKnownBucket(this, bucketId))
            {
                _isKnowBucket = true;
            }
        }

        internal void Merge(Bucket bucket)
        {
            for (int index = 0; index < bucket.RequestCount(); index++)
            {
                Request request = bucket.GetRequest(0);
                QueueRequest(request);
                bucket.DequeueRequest(request);
            }
        }
    }
}
