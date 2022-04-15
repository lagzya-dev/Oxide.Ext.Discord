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
            if (days < 1)
            {
                throw new InvalidGuildPruneException("Days cannot be less than 1");
            }
            
            if (days > 30)
            {
                throw new InvalidGuildPruneException("Days cannot be more than 30");
            }
        }
    }
}