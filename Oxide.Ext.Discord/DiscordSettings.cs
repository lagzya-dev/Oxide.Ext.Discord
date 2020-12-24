using System;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord
{
    public class DiscordSettings
    {
        public string ApiToken;

        [Obsolete]
        public bool Debugging
        {
            get => LogLevel < LogLevel.Warning;
            set => LogLevel = value ? LogLevel.Debug : LogLevel.Info;
        }

        public LogLevel LogLevel;
        
        public BotIntents Intents = BotIntents.All;
    }
}