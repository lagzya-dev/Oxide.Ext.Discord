using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class TypingStart
    {
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        [JsonProperty("user_id")]
        public Snowflake UserId { get; set; }

        [JsonProperty("timestamp")]
        public int? Timestamp { get; set; }
        
        [JsonProperty("member")]
        public GuildMember Member { get; set; }
    }
}
