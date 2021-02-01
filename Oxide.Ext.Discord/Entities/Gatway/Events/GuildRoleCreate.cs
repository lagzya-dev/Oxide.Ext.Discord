using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Roles;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#guild-role-create">Guild Role Create</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildRoleCreate
    {
        /// <summary>
        /// The id of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        /// <summary>
        /// The role created
        /// </summary>
        [JsonProperty("role")]
        public Role Role { get; set; }
    }
}
