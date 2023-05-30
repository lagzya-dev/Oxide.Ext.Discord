using System;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Factory
{
    /// <summary>
    /// Generates a unique snowflake ID
    /// </summary>
    public sealed class SnowflakeIdFactory : Singleton<SnowflakeIdFactory>
    {
        private DateTimeOffset _currentTime;
        private ulong _increment;
        private readonly TimeSpan _diffCompare = TimeSpan.FromMilliseconds(1);
        private readonly object _sync = new object();

        private SnowflakeIdFactory() { }
        
        /// <summary>
        /// Returns the generated snowflake ID
        /// </summary>
        /// <returns></returns>
        public Snowflake Generate()
        {
            lock (_sync)
            {
                if (DateTimeOffset.UtcNow - _currentTime >= _diffCompare)
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