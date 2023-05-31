using Oxide.Ext.Discord.Connections;

namespace Oxide.Ext.Discord.Exceptions
{
    /// <summary>
    /// Represents a bot token mismatch
    /// </summary>
    public class TokenMismatchException : BaseDiscordException
    {
        private TokenMismatchException(string message) : base(message) { }

        internal static void ThrowIfMismatchedToken(DiscordClient client, BotSettings expected)
        {
            if (client.Settings.ApiToken != expected.ApiToken) 
                throw new TokenMismatchException($"Failed to add client for plugin {client.PluginName}. Token {client.Settings.HiddenToken} does not match BotClient {expected.HiddenToken}");
        }
    }
}