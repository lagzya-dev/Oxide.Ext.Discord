namespace Oxide.Ext.Discord.Exceptions.Entities.Channels
{
    /// <summary>
    /// Represents an error in channel invite
    /// </summary>
    public class InvalidChannelInviteException : BaseDiscordException
    {
        private InvalidChannelInviteException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidMaxAge(int? maxAage)
        {
            if (maxAage.HasValue && (maxAage.Value < 0 || maxAage.Value > 604800))
            {
                throw new InvalidChannelInviteException("MaxAge cannot be less than 0 or more than 604800");
            }
        }
        
        internal static void ThrowIfInvalidMaxUses(int? maxUses)
        {
            if (maxUses.HasValue && (maxUses.Value < 0 || maxUses.Value > 100))
            {
                throw new InvalidChannelInviteException("MaxUses cannot be less than 0 or more than 100");
            }
        }
        
        internal static void ThrowIfInvalidTargetUser(Discord.Entities.Snowflake? targetUser)
        {
            if (targetUser.HasValue && !targetUser.Value.IsValid())
            {
                throw new InvalidChannelInviteException("TargetUser is not a valid snowflake ID");
            }
        }
    }
}