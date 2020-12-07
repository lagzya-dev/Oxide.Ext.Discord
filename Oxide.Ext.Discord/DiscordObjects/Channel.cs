using System.Text;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.DiscordObjects
{
    using System;
    using System.Collections.Generic;
    using Oxide.Ext.Discord.REST;

    public class Channel : ChannelCreate
    {
        public string id { get; set; }
        
        public string guild_id { get; set; }
        
        public string last_message_id { get; set; }
        
        public List<User> recipients { get; set; }

        public string icon { get; set; }

        public string owner_id { get; set; }

        public string application_id { get; set; }

        public DateTime? last_pin_timestamp { get; set; } 

        public static void GetChannel(DiscordClient client, string channelID, Action<Channel> callback = null)
        {
            client.REST.DoRequest($"/channels/{channelID}", RequestMethod.GET, null, callback);
        }

        public void ModifyChannel(DiscordClient client, ChannelCreate newChannel, Action<Channel> callback = null)
        {
            client.REST.DoRequest($"/channels/{id}", RequestMethod.PATCH, newChannel, callback);
        }

        public void DeleteChannel(DiscordClient client, Action<Channel> callback = null)
        {
            client.REST.DoRequest($"/channels/{id}", RequestMethod.DELETE, null, callback);
        }

        public void GetChannelMessages(DiscordClient client, Action<List<Message>> callback = null)
        {
            client.REST.DoRequest($"/channels/{id}/messages", RequestMethod.GET, null, callback);
        }

        public void GetChannelMessage(DiscordClient client, Message message, Action<Message> callback = null) => GetChannelMessage(client, message.id, callback);

        public void GetChannelMessage(DiscordClient client, string messageID, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/channels/{id}/messages/{messageID}", RequestMethod.GET, null, callback);
        }

        public void CreateMessage(DiscordClient client, MessageCreate message, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/channels/{id}/messages", RequestMethod.POST, message, callback);
        }

        public void CreateMessage(DiscordClient client, string message, Action<Message> callback = null)
        {
            MessageCreate createMessage = new MessageCreate()
            {
                content = message
            };

            client.REST.DoRequest($"/channels/{id}/messages", RequestMethod.POST, createMessage, callback);
        }

        public void CreateMessage(DiscordClient client, Embed embed, Action<Message> callback = null)
        {
            MessageCreate createMessage = new MessageCreate()
            {
                embed = embed
            };

            client.REST.DoRequest($"/channels/{id}/messages", RequestMethod.POST, createMessage, callback);
        }

        public void BulkDeleteMessages(DiscordClient client, string[] messageIds, Action callback = null)
        {
            var jsonObj = new Dictionary<string, string[]>()
            {
                { "messages", messageIds }
            };

            client.REST.DoRequest($"/channels/{id}/messages/bulk-delete", RequestMethod.POST, jsonObj, callback);
        }

        public void EditChannelPermissions(DiscordClient client, Overwrite overwrite, Action callback = null)
        {
            client.REST.DoRequest($"/channels/{id}/permissions/{overwrite.Id}", RequestMethod.PUT, overwrite, callback);
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
            if (type == ChannelType.DM || type == ChannelType.GROUP_DM)
            {
                throw new Exception("You can only get channel invites for guild channels.");
            }
            
            client.REST.DoRequest($"/channels/{id}/invites", RequestMethod.GET, null, callback);
        }

        public void CreateChannelInvite(DiscordClient client, ChannelInvite invite, Action<Invite> callback = null)
        {
            client.REST.DoRequest<Invite>($"/channels/{id}/invites", RequestMethod.POST, invite, callback);
        }

        public void DeleteChannelPermission(DiscordClient client, Overwrite overwrite, Action callback) => DeleteChannelPermission(client, overwrite.Id, callback);

        public void DeleteChannelPermission(DiscordClient client, string overwriteID, Action callback)
        {
            client.REST.DoRequest($"/channels/{id}/permissions/{overwriteID}", RequestMethod.DELETE, null, callback);
        }

        public void FollowNewsChannel(DiscordClient client, string targetChannelId, Action<Channel> callback)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["webhook_channel_id"] = targetChannelId
            };
            
            client.REST.DoRequest($"/channels/{id}/invites", RequestMethod.POST, data, callback);
        }

        public void TriggerTypingIndicator(DiscordClient client, Action callback)
        {
            client.REST.DoRequest($"/channels/{id}/typing", RequestMethod.POST, null, callback);
        }

        public void GetPinnedMessages(DiscordClient client, Action<List<Message>> callback = null)
        {
            client.REST.DoRequest<List<Message>>($"/channels/{id}/pins", RequestMethod.GET, null, callback);
        }

        public void GroupDMAddRecipient(DiscordClient client, User user, string accessToken, Action callback = null) => GroupDMAddRecipient(client, user.id, accessToken, user.username, callback);

        public void GroupDMAddRecipient(DiscordClient client, string userID, string accessToken, string nick, Action callback = null)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "access_token", accessToken },
                { "nick", nick }
            };

            client.REST.DoRequest($"/channels/{id}/recipients/{userID}", RequestMethod.PUT, jsonObj, callback);
        }

        public void GroupDMRemoveRecipient(DiscordClient client, string userID, Action callback)
        {
            client.REST.DoRequest($"/channels/{id}/recipients/{userID}", RequestMethod.DELETE, null, callback);
        }
    }
}