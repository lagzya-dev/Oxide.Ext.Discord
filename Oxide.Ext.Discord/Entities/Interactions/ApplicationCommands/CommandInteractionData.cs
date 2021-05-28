using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction-applicationcommandinteractiondata">ApplicationCommandInteractionData</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandInteractionData
    {
        /// <summary>
        /// ID of the invoked command
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// The name of the invoked command
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// converted users + roles + channels
        /// </summary>
        [JsonProperty("resolved")]
        public CommandInteractionDataResolved Resolved { get; set; }
        
        /// <summary>
        /// The params + values from the user
        /// </summary>
        [JsonProperty("options")]
        public List<CommandInteractionDataOption> Options { get; set; }
        
        /// <summary>
        /// For components, the custom_id of the component
        /// </summary>
        [JsonProperty("custom_id")]
        public string CustomId { get; set; }
        
        /// <summary>
        /// For components, the type of the component
        /// </summary>
        [JsonProperty("component_type")]
        public MessageComponentType? ComponentType { get; set; }
    }
}