using System.Collections.Generic;
using System.Threading;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;
namespace Oxide.Ext.Discord.Rest
{
    /// <summary>
    /// Represents a discord API buck for a group of requests
    /// </summary>
    public class Bucket : List<Request>
    {
        /// <summary>
        /// The ID of this bucket which is based on the route
        /// </summary>
        public string BucketId;
        
        /// <summary>
        /// The number of requests that can be made
        /// </summary>
        public int RateLimit;

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
        public double ErrorResendDelayUntil;

        /// <summary>
        /// Rest Handler for the bucker
        /// </summary>
        public readonly RestHandler Handler;

        private Thread _thread;

        private readonly ILogger _logger;

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
            _logger.Debug($"New Bucket Created with id: {bucketId}");
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
        /// Returns if this bucket is ready to be cleaned up.
        /// Should be cleaned up if the thread is not null and the RateLimitReset has expired
        /// </summary>
        /// <returns>True if we should cleanup the bucket; false otherwise</returns>
        public bool ShouldCleanup() => (_thread == null || !_thread.IsAlive) && Time.TimeSinceEpoch() > RateLimitReset;

        /// <summary>
        /// Queues a new request for the buck
        /// </summary>
        /// <param name="request">Request to be queued</param>
        public void Queue(Request request)
        {
            lock (this)
            {
                Add(request);
            }

            if (_thread == null || !_thread.IsAlive)
            {
                _thread = new Thread(RunThread) {IsBackground = true};
                _thread.Start();
            }
        }

        private void RunThread()
        {
            try
            {
                while (Count > 0)
                {
                    FireRequests();
                }
            }
            catch (ThreadAbortException)
            {
                _logger.Debug("Bucket thread has been aborted.");
            }
        }

        private void FireRequests()
        {
            double timeSince = Time.TimeSinceEpoch();
            if (Handler.RateLimit.HasReachedRateLimit)
            {
                int resetIn = (int) ((Handler.RateLimit.NextBucketReset - timeSince) * 1000);
                _logger.Debug($"Global Rate limit hit. Sleeping until Reset: {resetIn}ms");
                Thread.Sleep(resetIn);
                return;
            }
            
            if (RateLimitRemaining == 0 && RateLimitReset > timeSince)
            {
                int resetIn = (int) ((RateLimitReset - timeSince) * 1000);
                _logger.Debug($"Bucket Rate limit hit. Sleeping until Reset: {resetIn}ms");
                Thread.Sleep(resetIn);
                return;
            }

            if (ErrorResendDelayUntil > timeSince)
            {
                int resetIn = (int) ((ErrorResendDelayUntil - timeSince) * 1000);
                _logger.Debug($"Web request error occured delaying next send until: {resetIn}ms ");
                Thread.Sleep(resetIn);
                return;
            }

            for (int index = 0; index < Count; index++)
            {
                Request request = this[index];
                if (request.HasTimedOut())
                {
                    request.Close(false);
                }
            }

            //It's possible we removed a request that has timed out.
            if (Count == 0)
            {
                return;
            }
            
            Handler.RateLimit.FiredRequest();
            this[0].Fire(this);
        }
    }
}
