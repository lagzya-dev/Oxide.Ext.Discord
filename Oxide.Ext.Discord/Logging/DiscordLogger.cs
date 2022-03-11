using System;
using Oxide.Core;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a discord extension logger
    /// </summary>
    internal class DiscordLogger : ILogger
    {
        private DiscordLogLevel _logLevel;
        
        /// <summary>
        /// Creates a new logger with the given log level
        /// </summary>
        /// <param name="logLevel">Log level of the logger</param>
        public DiscordLogger(DiscordLogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        /// <inheritdoc/>
        public void Log(DiscordLogLevel level, string message, Exception exception = null)
        {
            if (!IsLogging(level))
            {
                return;
            }

            string log = $"[Discord Extension] [{level.ToString()}]: {message}";
            switch (level)
            {
                case DiscordLogLevel.Debug:
                case DiscordLogLevel.Warning:
                    Interface.Oxide.LogWarning(log);
                    break;
                case DiscordLogLevel.Error:
                    Interface.Oxide.LogError(log);
                    break;
                case DiscordLogLevel.Exception:
                    Interface.Oxide.LogException(log, exception);
                    break;
                default:
                    Interface.Oxide.LogInfo(log);
                    break;
            }
        }

        /// <inheritdoc/>
        public void UpdateLogLevel(DiscordLogLevel level)
        {
            _logLevel = level;
        }

        /// <inheritdoc/>
        public bool IsLogging(DiscordLogLevel level)
        {
            return level >= _logLevel;
        }
    }
}