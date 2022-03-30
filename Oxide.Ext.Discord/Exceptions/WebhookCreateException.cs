namespace Oxide.Ext.Discord.Exceptions
{
    /// <summary>
    /// Represents a Webhook Create Exception
    /// </summary>
    public class WebhookCreateException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Error Message</param>
        public WebhookCreateException(string message) : base(message) { }
    }
}