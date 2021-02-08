namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction-response-interactionresponsetype">InteractionResponseType</a>
    /// </summary>
    public enum InteractionResponseType
    {
        /// <summary>
        /// Acknowledges a Ping
        /// </summary>
        Pong = 1,
        
        /// <summary>
        /// Acknowledge a command without sending a message, eating the user's input
        /// </summary>
        Acknowledge = 2,
        
        /// <summary>
        /// Respond with a message, eating the user's input
        /// </summary>
        ChannelMessage = 3,
        
        /// <summary>
        /// Respond with a message, showing the user's input
        /// </summary>
        ChannelMessageWithSource = 4,
        
        /// <summary>
        /// Acknowledge a command without sending a message, showing the user's input
        /// </summary>
        AcknowledgeWithSource = 5
    }
}