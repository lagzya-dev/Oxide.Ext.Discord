using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#message-object-message-types">Message Types</a>
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The default message type
        /// </summary>
        [Description("DEFAULT")]
        Default = 0,
        
        /// <summary>
        /// The message when a recipient is added
        /// </summary>
        [Description("RECIPIENT_ADD")]
        RecipientAdd = 1,
        
        /// <summary>
        /// The message when a recipient is removed
        /// </summary>
        [Description("RECIPIENT_REMOVE")]
        RecipientRemove = 2,
        
        /// <summary>
        /// The message when a user is called
        /// </summary>
        [Description("CALL")]
        Call = 3,
        
        /// <summary>
        /// The message when a channel name is changed
        /// </summary>
        [Description("CHANNEL_NAME_CHANGE")]
        ChannelNameChange = 4,
        
        /// <summary>
        /// The message when a channel icon is changed
        /// </summary>
        [Description("CHANNEL_ICON_CHANGE")]
        ChannelIconChange = 5,
        
        /// <summary>
        /// The message when another message is pinned
        /// </summary>
        [Description("CHANNEL_PINNED_MESSAGE")]
        ChannelPinnedMessage = 6,
        
        /// <summary>
        /// The message when a new member joined
        /// </summary>
        [Description("GUILD_MEMBER_JOIN")]
        GuildMemberJoin = 7,
        
        /// <summary>
        ///  The message for when a user boosts a guild
        /// </summary>
        [Description("USER_PREMIUM_GUILD_SUBSCRIPTION")]
        UserPremiumGuildSubscription = 8,
        
        /// <summary>
        /// The message for when a guild reaches Tier 1 of Nitro boosts
        /// </summary>
        [Description("USER_PREMIUM_GUILD_SUBSCRIPTION_TIER_1")]
        UserPremiumGuildSubscriptionTier1 = 9,
        
        /// <summary>
        /// The message for when a guild reaches Tier 2 of Nitro boosts
        /// </summary>
        [Description("USER_PREMIUM_GUILD_SUBSCRIPTION_TIER_2")]
        UserPremiumGuildSubscriptionTier2 = 10,
        
        /// <summary>
        /// The message for when a guild reaches Tier 3 of Nitro boosts
        /// </summary>
        [Description("USER_PREMIUM_GUILD_SUBSCRIPTION_TIER_3")]
        UserPremiumGuildSubscriptionTier3 = 11,
        
        /// <summary>
        /// The message for when a news channel subscription is added to a text channel
        /// </summary>
        [Description("ChannelFollowAdd")]
        ChannelFollowAdd = 12,
        
        /// <summary>
        /// The message for when a guild discovery is disqualified
        /// </summary>
        [Description("GuildDiscoveryDisqualified")]
        GuildDiscoveryDisqualified = 14,
        
        /// <summary>
        /// The message for when a guild discovery is requalified
        /// </summary>
        [Description("GuildDiscoveryRequalified")]
        GuildDiscoveryRequalified = 15,
        
        /// <summary>
        /// The message for grace period initial warning
        /// </summary>
        [Description("GUILD_DISCOVERY_GRACE_PERIOD_INITIAL_WARNING")]
        GuildDiscoveryGracePeriodInitialWarning = 16,
        
        /// <summary>
        /// The message for grace period final warning
        /// </summary>
        [Description("GUILD_DISCOVERY_GRACE_PERIOD_FINAL_WARNING")]
        GuildDiscoveryGracePeriodFinalWarning = 17,
        
        /// <summary>
        /// The message for when the message is a reply
        /// </summary>
        [Description("REPLY")]
        Reply = 19,
        
        /// <summary>
        /// The message for when the message is an application command
        /// </summary>
        [Description("APPLICATION_COMMAND")]
        ApplicationCommand = 20
    }
}