using System;
using System.Text;
using Oxide.Core;
using Oxide.Ext.Discord.Cache;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a Console Logger for Discord
    /// </summary>
    internal class DiscordConsoleLogger
    {
        private readonly StringBuilder _sb = new StringBuilder();
        private readonly object _sync = new object();
        
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
                _sb.Length = 0;
                _sb.Append("[Discord Extension] [");
                _sb.Append(EnumCache<DiscordLogLevel>.ToString(level));
                _sb.Append("]: ");
                _sb.Append(message);
                object[] args = ArrayPool.Get(0);
                switch (level)
                {
                    case DiscordLogLevel.Debug:
                    case DiscordLogLevel.Warning:
                        Interface.Oxide.LogWarning(_sb.ToString(), args);
                        break;
                    case DiscordLogLevel.Error:
                        Interface.Oxide.LogError(_sb.ToString(), args);
                        break;
                    case DiscordLogLevel.Exception:
                        Interface.Oxide.LogException(_sb.ToString(), ex);
                        break;
                    default:
                        Interface.Oxide.LogInfo(_sb.ToString(), args);
                        break;
                }
                ArrayPool.Free(args);
            }
        }
    }
}