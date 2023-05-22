using Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents;

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
            const int maxLimit = 100;
            if (limit > maxLimit)
            {
                throw new InvalidGuildScheduledEventLookupException($"{nameof(ScheduledEventUsersLookup)}.{nameof(ScheduledEventUsersLookup.Limit)} cannot be greater than {maxLimit}");
            }
        }
    }
}