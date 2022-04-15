namespace Oxide.Ext.Discord.Exceptions.Entities.Guild.ScheduledEvents
{
    /// <summary>
    /// Represents an exception in guild schedule event lookup requests
    /// </summary>
    public class InvalidGuildScheduledEventLookupException : BaseDiscordException
    {
        private InvalidGuildScheduledEventLookupException(string message) : base(message) { }

        internal static void ThrowIfInvalidLimit(int? limit)
        {
            if (limit > 100)
            {
                throw new InvalidGuildScheduledEventLookupException("Limit cannot be greater than 100");
            }
        }
    }
}