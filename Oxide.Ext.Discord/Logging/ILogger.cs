using System;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents an interface for a logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Updates the log level for the current logger
        /// </summary>
        /// <param name="level">Level to update the logger to</param>
        void UpdateLogLevel(DiscordLogLevel level);

        /// <summary>
        /// Returns true if the logger is logging for the passed log level
        /// </summary>
        /// <param name="level">Log Level to check</param>
        /// <returns>True if the logger is logging for the given log level</returns>
        bool IsLogging(DiscordLogLevel level);

        /// <summary>
        /// Returns if the logger is logging for server console
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        bool IsConsoleLogging(DiscordLogLevel level);

        /// <summary>
        /// Returns if the logger is logging for file logger
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        bool IsFileLogging(DiscordLogLevel level);

        /// <summary>
        /// Log the message with the specified level
        /// </summary>
        /// <param name="level">Log Level for the message</param>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception for the log</param>
        void Log(DiscordLogLevel level, string message, Exception exception = null);
    }
}