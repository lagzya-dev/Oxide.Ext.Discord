using System;
using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Permissions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/permissions#permissions-bitwise-permission-flags">Permission Flags</a> for user or role
    /// </summary>
    [Flags]
    public enum PermissionFlags : ulong
    {
        /// <summary>
        /// Represents No Permissions
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Represents all possible Permissions Flags
        /// </summary>
        All = ulong.MaxValue,
        
        /// <summary>
        /// Allows creation of instant invites
        /// Channel Type (Text, Voice, Stage)
        /// </summary>
        [Description("CREATE_INSTANT_INVITE")]
        CreateInstantInvite = 1 << 0,
        
        /// <summary>
        /// Allows kicking members
        /// </summary>
        [Description("KICK_MEMBERS")]
        KickMembers = 1 << 1,
        
        /// <summary>
        /// Allows banning members
        /// </summary>
        [Description("BAN_MEMBERS")]
        BanMembers = 1 << 2,
        
        /// <summary>
        /// Allows all permissions and bypasses channel permission overwrites
        /// </summary>
        [Description("ADMINISTRATOR")]
        Administrator = 1 << 3,
        
        /// <summary>
        /// Allows management and editing of channels
        /// Channel Type (Text, Voice, Stage)
        /// </summary>
        [Description("MANAGE_CHANNELS")]
        ManageChannels = 1 << 4,
        
        /// <summary>
        /// Allows management and editing of the guild
        /// </summary>
        [Description("MANAGE_GUILD")]
        ManageGuild = 1 << 5,
        
        /// <summary>
        /// Allows for the addition of reactions to messages
        /// Channel Type (Text)
        /// </summary>
        [Description("ADD_REACTIONS")]
        AddReactions = 1 << 6,
        
        /// <summary>
        /// Allows for viewing of audit logs
        /// </summary>
        [Description("VIEW_AUDIT_LOG")]
        ViewAuditLog = 1 << 7,
        
        /// <summary>
        /// Allows for using priority speaker in a voice channel
        /// Channel Type (Voice)
        /// </summary>
        [Description("PRIORITY_SPEAKER")]
        PrioritySpeaker = 1 << 8,
        
        /// <summary>
        /// Allows the user to go live
        /// Channel Type (Voice)
        /// </summary>
        [Description("STREAM")]
        Stream = 1 << 9,
        
        /// <summary>
        /// Allows guild members to view a channel, which includes reading messages in text channels and joining voice channels
        /// Channel Type (Text, Voice, Stage)
        /// </summary>
        [Description("VIEW_CHANNEL")]
        ViewChannel = 1 << 10,
        
        /// <summary>
        /// Allows for sending messages in a channel
        /// Channel Type (Text)
        /// </summary>
        [Description("SEND_MESSAGES")]
        SendMessages = 1 << 11,
        
        /// <summary>
        /// Allows for sending of /tts messages
        /// Channel Type (Text)
        /// </summary>
        [Description("SEND_TTS_MESSAGES")]
        SendTtsMessages = 1 << 12,
        
        /// <summary>
        /// Allows for deletion of other users messages
        /// Channel Type (Text)
        /// </summary>
        [Description("MANAGE_MESSAGES")]
        ManageMessages = 1 << 13,
        
        /// <summary>
        /// Links sent by users with this permission will be auto-embedded
        /// Channel Type (Text)
        /// </summary>
        [Description("EMBED_LINKS")]
        EmbedLinks = 1 << 14,
        
        /// <summary>
        /// Allows for uploading images and files
        /// Channel Type (Text)
        /// </summary>
        [Description("ATTACH_FILES")]
        AttachFiles = 1 << 15,
        
        /// <summary>
        /// Allows for reading of message history
        /// Channel Type (Text)
        /// </summary>
        [Description("READ_MESSAGE_HISTORY")]
        ReadMessageHistory = 1 << 16,
        
        /// <summary>
        /// Allows for using the @everyone tag to notify all users in a channel,
        /// and the @here tag to notify all online users in a channel
        /// Channel Type (Text, Stage)
        /// </summary>
        [Description("MENTION_EVERYONE")]
        MentionEveryone = 1 << 17,
        
        /// <summary>
        /// Allows the usage of custom emojis from other servers
        /// Channel Type (Text)
        /// </summary>
        [Description("USE_EXTERNAL_EMOJIS")]
        UseExternalEmojis = 1 << 18,
        
        /// <summary>
        /// Allows for viewing guild insights
        /// </summary>
        [Description("VIEW_GUILD_INSIGHTS")]
        ViewGuildInsights = 1 << 19,
        
        /// <summary>
        /// Allows for joining of a voice channel
        /// Channel Type (Voice, Stage)
        /// </summary>
        [Description("CONNECT")]
        Connect = 1 << 20,
        
        /// <summary>
        /// Allows for speaking in a voice channel
        /// Channel Type (Voice)
        /// </summary>
        [Description("SPEAK")]
        Speak = 1 << 21,
        
        /// <summary>
        /// Allows for muting members in a voice channel
        /// Channel Type (Voice, Stage)
        /// </summary>
        [Description("MUTE_MEMBERS")]
        MuteMembers = 1 << 22,
        
        /// <summary>
        /// Allows for deafening of members in a voice channel
        /// Channel Type (Voice, Stage)
        /// </summary>
        [Description("DEAFEN_MEMBERS")]
        DeafanMembers = 1 << 23,
        
        /// <summary>
        /// Allows for moving of members between voice channels
        /// Channel Type (Voice, Stage)
        /// </summary>
        [Description("MOVE_MEMBERS")]
        MoveMembers = 1 << 24,
        
        /// <summary>
        /// Allows for using voice-activity-detection in a voice channel
        /// Channel Type (Voice)
        /// </summary>
        [Description("USE_VAD")]
        UseVad = 1 << 25,
        
        /// <summary>
        /// Allows for modification of own nickname
        /// </summary>
        [Description("CHANGE_NICKNAME")]
        ChangeNickname = 1 << 26,
        
        /// <summary>
        /// Allows for modification of other users nicknames
        /// </summary>
        [Description("MANAGE_NICKNAMES")]
        ManageNicknames = 1 << 27,
        
        /// <summary>
        /// Allows management and editing of roles
        /// Channel Type (Text, Voice, Stage)
        /// </summary>
        [Description("MANAGE_ROLES")]
        ManageRoles = 1 << 28,
        
        /// <summary>
        /// Allows management and editing of webhooks
        /// Channel Type (Text)
        /// </summary>
        [Description("MANAGE_WEBHOOKS")]
        ManageWebhooks = 1 << 29,
        
        /// <summary>
        /// Allows management and editing of emojis, stickers, and soundboard sounds
        /// </summary>
        [Description("MANAGE_GUILD_EXPRESSIONS")]
        ManageGuildExpressions = 1 << 30,
        
        /// <summary>
        /// Allows management and editing of emojis
        /// </summary>
        [Description("MANAGE_EMOJIS_AND_STICKERS")]
        [Obsolete("Replace with ManageGuildExpressions")]
        ManageEmojisAndStickers = 1 << 30,
        
        /// <summary>
        /// Allows members to use application commands, including slash commands and context menu commands.
        /// </summary>
        [Description("USE_APPLICATION_COMMANDS")]
        UseSlashCommands = 1ul << 31,
        
        /// <summary>
        /// Allows for requesting to speak in stage channels.
        /// Channel Type (Stage)
        /// (This permission is under active development and may be changed or removed.)
        /// </summary>
        [Description("REQUEST_TO_SPEAK")]
        RequestToSpeak = 1ul << 32,
        
        /// <summary>
        /// Allows for creating, editing, and deleting scheduled events
        /// Channel Type (Voice, Stage)
        /// </summary>
        [Description("MANAGE_EVENTS")]
        ManageEvents = 1ul << 33,
        
        /// <summary>
        /// Allows for deleting and archiving threads, and viewing all private threads
        /// Channel Type (Text)
        /// </summary>
        [Description("MANAGE_THREADS")]
        ManageThreads = 1ul << 34,
        
        /// <summary>
        /// Allows for creating and participating in threads
        /// Channel Type (Text)
        /// </summary>
        [Obsolete("This flag has been deprecated and will be removed in a future update. This flag is replaced by CreatePublicThreads")]
        [Description("USE_PUBLIC_THREADS")]
        UsePublicThreads = 1ul << 35,
        
        /// <summary>
        /// Allows for creating threads
        /// Channel Type (Text)
        /// </summary>
        [Description("CREATE_PUBLIC_THREADS")]
        CreatePublicThreads = 1ul << 35,
        
        /// <summary>
        /// Allows for creating and participating in private threads
        /// Channel Type (Text)
        /// </summary>
        [Obsolete("This flag has been deprecated and will be removed in a future update. This flag is replaced by CreatePrivateThreads")] 
        [Description("USE_PRIVATE_THREADS")]
        UsePrivateThreads = 1ul << 36,
        
        /// <summary>
        /// Allows for creating private threads
        /// Channel Type (Text)
        /// </summary>
        [Description("CREATE_PRIVATE_THREADS")]
        CreatePrivateThreads = 1ul << 36,
        
        /// <summary>
        /// Allows the usage of custom stickers from other servers
        /// Channel Type (Text)
        /// </summary>
        [Description("USE_EXTERNAL_STICKERS")]
        UseExternalStickers = 1ul << 37,    
        
        /// <summary>
        /// Allows for sending messages in threads
        /// Channel Type (Text)
        /// </summary>
        [Description("SEND_MESSAGES_IN_THREADS")]
        SendMessagesInThreads = 1ul << 38,
        
        /// <summary>
        /// Allows for launching activities (applications with the `EMBEDDED` flag) in a voice channel
        /// Channel Type (Voice)
        /// </summary>
        [Obsolete("Replaced with UseEmbeddedActivities")]
        [Description("START_EMBEDDED_ACTIVITIES")]
        StartEmbeddedActivities = 1ul << 39,
        
        /// <summary>
        /// Allows for using Activities (applications with the `EMBEDDED` flag) in a voice channel
        /// Channel Type (Voice)
        /// </summary>
        [Description("USE_EMBEDDED_ACTIVITIES")]
        UseEmbeddedActivities = 1ul << 39,
        
        /// <summary>
        /// Allows for timing out users to prevent them from sending or reacting to messages in chat and threads, and from speaking in voice and stage channels
        /// </summary>
        [Description("MODERATE_MEMBERS")]
        ModerateMembers = 1ul << 40,
        
        /// <summary>
        /// Allows for viewing role subscription insights
        /// </summary>
        [Description("VIEW_CREATOR_MONETIZATION_ANALYTICS")]
        ViewCreatorMonetizationAnalytics = 1ul << 41,
        
        /// <summary>
        /// Allows for using soundboard in a voice channel
        /// </summary>
        [Description("USE_SOUNDBOARD")]
        UseSoundboard = 1ul << 42,
    }
}