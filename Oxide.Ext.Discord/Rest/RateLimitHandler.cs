using System;
using System.Timers;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Rest
{
    /// <summary>
    /// Represents a global rate limit handler for a bot
    /// </summary>
    public class RateLimitHandler
    {
        /// <summary>
        /// How many requests have been made globally for the bot since the last 60 second period wiped it
        /// </summary>
        public int Global;

        private Timer _timer;
        private double _lastReset;
        private double _retryAfter;

        private const int MaxRequestsPerMinute = 110;
        private const int ResetInterval = ResetIntervalSeconds * 1000;
        private const int ResetIntervalSeconds = 60;

        /// <summary>
        /// Creates a new global rate limit for a bot
        /// </summary>
        public RateLimitHandler()
        {
            _timer = new Timer(ResetInterval);
            _timer.Elapsed += ResetGlobal;
            _timer.Start();
            _lastReset = Time.TimeSinceEpoch();
        }

        private void ResetGlobal(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                Global = 0;
                _lastReset = Time.TimeSinceEpoch();
            }
        }

        /// <summary>
        /// Called when an API request is fired
        /// </summary>
        public void FiredRequest()
        {
            lock (this)
            {
                Global += 1;
            }
        }

        /// <summary>
        /// Called if we receive a header notifying us of hitting the rate limit
        /// </summary>
        /// <param name="retryAfter">How long until we should retry API request again</param>
        public void ReachedRateLimit(double retryAfter)
        {
            Global = MaxRequestsPerMinute;
            _retryAfter = retryAfter;
        }

        /// <summary>
        /// Returns true if we have reached the global rate limit 
        /// </summary>
        public bool HasReachedRateLimit => Global >= MaxRequestsPerMinute;

        /// <summary>
        /// Returns how long until the current rate limit period will expire
        /// </summary>
        public double NextBucketReset => Math.Max(_lastReset + ResetIntervalSeconds, Time.TimeSinceEpoch() + _retryAfter) ;

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