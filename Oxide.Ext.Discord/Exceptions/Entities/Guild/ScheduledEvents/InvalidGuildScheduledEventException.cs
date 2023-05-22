namespace Oxide.Ext.Discord.Exceptions.Entities.Guild.ScheduledEvents
{
    /// <summary>
    /// Represents an exception in guild scheduled events
    /// </summary>
    public class InvalidGuildScheduledEventException : BaseDiscordException
    {
        private InvalidGuildScheduledEventException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            const int maxLength = 100;
            
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidGuildScheduledEventException($"Name cannot be less than 1 character");
            }
            
            if (name.Length > maxLength)
            {
                throw new InvalidGuildScheduledEventException($"Name cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidDescription(string description)
        {
            const int maxLength = 1000;
            
            if (!string.IsNullOrEmpty(description) && description.Length > maxLength)
            {
                throw new InvalidGuildScheduledEventException($"Description cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidLocation(string location)
        {
            const int maxLength = 100;
            
            if (!string.IsNullOrEmpty(location) && location.Length > 100)
            {
                throw new InvalidGuildScheduledEventException($"Location cannot be more than {maxLength} characters");
            }
        }
    }
}