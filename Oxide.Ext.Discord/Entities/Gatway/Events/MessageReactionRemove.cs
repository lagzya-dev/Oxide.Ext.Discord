using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Emojis;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class MessageReactionRemove
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
    }
}