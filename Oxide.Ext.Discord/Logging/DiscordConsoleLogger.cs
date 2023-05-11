using System;
using Oxide.Core;
using Oxide.Ext.Discord.Cache;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a Console Logger for Discord
    /// </summary>
    internal class DiscordConsoleLogger
    {
        private readonly object _sync = new object();
        private readonly string _format;
        private readonly object[] _empty = Array.Empty<object>();

        public DiscordConsoleLogger(string format)
        {
            _format = format;
        }
        
        /// <summary>
        /// Adds a message to the server console
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        internal void AddMessage(DiscordLogLevel level, string message, Exception ex)
        {
            lock (_sync)
            {
                message = string.Format(_format, EnumCache<DiscordLogLevel>.Instance.ToString(level), message);
                
                switch (level)
                {
                    case DiscordLogLevel.Debug:
                    case DiscordLogLevel.Warning:
                        Interface.Oxide.LogWarning(message, _empty);
                        break;
                    case DiscordLogLevel.Error:
                        Interface.Oxide.LogError(message, _empty);
                        break;
                    case DiscordLogLevel.Exception:
                        Interface.Oxide.LogException(message, ex);
                        break;
                    default:
                        Interface.Oxide.LogInfo(message, _empty);
                        break;
                }
            }
        }
    }
}