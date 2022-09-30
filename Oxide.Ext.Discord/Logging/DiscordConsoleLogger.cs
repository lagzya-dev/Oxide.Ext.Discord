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
        private readonly object[] _args = new object[2];
        private readonly object _sync = new object();
        private readonly string _format;

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
                _args[0] = EnumCache<DiscordLogLevel>.ToString(level);
                _args[1] = message;
                message = string.Format(_format, _args);
                
                switch (level)
                {
                    case DiscordLogLevel.Debug:
                    case DiscordLogLevel.Warning:
                        Interface.Oxide.LogWarning(message, _args);
                        break;
                    case DiscordLogLevel.Error:
                        Interface.Oxide.LogError(message, _args);
                        break;
                    case DiscordLogLevel.Exception:
                        Interface.Oxide.LogException(message, ex);
                        break;
                    default:
                        Interface.Oxide.LogInfo(message, _args);
                        break;
                }
            }
        }
    }
}