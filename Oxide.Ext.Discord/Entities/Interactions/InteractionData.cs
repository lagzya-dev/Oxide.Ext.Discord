using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction-applicationcommandinteractiondata">ApplicationCommandInteractionData</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InteractionData
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
        /// The type of the invoked command
        /// </summary>
        [JsonProperty("type")]
        public CommandType Type { get; set; }
        
        /// <summary>
        /// converted users + roles + channels
        /// </summary>
        [JsonProperty("resolved")]
        public InteractionDataResolved Resolved { get; set; }
        
        /// <summary>
        /// The params + values from the user
        /// </summary>
        [JsonProperty("options")]
        public List<InteractionDataOption> Options { get; set; }
        
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
        
        /// <summary>
        /// Id the of user or message targeted by a user or message command
        /// </summary>
        [JsonProperty("target_id")]
        public Snowflake? TargetId { get; set; }
    }
}