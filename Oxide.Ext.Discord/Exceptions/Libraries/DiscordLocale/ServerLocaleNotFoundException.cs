using Oxide.Ext.Discord.Libraries.Locale;

namespace Oxide.Ext.Discord.Exceptions.Libraries.DiscordLocale
{
    /// <summary>
    /// Exception thrown when Server Locale is not found
    /// </summary>
    public class ServerLocaleNotFoundException : BaseDiscordException
    {
        private ServerLocaleNotFoundException(string message) : base(message) { }

        internal static ServerLocaleNotFoundException NotFound(Discord.Libraries.Locale.DiscordLocale id) => throw new ServerLocaleNotFoundException($"Failed to find discord locale for server locale '{id.Id}'");
        internal static ServerLocaleNotFoundException NotFound(ServerLocale id) => throw new ServerLocaleNotFoundException($"Failed to find server local '{id.Id}'");

        internal static void ThrowNotFound(Discord.Libraries.Locale.DiscordLocale id) => throw NotFound(id);
        internal static void ThrowNotFound(ServerLocale id) => throw NotFound(id);
    }
}