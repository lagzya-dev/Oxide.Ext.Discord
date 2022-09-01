namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    /// <summary>
    /// Represents an error in channel ban
    /// </summary>
    public class InvalidGuildBanException : BaseDiscordException
    {
        private InvalidGuildBanException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidDeleteMessageDays(int? deleteMessageDays)
        {
            if (deleteMessageDays.HasValue && (deleteMessageDays.Value < 0 || deleteMessageDays.Value > 7))
            {
                throw new InvalidGuildBanException("DeleteMessageDays must be between 0-7 days");
            }
        }
        
        internal static void ThrowIfInvalidDeleteMessageSeconds(int? deleteMessageDays)
        {
            if (deleteMessageDays.HasValue && (deleteMessageDays.Value < 0 || deleteMessageDays.Value > 604800))
            {
                throw new InvalidGuildBanException("DeleteMessageSeconds must be between 0-604800 seconds");
            }
        }
    }
}