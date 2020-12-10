namespace Oxide.Ext.Discord.Helpers
{
    using System;

    public static class Time
    {
        public static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static int TimeSinceEpoch() => (int)Math.Floor((DateTime.UtcNow - Epoch).TotalSeconds);
        public static int TimeSinceEpoch(DateTime time) => (int)Math.Floor((time - Epoch).TotalSeconds);
    }
}
