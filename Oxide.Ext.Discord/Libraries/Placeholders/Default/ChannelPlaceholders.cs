using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class ChannelPlaceholders
    {
        public static void Id(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Id);
        public static void Name(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Name);
        public static void Icon(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.IconUrl);
        public static void Topic(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Topic);
        public static void Mention(StringBuilder builder, PlaceholderState state, DiscordChannel channel) => PlaceholderFormatting.Replace(builder, state, channel.Mention);

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "channel", nameof(DiscordChannel));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.id", dataKey, Id);
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.name", dataKey, Name);
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.icon", dataKey, Icon);
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.topic", dataKey, Topic);
            placeholders.RegisterPlaceholder<DiscordChannel>(plugin, $"{placeholderPrefix}.mention", dataKey, Mention);
        }
    }
}