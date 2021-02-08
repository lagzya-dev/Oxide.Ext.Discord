using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class IntegrationDelete
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
    }
}