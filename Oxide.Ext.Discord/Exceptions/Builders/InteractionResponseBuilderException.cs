using Oxide.Ext.Discord.Entities.Interactions;

namespace Oxide.Ext.Discord.Exceptions.Builders
{
    /// <summary>
    /// Represents an Interaction Response Builder Exception
    /// </summary>
    public class InteractionResponseBuilderException : BaseDiscordException
    {
        private InteractionResponseBuilderException(string message) : base(message) { }

        internal static void ThrowIfInteractionIsAutoComplete(InteractionType type)
        {
            if (type == InteractionType.ApplicationCommandAutocomplete)
            {
                throw new InteractionResponseBuilderException("Cannot call this method because you can only add Auto Complete Choices to this interaction response");
            }
        }
        
        internal static void ThrowIfInteractionIsNotAutoComplete(InteractionType type)
        {
            if (type != InteractionType.ApplicationCommandAutocomplete)
            {
                throw new InteractionResponseBuilderException("Cannot call this method because this is not an Auto Complete interaction");
            }
        }

        internal static void ThrowIfInteractionIsModalSubmit(InteractionType type)
        {
            if (type == InteractionType.ModalSubmit)
            {
                throw new InteractionResponseBuilderException("You cannot open a modal from another modal");
            }
        }
    }
}