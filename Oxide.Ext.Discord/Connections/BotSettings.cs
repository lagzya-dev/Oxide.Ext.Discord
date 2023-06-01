using System;
using System.Text;
using System.Text.RegularExpressions;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Connections
{
    public class BotSettings : BaseConnectSettings
    {
        public readonly string HiddenToken;
        
        public readonly Snowflake ApplicationId;
        
        internal readonly DateTimeOffset CreationDate;

        public BotSettings(ClientSettings settings, BotToken token) : base(settings.ApiToken, settings.Intents, settings.LogLevel)
        {
            HiddenToken = token.HiddenToken;
            ApplicationId = token.ApplicationId;
            CreationDate = token.CreationDate;
        }
    }
}