using System.ComponentModel;
using System.Runtime.Serialization;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-nsfw-level">Guild NSFW Level</a>
    /// </summary>
    public enum GuildNsfwLevel
    {
        /// <summary>
        /// Default NSFW Level
        /// </summary>
        [EnumMember(Value = "DEFAULT")]
        Default = 0,
        
        /// <summary>
        /// Guild is explicitly NSFW
        /// </summary>
        [EnumMember(Value = "EXPLICIT")]
        Explicit = 1,
        
        /// <summary>
        /// Guild is safe from NSFW
        /// </summary>
        [EnumMember(Value = "SAFE")]
        Safe = 2,
        
        /// <summary>
        /// Guild is age restricted
        /// </summary>
        [EnumMember(Value = "AGE_RESTRICTED")]
        AgeRestricted = 3
    }
}