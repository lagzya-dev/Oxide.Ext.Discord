using System;
using System.Collections.Generic;
using System.Threading;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.REST
{
    public class Bucket : List<Request>
    {
        public int Limit { get; set; }

        public int Remaining { get; set; }
        
        public double Reset { get; set; }

        public readonly BotRestHandler Handler;

        private Thread _thread;
        
        public string BucketId { get; }

        private readonly Logger<Bucket> _logger;

        public Bucket(BotRestHandler handler, string bucketId, LogLevel logLevel)
        {
            Handler = handler;
            BucketId = bucketId;
            _logger = new Logger<Bucket>(logLevel);
            _logger.LogDebug($"New Bucket Created with id: {bucketId}");
        }

        public void Close()
        {
            _thread?.Abort();
            _thread = null;
        }

        public bool ShouldCleanup(double currentTime) => (_thread == null || !_thread.IsAlive) && currentTime > Reset;

        public void Queue(Request request)
        {
            lock (this)
            {
                Add(request);
            }

            if (_thread == null || !_thread.IsAlive)
            {
                _thread = new Thread(RunThread);
                _thread.Start();
            }
        }

        private void RunThread()
        {
            while (Count > 0)
            {
                FireRequests();
            }
        }

        private void FireRequests()
        {
            double timeSince = Time.TimeSinceEpoch();
            if (Handler.RateLimit.HasReachedRateLimit)
            {
                int resetIn = (int) ((Handler.RateLimit.NextBucketReset - timeSince) * 1000);
                _logger.LogDebug($"Global Rate limit hit. Sleeping until Reset: {resetIn}ms ");
                Thread.Sleep(resetIn);
                return;
            }
            
            if (Remaining == 0 && Reset > timeSince)
            {
                int resetIn = (int) ((Reset - timeSince) * 1000);
                _logger.LogDebug($"Bucket Rate limit hit. Sleeping until Reset: {resetIn}ms ");
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

                if (request.InProgress)
                {
                    return;
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
