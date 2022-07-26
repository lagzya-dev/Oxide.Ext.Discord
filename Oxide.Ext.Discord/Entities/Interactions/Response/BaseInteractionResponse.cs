namespace Oxide.Ext.Discord.Entities.Interactions.Response
{
    /// <summary>
    /// Represents a Base Interaction response
    /// </summary>
    public abstract class BaseInteractionResponse
    {
        /// <summary>
        /// The type of response
        /// </summary>
        public InteractionResponseType Type { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseInteractionResponse() {}

        /// <summary>
        /// Constructor with <see cref="InteractionResponseType"/>
        /// </summary>
        /// <param name="type"></param>
        protected BaseInteractionResponse(InteractionResponseType type)
        {
            Type = type;
        }
    }
}