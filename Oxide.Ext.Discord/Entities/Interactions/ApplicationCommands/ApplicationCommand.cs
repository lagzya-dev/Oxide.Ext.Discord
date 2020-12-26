using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommand : ApplicationCommandCreate
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
    }
}