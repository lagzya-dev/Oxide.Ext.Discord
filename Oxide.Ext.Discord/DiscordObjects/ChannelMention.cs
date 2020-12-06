using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class ChannelMention
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("type")]
        public ChannelType Type { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}