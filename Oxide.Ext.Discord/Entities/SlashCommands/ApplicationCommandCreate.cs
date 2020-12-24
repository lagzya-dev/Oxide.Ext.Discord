using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.SlashCommands
{
    public class ApplicationCommandCreate
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("options")]
        public List<ApplicationCommandOption> Options { get; set; }
    }
}