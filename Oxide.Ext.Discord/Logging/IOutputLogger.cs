using System;

namespace Oxide.Ext.Discord.Logging
{
    public interface IOutputLogger
    {
        void AddMessage(DiscordLogLevel level, string log, object[] args, Exception ex);

        void OnShutdown();
    }
}