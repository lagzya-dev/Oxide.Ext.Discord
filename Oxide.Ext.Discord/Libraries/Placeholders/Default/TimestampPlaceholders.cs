using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// Timestamp placeholders
    /// </summary>
    public static class TimestampPlaceholders
    {
        internal const string TimestampName = "Timestamp";
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void Timestamp(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void ShortTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortTime));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void Longtime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongTime));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void ShortDate(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDate));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void LongDate(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDate));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void ShortDateTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void LongDateTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDateTime));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void RelativeTime(StringBuilder builder, PlaceholderState state, ulong timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.RelativeTime));

        internal static void RegisterPlaceholders() => RegisterPlaceholders(DiscordExtensionCore.Instance, "timestamp");

        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = TimestampName)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}", dataKey, Timestamp);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.shortime", dataKey, ShortTime);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.longtime", dataKey, Longtime);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.shortdate", dataKey, ShortDate);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.longdate", dataKey, LongDate);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.shortdatetime", dataKey, ShortDateTime);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.longdatetime", dataKey, LongDateTime);
            placeholders.RegisterPlaceholder<ulong>(plugin, $"{placeholderPrefix}.relativetime", dataKey, RelativeTime);
        }
    }
}