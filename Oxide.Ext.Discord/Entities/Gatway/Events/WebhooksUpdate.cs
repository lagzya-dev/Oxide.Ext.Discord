using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WebhooksUpdate
    {
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }

        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
    }
}
