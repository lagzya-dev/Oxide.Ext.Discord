using System;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Helper methods relating to time
    /// </summary>
    public static class TimeHelpers
    {
        /// <summary>
        /// Gets how many seconds since the linux epoch
        /// </summary>
        /// <returns>Seconds since linux epoch</returns>
        public static long SecondsSinceEpoch() => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        
        /// <summary>
        /// Gets how many seconds since the linux epoch
        /// </summary>
        /// <returns>Seconds since linux epoch</returns>
        public static long MillisecondsSinceEpoch() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        /// <summary>
        /// Converts the milliseconds since linux epoch to a <see cref="DateTimeOffset"/>
        /// </summary>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffsetFromMilliseconds(this long milliseconds) => DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);

        /// <summary>
        /// Converts the seconds since linux epoch to a DateTime
        /// </summary>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffsetFromSeconds(this double seconds) => DateTimeOffset.FromUnixTimeSeconds(0).AddSeconds(seconds);
    }
}
