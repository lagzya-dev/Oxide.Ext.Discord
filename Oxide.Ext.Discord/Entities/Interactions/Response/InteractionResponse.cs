using Newtonsoft.Json;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Ext.Discord.Entities.Interactions.Response
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object">Interaction Response</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InteractionResponse : BaseInteractionResponse<InteractionCallbackData>, IDiscordValidation
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InteractionResponse() {}
        
        /// <summary>
        /// Creates a <see cref="InteractionResponse"/>
        /// </summary>
        /// <param name="type">The type of the response</param>
        /// <param name="data">The data for the response</param>
        public InteractionResponse(InteractionResponseType type, InteractionCallbackData data)
        {
            Type = type;
            Data = data;
        }
        
        /// <inheritdoc/>
        public void Validate()
        {
            Data?.Validate();
        }
    }
}