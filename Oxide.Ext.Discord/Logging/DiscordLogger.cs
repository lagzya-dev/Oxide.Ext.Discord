using System;
using Oxide.Ext.Discord.Configuration;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a discord extension logger
    /// </summary>
    internal class DiscordLogger : ILogger
    {
        private DiscordLogLevel _logLevel;
        private readonly DiscordLoggingConfig _config;
        internal static readonly DiscordConsoleLogger ConsoleLogger = new DiscordConsoleLogger();
        internal static readonly DiscordFileLogger FileLogger = new DiscordFileLogger();
        
        /// <summary>
        /// Creates a new logger with the given log level
        /// </summary>
        /// <param name="logLevel">Log level of the logger</param>
        public DiscordLogger(DiscordLogLevel logLevel)
        {
            _logLevel = logLevel;
            _config = DiscordExtension.DiscordConfig.Logging;
        }

        /// <inheritdoc/>
        public void Log(DiscordLogLevel level, string message, Exception exception = null)
        {
            if (IsConsoleLogging(level))
            {
                ConsoleLogger.AddMessage(level, message, exception);
            }

            if (IsFileLogging(level))
            {
                FileLogger.AddMessage(level, message, exception);
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
            return level >= _logLevel && (level >= _config.ConsoleLogLevel || level >= _config.FileLogLevel);
        }
        
        public bool IsConsoleLogging(DiscordLogLevel level)
        {
            return level >= _logLevel && level >= _config.ConsoleLogLevel;
        }
        
        public bool IsFileLogging(DiscordLogLevel level)
        {
            return level >= _logLevel && level >= _config.FileLogLevel;
        }
    }
}