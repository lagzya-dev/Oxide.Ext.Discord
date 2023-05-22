using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    /// <summary>
    /// Represents an exception in guild prune requests
    /// </summary>
    public class InvalidGuildPruneException : BaseDiscordException
    {
        private InvalidGuildPruneException(string message) : base(message) { }

        internal static void ThrowIfInvalidDays(int days)
        {
            const int minDays = 1;
            const int maxDays = 30;

            if (days < minDays)
            {
                throw new InvalidGuildPruneException($"{nameof(GuildPruneGet)}.{nameof(GuildPruneGet.Days)} cannot be less than {minDays}");
            }
            
            if (days > maxDays)
            {
                throw new InvalidGuildPruneException($"{nameof(GuildPruneGet)}.{nameof(GuildPruneGet.Days)} cannot be more than {maxDays}");
            }
        }
    }
}