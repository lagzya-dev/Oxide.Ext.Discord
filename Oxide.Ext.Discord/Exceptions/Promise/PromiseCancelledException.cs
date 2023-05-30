namespace Oxide.Ext.Discord.Exceptions.Promise
{
    public class PromiseCancelledException : BaseDiscordException
    {
        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        public PromiseCancelledException(string message) : base(message)
        {

        }
    }
}