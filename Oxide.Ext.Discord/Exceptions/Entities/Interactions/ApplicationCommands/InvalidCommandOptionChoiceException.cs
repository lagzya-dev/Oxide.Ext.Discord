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
            const int maxChoices = 25;
            
            if (count > maxChoices)
            {
                throw new InvalidCommandOptionChoiceException($"Cannot have more than {maxChoices} Command Option Choices");
            }
        }

        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            const int maxLength = 100;
            
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidCommandOptionChoiceException("Name cannot be less than 1 character");
            }
            
            if (name.Length > maxLength)
            {
                throw new InvalidCommandOptionChoiceException($"Name cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidStringValue(string value)
        {
            const int maxLength = 100;
            
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidCommandOptionChoiceException("Value cannot be less than 1 character");
            }
            
            if (value.Length > maxLength)
            {
                throw new InvalidCommandOptionChoiceException($"Value cannot be more than {maxLength} characters");
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