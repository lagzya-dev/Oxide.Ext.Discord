using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.SlashCommands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandOption
    {
        [JsonProperty("type")]
        public ApplicationCommandOptionType Type { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("default")]
        public bool? Default { get; set; }
        
        [JsonProperty("required")]
        public bool? Required { get; set; }
        
        [JsonProperty("choices")]
        public List<ApplicationCommandOptionChoice> Choices { get; set; }
        
        [JsonProperty("options")]
        public List<ApplicationCommandOption> Options { get; set; }
    }
}