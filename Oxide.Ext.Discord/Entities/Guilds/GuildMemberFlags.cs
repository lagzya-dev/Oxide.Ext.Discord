using System;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-flags">Guild Member Flags</a>
    /// </summary>
    [Flags]
    public enum GuildMemberFlags
    {
        /// <summary>
        /// No Flags
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Member has left and rejoined the guild
        /// Editable: False
        /// </summary>
        DidRejoin = 1 << 0,
        
        /// <summary>
        /// Member has completed onboarding
        /// Editable: False
        /// </summary>
        CompletedOnboarding = 1 << 1,
        
        /// <summary>
        /// Member is exempt from guild verification requirements
        /// Editable: True
        /// </summary>
        BypassesVerification = 1 << 2,
        
        /// <summary>
        /// Member has started onboarding
        /// Editable: False
        /// </summary>
        StartedOnboarding = 1 << 3,
    }
}