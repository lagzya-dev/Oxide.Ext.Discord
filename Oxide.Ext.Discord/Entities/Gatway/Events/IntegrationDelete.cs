using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class IntegrationDelete
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }
    }
}