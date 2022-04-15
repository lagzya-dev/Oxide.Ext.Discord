namespace Oxide.Ext.Discord.Exceptions.Entities.Guild
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
        private InvalidGuildMemberSearch(string message): base(message)
        {
            
        }
    }
}