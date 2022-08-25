using System.Text;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class PlayerPlaceholders
    {
        private static void Id(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Id);
        private static void Name(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Name);
        private static void Health(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Health);
        private static void Position(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Position());
        private static void Ping(StringBuilder builder, PlaceholderState state, IPlayer player) => PlaceholderFormatting.Replace(builder, state, player.Ping);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<IPlayer>("player.id", Id);
            placeholders.RegisterInternalPlaceholder<IPlayer>("player.name", Name);
            placeholders.RegisterInternalPlaceholder<IPlayer>("player.health", Health);
            placeholders.RegisterInternalPlaceholder<IPlayer>("player.position", Position);
            placeholders.RegisterInternalPlaceholder<IPlayer>("player.ping", Ping);
        }
    }
}