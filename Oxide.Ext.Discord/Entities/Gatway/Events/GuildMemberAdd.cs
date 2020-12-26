using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildMemberAdd : GuildMember
    {
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
