# DiscordChannel class

Represents a guild or DM [Channel Structure](https://discord.com/developers/docs/resources/channel#channel-object) within Discord.

```csharp
public class DiscordChannel : IDebugLoggable, ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [DiscordChannel](DiscordChannel/DiscordChannel.md)() | The default constructor. |
| [ApplicationId](DiscordChannel/ApplicationId.md) { get; set; } | Application id of the group DM creator if it is bot-created |
| [AppliedTags](DiscordChannel/AppliedTags.md) { get; set; } | The IDs of the set of tags that have been applied to a thread in a GUILD_FORUM channel |
| [AvailableTags](DiscordChannel/AvailableTags.md) { get; set; } | The set of tags that can be used in a GUILD_FORUM channel Limited to 20 |
| [Bitrate](DiscordChannel/Bitrate.md) { get; set; } | The bitrate (in bits) of the voice channel |
| [DefaultAutoArchiveDuration](DiscordChannel/DefaultAutoArchiveDuration.md) { get; set; } | Default duration for newly created threads, in minutes, to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080 |
| [DefaultForumLayout](DiscordChannel/DefaultForumLayout.md) { get; set; } | The default [`ForumLayoutTypes`](./ForumLayoutTypes.md) used to display posts in GUILD_FORUM channels. Defaults to NotSet, which indicates a layout view has not been set by a channel admin |
| [DefaultReactionEmoji](DiscordChannel/DefaultReactionEmoji.md) { get; set; } | The emoji to show in the add reaction button on a thread in a GUILD_FORUM channel |
| [DefaultSortOrder](DiscordChannel/DefaultSortOrder.md) { get; set; } | The default [`SortOrderType`](./SortOrderType.md) used to order posts in `GUILD_FORUM` channels |
| [DefaultThreadRateLimitPerUser](DiscordChannel/DefaultThreadRateLimitPerUser.md) { get; set; } | The initial rate_limit_per_user to set on newly created threads in a channel. this field is copied to the thread at creation time and does not live update. |
| [Flags](DiscordChannel/Flags.md) { get; set; } | Flags for this channel |
| [GuildId](DiscordChannel/GuildId.md) { get; set; } | the ID of the guild Warning: May be missing for some channel objects received over gateway guild dispatches |
| [Icon](DiscordChannel/Icon.md) { get; set; } | icon hash of the group DM |
| [IconUrl](DiscordChannel/IconUrl.md) { get; } | Returns the Icon URL for the given channel |
| [Id](DiscordChannel/Id.md) { get; set; } | The ID of this channel |
| [LastMessageId](DiscordChannel/LastMessageId.md) { get; set; } | The id of the last message sent in this channel (or thread for GUILD_FORUM channels) May not point to an existing or valid message or thread |
| [LastPinTimestamp](DiscordChannel/LastPinTimestamp.md) { get; set; } | When the last pinned message was pinned. This may be null in events such as GUILD_CREATE when a message is not pinned. |
| [Managed](DiscordChannel/Managed.md) { get; set; } | For group DM channels: whether the channel is managed by an application via the `gdm.join` OAuth2 scope |
| [Member](DiscordChannel/Member.md) { get; set; } | Thread member object for the current user, if they have joined the thread, only included on certain API endpoints |
| [MemberCount](DiscordChannel/MemberCount.md) { get; set; } | An approximate count of users in a thread, stops counting at 50 |
| [Mention](DiscordChannel/Mention.md) { get; } | Returns a string to mention this channel in a message |
| [MessageCount](DiscordChannel/MessageCount.md) { get; set; } | umber of messages (not including the initial messages or deleted messages) in a thread (if the thread was created before July 1, 2022, it stops counting at 50) |
| [Name](DiscordChannel/Name.md) { get; set; } | The name of the channel (1-100 characters) |
| [Nsfw](DiscordChannel/Nsfw.md) { get; set; } | Whether the channel is nsfw |
| [OwnerId](DiscordChannel/OwnerId.md) { get; set; } | ID of the DM creator |
| [ParentId](DiscordChannel/ParentId.md) { get; set; } | ID of the parent category for a channel (each parent category can contain up to 50 channels) |
| [PermissionOverwrites](DiscordChannel/PermissionOverwrites.md) { get; set; } | Explicit permission overwrites for members and roles [`Overwrite`](./Overwrite.md) |
| [Permissions](DiscordChannel/Permissions.md) { get; set; } | Default duration for newly created threads, in minutes, to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080 |
| [Position](DiscordChannel/Position.md) { get; set; } | Sorting position of the channel |
| [RateLimitPerUser](DiscordChannel/RateLimitPerUser.md) { get; set; } | Amount of seconds a user has to wait before sending another message (0-21600); bots, as well as users with the permission manage_messages or manage_channel, are unaffected |
| [Recipients](DiscordChannel/Recipients.md) { get; set; } | The recipients of the DM |
| [RtcRegion](DiscordChannel/RtcRegion.md) { get; set; } | Voice region id for the voice channel, automatic when set to null |
| [ThreadMembers](DiscordChannel/ThreadMembers.md) { get; } | List of thread members if channel is thread; Null Otherwise. |
| [ThreadMetadata](DiscordChannel/ThreadMetadata.md) { get; set; } | Thread-specific fields not needed by other channels |
| [Topic](DiscordChannel/Topic.md) { get; set; } | The channel topic (0-1024 characters) |
| [TotalMessageSent](DiscordChannel/TotalMessageSent.md) { get; set; } | Number of messages ever sent in a thread, it's similar to message_count on message creation, but will not decrement the number when a message is deleted |
| [Type](DiscordChannel/Type.md) { get; set; } | the type of channel [`ChannelType`](./ChannelType.md) |
| [UserLimit](DiscordChannel/UserLimit.md) { get; set; } | The user limit of the voice channel |
| [VideoQualityMode](DiscordChannel/VideoQualityMode.md) { get; set; } | The camera video quality mode of the voice channel 1 when not present |
| [AddThreadMember](DiscordChannel/AddThreadMember.md)(…) | Adds another user to a thread. Requires the ability to send messages in the thread. Also requires the thread is not archived. See [Add Thread Member](https://discord.com/developers/docs/resources/channel#add-thread-member) |
| [BulkDeleteMessages](DiscordChannel/BulkDeleteMessages.md)(…) | Delete multiple messages in a single request. This endpoint can only be used on guild channels and requires the MANAGE_MESSAGES permission. See [Bulk Delete Messages](https://discord.com/developers/docs/resources/channel#bulk-delete-messages) |
| [CreateGlobalTemplateMessage](DiscordChannel/CreateGlobalTemplateMessage.md)(…) | Creates a message in a text channel using a global message template |
| [CreateInvite](DiscordChannel/CreateInvite.md)(…) | Create a new invite object for the channel. Only usable for guild channels. Requires the CREATE_INSTANT_INVITE permission. See [Create Channel Invite](https://discord.com/developers/docs/resources/channel#create-channel-invite) |
| [CreateMessage](DiscordChannel/CreateMessage.md)(…) | Post a message to a guild text or DM channel. If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user. If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken. See [Create Message](https://discord.com/developers/docs/resources/channel#create-message) (4 methods) |
| [CreateTemplateMessage](DiscordChannel/CreateTemplateMessage.md)(…) | Creates a message in a text channel using a localized message template |
| [Delete](DiscordChannel/Delete.md)(…) | Delete a channel, or close a private message. Requires the MANAGE_CHANNELS or MANAGE_THREADS permission for the guild depending on the channel type. See [Delete/Close Channel](https://discord.com/developers/docs/resources/channel#deleteclose-channel) |
| [DeletePermission](DiscordChannel/DeletePermission.md)(…) | Delete a channel permission overwrite for a user or role in a channel. Only usable for guild channels. Requires the MANAGE_ROLES permission. See [Delete Channel Permission](https://discord.com/developers/docs/resources/channel#delete-channel-permission) (2 methods) |
| [EditGroupDmChannel](DiscordChannel/EditGroupDmChannel.md)(…) | Update a group dm channel's settings. See [Modify Channel](https://discord.com/developers/docs/resources/channel#modify-channel) |
| [EditGuildChannel](DiscordChannel/EditGuildChannel.md)(…) | Update a guild channel's settings. Requires the MANAGE_CHANNELS permission for the guild. See [Modify Channel](https://discord.com/developers/docs/resources/channel#modify-channel) |
| [EditPermissions](DiscordChannel/EditPermissions.md)(…) | Edit the channel permission overwrites for a user or role in a channel. Only usable for guild channels. Requires the MANAGE_ROLES permission. See [Edit Channel Permissions](https://discord.com/developers/docs/resources/channel#edit-channel-permissions) |
| [EditThreadChannel](DiscordChannel/EditThreadChannel.md)(…) | Update a thread channel's settings. Requires the MANAGE_THREADS permission for the guild. See [Modify Channel](https://discord.com/developers/docs/resources/channel#modify-channel) |
| [FollowNewsChannel](DiscordChannel/FollowNewsChannel.md)(…) | Follow a News Channel to send messages to a target channel. Requires the MANAGE_WEBHOOKS permission in the target channel. See [Follow Announcement Channel](https://discord.com/developers/docs/resources/channel#follow-announcement-channel) |
| [GetInvites](DiscordChannel/GetInvites.md)(…) | Returns a list of invite objects (with invite metadata) for the channel. Only usable for guild channels. Requires the MANAGE_CHANNELS permission. See [Get Channel Invites](https://discord.com/developers/docs/resources/channel#get-channel-invites) |
| [GetMessage](DiscordChannel/GetMessage.md)(…) | Returns a specific message in the channel. If operating on a guild channel, this endpoint requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user. See [Get Channel Messages](https://discord.com/developers/docs/resources/channel#get-channel-message) |
| [GetMessages](DiscordChannel/GetMessages.md)(…) | Returns the messages for a channel. If operating on a guild channel, this endpoint requires the VIEW_CHANNEL permission to be present on the current user. If the current user is missing the 'READ_MESSAGE_HISTORY' permission in the channel then this will return no messages (since they cannot read the message history). See [Get Channel Messages](https://discord.com/developers/docs/resources/channel#get-channel-messages) |
| [GetPinnedMessages](DiscordChannel/GetPinnedMessages.md)(…) | Returns all pinned messages in the channel See [Get Pinned Messages](https://discord.com/developers/docs/resources/channel#get-pinned-messages) |
| [GetStageInstance](DiscordChannel/GetStageInstance.md)(…) | Gets the stage instance associated with the Stage channel, if it exists. See [Get Stage Instance](https://discord.com/developers/docs/resources/stage-instance#get-stage-instance) |
| [GetThreadMember](DiscordChannel/GetThreadMember.md)(…) | Returns a thread member object for the specified user if they are a member of the thread returns a 404 response otherwise. See [Remove Thread Member](https://discord.com/developers/docs/resources/channel#get-thread-member) |
| [GroupDmAddRecipient](DiscordChannel/GroupDmAddRecipient.md)(…) | Adds a recipient to a Group DM using their access token See [Group DM Add Recipient](https://discord.com/developers/docs/resources/channel#group-dm-add-recipient) (2 methods) |
| [GroupDmRemoveRecipient](DiscordChannel/GroupDmRemoveRecipient.md)(…) | Removes a recipient from a Group DM See [Group DM Remove Recipient](https://discord.com/developers/docs/resources/channel#group-dm-remove-recipient) |
| [IsDmChannel](DiscordChannel/IsDmChannel.md)() | Returns if a channel is a DM channel |
| [IsGuildChannel](DiscordChannel/IsGuildChannel.md)() | Returns if the channel is a guild channel |
| [IsThreadChannel](DiscordChannel/IsThreadChannel.md)() | Returns if a channel is a thread channel |
| [JoinThread](DiscordChannel/JoinThread.md)(…) | Adds the bot to the thread. Also requires the thread is not archived. See [Join Thread](https://discord.com/developers/docs/resources/channel#join-thread) |
| [LeaveThread](DiscordChannel/LeaveThread.md)(…) | Removes the bot from the thread. Also requires the thread is not archived. See [Leave Thread](https://discord.com/developers/docs/resources/channel#leave-thread) |
| [ListJoinedPrivateArchivedThreads](DiscordChannel/ListJoinedPrivateArchivedThreads.md)(…) | Returns archived threads in the channel that are of type GUILD_PRIVATE_THREAD, and the user has joined. Threads are ordered by their id, in descending order. Requires the READ_MESSAGE_HISTORY permission. See [List Joined Private Archived Threads](https://discord.com/developers/docs/resources/channel#list-joined-private-archived-threads) |
| [ListPrivateArchivedThreads](DiscordChannel/ListPrivateArchivedThreads.md)(…) | Returns archived threads in the channel that are of type GUILD_PRIVATE_THREAD. Threads are ordered by archive_timestamp, in descending order. Requires both the READ_MESSAGE_HISTORY and MANAGE_THREADS permissions. See [List Private Archived Threads](https://discord.com/developers/docs/resources/channel#list-private-archived-threads) |
| [ListPublicArchivedThreads](DiscordChannel/ListPublicArchivedThreads.md)(…) | Returns archived threads in the channel that are public. When called on a GUILD_TEXT channel, returns threads of type GUILD_PUBLIC_THREAD. When called on a GUILD_NEWS channel returns threads of type GUILD_NEWS_THREAD. Threads are ordered by archive_timestamp, in descending order. Requires the READ_MESSAGE_HISTORY permission. See [List Public Archived Threads](https://discord.com/developers/docs/resources/channel#list-public-archived-threads) |
| [ListThreadMembers](DiscordChannel/ListThreadMembers.md)(…) | Returns array of thread members objects that are members of the thread. This endpoint is restricted according to whether the GUILD_MEMBERS Privileged Intent is enabled for your application. See [List Thread Members](https://discord.com/developers/docs/resources/channel#list-thread-members) |
| [LogDebug](DiscordChannel/LogDebug.md)(…) |  |
| [RemoveThreadMember](DiscordChannel/RemoveThreadMember.md)(…) | Removes another user from a thread. Requires the MANAGE_THREADS permission or that you are the creator of the thread. Also requires the thread is not archived. See [Remove Thread Member](https://discord.com/developers/docs/resources/channel#remove-thread-member) |
| [StartThreadFromMessage](DiscordChannel/StartThreadFromMessage.md)(…) | Creates a new public thread from a message See [Start Thread with Message](https://discord.com/developers/docs/resources/channel#start-thread-from-message) |
| [StartThreadInForumChannel](DiscordChannel/StartThreadInForumChannel.md)(…) | Creates a new thread in a forum channel, and sends a message within the created thread. Returns a channel, with a nested message object See [Start Thread in Forum Channel](https://discord.com/developers/docs/resources/channel#start-thread-in-forum-channel) |
| [StartThreadWithoutMessage](DiscordChannel/StartThreadWithoutMessage.md)(…) | Creates a new thread that is not connected to an existing message. The created thread is always a GUILD_PRIVATE_THREAD See [Start Thread without Message](https://discord.com/developers/docs/resources/channel#start-thread-without-message) |
| [TriggerTypingIndicator](DiscordChannel/TriggerTypingIndicator.md)(…) | Post a typing indicator for the specified channel. Generally bots should not implement this route. However, if a bot is responding to a command and expects the computation to take a few seconds, this endpoint may be called to let the user know that the bot is processing their message. See [Trigger Typing Indicator](https://discord.com/developers/docs/resources/channel#trigger-typing-indicator) |
| static [Create](DiscordChannel/Create.md)(…) | Create a new channel object for the guild. Requires the MANAGE_CHANNELS permission. See [Create Guild Channel](https://discord.com/developers/docs/resources/guild#create-guild-channel) |
| static [Get](DiscordChannel/Get.md)(…) | Get a channel by ID See [Get Channel](https://discord.com/developers/docs/resources/channel#get-channel) If the channel is a thread, a thread member object is included in the returned result. |

## See Also

* interface [IDebugLoggable](../../Interfaces/Logging/IDebugLoggable.md)
* interface [ISnowflakeEntity](../../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordChannel.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Channels/DiscordChannel.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
