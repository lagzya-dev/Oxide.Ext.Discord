using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Channels.Threads;
using Oxide.Ext.Discord.Entities.Invites;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Helpers.Converters;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Entities.Channels
{
    /// <summary>
    /// Represents a guild or DM <a href="https://discord.com/developers/docs/resources/channel#channel-object">Channel Structure</a> within Discord.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Channel : ISnowflakeEntity
    {
        /// <summary>
        /// The ID of this channel
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// the type of channel <see cref="ChannelType"/>
        /// </summary>
        [JsonProperty("type")]
        public ChannelType Type { get; set; }
        
        /// <summary>
        /// the ID of the guild (
        /// Warning: May be missing for some channel objects received over gateway guild dispatches
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }
        
        /// <summary>
        /// Sorting position of the channel
        /// </summary>
        [JsonProperty("position")]
        public int? Position { get; set; }
        
        /// <summary>
        /// Explicit permission overwrites for members and roles <see cref="Overwrite"/>
        /// </summary>
        [JsonConverter(typeof(HashListConverter<Overwrite>))]
        [JsonProperty("permission_overwrites")]
        public Hash<Snowflake, Overwrite> PermissionOverwrites { get; set; }
        
        /// <summary>
        /// The name of the channel (2-100 characters)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The channel topic (0-1024 characters)
        /// </summary>
        [JsonProperty("topic")]        
        public string Topic { get; set; }
        
        /// <summary>
        /// Whether the channel is nsfw
        /// </summary>
        [JsonProperty("nsfw")]
        public bool? Nsfw { get; set; }
        
        /// <summary>
        /// The id of the last message sent in this channel (may not point to an existing or valid message)
        /// </summary>
        [JsonProperty("last_message_id")]        
        public Snowflake? LastMessageId { get; set; }
        
        /// <summary>
        /// The bitrate (in bits) of the voice channel
        /// </summary>
        [JsonProperty("bitrate")]
        public int? Bitrate { get; set; }
        
        /// <summary>
        /// The user limit of the voice channel
        /// </summary>
        [JsonProperty("user_limit")]
        public int? UserLimit { get; set; }
        
        /// <summary>
        /// Amount of seconds a user has to wait before sending another message (0-21600);
        /// bots, as well as users with the permission manage_messages or manage_channel, are unaffected
        /// </summary>
        [JsonProperty("rate_limit_per_user")]
        public int? RateLimitPerUser { get; set; }
        
        /// <summary>
        /// The recipients of the DM
        /// </summary>
        [JsonConverter(typeof(HashListConverter<DiscordUser>))]
        [JsonProperty("recipients")]
        public Hash<Snowflake, DiscordUser> Recipients { get; set; }
        
        /// <summary>
        /// icon hash
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        /// <summary>
        /// ID of the DM creator
        /// </summary>
        [JsonProperty("owner_id")]
        public Snowflake? OwnerId { get; set; }
        
        /// <summary>
        /// Application id of the group DM creator if it is bot-created
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake? ApplicationId { get; set; }
        
        /// <summary>
        /// ID of the parent category for a channel (each parent category can contain up to 50 channels)
        /// </summary>
        [JsonProperty("parent_id")]
        public Snowflake? ParentId { get; set; }
        
        /// <summary>
        /// When the last pinned message was pinned.
        /// This may be null in events such as GUILD_CREATE when a message is not pinned.
        /// </summary>
        [JsonProperty("last_pin_timestamp")]
        public DateTime? LastPinTimestamp { get; set; }
        
        /// <summary>
        /// An approximate count of messages in a thread, stops counting at 50
        /// </summary>
        [JsonProperty("message_count")]
        public int? MessageCount { get; set; }
        
        /// <summary>
        /// An approximate count of users in a thread, stops counting at 50
        /// </summary>
        [JsonProperty("member_count")]
        public int? MemberCount { get; set; }
        
        /// <summary>
        /// Thread-specific fields not needed by other channels
        /// </summary>
        [JsonProperty("thread_metadata")]
        public ThreadMetadata ThreadMetadata { get; set; }
        
        /// <summary>
        /// Thread member object for the current user, if they have joined the thread, only included on certain API endpoints
        /// </summary>
        [JsonProperty("member")]
        public ThreadMember Member { get; set; }
        
        /// <summary>
        /// Thread Members for the current thread.
        /// Note: This is not sent by discord but maintained by socket events
        /// </summary>
        public Hash<Snowflake, ThreadMember> ThreadMembers { get; set; }

        /// <summary>
        /// Create a new channel object for the guild.
        /// Requires the MANAGE_CHANNELS permission.
        /// See <a href="https://discord.com/developers/docs/resources/guild#create-guild-channel">Create Guild Channel</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild to create the channel in</param>
        /// <param name="channel">Channel to create</param>
        /// <param name="callback">Callback with created channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void CreateGuildChannel(DiscordClient client, Snowflake guildId, ChannelCreate channel, Action<Channel> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{guildId}/channels", RequestMethod.POST, channel, callback, error);
        }

        /// <summary>
        /// Get a channel by ID
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-channel">Get Channel</a>
        /// If the channel is a thread, a thread member object is included in the returned result.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">ID of the channel to get</param>
        /// <param name="callback">Callback with the channel object</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetChannel(DiscordClient client, Snowflake channelId, Action<Channel> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{channelId}", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Update a group dm channel's settings.
        /// See <a href="https://discord.com/developers/docs/resources/channel#modify-channel">Modify Channel</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="update">Update to be made to the channel. All fields are optional</param>
        /// <param name="callback">Callback with the updated channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ModifyGroupDmChannel(DiscordClient client, GroupDmChannelUpdate update, Action<Channel> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}", RequestMethod.PATCH, update, callback, error);
        }

        /// <summary>
        /// Update a guild channel's settings.
        /// Requires the MANAGE_CHANNELS permission for the guild.
        /// See <a href="https://discord.com/developers/docs/resources/channel#modify-channel">Modify Channel</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="update">Update to be made to the channel. All fields are optional</param>
        /// <param name="callback">Callback with the updated channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ModifyGuildChannel(DiscordClient client, GuildChannelUpdate update, Action<Channel> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}", RequestMethod.PATCH, update, callback, error);
        }
        
        /// <summary>
        /// Update a thread channel's settings.
        /// Requires the MANAGE_THREADS permission for the guild.
        /// See <a href="https://discord.com/developers/docs/resources/channel#modify-channel">Modify Channel</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="update">Update to be made to the channel. All fields are optional</param>
        /// <param name="callback">Callback with the updated channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ModifyThreadChannel(DiscordClient client, ThreadChannelUpdate update, Action<Channel> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}", RequestMethod.PATCH, update, callback, error);
        }

        /// <summary>
        /// Delete a channel, or close a private message.
        /// Requires the MANAGE_CHANNELS or MANAGE_THREADS permission for the guild depending on the channel type.
        /// See <a href="https://discord.com/developers/docs/resources/channel#deleteclose-channel">Delete/Close Channel</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the deleted channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteChannel(DiscordClient client, Action<Channel> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Returns the messages for a channel.
        /// If operating on a guild channel, this endpoint requires the VIEW_CHANNEL permission to be present on the current user.
        /// If the current user is missing the 'READ_MESSAGE_HISTORY' permission in the channel then this will return no messages (since they cannot read the message history).
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-channel-messages">Get Channel Messages</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="request">Optional request filters</param>
        /// <param name="callback">Callback with list of channel messages</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetChannelMessages(DiscordClient client, GetChannelMessagesRequest request = null, Action<List<DiscordMessage>> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/messages{request?.ToQueryString()}", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Returns a specific message in the channel.
        /// If operating on a guild channel, this endpoint requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user.
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-channel-message">Get Channel Messages</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID of the message</param>
        /// <param name="callback">Callback with the message for the ID</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetChannelMessage(DiscordClient client, Snowflake messageId, Action<DiscordMessage> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/messages/{messageId}", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Post a message to a guild text or DM channel.
        /// If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user.
        /// If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken.
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message to be created</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateMessage(DiscordClient client, MessageCreate message, Action<DiscordMessage> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/messages", RequestMethod.POST, message, callback, error);
        }

        /// <summary>
        /// Post a message to a guild text or DM channel.
        /// If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user.
        /// If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken.
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Content of the message</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateMessage(DiscordClient client, string message, Action<DiscordMessage> callback = null, Action<RestError> error = null)
        {
            MessageCreate createMessage = new MessageCreate
            {
                Content = message
            };

            client.Bot.Rest.DoRequest($"/channels/{Id}/messages", RequestMethod.POST, createMessage, callback, error);
        }

        /// <summary>
        /// Post a message to a guild text or DM channel.
        /// If operating on a guild channel, this endpoint requires the SEND_MESSAGES permission to be present on the current user.
        /// If the tts field is set to true, the SEND_TTS_MESSAGES permission is required for the message to be spoken.
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-message">Create Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="embed">Embed to be send in the message</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateMessage(DiscordClient client, Embed embed, Action<DiscordMessage> callback = null, Action<RestError> error = null)
        {
            MessageCreate createMessage = new MessageCreate
            {
                Embed = embed
            };

            client.Bot.Rest.DoRequest($"/channels/{Id}/messages", RequestMethod.POST, createMessage, callback, error);
        }

        /// <summary>
        /// Delete multiple messages in a single request.
        /// This endpoint can only be used on guild channels and requires the MANAGE_MESSAGES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#bulk-delete-messages">Bulk Delete Messages</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageIds">Collect of message ids to delete (Between 2 - 100)</param>
        /// <param name="callback">Callback once the action is complete</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void BulkDeleteMessages(DiscordClient client, ICollection<Snowflake> messageIds, Action callback = null, Action<RestError> error = null)
        {
            if (messageIds.Count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(messageIds), "Cannot delete less than 2 messages");
            }

            if (messageIds.Count > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(messageIds), "Cannot delete more than 100 messages");
            }
            
            Dictionary<string, ICollection<Snowflake>> data = new Dictionary<string, ICollection<Snowflake>>
            {
                ["messages"] = messageIds 
            };

            client.Bot.Rest.DoRequest($"/channels/{Id}/messages/bulk-delete", RequestMethod.POST, data, callback, error);
        }

        /// <summary>
        /// Edit the channel permission overwrites for a user or role in a channel.
        /// Only usable for guild channels.
        /// Requires the MANAGE_ROLES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#edit-channel-permissions">Edit Channel Permissions</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="overwrite">Overwrite to edit with changes</param>
        /// <param name="callback">Callback once the action is complete</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditChannelPermissions(DiscordClient client, Overwrite overwrite, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/permissions/{overwrite.Id}", RequestMethod.PUT, overwrite, callback, error);
        }

        /// <summary>
        /// Edit the channel permission overwrites for a user or role in a channel.
        /// Only usable for guild channels.
        /// Requires the MANAGE_ROLES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#edit-channel-permissions">Edit Channel Permissions</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="overwriteId">ID of the overwrite to edit</param>
        /// <param name="allow">Allow Permission Flags</param>
        /// <param name="deny">Deny Permission Flags</param>
        /// <param name="type">Permission Type Flag</param>
        /// <param name="callback">Callback once the action is complete</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditChannelPermissions(DiscordClient client, Snowflake overwriteId, PermissionFlags allow, PermissionFlags deny, PermissionType type, Action callback = null, Action<RestError> error = null)
        {
            Overwrite overwrite = new Overwrite
            {
                Id = overwriteId,
                Type = type,
                Allow = allow,
                Deny = deny
            };

            EditChannelPermissions(client, overwrite, callback, error);
        }

        /// <summary>
        /// Returns a list of invite objects (with invite metadata) for the channel.
        /// Only usable for guild channels.
        /// Requires the MANAGE_CHANNELS permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-channel-invites">Get Channel Invites</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with list of invites for the channel</param>
        /// <exception cref="Exception">Thrown when the channel type is Dm or GroupDm</exception>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetChannelInvites(DiscordClient client, Action<List<Invite>> callback = null, Action<RestError> error = null)
        {
            if (Type == ChannelType.Dm || Type == ChannelType.GroupDm)
            {
                throw new Exception("You can only get channel invites for guild channels.");
            }
            
            client.Bot.Rest.DoRequest($"/channels/{Id}/invites", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Create a new invite object for the channel.
        /// Only usable for guild channels.
        /// Requires the CREATE_INSTANT_INVITE permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#create-channel-invite">Create Channel Invite</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="invite">Invite to create</param>
        /// <param name="callback">Callback with the created invite</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateChannelInvite(DiscordClient client, ChannelInvite invite, Action<Invite> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/invites", RequestMethod.POST, invite, callback, error);
        }

        /// <summary>
        /// Delete a channel permission overwrite for a user or role in a channel.
        /// Only usable for guild channels.
        /// Requires the MANAGE_ROLES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-channel-permission">Delete Channel Permission</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="overwrite">Overwrite to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteChannelPermission(DiscordClient client, Overwrite overwrite, Action callback = null, Action<RestError> error = null) => DeleteChannelPermission(client, overwrite.Id, callback, error);

        /// <summary>
        /// Delete a channel permission overwrite for a user or role in a channel.
        /// Only usable for guild channels.
        /// Requires the MANAGE_ROLES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-channel-permission">Delete Channel Permission</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="overwriteId">Overwrite ID to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteChannelPermission(DiscordClient client, Snowflake overwriteId, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/permissions/{overwriteId}", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Follow a News Channel to send messages to a target channel.
        /// Requires the MANAGE_WEBHOOKS permission in the target channel.
        /// See <a href="https://discord.com/developers/docs/resources/channel#follow-news-channel">Delete Channel Permission</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="webhookChannelId">ID of target channel</param>
        /// <param name="callback">callback with the followed channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void FollowNewsChannel(DiscordClient client, Snowflake webhookChannelId, Action<FollowedChannel> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/followers?webhook_channel_id={webhookChannelId}", RequestMethod.POST, null, callback, error);
        }

        /// <summary>
        /// Post a typing indicator for the specified channel.
        /// Generally bots should not implement this route. However, if a bot is responding to a command and expects the computation to take a few seconds, this endpoint may be called to let the user know that the bot is processing their message.
        /// See <a href="https://discord.com/developers/docs/resources/channel#trigger-typing-indicator">Trigger Typing Indicator</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void TriggerTypingIndicator(DiscordClient client, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/typing", RequestMethod.POST, null, callback, error);
        }

        /// <summary>
        /// Returns all pinned messages in the channel
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-pinned-messages">Get Pinned Messages</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback of all the pinned messages</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetPinnedMessages(DiscordClient client, Action<List<DiscordMessage>> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/pins", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Adds a recipient to a Group DM using their access token
        /// See <a href="https://discord.com/developers/docs/resources/channel#group-dm-add-recipient">Group DM Add Recipient</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="user">User to add</param>
        /// <param name="accessToken">Users access token</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GroupDmAddRecipient(DiscordClient client, DiscordUser user, string accessToken, Action callback = null, Action<RestError> error = null) => GroupDmAddRecipient(client, user.Id, accessToken, user.Username, callback, error);
        
        /// <summary>
        /// Adds a recipient to a Group DM using their access token
        /// See <a href="https://discord.com/developers/docs/resources/channel#group-dm-add-recipient">Group DM Add Recipient</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User to add</param>
        /// <param name="accessToken">Users access token</param>
        /// <param name="nick">User nickname</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GroupDmAddRecipient(DiscordClient client, Snowflake userId, string accessToken, string nick, Action callback = null, Action<RestError> error = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                ["access_token"] = accessToken,
                ["nick"] = nick
            };

            client.Bot.Rest.DoRequest($"/channels/{Id}/recipients/{userId}", RequestMethod.PUT, data, callback, error);
        }
        
        /// <summary>
        /// Removes a recipient from a Group DM
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to remove</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GroupDmRemoveRecipient(DiscordClient client, Snowflake userId, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/recipients/{userId}", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Creates a new public thread from a message
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">ID of the message to start the thread from</param>
        /// <param name="create">Data to use when creating the thread</param>
        /// <param name="callback">Callback with the thread once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void StartThreadWithMessage(DiscordClient client, Snowflake messageId, ThreadCreate create, Action<Channel> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/messages/{messageId}/threads", RequestMethod.POST, create, callback, error);
        }
        
        /// <summary>
        /// Creates a new thread that is not connected to an existing message. The created thread is always a GUILD_PRIVATE_THREAD
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="create">Data to use when creating the thread</param>
        /// <param name="callback">Callback with the thread once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void StartThreadWithoutMessage(DiscordClient client, ThreadCreate create, Action<Channel> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/threads", RequestMethod.POST, create, callback, error);
        }
        
        /// <summary>
        /// Adds the bot to the thread
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the thread once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void JoinThread(DiscordClient client, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/thread-members/@me", RequestMethod.PUT, null, callback, error);
        }

        /// <summary>
        /// Adds another user to a thread.
        /// Requires the ability to send messages in the thread. Also requires the thread is not archived.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">ID of the user to thread</param>
        /// <param name="callback">Callback with the thread once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void AddUserToThread(DiscordClient client, Snowflake userId, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/thread-members/{userId}", RequestMethod.PUT, null, callback, error);
        }
        
        /// <summary>
        /// Removes the bot from the thread
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the thread once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void LeaveThread(DiscordClient client, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/thread-members/@me", RequestMethod.DELETE, null, callback, error);
        }
        
        /// <summary>
        /// Removes another user from a thread.
        /// Requires the MANAGE_THREADS permission or that you are the creator of the thread. Also requires the thread is not archived.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">ID of the user to thread</param>
        /// <param name="callback">Callback with the thread once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void RemoveUserFromThread(DiscordClient client, Snowflake userId, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/thread-members/{userId}", RequestMethod.DELETE, null, callback, error);
        }
        
        /// <summary>
        /// Returns array of thread members objects that are members of the thread.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the list of thread members</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ListThreadMembers(DiscordClient client, Action<List<ThreadMember>> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/thread-members", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Returns all active threads in the channel, including public and private threads. Threads are ordered by their id, in descending order.
        /// Requires the READ_MESSAGE_HISTORY permission.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the thread list information</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ListActiveThreads(DiscordClient client, Action<ThreadList> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/threads/active", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns archived threads in the channel that are public.
        /// When called on a GUILD_TEXT channel, returns threads of type GUILD_PUBLIC_THREAD. When called on a GUILD_NEWS channel returns threads of type GUILD_NEWS_THREAD. Threads are ordered by archive_timestamp, in descending order.
        /// Requires the READ_MESSAGE_HISTORY permission.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="lookup">The options to use when looking up the archived threads</param>
        /// <param name="callback">Callback with the thread list information</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ListPublicArchivedThreads(DiscordClient client, ThreadArchivedLookup lookup, Action<ThreadList> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/threads/archived/public{lookup.ToQueryString()}", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Returns archived threads in the channel that are of type GUILD_PRIVATE_THREAD.
        /// Threads are ordered by archive_timestamp, in descending order.
        /// Requires both the READ_MESSAGE_HISTORY and MANAGE_THREADS permissions.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="lookup">The options to use when looking up the archived threads</param>
        /// <param name="callback">Callback with the thread list information</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ListPrivateArchivedThreads(DiscordClient client, ThreadArchivedLookup lookup, Action<ThreadList> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/threads/archived/public{lookup.ToQueryString()}", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Returns archived threads in the channel that are of type GUILD_PRIVATE_THREAD, and the user has joined.
        /// Threads are ordered by their id, in descending order.
        /// Requires the READ_MESSAGE_HISTORY permission.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="lookup">The options to use when looking up the archived threads</param>
        /// <param name="callback">Callback with the thread list information</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ListJoinedPrivateArchivedThreads(DiscordClient client, ThreadArchivedLookup lookup, Action<ThreadList> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/users/@me/threads/archived/private{lookup.ToQueryString()}", RequestMethod.GET, null, callback, error);
        }

        internal Channel Update(Channel channel)
        {
            Channel previous = (Channel)MemberwiseClone();
            if (channel.Name != null)
            {
                Name = channel.Name;
            }

            Type = channel.Type;

            if (channel.Position != null)
            {
                Position = channel.Position;
            }
            
            if (channel.Topic != null)
            {
                Topic = channel.Topic;
            }
            
            if (channel.Nsfw != null)
            {
                Nsfw = channel.Nsfw;
            }
            
            if (channel.RateLimitPerUser != null)
            {
                RateLimitPerUser = channel.RateLimitPerUser;
            }
            
            if (channel.Bitrate != null)
            {
                Bitrate = channel.Bitrate;
            }
            
            if (channel.UserLimit != null)
            {
                UserLimit = channel.UserLimit;
            }
            
            if (channel.PermissionOverwrites != null)
            {
                PermissionOverwrites = channel.PermissionOverwrites;
            }

            ParentId = channel.ParentId;
            return previous;
        }
    }
}