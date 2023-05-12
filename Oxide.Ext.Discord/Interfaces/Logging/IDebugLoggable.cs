using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Interfaces.Logging
{
    public interface IDebugLoggable
    {
        void LogDebug(DebugLogger logger);
    }
}