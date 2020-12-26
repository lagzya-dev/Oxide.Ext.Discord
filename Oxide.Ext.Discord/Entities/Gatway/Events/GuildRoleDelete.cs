using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildRoleDelete
    {
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }

        [JsonProperty("role_id")]
        public Snowflake RoleId { get; set; }
    }
}
