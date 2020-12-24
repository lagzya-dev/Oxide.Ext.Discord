using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Emojis;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageReactionRemoveEmoji
    {
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