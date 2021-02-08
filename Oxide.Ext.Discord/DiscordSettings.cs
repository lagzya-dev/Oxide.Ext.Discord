using Oxide.Ext.Discord.Entities.Gatway;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord
{
    /// <summary>
    /// Represents settings used to connect to discord
    /// </summary>
    public class DiscordSettings
    {
        /// <summary>
        /// API token for the bot
        /// </summary>
        public string ApiToken;

        /// <summary>
        /// Discord Extension Logging Level.
        /// </summary>
        public LogLevel LogLevel = LogLevel.Info;
        
        /// <summary>
        /// Intents that your bot needs to work
        /// </summary>
        public BotIntents Intents = BotIntents.None;
        
        /// <summary>
        /// If the client should be shutdown on plugin unload.
        /// Recommend not changing this unless you know what you're doing
        /// </summary>
        public bool CloseOnUnload = true;
    }
}