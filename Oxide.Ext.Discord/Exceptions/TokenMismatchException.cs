using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Factory;

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
            {
                BotToken token = BotTokenFactory.Instance.CreateFromToken(client.Settings.ApiToken, client.PluginName);
                throw new TokenMismatchException($"Failed to add client for plugin {client.PluginName}. Token {token.HiddenToken} does not match BotClient {expected.HiddenToken}");
            }
        }
    }
}