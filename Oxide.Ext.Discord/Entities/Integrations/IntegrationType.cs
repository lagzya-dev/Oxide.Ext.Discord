using System.Runtime.Serialization;

namespace Oxide.Ext.Discord.Entities.Integrations
{
    /// <summary>
    /// Represents Integrations types
    /// </summary>
    public enum IntegrationType
    {
        /// <summary>
        /// Integration is for twitch
        /// </summary>
        [EnumMember(Value = "twitch")] 
        Twitch,
        
        /// <summary>
        /// Integration is for youtube
        /// </summary>
        [EnumMember(Value = "youtube")] 
        Youtube,
        
        /// <summary>
        /// integration is for discord
        /// </summary>
        [EnumMember(Value = "discord")] 
        Discord
    }
}