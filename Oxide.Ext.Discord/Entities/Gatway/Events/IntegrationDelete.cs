using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents Integration Delete Structure TODO:Add URL Once Published
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class IntegrationDelete
    {
        /// <summary>
        /// ID of the integration
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// Guild ID the integration was in
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        /// <summary>
        /// Application ID of the integration
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
    }
}