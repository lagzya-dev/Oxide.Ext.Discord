using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Messages.Attachments;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Messages
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Message : MessageCreate
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }

        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }

        [JsonProperty("author")]
        public DiscordUser Author { get; set; }

        [JsonProperty("member")]
        public GuildMember Member { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("edited_timestamp")]
        public string EditedTimestamp { get; set; }

        [JsonProperty("mention_everyone")]
        public bool MentionEveryone { get; set; }

        [JsonProperty("mentions")]
        public List<DiscordUser> Mentions { get; set; }

        [JsonProperty("mention_roles")]
        public List<string> MentionRoles { get; set; }
        
        [JsonProperty("mention_channels")]
        public List<ChannelMention> MentionsChannels { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("embeds")]
        public List<Embed> Embeds { get; set; }

        [JsonProperty("reactions")]
        public List<Reaction> Reactions { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("webhook_id")]
        public Snowflake WebhookId { get; set; }

        [JsonProperty("type")]
        public MessageType? Type { get; set; }
        
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

        public void Reply(DiscordClient client, Message message, Snowflake messageId, Action<Message> callback = null)
        {
            message.MessageReference = new MessageReference {MessageId = messageId};
            client.Bot.Rest.DoRequest($"/channels/{ChannelId}/messages", RequestMethod.POST, message, callback);
        }

        public void Reply(DiscordClient client, string message, Snowflake messageId, Action<Message> callback = null)
        {
            Message newMessage = new Message()
            {
                Content = message
            };

            Reply(client, newMessage, messageId, callback);
        }

        public void Reply(DiscordClient client, Embed embed, Snowflake messageId, Action<Message> callback = null)
        {
            Message newMessage = new Message()
            {
                Embed = embed
            };

            Reply(client, newMessage, messageId, callback);
        }
        
        public void CrossPostMessage(DiscordClient client, Snowflake messageId, Action<Message> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{Id}/messages/{messageId}/crosspost", RequestMethod.POST, null, callback);
        }
        
        public void CrossPostMessage(DiscordClient client, Message message, Action<Message> callback = null)
        {
            CrossPostMessage(client, message.Id, callback);
        }

        public void CreateReaction(DiscordClient client, string emoji, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{ChannelId}/messages/{Id}/reactions/{Uri.EscapeDataString(emoji)}/@me", RequestMethod.PUT, null, callback);
        }

        public void DeleteOwnReaction(DiscordClient client, string emoji, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{ChannelId}/messages/{Id}/reactions/{Uri.EscapeDataString(emoji)}/@me", RequestMethod.DELETE, null, callback);
        }

        public void DeleteUserReaction(DiscordClient client, string emoji, DiscordUser user, Action callback = null) => DeleteUserReaction(client, emoji, user.Id, callback);

        public void DeleteUserReaction(DiscordClient client, string emoji, Snowflake userId, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{ChannelId}/messages/{Id}/reactions/{emoji}/{userId}", RequestMethod.DELETE, null, callback);
        }

        public void GetReactions(DiscordClient client, string emoji, Action<List<DiscordUser>> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{ChannelId}/messages/{Id}/reactions/{Uri.EscapeDataString(emoji)}", RequestMethod.GET, null, callback);
        }

        public void DeleteAllReactions(DiscordClient client, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{ChannelId}/messages/{Id}/reactions", RequestMethod.DELETE, null, callback);
        }
        
        public void DeleteAllReactionsForEmoji(DiscordClient client, string emoji, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{ChannelId}/messages/{Id}/reactions/{Uri.EscapeDataString(emoji)}", RequestMethod.DELETE, null, callback);
        }

        public void EditMessage(DiscordClient client, Action<Message> callback = null)
        {
            client.Bot.Rest.DoRequest<Message>($"/channels/{ChannelId}/messages/{Id}", RequestMethod.PATCH, this, callback);
        }

        public void DeleteMessage(DiscordClient client, Action<Message> callback = null)
        {
            client.Bot.Rest.DoRequest<Message>($"/channels/{ChannelId}/messages/{Id}", RequestMethod.DELETE, null, callback);
        }

        public void AddPinnedChannelMessage(DiscordClient client, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{ChannelId}/pins/{Id}", RequestMethod.PUT, null, callback);
        }

        public void DeletePinnedChannelMessage(DiscordClient client, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{ChannelId}/pins/{Id}", RequestMethod.DELETE, null, callback);
        }
    }
}
