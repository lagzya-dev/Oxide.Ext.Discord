using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.SlashCommands;

namespace Oxide.Ext.Discord.Entities.Interactions
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