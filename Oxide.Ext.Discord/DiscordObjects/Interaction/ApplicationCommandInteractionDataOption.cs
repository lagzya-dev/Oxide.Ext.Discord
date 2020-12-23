using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.DiscordObjects.SlashCommands;

namespace Oxide.Ext.Discord.DiscordObjects.Interaction
{
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