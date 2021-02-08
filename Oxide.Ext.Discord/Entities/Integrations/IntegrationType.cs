using System.ComponentModel;

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
        Discord
    }
}