using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;

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
            const int minAge = 0;
            const int maxAge = 604800;

            if (!maxAage.HasValue)
            {
                return;
            }
            
            if (maxAage.Value < minAge)
            {
                throw new InvalidChannelInviteException($"{nameof(ChannelInvite)}.{nameof(ChannelInvite.MaxAge)} cannot be less than {minAge}");
            }
                
            if (maxAage.Value > maxAge)
            {
                throw new InvalidChannelInviteException($"{nameof(ChannelInvite)}.{nameof(ChannelInvite.MaxAge)}  cannot be more than {maxAge}");
            }
        }
        
        internal static void ThrowIfInvalidMaxUses(int? maxUses)
        {
            const int minUse = 0;
            const int maxUse = 100;

            if (!maxUses.HasValue)
            {
                return;
            }
            
            if (maxUses.Value < minUse)
            {
                throw new InvalidChannelInviteException($"{nameof(ChannelInvite)}.{nameof(ChannelInvite.MaxUses)} cannot be less than {minUse}");
            }
                
            if (maxUses.Value > maxUse)
            {
                throw new InvalidChannelInviteException($"{nameof(ChannelInvite)}.{nameof(ChannelInvite.MaxUses)} cannot be more than {maxUse}");
            }
        }
        
        internal static void ThrowIfInvalidTargetUser(Snowflake? targetUser)
        {
            if (targetUser.HasValue && !targetUser.Value.IsValid())
            {
                throw new InvalidChannelInviteException($"{nameof(ChannelInvite)}.{nameof(ChannelInvite.TargetUser)} is not a valid snowflake ID");
            }
        }
    }
}