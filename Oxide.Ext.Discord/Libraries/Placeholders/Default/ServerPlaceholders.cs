using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class ServerPlaceholders
    {
        public static void Name(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.Name);
        public static void Players(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.Players);
        public static void MaxPlayers(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.MaxPlayers);
        public static void Version(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.Version);
        public static void Protocol(StringBuilder builder, PlaceholderState state, IServer server) => PlaceholderFormatting.Replace(builder, state, server.Protocol);
        
        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "server", nameof(IServer));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.players", dataKey, Players);
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.players.max", dataKey, MaxPlayers);
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.version", dataKey, Version);
            placeholders.RegisterPlaceholder<IServer>(plugin, $"{placeholderPrefix}.protocol", dataKey, Protocol);
        }
    }
}