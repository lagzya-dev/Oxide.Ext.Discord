using Newtonsoft.Json;
using Oxide.Ext.Discord.Validations;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object">Interaction Response</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InteractionResponse : IDiscordValidation
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
        public InteractionCallbackData Data { get; set; }

        /// <inheritdoc/>
        public void Validate()
        {
            Data?.Validate();
        }
    }
}