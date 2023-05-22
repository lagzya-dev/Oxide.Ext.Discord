using System;
using System.Threading;
using System.Timers;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;
using Timer = System.Timers.Timer;

namespace Oxide.Ext.Discord.RateLimits
{
    /// <summary>
    /// Represents a base rate limit for websocket and rest api requests
    /// </summary>
    public abstract class BaseRateLimit
    {
        /// <summary>
        /// The number of requests that have executed since the last reset
        /// </summary>
        protected int NumRequests;
        
        /// <summary>
        /// The Last Reset Time
        /// </summary>
        protected long LastReset;
        
        /// <summary>
        /// The max number of requests this rate limit can handle per interval
        /// </summary>
        protected readonly int MaxRequests;
        
        /// <summary>
        /// The interval in which this resets at
        /// </summary>
        protected readonly long ResetInterval;

        /// <summary>
        /// Logger for the rate limit
        /// </summary>
        protected readonly ILogger Logger;
        
        private Timer _timer;

        /// <summary>
        /// Base Rate Limit Constructor
        /// </summary>
        /// <param name="maxRequests">Max requests per interval</param>
        /// <param name="interval">Reset Interval in milliseconds</param>
        /// <param name="logger">Logger</param>
        protected BaseRateLimit(int maxRequests, long interval, ILogger logger)
        {
            MaxRequests = maxRequests;
            ResetInterval = interval;
            Logger = logger;
            
            _timer = new Timer(interval);
            _timer.Elapsed += ResetRateLimit;
            _timer.Start();
            LastReset = TimeHelpers.MillisecondsSinceEpoch();
        }
        
        private void ResetRateLimit(object sender, ElapsedEventArgs e)
        {
            OnRateLimitReset();
            Interlocked.Exchange(ref NumRequests, 0);
            Interlocked.Exchange(ref LastReset, TimeHelpers.MillisecondsSinceEpoch());
        }
        
        /// <summary>
        /// Called when an API request is fired
        /// </summary>
        protected void FiredRequestInternal()
        {
            Interlocked.Add(ref NumRequests, 1);
        }

        /// <summary>
        /// Called when the rate limit is reset
        /// </summary>
        protected abstract void OnRateLimitReset();

        /// <summary>
        /// Returns true if we have reached the global rate limit 
        /// </summary>
        public bool HasReachedRateLimit => NumRequests >= MaxRequests;

        /// <summary>
        /// Returns how long until the current rate limit period will expire
        /// </summary>
        public virtual DateTimeOffset NextReset() => (LastReset + ResetInterval).ToDateTimeOffsetFromMilliseconds();

        /// <summary>
        /// Called when a bot is shutting down
        /// </summary>
        public void Shutdown()
        {
            if (_timer == null)
            {
                return;
            }
            
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
        }
    }
}