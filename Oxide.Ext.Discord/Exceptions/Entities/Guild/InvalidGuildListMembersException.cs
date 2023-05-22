using Oxide.Ext.Discord.Entities.Guilds;

namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    /// <summary>
    /// Represents an exception in guid list request
    /// </summary>
    public class InvalidGuildListMembersException : BaseDiscordException
    {
        private InvalidGuildListMembersException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidLimit(int? limit)
        {
            const int minLimit = 0;
            const int maxLimit = 1000;
            
            if (limit < minLimit)
            {
                throw new InvalidGuildListMembersException($"{nameof(GuildListMembers)}.{nameof(GuildListMembers.Limit)} cannot be less than {minLimit}");
            }
            
            if (limit > maxLimit)
            {
                throw new InvalidGuildListMembersException($"{nameof(GuildListMembers)}.{nameof(GuildListMembers.Limit)} cannot be more than {maxLimit}");
            }
        }
    }
}