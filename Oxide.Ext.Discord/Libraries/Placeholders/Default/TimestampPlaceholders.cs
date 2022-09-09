using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class TimestampPlaceholders
    {
        public static void Timestamp(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp));
        public static void ShorTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortTime));
        public static void Longtime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongTime));
        public static void ShortDate(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDate));
        public static void LongDate(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDate));
        public static void ShortDateTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDateTime));
        public static void LongDateTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDateTime));
        public static void RelativeTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.RelativeTime));

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "timestamp", GetDataKey());
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}", dataKey, Timestamp);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.shortime", dataKey, ShorTime);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.longtime", dataKey, Longtime);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.shortdate", dataKey, ShortDate);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.longdate", dataKey, LongDate);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.shortdatetime", dataKey, ShortDateTime);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.longdatetime", dataKey, LongDateTime);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.relativetime", dataKey, RelativeTime);
        }

        private static string GetDataKey() => PlaceholderData.TimestampName;
    }
}