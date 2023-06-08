using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#message-object-message-types">Message Types</a>
    /// </summary>
    public enum MessageType : byte
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
        [Description("USER_JOIN")]
        UserJoin = 7,
        
        /// <summary>
        ///  The message for when a user boosts a guild
        /// </summary>
        [Description("GUILD_BOOST")]
        GuildBoost = 8,
        
        /// <summary>
        /// The message for when a guild reaches Tier 1 of Nitro boosts
        /// </summary>
        [Description("GUILD_BOOST_TIER_1")]
        GuildBoostTier1 = 9,
        
        /// <summary>
        /// The message for when a guild reaches Tier 2 of Nitro boosts
        /// </summary>
        [Description("GUILD_BOOST_TIER_2")]
        GuildBoostTier2 = 10,
        
        /// <summary>
        /// The message for when a guild reaches Tier 3 of Nitro boosts
        /// </summary>
        [Description("GUILD_BOOST_TIER_3")]
        GuildBoostTier3 = 11,
        
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
        [Description("GUILD_DISCOVERY_REQUALIFIED")]
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
        /// The message created a thread
        /// </summary>
        [Description("THREAD_CREATED")]
        ThreadCreated = 18,
        
        /// <summary>
        /// The message for when the message is a reply
        /// </summary>
        [Description("REPLY")]
        Reply = 19,
        
        /// <summary>
        /// The message for when the message is an application command
        /// </summary>
        [Description("CHAT_INPUT_COMMAND")]
        ChatInputCommand = 20,
        
        /// <summary>
        /// Starter message for a thread
        /// </summary>
        [Description("THREAD_STARTER_MESSAGE")]
        ThreadStarterMessage = 21,

        /// <summary>
        /// Reminder for a guild invite
        /// </summary>
        [Description("GUILD_INVITE_REMINDER")]
        GuildInviteReminder = 22,

        /// <summary>
        /// Reminder for a guild invite
        /// </summary>
        [Description("CONTEXT_MENU_COMMAND")]
        ContextMenuCommand = 23,

        /// <summary>
        /// Message is an auto mod action
        /// </summary>
        [Description("AUTO_MODERATION_ACTION")]
        AutoModerationAction = 24,
        
        /// <summary>
        /// Message is a role subscription purchase
        /// </summary>
        [Description("ROLE_SUBSCRIPTION_PURCHASE")]
        RoleSubscriptionPurchase = 25,
        
        /// <summary>
        /// Message is a interaction premium upsell
        /// </summary>
        [Description("INTERACTION_PREMIUM_UPSELL")]
        InteractionPremiumUpsell = 26,
        
        /// <summary>
        /// Message is a stage start
        /// </summary>
        [Description("STAGE_START")]
        StageStart = 27,
        
        /// <summary>
        /// Message is a stage end
        /// </summary>
        [Description("STAGE_END")]
        StageEnd = 28,
        
        /// <summary>
        /// Message is a stage speaker
        /// </summary>
        [Description("STAGE_SPEAKER")]
        StageSpeaker = 29,
        
        /// <summary>
        /// Message is a stage raise hand
        /// </summary>
        [Description("STAGE_RAISE_HAND")]
        StageRaiseHand = 30,
        
        /// <summary>
        /// Message is a stage topic
        /// </summary>
        [Description("STAGE_TOPIC")]
        StageTopic = 31,
        
        /// <summary>
        /// Message is a Guild Application Premium Subscription
        /// </summary>
        [Description("GUILD_APPLICATION_PREMIUM_SUBSCRIPTION")]
        GuildApplicationPremiumSubscription = 32,
    }
}