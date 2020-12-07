using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Oxide.Ext.Discord.Helpers;
    using Oxide.Ext.Discord.REST;
    
    public class Message : MessageCreate
    {
        public string id { get; set; }

        public string channel_id { get; set; }

        public string guild_id { get; set; }

        public User author { get; set; }

        public GuildMember member { get; set; }

        public string timestamp { get; set; }

        public string edited_timestamp { get; set; }

        public bool mention_everyone { get; set; }

        public List<User> mentions { get; set; }

        public List<string> mention_roles { get; set; }
        
        [JsonProperty("mention_channels")]
        public List<ChannelMention> MentionsChannels { get; set; }

        public List<Attachment> attachments { get; set; }

        public List<Embed> embeds { get; set; }

        public List<Reaction> reactions { get; set; }

        public bool pinned { get; set; }

        public string webhook_id { get; set; }

        public MessageType? type { get; set; }
        
        [JsonProperty("activity")]
        public MessageActivity Activity { get; set; }
        
        [JsonProperty("application")]
        public MessageApplication Application { get; set; }

        [JsonProperty("flags")]
        public MessageFlags Flags { get; set; }
        
        [JsonProperty("stickers")]
        public List<MessageSticker> Stickers { get; set; }
        
        [JsonProperty("referenced_message")]
        public Message ReferencedMessage { get; private set; }

        public void Reply(DiscordClient client, Message message, bool ping = true, string messageId = null, Action<Message> callback = null)
        {
            if (ping && !string.IsNullOrEmpty(message.content) && !message.content.Contains($"<@{author.id}>"))
            {
                message.content = $"<@{author.id}> {message.content}";
            }

            if (!string.IsNullOrEmpty(messageId))
            {
                message.MessageReference = new MessageReference {MessageId = messageId};
            }

            client.REST.DoRequest($"/channels/{channel_id}/messages", RequestMethod.POST, message, callback);
        }

        public void Reply(DiscordClient client, string message, bool ping = true, string messageId = null, Action<Message> callback = null)
        {
            Message newMessage = new Message()
            {
                content = ping ? $"<@{author.id}> {message}" : message
            };
            
            if (!string.IsNullOrEmpty(messageId))
            {
                newMessage.MessageReference = new MessageReference {MessageId = messageId};
            }

            Reply(client, newMessage, ping, messageId, callback);
        }

        public void Reply(DiscordClient client, Embed embed, bool ping = true, string messageId = null, Action<Message> callback = null)
        {
            Message newMessage = new Message()
            {
                content = ping ? $"<@{author.id}>" : null,
                embed = embed
            };

            Reply(client, newMessage, ping, messageId, callback);
        }
        
        public void CrossPostMessage(DiscordClient client, string messageId, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/channels/{id}/messages/{messageId}/crosspost", RequestMethod.POST, null, callback);
        }
        
        public void CrossPostMessage(DiscordClient client, Message message, Action<Message> callback = null)
        {
            CrossPostMessage(client, message.id, callback);
        }

        public void CreateReaction(DiscordClient client, string emoji, Action callback = null)
        {
            byte[] encodedEmoji = Encoding.UTF8.GetBytes(emoji);
            string hexString = HttpUtility.UrlEncode(encodedEmoji);
            
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions/{hexString}/@me", RequestMethod.PUT, null, callback);
        }

        public void DeleteOwnReaction(DiscordClient client, string emoji, Action callback = null)
        {
            byte[] encodedEmoji = Encoding.UTF8.GetBytes(emoji);
            string hexString = HttpUtility.UrlEncode(encodedEmoji);
            
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions/{hexString}/@me", RequestMethod.DELETE, null, callback);
        }

        public void DeleteUserReaction(DiscordClient client, string emoji, User user, Action callback = null) => DeleteUserReaction(client, emoji, user.id, callback);

        public void DeleteUserReaction(DiscordClient client, string emoji, string userID, Action callback = null)
        {
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions/{emoji}/{userID}", RequestMethod.DELETE, null, callback);
        }

        public void GetReactions(DiscordClient client, string emoji, Action<List<User>> callback = null)
        {
            byte[] encodedEmoji = Encoding.UTF8.GetBytes(emoji);
            string hexString = HttpUtility.UrlEncode(encodedEmoji);

            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions/{hexString}", RequestMethod.GET, null, callback);
        }

        public void DeleteAllReactions(DiscordClient client, Action callback = null)
        {
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions", RequestMethod.DELETE, null, callback);
        }
        
        public void DeleteAllReactionsForEmoji(DiscordClient client, string emoji, Action callback = null)
        {
            byte[] encodedEmoji = Encoding.UTF8.GetBytes(emoji);
            string hexString = HttpUtility.UrlEncode(encodedEmoji);
            
            client.REST.DoRequest($"/channels/{channel_id}/messages/{id}/reactions/{hexString}", RequestMethod.DELETE, null, callback);
        }

        public void EditMessage(DiscordClient client, Action<Message> callback = null)
        {
            client.REST.DoRequest<Message>($"/channels/{channel_id}/messages/{id}", RequestMethod.PATCH, this, callback);
        }

        public void DeleteMessage(DiscordClient client, Action<Message> callback = null)
        {
            client.REST.DoRequest<Message>($"/channels/{channel_id}/messages/{id}", RequestMethod.DELETE, null, callback);
        }

        public void AddPinnedChannelMessage(DiscordClient client, Action callback = null)
        {
            client.REST.DoRequest($"/channels/{channel_id}/pins/{id}", RequestMethod.PUT, null, callback);
        }

        public void DeletePinnedChannelMessage(DiscordClient client, Action callback = null)
        {
            client.REST.DoRequest($"/channels/{channel_id}/pins/{id}", RequestMethod.DELETE, null, callback);
        }
    }
}
