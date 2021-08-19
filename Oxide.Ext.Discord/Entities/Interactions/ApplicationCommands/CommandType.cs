using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="">Application Command Type</a>
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// Slash commands; a text-based command that shows up when a user types /
        /// </summary>
        [Description("CHAT_INPUT")]
        ChatInput,
        
        /// <summary>
        /// A UI-based command that shows up when you right click or tap on a user
        /// </summary>
        [Description("USER")]
        User,
        
        /// <summary>
        /// A UI-based command that shows up when you right click or tap on a messages
        /// </summary>
        [Description("MESSAGE")]
        Message
    }
}