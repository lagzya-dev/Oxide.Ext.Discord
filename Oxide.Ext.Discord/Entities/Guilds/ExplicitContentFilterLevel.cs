using System.Runtime.Serialization;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-object-explicit-content-filter-level">Explicit Content Filter Level</a>
    /// </summary>
    public enum ExplicitContentFilterLevel
    {
        /// <summary>
        /// Disable explicit content filter
        /// </summary>
        [EnumMember(Value = "DISABLED")]
        Disabled = 0,
        
        /// <summary>
        /// Filter for only members without roles
        /// </summary>
        [EnumMember(Value = "MEMBERS_WITHOUT_ROLES")]
        MembersWithoutRoles = 1,
        
        /// <summary>
        /// Filter for all members
        /// </summary>
        [EnumMember(Value = "ALL_MEMBERS")]
        AllMembers = 2
    }
}