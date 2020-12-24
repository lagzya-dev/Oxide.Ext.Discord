using System;

namespace Oxide.Ext.Discord.Helpers
{
    public static class Time
    {
        private static DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static int TimeSinceEpoch() => (int)(DateTime.UtcNow - _epoch).TotalSeconds;
        public static DateTime ToDateTime(this int timestamp) => _epoch.AddSeconds(timestamp);
        public static int ToUnixTimeStamp(this DateTime dt) => (int) (dt - _epoch).TotalSeconds;
    }
}
