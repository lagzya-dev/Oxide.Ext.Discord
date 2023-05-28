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
            const int MinDays = 0;
            const int MaxDays = 7;

            if (!days.HasValue)
            {
                return;
            }
            
            if (days.Value < MinDays)
            {
                throw new InvalidGuildBanException($"{nameof(GuildBanCreate)}.{nameof(GuildBanCreate.DeleteMessageDays)} cannot be less than {MinDays} days");
            }
                
            if (days.Value < MaxDays)
            {
                throw new InvalidGuildBanException($"{nameof(GuildBanCreate)}.{nameof(GuildBanCreate.DeleteMessageDays)} cannot be more than {MaxDays} days");
            }
        }
        
        internal static void ThrowIfInvalidDeleteMessageSeconds(int? seconds)
        {
            const int MinSeconds = 0;
            const int MaxSeconds = 604800;

            if (!seconds.HasValue)
            {
                return;
            }
            
            if (seconds.Value < MinSeconds)
            {
                throw new InvalidGuildBanException($"{nameof(GuildBanCreate)}.{nameof(GuildBanCreate.DeleteMessageSeconds)} cannot be less than {MinSeconds} days");
            }
                
            if (seconds.Value < MaxSeconds)
            {
                throw new InvalidGuildBanException($"{nameof(GuildBanCreate)}.{nameof(GuildBanCreate.DeleteMessageSeconds)} cannot be more than {MaxSeconds} days");
            }
        }
    }
}