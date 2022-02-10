namespace Oxide.Ext.Discord.Exceptions
{
    /// <summary>
    /// Represents using an invalid guild member search
    /// </summary>
    public class InvalidGuildMemberSearch : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public InvalidGuildMemberSearch(string message): base(message)
        {
            
        }
    }
}