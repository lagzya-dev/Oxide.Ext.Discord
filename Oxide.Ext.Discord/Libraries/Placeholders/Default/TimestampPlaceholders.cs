using System.Text;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal static class TimestampPlaceholders
    {
        private static void Timestamp(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp));
        private static void ShorTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortTime));
        private static void Longtime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongTime));
        private static void ShortDate(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDate));
        private static void LongDate(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDate));
        private static void ShortDateTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDateTime));
        private static void LongDateTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDateTime));
        private static void RelativeTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.RelativeTime));

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<ulong>("timestamp", GetDataKey(), Timestamp);
            placeholders.RegisterInternalPlaceholder<ulong>("timestamp.shortime", GetDataKey(), ShorTime);
            placeholders.RegisterInternalPlaceholder<ulong>("timestamp.longtime", GetDataKey(), Longtime);
            placeholders.RegisterInternalPlaceholder<ulong>("timestamp.shortdate", GetDataKey(), ShortDate);
            placeholders.RegisterInternalPlaceholder<ulong>("timestamp.longdate", GetDataKey(), LongDate);
            placeholders.RegisterInternalPlaceholder<ulong>("timestamp.shortdatetime", GetDataKey(), ShortDateTime);
            placeholders.RegisterInternalPlaceholder<ulong>("timestamp.longdatetime", GetDataKey(), LongDateTime);
            placeholders.RegisterInternalPlaceholder<ulong>("timestamp.relativetime", GetDataKey(), RelativeTime);
        }

        private static string GetDataKey() => PlaceholderData.TimestampName;
    }
}