namespace Oxide.Ext.Discord.Entities.Interactions.Response
{
    /// <summary>
    /// Represents a Base Interaction Response with generic data {T}
    /// </summary>
    /// <typeparam name="T">{T} data type</typeparam>
    public abstract class BaseInteractionResponse<T> : BaseInteractionResponse
    {
        /// <summary>
        /// Response to the Interaction
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseInteractionResponse() { }

        /// <summary>
        /// Constructor with Data and response type
        /// </summary>
        /// <param name="type">Response type for the interaction</param>
        /// <param name="data">Data for the interaction</param>
        protected BaseInteractionResponse(InteractionResponseType type, T data) : base(type)
        {
            Data = data;
        }
    }
}