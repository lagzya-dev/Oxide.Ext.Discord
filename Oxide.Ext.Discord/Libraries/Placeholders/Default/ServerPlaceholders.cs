using System.Text;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class ServerPlaceholders
    {
        private static void Name(StringBuilder builder, PlaceholderMatch match, IServer server) => PlaceholderFormatting.Replace(builder, match, server.Name);
        private static void Players(StringBuilder builder, PlaceholderMatch match, IServer server) => PlaceholderFormatting.Replace(builder, match, server.Players);
        private static void MaxPlayers(StringBuilder builder, PlaceholderMatch match, IServer server) => PlaceholderFormatting.Replace(builder, match, server.MaxPlayers);
        private static void Version(StringBuilder builder, PlaceholderMatch match, IServer server) => PlaceholderFormatting.Replace(builder, match, server.Version);
        private static void Protocol(StringBuilder builder, PlaceholderMatch match, IServer server) => PlaceholderFormatting.Replace(builder, match, server.Protocol);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<IServer>("server.name", Name);
            placeholders.RegisterInternalPlaceholder<IServer>("server.players", Players);
            placeholders.RegisterInternalPlaceholder<IServer>("server.players.max", MaxPlayers);
            placeholders.RegisterInternalPlaceholder<IServer>("server.version", Version);
            placeholders.RegisterInternalPlaceholder<IServer>("server.protocol", Protocol);
        }
    }
}