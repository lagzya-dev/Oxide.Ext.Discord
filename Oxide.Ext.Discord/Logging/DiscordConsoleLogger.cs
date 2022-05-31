using System;
using System.Text;
using Oxide.Core;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a Console Logger for Discord
    /// </summary>
    internal class DiscordConsoleLogger
    {
        /// <summary>
        /// Adds a message to the server console
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        internal void AddMessage(DiscordLogLevel level, string message, Exception ex)
        {
            StringBuilder sb = DiscordPool.GetStringBuilder();
            sb.Append("[Discord Extension] [");
            sb.Append(EnumCache<DiscordLogLevel>.ToString(level));
            sb.Append("]: ");
            sb.Append(message);
            object[] args = ArrayPool.Get(0);
            switch (level)
            {
                case DiscordLogLevel.Debug:
                case DiscordLogLevel.Warning:
                    Interface.Oxide.LogWarning(sb.ToString(), args);
                    break;
                case DiscordLogLevel.Error:
                    Interface.Oxide.LogError(sb.ToString(), args);
                    break;
                case DiscordLogLevel.Exception:
                    Interface.Oxide.LogException(sb.ToString(), ex);
                    break;
                default:
                    Interface.Oxide.LogInfo(sb.ToString(), args);
                    break;
            }
            DiscordPool.FreeStringBuilder(ref sb);
            ArrayPool.Free(args);
        }
    }
}