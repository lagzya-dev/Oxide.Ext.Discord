using System.Text;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Types
{
    internal class TimestampPlaceholders : BasePlaceholders<ulong>
    {
        public TimestampPlaceholders()
        {
            RegisterPlaceholder("timestamp", Timestamp);
            RegisterPlaceholder("timestamp.shortime", ShorTime);
            RegisterPlaceholder("timestamp.longtime", Longtime);
            RegisterPlaceholder("timestamp.shortdate", ShortDate);
            RegisterPlaceholder("timestamp.longdate", LongDate);
            RegisterPlaceholder("timestamp.shortdatetime", ShortDateTime);
            RegisterPlaceholder("timestamp.longdatetime", LongDateTime);
            RegisterPlaceholder("timestamp.relativetime", RelativeTime);
        }
        
        private void Timestamp(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp));
        private void ShorTime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortTime));
        private void Longtime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongTime));
        private void ShortDate(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDate));
        private void LongDate(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDate));
        private void ShortDateTime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDateTime));
        private void LongDateTime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDateTime));
        private void RelativeTime(StringBuilder builder, ulong timestamp, PlaceholderMatch match) => Replace(builder, match, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.RelativeTime));

        protected override string GetDataKey()
        {
            return PlaceholderData.TimestampName;
        }
    }
}