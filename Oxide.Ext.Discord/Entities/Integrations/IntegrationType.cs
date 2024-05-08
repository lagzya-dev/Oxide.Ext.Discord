using System.ComponentModel;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Json;

namespace Oxide.Ext.Discord.Entities
{
    /// <summary>
    /// Represents Integrations types
    /// </summary>
    [JsonConverter(typeof(DiscordEnumConverter))]
    public enum IntegrationType : byte
    {
        /// <summary>
        /// Integration is for twitch
        /// </summary>
        [Description("twitch")] 
        Twitch,
        
        /// <summary>
        /// Integration is for youtube
        /// </summary>
        [Description("youtube")] 
        Youtube,
        
        /// <summary>
        /// integration is for discord
        /// </summary>
        [Description("discord")] 
        Discord,
        
        /// <summary>
        /// integration is for guild subscription
        /// </summary>
        [Description("guild_subscription")] 
        GuildSubscription
    }
}