using System;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Factory
{
    /// <summary>
    /// Generates a unique snowflake ID
    /// </summary>
    public class SnowflakeIdFactory : Singleton<SnowflakeIdFactory>
    {
        private DateTimeOffset _currentTime;
        private ulong _increment;
        private readonly TimeSpan DiffCompare = TimeSpan.FromMilliseconds(1);
        private readonly object Sync = new object();

        private SnowflakeIdFactory() { }
        
        /// <summary>
        /// Returns the generated snowflake ID
        /// </summary>
        /// <returns></returns>
        public Snowflake Generate()
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