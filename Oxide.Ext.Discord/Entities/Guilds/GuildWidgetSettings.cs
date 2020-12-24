using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds
{
    public class GuildWidgetSettings
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
    }
}
