using Oxide.Ext.Discord.Builders.MessageComponents;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;

namespace Oxide.Ext.Discord.Exceptions.Builders
{
    /// <summary>
    /// Represents an exception in Message Component Builder
    /// </summary>
    public class MessageComponentBuilderException : BaseDiscordException
    {
        private MessageComponentBuilderException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidActionButtonStyle(ButtonStyle style)
        {
            if (style == ButtonStyle.Link)
            {
                throw new MessageComponentBuilderException($"Cannot add link button as action button. Please use {nameof(MessageComponentBuilder.AddLinkButton)} instead");
            }
        }
    }
}