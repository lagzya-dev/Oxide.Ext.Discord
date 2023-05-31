using System.Text;
using Oxide.Ext.Discord.Entities.Gateway;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Connections
{
    public abstract class BaseConnectSettings
    {
        /// <summary>
        /// API token for the bot
        /// </summary>
        public readonly string ApiToken;
        
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

        protected BaseConnectSettings(){}
        
        protected BaseConnectSettings(string apiToken, GatewayIntents intents, DiscordLogLevel logLevel)
        {
            ApiToken = apiToken;
            Intents = intents;
            LogLevel = logLevel;
        }
        
        protected string GenerateHiddenToken()
        {
            StringBuilder sb = DiscordPool.Internal.GetStringBuilder();

            int last = ApiToken.LastIndexOf('.') + 1;
            sb.Append(ApiToken.Substring(0, last));
            sb.Append('#', ApiToken.Length - last);

            return DiscordPool.Internal.FreeStringBuilderToString(sb);
        }
    }
}