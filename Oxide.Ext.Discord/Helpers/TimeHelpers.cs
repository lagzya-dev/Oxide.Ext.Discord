using System;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Helper methods relating to time
    /// </summary>
    public static class TimeHelpers
    {
        /// <summary>
        /// DateTime since linux epoch
        /// </summary>
        public static readonly DateTimeOffset LinuxEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        /// <summary>
        /// Gets how many seconds since the linux epoch
        /// </summary>
        /// <returns>Seconds since linux epoch</returns>
        public static double TimeSinceEpoch() => (DateTimeOffset.UtcNow - LinuxEpoch).TotalSeconds;

        /// <summary>
        /// Converts the seconds since linux epoch to a DateTime
        /// </summary>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffsetFromMilliseconds(this long milliseconds) => LinuxEpoch.AddMilliseconds(milliseconds);
        
        /// <summary>
        /// Converts the seconds since linux epoch to a DateTime
        /// </summary>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffsetFromSeconds(this double seconds) => LinuxEpoch.AddSeconds(seconds);
        
        /// <summary>
        /// Converts the seconds since linux epoch to a DateTime
        /// </summary>
        /// <returns></returns>
        public static double SecondsUntilTime(this DateTimeOffset time) => (time - DateTimeOffset.UtcNow).TotalSeconds;

        /// <summary>
        /// Gets the time since the linux epoch and the given date time
        /// </summary>
        /// <param name="date">DateTime to get total second for</param>
        /// <returns>Total seconds since linux epoch for date time</returns>
        public static double ToUnixTimeStamp(this DateTimeOffset date) => (date - LinuxEpoch).TotalSeconds;
    }
}
