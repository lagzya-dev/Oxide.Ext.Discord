using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.SlashCommands
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandOptionChoice
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}