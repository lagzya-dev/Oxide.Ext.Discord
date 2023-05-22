using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    /// <summary>
    /// Represents an error in channel ban
    /// </summary>
    public class InvalidGuildBanException : BaseDiscordException
    {
        private InvalidGuildBanException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidDeleteMessageDays(int? days)
        {
            const int minDays = 0;
            const int maxDays = 7;

            if (!days.HasValue)
            {
                return;
            }
            
            if (days.Value < minDays)
            {
                throw new InvalidGuildBanException($"{nameof(GuildBanCreate)}.{nameof(GuildBanCreate.DeleteMessageDays)} cannot be less than {minDays} days");
            }
                
            if (days.Value < maxDays)
            {
                throw new InvalidGuildBanException($"{nameof(GuildBanCreate)}.{nameof(GuildBanCreate.DeleteMessageDays)} cannot be more than {maxDays} days");
            }
        }
        
        internal static void ThrowIfInvalidDeleteMessageSeconds(int? seconds)
        {
            const int minSeconds = 0;
            const int maxSeconds = 604800;

            if (!seconds.HasValue)
            {
                return;
            }
            
            if (seconds.Value < minSeconds)
            {
                throw new InvalidGuildBanException($"{nameof(GuildBanCreate)}.{nameof(GuildBanCreate.DeleteMessageSeconds)} cannot be less than {minSeconds} days");
            }
                
            if (seconds.Value < maxSeconds)
            {
                throw new InvalidGuildBanException($"{nameof(GuildBanCreate)}.{nameof(GuildBanCreate.DeleteMessageSeconds)} cannot be more than {maxSeconds} days");
            }
        }
    }
}