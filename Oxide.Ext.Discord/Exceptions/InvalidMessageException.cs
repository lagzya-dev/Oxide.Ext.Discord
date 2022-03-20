using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages;
namespace Oxide.Ext.Discord.Exceptions
{
    /// <summary>
    /// Represents an invalid message
    /// </summary>
    public class InvalidMessageException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public InvalidMessageException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="create">Message Create that caused the error</param>
        public InvalidMessageException(string message, MessageCreate create) : base($"{message}\nMessage:{JsonConvert.SerializeObject(create, Formatting.Indented)}")
        {
            
        }
    }
}