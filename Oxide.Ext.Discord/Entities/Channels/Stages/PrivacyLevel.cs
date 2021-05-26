
using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Channels.Stages
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/stage-instance#privacy-level">Stage Privacy Level</a> within Discord.
    /// </summary>
    public enum PrivacyLevel
    {
        /// <summary>
        /// The Stage instance is visible publicly, such as on Stage discovery.
        /// </summary>
        [Description("PUBLIC")]
        Public = 1,
        
        /// <summary>
        ///  The Stage instance is visible to only guild members.
        /// </summary>
        [Description("GUILD_ONLY")]
        GuildOnly = 2
    }
}