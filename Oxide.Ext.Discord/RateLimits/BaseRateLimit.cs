using System;
using System.Threading;
using System.Timers;
using Oxide.Ext.Discord.Helpers;
using Timer = System.Timers.Timer;

namespace Oxide.Ext.Discord.RateLimits
{
    /// <summary>
    /// Represents a base rate limit for websocket and rest api requests
    /// </summary>
    public class BaseRateLimit
    {
        /// <summary>
        /// The number of requests that have executed since the last reset
        /// </summary>
        protected int NumRequests;
        
        /// <summary>
        /// The Last Reset Time
        /// </summary>
        protected double LastReset;
        
        /// <summary>
        /// The max number of requests this rate limit can handle per interval
        /// </summary>
        protected readonly int MaxRequests;
        
        /// <summary>
        /// The interval in which this resets at
        /// </summary>
        protected readonly double ResetInterval;
        
        private Timer _timer;

        /// <summary>
        /// Base Rate Limit Constructor
        /// </summary>
        /// <param name="maxRequests">Max requests per interval</param>
        /// <param name="interval">Reset Interval</param>
        protected BaseRateLimit(int maxRequests, double interval)
        {
            MaxRequests = maxRequests;
            ResetInterval = interval;
            
            _timer = new Timer(interval);
            _timer.Elapsed += ResetRateLimit;
            _timer.Start();
            LastReset = TimeHelpers.TimeSinceEpoch();
        }
        
        private void ResetRateLimit(object sender, ElapsedEventArgs e)
        {
            Interlocked.Exchange(ref NumRequests, 0);
            Interlocked.Exchange(ref LastReset, TimeHelpers.TimeSinceEpoch());
        }
        
        /// <summary>
        /// Called when an API request is fired
        /// </summary>
        public void FiredRequest()
        {
            Interlocked.Add(ref NumRequests, 1);
        }
        
        /// <summary>
        /// Returns true if we have reached the global rate limit 
        /// </summary>
        public bool HasReachedRateLimit => NumRequests >= MaxRequests;

        /// <summary>
        /// Returns how long until the current rate limit period will expire
        /// </summary>
        public virtual DateTimeOffset NextReset() => (LastReset + ResetInterval).ToDateTimeOffsetFromSeconds();

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