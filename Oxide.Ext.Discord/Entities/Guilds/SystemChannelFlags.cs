using System;
using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/guild#guild-object-system-channel-flags">System Channel Flags</a>
    /// </summary>
    [Flags]
    public enum SystemChannelFlags
    {
        /// <summary>
        /// Suppress member join notifications
        /// </summary>
        [Description("SUPPRESS_JOIN_NOTIFICATIONS")]
        SuppressJoinNotifications = 1 << 0,
        
        /// <summary>
        /// Suppress server boost notifications
        /// </summary>
        [Description("SUPPRESS_PREMIUM_SUBSCRIPTIONS")]
        SuppressPremiumSubscriptions = 1 << 1
    }
}