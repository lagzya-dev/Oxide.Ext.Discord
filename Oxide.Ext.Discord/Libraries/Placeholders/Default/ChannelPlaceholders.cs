using System.Text;
using Oxide.Ext.Discord.Entities.Channels;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class ChannelPlaceholders
    {
        private static void Id(StringBuilder builder, PlaceholderMatch match, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, match, channel.Id);
        private static void Name(StringBuilder builder, PlaceholderMatch match, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, match, channel.Name);
        private static void Icon(StringBuilder builder, PlaceholderMatch match, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, match, channel.IconUrl);
        private static void Topic(StringBuilder builder, PlaceholderMatch match, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, match, channel.Topic);
        private static void Mention(StringBuilder builder, PlaceholderMatch match, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, match, channel.Mention);

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