using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.AppCommands;

namespace Oxide.Ext.Discord.Exceptions.Libraries
{
    /// <summary>
    /// Represents an Exception in <see cref="DiscordAppCommand"/> Oxide Library
    /// </summary>
    public class DiscordAppCommandException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        protected DiscordAppCommandException(string message) : base(message) { }

        internal static void ThrowIfMessageComponent(InteractionType type)
        {
            if (type == InteractionType.MessageComponent)
            {
                throw new DiscordAppCommandException($"Cannot use InteractionType.MessageComponent type in {nameof(DiscordAppCommand)}.{nameof(DiscordAppCommand.AddApplicationCommand)}. Please use {nameof(DiscordAppCommand)}.{nameof(DiscordAppCommand.AddMessageComponentCommand)} instead.");
            }
        }
    }
}