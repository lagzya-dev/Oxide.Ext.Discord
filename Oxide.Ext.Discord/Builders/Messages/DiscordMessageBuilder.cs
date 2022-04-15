using Oxide.Ext.Discord.Builders.Messages.BaseBuilders;
using Oxide.Ext.Discord.Entities.Messages;

namespace Oxide.Ext.Discord.Builders.Messages
{
    /// <summary>
    /// Represents a builder for <see cref="MessageCreate"/>
    /// </summary>
    public class DiscordMessageBuilder : BaseChannelMessageBuilder<MessageCreate, DiscordMessageBuilder>
    {
        /// <summary>
        /// Constructor creating a new message
        /// </summary>
        public DiscordMessageBuilder() : this(new MessageCreate()) { }
        
        /// <summary>
        /// Constructor to use existing message
        /// </summary>
        /// <param name="message">Message to use</param>
        public DiscordMessageBuilder(MessageCreate message) : base(message) { }
    }
}