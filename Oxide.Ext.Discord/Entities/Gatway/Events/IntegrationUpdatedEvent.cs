using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Integrations;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents Integration Update Structure TODO:Add URL Once Published
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class IntegrationUpdatedEvent : Integration
    {
        /// <summary>
        /// Guild ID the integration was updated In
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}