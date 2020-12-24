using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandInteractionData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("options")]
        public List<ApplicationCommandInteractionDataOption> Options { get; set; }
    }
}