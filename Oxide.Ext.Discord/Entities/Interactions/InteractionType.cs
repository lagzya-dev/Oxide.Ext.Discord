namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction-interactiontype">InteractionType</a>
    /// </summary>
    public enum InteractionType
    {
        /// <summary>
        /// The interaction is a ping
        /// </summary>
        Ping = 1,
        
        /// <summary>
        /// The interaction is a user using a command
        /// </summary>
        ApplicationCommand = 2,
        
        /// <summary>
        /// The interaction is a message component
        /// </summary>
        MessageComponent = 3
    }
}