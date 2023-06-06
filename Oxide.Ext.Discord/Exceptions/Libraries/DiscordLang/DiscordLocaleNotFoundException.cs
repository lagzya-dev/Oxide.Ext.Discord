using Oxide.Ext.Discord.Libraries.Locale;

namespace Oxide.Ext.Discord.Exceptions.Libraries.DiscordLang
{
    public class DiscordLocaleNotFoundException : BaseDiscordException
    {
        private DiscordLocaleNotFoundException(string message) : base(message) { }
        
        internal static DiscordLocaleNotFoundException NotFound(ServerLocale id) => new DiscordLocaleNotFoundException($"Failed to find discord locale for server locale '{id.Id}'");
        internal static void ThrowNotFound(ServerLocale id) => throw NotFound(id);
    }
}