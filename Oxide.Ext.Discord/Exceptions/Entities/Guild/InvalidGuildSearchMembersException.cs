namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
{
    /// <summary>
    /// Represents an exception in guild member search requests
    /// </summary>
    public class InvalidGuildSearchMembersException : BaseDiscordException
    {
        private InvalidGuildSearchMembersException(string message) : base(message) { }

        internal static void ThrowIfInvalidQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new InvalidGuildSearchMembersException("Query cannot be less than 1 character");
            }
        }
        
        internal static void ThrowIfInvalidLimit(int? limit)
        {
            if (limit < 0)
            {
                throw new InvalidGuildSearchMembersException("Limit cannot be less than 0");
            }
            
            if (limit > 1000)
            {
                throw new InvalidGuildSearchMembersException("Limit cannot be more than 1000");
            }
        }
    }
}