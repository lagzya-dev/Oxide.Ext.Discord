using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.SlashCommands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommand : ApplicationCommandCreate
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }
    }
}