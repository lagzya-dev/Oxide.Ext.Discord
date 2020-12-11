using System;

namespace Oxide.Ext.Discord.Logging
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogException(string message, Exception ex);
    }
}