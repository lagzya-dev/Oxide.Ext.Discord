using Oxide.Ext.Discord.Entities.Gateway;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Connections
{
    public abstract class BaseConnectSettings
    {
        /// <summary>
        /// API token for the bot
        /// </summary>
        public string ApiToken { get; set; }
        
        /// <summary>
        /// Discord Extension Logging Level.
        /// See <see cref="LogLevel"/>
        /// </summary>
        public DiscordLogLevel LogLevel { get; set; }
        
        /// <summary>
        /// Intents that your bot needs to work
        /// See <see cref="GatewayIntents"/>
        /// </summary>
        public GatewayIntents Intents { get; set; }
        
        protected BaseConnectSettings(){}
        
        protected BaseConnectSettings(string apiToken, GatewayIntents intents, DiscordLogLevel logLevel)
        {
            ApiToken = apiToken;
            Intents = intents;
            LogLevel = logLevel;
        }
        
        /// <summary>
        /// Returns if the settings has the given intents
        /// </summary>
        /// <param name="intents">Intents to be compared against</param>
        /// <returns>True if settings has the given intents; False otherwise</returns>
        public bool HasIntents(GatewayIntents intents)
        {
            return (Intents & intents) == intents;
        }
        
        /// <summary>
        /// Returns if the settings has any intent specified
        /// </summary>
        /// <param name="intents">Intents to compare against</param>
        /// <returns>True if settings has at least one of the given intents</returns>
        public bool HasAnyIntent(GatewayIntents intents)
        {
            return (Intents & intents) != 0;
        }
    }
}