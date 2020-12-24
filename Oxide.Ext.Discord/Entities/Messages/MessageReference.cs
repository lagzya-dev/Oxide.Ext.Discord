using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
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