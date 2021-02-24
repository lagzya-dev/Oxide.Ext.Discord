using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Integrations;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    /// <summary>
    /// Represents Integration Create Structure TODO:Add URL Once Published
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class IntegrationCreate : Integration
    {
        /// <summary>
        /// Guild Id the Integration was created in
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}