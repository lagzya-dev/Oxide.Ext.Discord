using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class TypingStart
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("timestamp")]
        public int? Timestamp { get; set; }
        
        [JsonProperty("member")]
        public GuildMember Member { get; set; }
    }
}
