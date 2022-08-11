using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Libraries.Placeholders.Types;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class ServerPlaceholders : PlaceholderCollection<IServer>
    {
        private void Name(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.Name);
        private void Players(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.Players);
        private void MaxPlayers(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.MaxPlayers);
        private void Version(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.Version);
        private void Protocol(StringBuilder builder, IServer server, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, server.Protocol);

        public override void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterPlaceholderInternal<IServer>("server.name", GetDataKey(), Name);
            placeholders.RegisterPlaceholderInternal<IServer>("server.players", GetDataKey(), Players);
            placeholders.RegisterPlaceholderInternal<IServer>("server.players.max", GetDataKey(), MaxPlayers);
            placeholders.RegisterPlaceholderInternal<IServer>("server.version", GetDataKey(), Version);
            placeholders.RegisterPlaceholderInternal<IServer>("server.protocol", GetDataKey(), Protocol);
        }
    }
}