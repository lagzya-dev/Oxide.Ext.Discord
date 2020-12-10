using System;
using System.Timers;
using Oxide.Core;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.REST
{
    public class BotRateLimit
    {
        public int Global;

        private Timer _timer;

        private const int MaxRequestsPerMinute = 110;
        private const int ResetInterval = 60 * 1000;

        private DateTime _lastReset;

        public BotRateLimit()
        {
            _timer = new Timer(ResetInterval);
            _timer.Elapsed += ResetGlobal;
            _lastReset = DateTime.UtcNow;
        }

        private void ResetGlobal(object sender, ElapsedEventArgs e)
        {
            Global = 0;
            _lastReset = DateTime.UtcNow;
        }

        public void FiredRequest()
        {
            Global += 1;
        }

        public void ReachedRateLimit()
        {
            Interface.Oxide.LogError($"[Discord Extension] [Error] Bot has reached rate limit... Remaining Requests: {MaxRequestsPerMinute - Global}");
            Global = MaxRequestsPerMinute;
        }

        public bool HasReachedRateLimit => Global >= MaxRequestsPerMinute;

        public int NextBucketReset => Time.TimeSinceEpoch(_lastReset + TimeSpan.FromSeconds(ResetInterval));

        public void CloseBucket()
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