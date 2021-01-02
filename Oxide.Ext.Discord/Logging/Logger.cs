using System;
using Oxide.Core;

namespace Oxide.Ext.Discord.Logging
{
    public class Logger<T> : ILogger
    {
        private readonly LogLevel _logLevel;
        
        public Logger(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public void LogDebug(string message)
        {
            Log(LogLevel.Debug, message);
        }
        
        public void LogInfo(string message)
        {
            Log(LogLevel.Info, message);
        }
        
        public void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }
        
        public void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }
        
        public void LogException(string message, Exception ex)
        {
            Log(LogLevel.Exception, message, ex);
        }

        private void Log(LogLevel level, string message, object data = null)
        {
            if (level < _logLevel)
            {
                return;
            }

            string log;
            if (level == LogLevel.Error || level == LogLevel.Exception)
            {
                log = $"[Discord Extension] [{typeof(T).Name}] [{level}]: {message}";
            }
            else
            {
                log = $"[Discord Extension] [{level}]: {message}";
            }

            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Warning:
                    Interface.Oxide.LogWarning(log);
                    break;
                case LogLevel.Error:
                    Interface.Oxide.LogError(log);
                    break;
                case LogLevel.Exception:
                    Interface.Oxide.LogException(log, (Exception)data);
                    break;
                default:
                    Interface.Oxide.LogInfo(log);
                    break;
            }
        }
    }
}