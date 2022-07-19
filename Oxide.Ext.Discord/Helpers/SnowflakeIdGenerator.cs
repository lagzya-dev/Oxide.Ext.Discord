using System;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Helpers
{
    /// <summary>
    /// Generates a unique snowflake ID
    /// </summary>
    public static class SnowflakeIdGenerator
    {
        private static DateTimeOffset _currentTime;
        private static ulong _increment;
        private static readonly TimeSpan DiffCompare = TimeSpan.FromMilliseconds(1);
        private static readonly object Sync = new object();
        
        /// <summary>
        /// Returns the generated snowflake ID
        /// </summary>
        /// <returns></returns>
        public static Snowflake Generate()
        {
            lock (Sync)
            {
                if (DateTimeOffset.UtcNow - _currentTime >= DiffCompare)
                {
                    _currentTime = DateTimeOffset.UtcNow;
                    _increment = 0;
                }

                Snowflake id = new Snowflake(_currentTime, _increment);
                _increment++;
                return id;
            }
        }
    }
}