namespace Oxide.Ext.Discord.Exceptions
{
    public class DiscordClientException : BaseDiscordException
    {
        public DiscordClientException(string message) : base(message) {}

        internal static DiscordClientException NotConnected() => new DiscordClientException("DiscordClient is not connected.");
    }
}