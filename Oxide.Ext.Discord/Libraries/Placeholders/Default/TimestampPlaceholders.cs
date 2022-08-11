using System.Text;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Libraries.Placeholders.Types;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class TimestampPlaceholders : PlaceholderCollection<ulong>
    {
        private void Timestamp(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp));
        private void ShorTime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortTime));
        private void Longtime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongTime));
        private void ShortDate(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDate));
        private void LongDate(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDate));
        private void ShortDateTime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDateTime));
        private void LongDateTime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDateTime));
        private void RelativeTime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.RelativeTime));

        public override void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterPlaceholderInternal<ulong>("timestamp", GetDataKey(), Timestamp);
            placeholders.RegisterPlaceholderInternal<ulong>("timestamp.shortime", GetDataKey(), ShorTime);
            placeholders.RegisterPlaceholderInternal<ulong>("timestamp.longtime", GetDataKey(), Longtime);
            placeholders.RegisterPlaceholderInternal<ulong>("timestamp.shortdate", GetDataKey(), ShortDate);
            placeholders.RegisterPlaceholderInternal<ulong>("timestamp.longdate", GetDataKey(), LongDate);
            placeholders.RegisterPlaceholderInternal<ulong>("timestamp.shortdatetime", GetDataKey(), ShortDateTime);
            placeholders.RegisterPlaceholderInternal<ulong>("timestamp.longdatetime", GetDataKey(), LongDateTime);
            placeholders.RegisterPlaceholderInternal<ulong>("timestamp.relativetime", GetDataKey(), RelativeTime);
        }

        protected override string GetDataKey()
        {
            return PlaceholderData.TimestampName;
        }
    }
}