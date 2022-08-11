using System.Text;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class ServerPlaceholders
    {
        private static void Name(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.Name);
        private static void Players(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.Players);
        private static void MaxPlayers(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.MaxPlayers);
        private static void Version(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.Version);
        private static void Protocol(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.Protocol);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<IServer>("server.name", GetDataKey(), Name);
            placeholders.RegisterInternalPlaceholder<IServer>("server.players", GetDataKey(), Players);
            placeholders.RegisterInternalPlaceholder<IServer>("server.players.max", GetDataKey(), MaxPlayers);
            placeholders.RegisterInternalPlaceholder<IServer>("server.version", GetDataKey(), Version);
            placeholders.RegisterInternalPlaceholder<IServer>("server.protocol", GetDataKey(), Protocol);
        }
        
        private static string GetDataKey() => nameof(IServer);
    }
}