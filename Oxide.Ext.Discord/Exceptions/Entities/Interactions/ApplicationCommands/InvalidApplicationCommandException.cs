namespace Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents an invalid application command
    /// </summary>
    public class InvalidApplicationCommandException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        private InvalidApplicationCommandException(string message) : base(message)
        {
            
        }
        
        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidApplicationCommandException("Name cannot be less than 1 character");
            }
            
            if (name.Length > 32)
            {
                throw new InvalidApplicationCommandException("Name cannot be more than 32 characters");
            }
        }
        
        internal static void ThrowIfInvalidDescription(string description, bool allowNullOrEmpty)
        {
            if (!allowNullOrEmpty && string.IsNullOrEmpty(description))
            {
                throw new InvalidApplicationCommandException("Description cannot be less than 1 character");
            }
            
            if (description.Length > 100)
            {
                throw new InvalidApplicationCommandException("Description cannot be more than 100 characters");
            }
        }
    }
}