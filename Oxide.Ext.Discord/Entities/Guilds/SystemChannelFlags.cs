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
        SuppressPremiumSubscriptions = 1 << 1,
        
        /// <summary>
        /// Suppress server setup tips
        /// </summary>
        [Description("SUPPRESS_GUILD_REMINDER_NOTIFICATIONS")]
        SuppressGuildReminderNotifications = 1 << 2,
        
        /// <summary>
        /// Hide member join sticker reply buttons
        /// </summary>
        [Description("SUPPRESS_JOIN_NOTIFICATION_REPLIES")]
        SuppressJoinNotificationReplies = 1 << 3,
        
        /// <summary>
        /// Suppress role subscription purchase and renewal notifications
        /// </summary>
        [Description("SUPPRESS_ROLE_SUBSCRIPTION_PURCHASE_NOTIFICATIONS")]
        SuppressRoleSubscriptionPurchaseNotifications = 1 << 4,
        
        /// <summary>
        /// Hide role subscription sticker reply buttons
        /// </summary>
        [Description("SUPPRESS_ROLE_SUBSCRIPTION_PURCHASE_NOTIFICATION_REPLIES")]
        SuppressRoleSubscriptionPurchaseNotificationReplies = 1 << 5,
    }
}