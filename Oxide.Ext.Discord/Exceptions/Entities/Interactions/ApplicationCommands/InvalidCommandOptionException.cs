using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents an error in application command option
    /// </summary>
    public class InvalidCommandOptionException : BaseDiscordException
    {
        private InvalidCommandOptionException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            const int maxLength = 32;
            
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidCommandOptionException("Name cannot be less than 1 character");
            }
            
            if (name.Length > maxLength)
            {
                throw new InvalidCommandOptionException($"Name cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidDescription(string description, bool allowNullOrEmpty)
        {
            const int maxLength = 100;
            
            if (!allowNullOrEmpty && string.IsNullOrEmpty(description))
            {
                throw new InvalidCommandOptionException("Description cannot be less than 1 character");
            }
            
            if (description.Length > maxLength)
            {
                throw new InvalidCommandOptionException($"Description cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidType(CommandOptionType type)
        {
            if (type == CommandOptionType.SubCommand || type == CommandOptionType.SubCommandGroup)
            {
                throw new InvalidCommandOptionException($"{type} is not allowed to be used here. Valid types are any non command type.");
            }
        }
        
        internal static void ThrowIfInvalidMinIntegerType(CommandOptionType type)
        {
            if (type != CommandOptionType.Integer && type != CommandOptionType.Number)
            {
                throw new InvalidCommandOptionException("Can only set min value for Integer or Number Type");
            }
        }
        
        internal static void ThrowIfInvalidMinNumberType(CommandOptionType type)
        {
            if (type != CommandOptionType.Number)
            {
                throw new InvalidCommandOptionException("Can only set min value for Number Type");
            }
        }
        
        internal static void ThrowIfInvalidMaxIntegerType(CommandOptionType type)
        {
            if (type != CommandOptionType.Integer && type != CommandOptionType.Number)
            {
                throw new InvalidCommandOptionException("Can only set max value for Integer or Number Type");
            }
        }
        
        internal static void ThrowIfInvalidMaxNumberType(CommandOptionType type)
        {
            if (type != CommandOptionType.Number)
            {
                throw new InvalidCommandOptionException("Can only set max value for Number Type");
            }
        }
        
        internal static void ThrowIfInvalidMinLengthType(CommandOptionType type)
        {
            if (type != CommandOptionType.String)
            {
                throw new InvalidCommandOptionException("Can only set min length for string Type");
            }
        }
        
        internal static void ThrowIfInvalidMaxLengthType(CommandOptionType type)
        {
            if (type != CommandOptionType.String)
            {
                throw new InvalidCommandOptionException("Can only set max length for string Type");
            }
        }
        
        internal static void ThrowIfInvalidMinLength(int minLength)
        {
            const int min = 0;
            const int maxLength = 6000;
            
            if (minLength < min)
            {
                throw new InvalidCommandOptionException($"Min length cannot be less than {min}");
            }

            if (minLength > maxLength)
            {
                throw new InvalidCommandOptionException($"Min length cannot be more than {maxLength}");
            }
        }
        
        internal static void ThrowIfInvalidMaxLength(int maxLength)
        {
            const int minLength = 1;
            const int max = 6000;
            
            if (maxLength < minLength)
            {
                throw new InvalidCommandOptionException($"Max length cannot be less than {minLength}");
            }

            if (maxLength > max)
            {
                throw new InvalidCommandOptionException($"Max length cannot be more than {max}");
            }
        }
        
        internal static void ThrowIfInvalidChannelType(CommandOptionType type)
        {
            if (type != CommandOptionType.Channel)
            {
                throw new InvalidCommandOptionException("Can only set ChannelTypes for CommandOptionType.Channel");
            }
        }
    }
}