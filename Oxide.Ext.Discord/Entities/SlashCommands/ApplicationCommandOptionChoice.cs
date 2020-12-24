using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.SlashCommands
{
    public class ApplicationCommandOptionChoice
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}