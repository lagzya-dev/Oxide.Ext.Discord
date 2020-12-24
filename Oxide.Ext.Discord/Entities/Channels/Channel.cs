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
    public class Channel : ChannelCreate
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("last_message_id")]        
        public string LastMessageId { get; set; }
                
        [JsonProperty("recipients")]
        public List<User> Recipients { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
        
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }
        
        [JsonProperty("last_pin_timestamp")]
        public DateTime? LastPinTimestamp { get; set; } 

        public static void GetChannel(DiscordClient client, string channelId, Action<Channel> callback = null)
        {
            client.REST.DoRequest($"/channels/{channelId}", RequestMethod.GET, null, callback);
        }

        public void ModifyChannel(DiscordClient client, ChannelCreate newChannel, Action<Channel> callback = null)
        {
            client.REST.DoRequest($"/channels/{Id}", RequestMethod.PATCH, newChannel, callback);
        }

        public void DeleteChannel(DiscordClient client, Action<Channel> callback = null)
        {
            client.REST.DoRequest($"/channels/{Id}", RequestMethod.DELETE, null, callback);
        }

        public void GetChannelMessages(DiscordClient client, Action<List<Message>> callback = null)
        {
            client.REST.DoRequest($"/channels/{Id}/messages", RequestMethod.GET, null, callback);
        }

        public void GetChannelMessage(DiscordClient client, Message message, Action<Message> callback = null) => GetChannelMessage(client, message.Id, callback);

        public void GetChannelMessage(DiscordClient client, string messageId, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/channels/{Id}/messages/{messageId}", RequestMethod.GET, null, callback);
        }

        public void CreateMessage(DiscordClient client, MessageCreate message, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/channels/{Id}/messages", RequestMethod.POST, message, callback);
        }

        public void CreateMessage(DiscordClient client, string message, Action<Message> callback = null)
        {
            MessageCreate createMessage = new MessageCreate()
            {
                Content = message
            };

            client.REST.DoRequest($"/channels/{Id}/messages", RequestMethod.POST, createMessage, callback);
        }

        public void CreateMessage(DiscordClient client, Embed embed, Action<Message> callback = null)
        {
            MessageCreate createMessage = new MessageCreate()
            {
                Embed = embed
            };

            client.REST.DoRequest($"/channels/{Id}/messages", RequestMethod.POST, createMessage, callback);
        }

        public void BulkDeleteMessages(DiscordClient client, string[] messageIds, Action callback = null)
        {
            var jsonObj = new Dictionary<string, string[]>()
            {
                { "messages", messageIds }
            };

            client.REST.DoRequest($"/channels/{Id}/messages/bulk-delete", RequestMethod.POST, jsonObj, callback);
        }

        public void EditChannelPermissions(DiscordClient client, Overwrite overwrite, Action callback = null)
        {
            client.REST.DoRequest($"/channels/{Id}/permissions/{overwrite.Id}", RequestMethod.PUT, overwrite, callback);
        }

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

        public void GetChannelInvites(DiscordClient client, Action<List<Invite>> callback = null)
        {
            if (Type == ChannelType.DM || Type == ChannelType.GROUP_DM)
            {
                throw new Exception("You can only get channel invites for guild channels.");
            }
            
            client.REST.DoRequest($"/channels/{Id}/invites", RequestMethod.GET, null, callback);
        }

        public void CreateChannelInvite(DiscordClient client, ChannelInvite invite, Action<Invite> callback = null)
        {
            client.REST.DoRequest<Invite>($"/channels/{Id}/invites", RequestMethod.POST, invite, callback);
        }

        public void DeleteChannelPermission(DiscordClient client, Overwrite overwrite, Action callback) => DeleteChannelPermission(client, overwrite.Id, callback);

        public void DeleteChannelPermission(DiscordClient client, string overwriteId, Action callback)
        {
            client.REST.DoRequest($"/channels/{Id}/permissions/{overwriteId}", RequestMethod.DELETE, null, callback);
        }

        public void FollowNewsChannel(DiscordClient client, string webhookChannelId, Action<FollowedChannel> callback)
        {
            client.REST.DoRequest($"/channels/{Id}/followers?webhook_channel_id={webhookChannelId}", RequestMethod.POST, null, callback);
        }

        public void TriggerTypingIndicator(DiscordClient client, Action callback)
        {
            client.REST.DoRequest($"/channels/{Id}/typing", RequestMethod.POST, null, callback);
        }

        public void GetPinnedMessages(DiscordClient client, Action<List<Message>> callback = null)
        {
            client.REST.DoRequest<List<Message>>($"/channels/{Id}/pins", RequestMethod.GET, null, callback);
        }

        public void GroupDmAddRecipient(DiscordClient client, User user, string accessToken, Action callback = null) => GroupDmAddRecipient(client, user.Id, accessToken, user.Username, callback);

        public void GroupDmAddRecipient(DiscordClient client, string userId, string accessToken, string nick, Action callback = null)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "access_token", accessToken },
                { "nick", nick }
            };

            client.REST.DoRequest($"/channels/{Id}/recipients/{userId}", RequestMethod.PUT, jsonObj, callback);
        }

        public void GroupDmRemoveRecipient(DiscordClient client, string userId, Action callback)
        {
            client.REST.DoRequest($"/channels/{Id}/recipients/{userId}", RequestMethod.DELETE, null, callback);
        }
    }
}