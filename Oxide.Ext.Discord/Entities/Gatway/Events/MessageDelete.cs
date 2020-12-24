using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class MessageDelete
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
