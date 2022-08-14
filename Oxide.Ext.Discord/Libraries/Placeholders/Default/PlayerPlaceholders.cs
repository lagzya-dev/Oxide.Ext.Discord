using System.Text;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class PlayerPlaceholders
    {
        private static void Id(StringBuilder builder, IPlayer player, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, player.Id);
        private static void Name(StringBuilder builder, IPlayer player, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, player.Name);
        private static void Health(StringBuilder builder, IPlayer player, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, player.Health);
        private static void Position(StringBuilder builder, IPlayer player, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, player.Position());
        private static void Ping(StringBuilder builder, IPlayer player, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, player.Ping);

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