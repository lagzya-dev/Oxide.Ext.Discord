using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageReactionRemoveAll
    {
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }

        [JsonProperty("message_id")]
        public Snowflake MessageId { get; set; }
        
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
