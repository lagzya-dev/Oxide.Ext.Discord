using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildIntergrationsUpdate
    {
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
