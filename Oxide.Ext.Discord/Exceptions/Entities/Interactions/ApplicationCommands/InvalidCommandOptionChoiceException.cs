using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents an error in application command option choice
    /// </summary>
    public class InvalidCommandOptionChoiceException : BaseDiscordException
    {
        private InvalidCommandOptionChoiceException(string message) : base(message) { }
        
        internal static void ThrowIfMaxChoices(int count)
        {
            if (count > 25)
            {
                ThrowMaxChoices();
            }
        }

        internal static void ThrowMaxChoices()
        {
            throw new InvalidCommandOptionChoiceException("Cannot have more than 25 Command Option Choices");
        }
        
        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidCommandOptionChoiceException("Name cannot be less than 1 character");
            }
            
            if (name.Length > 100)
            {
                throw new InvalidCommandOptionChoiceException("Name cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidStringValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidCommandOptionChoiceException("Value cannot be less than 1 character");
            }
            
            if (value.Length > 100)
            {
                throw new InvalidCommandOptionChoiceException("Value cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidType(CommandOptionType value, CommandOptionType expected)
        {
            if (value != expected)
            {
                throw new InvalidCommandOptionChoiceException($"Cannot add a {value} choice type to type: {expected}");
            }
        }
    }
}