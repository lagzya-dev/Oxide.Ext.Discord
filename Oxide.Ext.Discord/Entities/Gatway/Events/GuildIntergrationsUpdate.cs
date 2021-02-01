using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/topics/gateway#guild-integrations-update">Guild Integrations Update</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GuildIntergrationsUpdate
    {
        /// <summary>
        /// ID of the guild whose integrations were updated
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
