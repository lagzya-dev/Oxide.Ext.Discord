using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandInteractionDataOption
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("value")]
        public ApplicationCommandOptionType Value { get; set; }
        
        [JsonProperty("options")]
        public List<ApplicationCommandInteractionDataOption> Options { get; set; }
    }
}