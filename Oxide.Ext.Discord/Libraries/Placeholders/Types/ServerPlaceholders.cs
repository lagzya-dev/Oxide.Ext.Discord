using System.Text;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    internal class ServerPlaceholders : BasePlaceholders<IServer>
    {
        public ServerPlaceholders()
        {
            RegisterPlaceholder("server.name", Name);
            RegisterPlaceholder("server.players", Players);
            RegisterPlaceholder("server.players.max", MaxPlayers);
            RegisterPlaceholder("server.version", Version);
            RegisterPlaceholder("server.protocol", Protocol);
        }
        
        private void Name(StringBuilder builder, IServer server, PlaceholderMatch match) => Replace(builder, match, server.Name);
        private void Players(StringBuilder builder, IServer server, PlaceholderMatch match) => Replace(builder, match, server.Players.ToString(match.Format));
        private void MaxPlayers(StringBuilder builder, IServer server, PlaceholderMatch match) => Replace(builder, match, server.MaxPlayers.ToString(match.Format));
        private void Version(StringBuilder builder, IServer server, PlaceholderMatch match) => Replace(builder, match, server.Version);
        private void Protocol(StringBuilder builder, IServer server, PlaceholderMatch match) => Replace(builder, match, server.Protocol);

        protected override string GetDataKey()
        {
            return nameof(IServer);
        }
    }
}