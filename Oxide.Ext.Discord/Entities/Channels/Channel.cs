using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Invites;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Channels
{
    /// <summary>
    /// Represents a guild or DM <a href="https://discord.com/developers/docs/resources/channel#channel-object">Channel Structure</a> within Discord.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Channel : ChannelUpdate
    {
        /// <summary>
        /// The ID of this channel
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// the ID of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        /// <summary>
        /// The id of the last message sent in this channel (may not point to an existing or valid message)
        /// </summary>
        [JsonProperty("last_message_id")]        
        public string LastMessageId { get; set; }
                
        /// <summary>
        /// The recipients of the DM
        /// </summary>
        [JsonProperty("recipients")]
        public List<DiscordUser> Recipients { get; set; }
        
        /// <summary>
        /// icon hash
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        /// <summary>
        /// ID of the DM creator
        /// </summary>
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
        
        /// <summary>
        /// Application id of the group DM creator if it is bot-created
        /// </summary>
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }
        
        /// <summary>
        /// When the last pinned message was pinned.
        /// This may be null in events such as GUILD_CREATE when a message is not pinned.
        /// </summary>
        [JsonProperty("last_pin_timestamp")]
        public DateTime? LastPinTimestamp { get; set; } 

        /// <summary>
        /// Get a channel by ID
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-channel">Get Channel</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">ID of the channel to get</param>
        /// <param name="callback">Callback with the channel object</param>
        public static void GetChannel(DiscordClient client, string channelId, Action<Channel> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{channelId}", RequestMethod.GET, null, callback);
        }

        /// <summary>
        /// Update a channel's settings.
        /// Requires the MANAGE_CHANNELS permission for the guild.
        /// See <a href="https://discord.com/developers/docs/resources/channel#modify-channel">Modify Channel</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="update">Update to be made to the channel. All fields are optional</param>
        /// <param name="callback">Callback with the updated channel</param>
        public void ModifyChannel(DiscordClient client, ChannelUpdate update, Action<Channel> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}", RequestMethod.PATCH, update, callback);
        }

        /// <summary>
        /// Delete a channel, or close a private message.
        /// Requires the MANAGE_CHANNELS permission for the guild.
        /// See <a href="https://discord.com/developers/docs/resources/channel#deleteclose-channel">Delete/Close Channel</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the deleted channel</param>
        public void DeleteChannel(DiscordClient client, Action<Channel> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}", RequestMethod.DELETE, null, callback);
        }

        /// <summary>
        /// Returns the messages for a channel.
        /// If operating on a guild channel, this endpoint requires the VIEW_CHANNEL permission to be present on the current user.
        /// If the current user is missing the 'READ_MESSAGE_HISTORY' permission in the channel then this will return no messages (since they cannot read the message history).
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-channel-messages">Get Channel Messages</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with list of channel messages</param>
        public void GetChannelMessages(DiscordClient client, Action<List<Message>> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/messages", RequestMethod.GET, null, callback);
        }

        /// <summary>
        /// Returns a specific message in the channel.
        /// If operating on a guild channel, this endpoint requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user.
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-channel-message">Get Channel Messages</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID of the message</param>
        /// <param name="callback">Callback with the message for the ID</param>
        public void GetChannelMessage(DiscordClient client, string messageId, Action<Message> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/messages/{messageId}", RequestMethod.GET, null, callback);
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
        public void CreateMessage(DiscordClient client, MessageCreate message, Action<Message> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/messages", RequestMethod.POST, message, callback);
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
        public void CreateMessage(DiscordClient client, string message, Action<Message> callback = null)
        {
            MessageCreate createMessage = new MessageCreate()
            {
                Content = message
            };

            client.Bot.Rest.DoRequest($"/channels/{Id}/messages", RequestMethod.POST, createMessage, callback);
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
        public void CreateMessage(DiscordClient client, Embed embed, Action<Message> callback = null)
        {
            MessageCreate createMessage = new MessageCreate()
            {
                Embed = embed
            };

            client.Bot.Rest.DoRequest($"/channels/{Id}/messages", RequestMethod.POST, createMessage, callback);
        }

        /// <summary>
        /// Delete multiple messages in a single request.
        /// This endpoint can only be used on guild channels and requires the MANAGE_MESSAGES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#bulk-delete-messages">Bulk Delete Messages</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageIds">Collect of message ids to delete (Between 2 - 100)</param>
        /// <param name="callback">Callback once the action is complete</param>
        public void BulkDeleteMessages(DiscordClient client, ICollection<string> messageIds, Action callback = null)
        {
            if (messageIds.Count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(messageIds), "Cannot delete less than 2 messages");
            }

            if (messageIds.Count > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(messageIds), "Cannot delete more than 100 messages");
            }
            
            Dictionary<string, IEnumerable<string>> data = new Dictionary<string, IEnumerable<string>>
            {
                ["messages"] = messageIds 
            };

            client.Bot.Rest.DoRequest($"/channels/{Id}/messages/bulk-delete", RequestMethod.POST, data, callback);
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
        public void EditChannelPermissions(DiscordClient client, Overwrite overwrite, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/permissions/{overwrite.Id}", RequestMethod.PUT, overwrite, callback);
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
        public void EditChannelPermissions(DiscordClient client, string overwriteId, PermissionFlags allow, PermissionFlags deny, PermissionType type, Action callback = null)
        {
            Overwrite overwrite = new Overwrite
            {
                Id = overwriteId,
                Type = type,
                Allow = allow,
                Deny = deny
            };

            EditChannelPermissions(client, overwrite, callback);
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
        public void GetChannelInvites(DiscordClient client, Action<List<Invite>> callback = null)
        {
            if (Type == ChannelType.Dm || Type == ChannelType.GroupDm)
            {
                throw new Exception("You can only get channel invites for guild channels.");
            }
            
            client.Bot.Rest.DoRequest($"/channels/{Id}/invites", RequestMethod.GET, null, callback);
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
        public void CreateChannelInvite(DiscordClient client, ChannelInvite invite, Action<Invite> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/invites", RequestMethod.POST, invite, callback);
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
        public void DeleteChannelPermission(DiscordClient client, Overwrite overwrite, Action callback) => DeleteChannelPermission(client, overwrite.Id, callback);

        /// <summary>
        /// Delete a channel permission overwrite for a user or role in a channel.
        /// Only usable for guild channels.
        /// Requires the MANAGE_ROLES permission.
        /// See <a href="https://discord.com/developers/docs/resources/channel#delete-channel-permission">Delete Channel Permission</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="overwriteId">Overwrite ID to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void DeleteChannelPermission(DiscordClient client, string overwriteId, Action callback)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/permissions/{overwriteId}", RequestMethod.DELETE, null, callback);
        }

        /// <summary>
        /// Follow a News Channel to send messages to a target channel.
        /// Requires the MANAGE_WEBHOOKS permission in the target channel.
        /// See <a href="https://discord.com/developers/docs/resources/channel#follow-news-channel">Delete Channel Permission</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="webhookChannelId">ID of target channel</param>
        /// <param name="callback">callback with the followed channel</param>
        public void FollowNewsChannel(DiscordClient client, string webhookChannelId, Action<FollowedChannel> callback)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/followers?webhook_channel_id={webhookChannelId}", RequestMethod.POST, null, callback);
        }

        /// <summary>
        /// Post a typing indicator for the specified channel.
        /// Generally bots should not implement this route. However, if a bot is responding to a command and expects the computation to take a few seconds, this endpoint may be called to let the user know that the bot is processing their message.
        /// See <a href="https://discord.com/developers/docs/resources/channel#trigger-typing-indicator">Trigger Typing Indicator</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void TriggerTypingIndicator(DiscordClient client, Action callback)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/typing", RequestMethod.POST, null, callback);
        }

        /// <summary>
        /// Returns all pinned messages in the channel
        /// See <a href="https://discord.com/developers/docs/resources/channel#get-pinned-messages">Get Pinned Messages</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback of all the pinned messages</param>
        public void GetPinnedMessages(DiscordClient client, Action<List<Message>> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/pins", RequestMethod.GET, null, callback);
        }

        /// <summary>
        /// Adds a recipient to a Group DM using their access token
        /// See <a href="https://discord.com/developers/docs/resources/channel#group-dm-add-recipient">Group DM Add Recipient</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="user">User to add</param>
        /// <param name="accessToken">Users access token</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void GroupDmAddRecipient(DiscordClient client, DiscordUser user, string accessToken, Action callback = null) => GroupDmAddRecipient(client, user.Id, accessToken, user.Username, callback);

        /// <summary>
        /// Adds a recipient to a Group DM using their access token
        /// See <a href="https://discord.com/developers/docs/resources/channel#group-dm-add-recipient">Group DM Add Recipient</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User to add</param>
        /// <param name="accessToken">Users access token</param>
        /// <param name="nick">User nickname</param>
        /// <param name="callback">Callback once the action is completed</param>
        public void GroupDmAddRecipient(DiscordClient client, string userId, string accessToken, string nick, Action callback = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                ["access_token"] = accessToken,
                ["nick"] = nick
            };

            client.Bot.Rest.DoRequest($"/channels/{Id}/recipients/{userId}", RequestMethod.PUT, data, callback);
        }

        /// <summary>
        /// Removes a recipient from a Group DM
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to remove</param>
        /// <param name="callback">callback once the action is completed</param>
        public void GroupDmRemoveRecipient(DiscordClient client, string userId, Action callback)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/recipients/{userId}", RequestMethod.DELETE, null, callback);
        }
    }
}