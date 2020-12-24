using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages
{
    public class MessageReference
    {
        [JsonProperty("message_id")]
        public string MessageId { get; set; }
        
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}