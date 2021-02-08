using System;

namespace Oxide.Ext.Discord.Helpers
{
    public static class Time
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly DateTimeOffset DiscordEpoch = new DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero);

        public static int TimeSinceEpoch() => (int)(DateTime.UtcNow - Epoch).TotalSeconds;
        public static double TimeSinceEpoch(DateTime time) => (time - Epoch).TotalSeconds;
        public static DateTime ToDateTime(this int timestamp) => Epoch.AddSeconds(timestamp);
        public static int ToUnixTimeStamp(this DateTime dt) => (int) (dt - Epoch).TotalSeconds;
    }
}
