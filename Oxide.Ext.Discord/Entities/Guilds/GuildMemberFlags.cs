using System;
using Oxide.Ext.Discord.Attributes;

namespace Oxide.Ext.Discord.Entities;

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
    [DiscordEnum("DID_REJOIN")]
    DidRejoin = 1 << 0,
        
    /// <summary>
    /// Member has completed onboarding
    /// Editable: False
    /// </summary>
    [DiscordEnum("COMPLETED_ONBOARDING")]
    CompletedOnboarding = 1 << 1,
        
    /// <summary>
    /// Member is exempt from guild verification requirements
    /// Editable: True
    /// </summary>
    [DiscordEnum("BYPASSES_VERIFICATION")]
    BypassesVerification = 1 << 2,
        
    /// <summary>
    /// Member has started onboarding
    /// Editable: False
    /// </summary>
    [DiscordEnum("STARTED_ONBOARDING")]
    StartedOnboarding = 1 << 3,
}