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
        /// See <see cref="LogLevel"/>
        /// </summary>
        public LogLevel LogLevel = LogLevel.Info;
        
        /// <summary>
        /// Intents that your bot needs to work
        /// See <see cref="BotIntents"/>
        /// </summary>
        public BotIntents Intents = BotIntents.None;
        
        /// <summary>
        /// If the client should be removed from the bot when the plugin unloads
        /// This will keep the client for this plugin active even when unloaded and will add the same client back when loaded back
        /// Recommend not changing this unless you know what you're doing
        /// </summary>
        public bool CloseOnUnload = true;
    }
}