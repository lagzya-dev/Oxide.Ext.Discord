using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction-response">Interaction Response</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InteractionResponse
    {
        /// <summary>
        /// The type of response
        /// </summary>
        [JsonProperty("type")]
        public InteractionResponseType Type { get; set; }
        
        /// <summary>
        /// An optional response message
        /// </summary>
        [JsonProperty("data")]
        public ApplicationCommandCallbackData Data { get; set; }
    }
}