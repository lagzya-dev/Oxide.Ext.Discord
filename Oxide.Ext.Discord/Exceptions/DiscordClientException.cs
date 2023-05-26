namespace Oxide.Ext.Discord.Exceptions
{
    /// <summary>
    /// Exceptions for the <see cref="DiscordClient"/>
    /// </summary>
    public class DiscordClientException : BaseDiscordException
    {
        private DiscordClientException(string message) : base(message) {}

        internal static DiscordClientException NotConnected() => new DiscordClientException("DiscordClient is not connected.");
    }
}