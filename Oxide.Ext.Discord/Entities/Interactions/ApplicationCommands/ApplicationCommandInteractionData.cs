using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction-applicationcommandinteractiondata">ApplicationCommandInteractionData</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandInteractionData
    {
        /// <summary>
        /// ID of the invoked command
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// The name of the invoked command
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// The params + values from the user
        /// </summary>
        [JsonProperty("options")]
        public List<ApplicationCommandInteractionDataOption> Options { get; set; }
    }
}