using System.ComponentModel;
using System.Runtime.Serialization;

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
        [EnumMember(Value = "DEFAULT")]
        Default = 0,
        
        /// <summary>
        /// The message when a recipient is added
        /// </summary>
        [EnumMember(Value = "RECIPIENT_ADD")]
        RecipientAdd = 1,
        
        /// <summary>
        /// The message when a recipient is removed
        /// </summary>
        [EnumMember(Value = "RECIPIENT_REMOVE")]
        RecipientRemove = 2,
        
        /// <summary>
        /// The message when a user is called
        /// </summary>
        [EnumMember(Value = "CALL")]
        Call = 3,
        
        /// <summary>
        /// The message when a channel name is changed
        /// </summary>
        [EnumMember(Value = "CHANNEL_NAME_CHANGE")]
        ChannelNameChange = 4,
        
        /// <summary>
        /// The message when a channel icon is changed
        /// </summary>
        [EnumMember(Value = "CHANNEL_ICON_CHANGE")]
        ChannelIconChange = 5,
        
        /// <summary>
        /// The message when another message is pinned
        /// </summary>
        [EnumMember(Value = "CHANNEL_PINNED_MESSAGE")]
        ChannelPinnedMessage = 6,
        
        /// <summary>
        /// The message when a new member joined
        /// </summary>
        [EnumMember(Value = "GUILD_MEMBER_JOIN")]
        GuildMemberJoin = 7,
        
        /// <summary>
        ///  The message for when a user boosts a guild
        /// </summary>
        [EnumMember(Value = "USER_PREMIUM_GUILD_SUBSCRIPTION")]
        UserPremiumGuildSubscription = 8,
        
        /// <summary>
        /// The message for when a guild reaches Tier 1 of Nitro boosts
        /// </summary>
        [EnumMember(Value = "USER_PREMIUM_GUILD_SUBSCRIPTION_TIER_1")]
        UserPremiumGuildSubscriptionTier1 = 9,
        
        /// <summary>
        /// The message for when a guild reaches Tier 2 of Nitro boosts
        /// </summary>
        [EnumMember(Value = "USER_PREMIUM_GUILD_SUBSCRIPTION_TIER_2")]
        UserPremiumGuildSubscriptionTier2 = 10,
        
        /// <summary>
        /// The message for when a guild reaches Tier 3 of Nitro boosts
        /// </summary>
        [EnumMember(Value = "USER_PREMIUM_GUILD_SUBSCRIPTION_TIER_3")]
        UserPremiumGuildSubscriptionTier3 = 11,
        
        /// <summary>
        /// The message for when a news channel subscription is added to a text channel
        /// </summary>
        [EnumMember(Value = "ChannelFollowAdd")]
        ChannelFollowAdd = 12,
        
        /// <summary>
        /// The message for when a guild discovery is disqualified
        /// </summary>
        [EnumMember(Value = "GuildDiscoveryDisqualified")]
        GuildDiscoveryDisqualified = 14,
        
        /// <summary>
        /// The message for when a guild discovery is requalified
        /// </summary>
        [EnumMember(Value = "GuildDiscoveryRequalified")]
        GuildDiscoveryRequalified = 15,
        
        /// <summary>
        /// The message for grace period initial warning
        /// </summary>
        [EnumMember(Value = "GUILD_DISCOVERY_GRACE_PERIOD_INITIAL_WARNING")]
        GuildDiscoveryGracePeriodInitialWarning = 16,
        
        /// <summary>
        /// The message for grace period final warning
        /// </summary>
        [EnumMember(Value = "GUILD_DISCOVERY_GRACE_PERIOD_FINAL_WARNING")]
        GuildDiscoveryGracePeriodFinalWarning = 17,
        
        /// <summary>
        /// The message created a thread
        /// </summary>
        [EnumMember(Value = "THREAD_CREATED")]
        ThreadCreated = 18,
        
        /// <summary>
        /// The message for when the message is a reply
        /// </summary>
        [EnumMember(Value = "REPLY")]
        Reply = 19,
        
        /// <summary>
        /// The message for when the message is an application command
        /// </summary>
        [EnumMember(Value = "APPLICATION_COMMAND")]
        ApplicationCommand = 20,
        
        /// <summary>
        /// Starter message for a thread
        /// </summary>
        [EnumMember(Value = "THREAD_STARTER_MESSAGE")]
        ThreadStarterMessage = 20,
        
        /// <summary>
        /// Reminder for a guild invite
        /// </summary>
        [EnumMember(Value = "GUILD_INVITE_REMINDER")]
        GuildInviteReminder = 20
    }
}