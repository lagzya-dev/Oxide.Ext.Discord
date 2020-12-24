using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Roles;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class GuildRoleUpdate
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("role")]
        public Role Role { get; set; }
    }
}
