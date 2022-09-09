using System.Text;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class PlayerPlaceholders
    {
        public static void Id(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Id);
        public static void Name(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Name);
        public static void Health(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Health);
        public static void Position(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Position());
        public static void Ping(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Ping);
        public static void IsLinked(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.IsLinked());

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "player", nameof(IPlayer));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.health", dataKey, Health);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.position", dataKey, Position);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.ping", dataKey, Ping);
            placeholders.RegisterPlaceholder<IPlayer>(plugin, $"{placeholderPrefix}.islinked", dataKey, IsLinked);
        }
    }
}