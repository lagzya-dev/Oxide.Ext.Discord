using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageReference
    {
        [JsonProperty("message_id")]
        public Snowflake MessageId { get; set; }
        
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}