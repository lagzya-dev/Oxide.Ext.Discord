using System;

namespace Oxide.Ext.Discord.Logging
{
    internal class DiscordLogHandler
    {
        private readonly DiscordConsoleLogger _consoleLogger;
        private readonly DiscordFileLogger _fileLogger;

        public DiscordLogHandler(string pluginName, IDiscordLoggingConfig config, bool isExtension)
        {
            _consoleLogger = isExtension || config.ConsoleLogLevel != DiscordLogLevel.Off ? new DiscordConsoleLogger(config.ConsoleLogFormat) : null;
            _fileLogger = isExtension || config.FileLogLevel != DiscordLogLevel.Off ? new DiscordFileLogger(pluginName, config.FileLogFormat) : null;
        }

        public void LogConsole(DiscordLogLevel level, string message, Exception exception = null)
        {
            _consoleLogger.AddMessage(level, message, exception);
        }

        public void LogFile(DiscordLogLevel level, string message, Exception exception = null)
        {
            _fileLogger.AddMessage(level, message, exception);
        }

        public void Shutdown()
        {
            _fileLogger.OnShutdown();
        }
    }
}