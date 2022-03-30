namespace Oxide.Ext.Discord.Exceptions
{
    /// <summary>
    /// Represents an invalid discord nickname exception
    /// </summary>
    public class InvalidNicknameException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception Message</param>
        public InvalidNicknameException(string message) : base(message) { }
    }
}