using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Channels
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ChannelMention
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        [JsonProperty("type")]
        public ChannelType Type { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}