namespace Oxide.Ext.Discord.Exceptions.Libraries.Promise
{
    /// <summary>
    /// Exception when a promised is cancelled
    /// </summary>
    public class PromiseCancelledException : BaseDiscordException
    {
        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        internal PromiseCancelledException(string message) : base(message) { }
    }
}