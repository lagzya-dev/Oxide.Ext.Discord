using System;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Interfaces
{
    public interface IOutputLogger
    {
        void AddMessage(DiscordLogLevel level, string log, object[] args, Exception ex);

        void OnShutdown();
    }
}