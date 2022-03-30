namespace Oxide.Ext.Discord.Exceptions
{
    /// <summary>
    /// Represents an exception when modifying a user with invalid data
    /// </summary>
    public class ModifyUserException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Error Message</param>
        public ModifyUserException(string message) : base(message) { }
    }
}