using System;
using Oxide.Core;
namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a Console Logger for Discord
    /// </summary>
    internal class DiscordConsoleLogger
    {
        private const string ConsoleLogFormat = "[Discord Extension] [{0}]: {1}";
        
        /// <summary>
        /// Adds a message to the server console
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        internal void AddMessage(DiscordLogLevel level, string message, Exception ex)
        {
            object[] args = ArrayPool.Get(2);
            args[0] = level.ToString();
            args[1] = message;
            string log = string.Format(ConsoleLogFormat, args);
            ArrayPool.Free(args);
            args = ArrayPool.Get(0);
            switch (level)
            {
                case DiscordLogLevel.Debug:
                case DiscordLogLevel.Warning:
                    Interface.Oxide.LogWarning(log, args);
                    break;
                case DiscordLogLevel.Error:
                    Interface.Oxide.LogError(log, args);
                    break;
                case DiscordLogLevel.Exception:
                    Interface.Oxide.LogException(log, ex);
                    break;
                default:
                    Interface.Oxide.LogInfo(log, args);
                    break;
            }
            ArrayPool.Free(args);
        }
    }
}