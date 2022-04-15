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
            if (limit < 0)
            {
                throw new InvalidGuildListMembersException("Limit cannot be less than 0");
            }
            
            if (limit > 1000)
            {
                throw new InvalidGuildListMembersException("Limit cannot be more than 1000");
            }
        }
    }
}