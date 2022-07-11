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
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidCommandOptionException("Name cannot be less than 1 character");
            }
            
            if (name.Length > 32)
            {
                throw new InvalidCommandOptionException("Name cannot be more than 32 characters");
            }
        }
        
        internal static void ThrowIfInvalidDescription(string description, bool allowNullOrEmpty)
        {
            if (!allowNullOrEmpty && string.IsNullOrEmpty(description))
            {
                throw new InvalidCommandOptionException("Description cannot be less than 1 character");
            }
            
            if (description.Length > 100)
            {
                throw new InvalidCommandOptionException("Description cannot be more than 100 characters");
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
        
        internal static void ThrowIfInvalidChannelType(CommandOptionType type)
        {
            if (type != CommandOptionType.Channel)
            {
                throw new InvalidCommandOptionException("Can only set ChannelTypes for CommandOptionType.Channel");
            }
        }
    }
}