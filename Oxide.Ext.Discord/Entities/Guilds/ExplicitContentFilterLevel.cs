using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-object-explicit-content-filter-level">Explicit Content Filter Level</a>
    /// </summary>
    public enum ExplicitContentFilterLevel : byte
    {
        /// <summary>
        /// Disable explicit content filter
        /// </summary>
        [Description("DISABLED")]
        Disabled = 0,
        
        /// <summary>
        /// Filter for only members without roles
        /// </summary>
        [Description("MEMBERS_WITHOUT_ROLES")]
        MembersWithoutRoles = 1,
        
        /// <summary>
        /// Filter for all members
        /// </summary>
        [Description("ALL_MEMBERS")]
        AllMembers = 2
    }
}