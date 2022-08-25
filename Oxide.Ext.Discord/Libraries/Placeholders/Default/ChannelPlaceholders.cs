using System.Text;
using Oxide.Ext.Discord.Entities.Channels;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class ChannelPlaceholders
    {
        private static void Id(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Id);
        private static void Name(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Name);
        private static void Icon(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.IconUrl);
        private static void Topic(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Topic);
        private static void Mention(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Mention);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.id", Id);
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.name", Name);
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.icon", Icon);
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.topic", Topic);
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.mention", Mention);
        }
    }
}