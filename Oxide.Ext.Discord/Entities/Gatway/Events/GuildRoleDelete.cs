using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#guild-role-delete">Guild Role Delete</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildRoleDelete
    {
        /// <summary>
        /// ID of the guild
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        /// <summary>
        /// ID of the role
        /// </summary>
        [JsonProperty("role_id")]
        public string RoleId { get; set; }
    }
}
