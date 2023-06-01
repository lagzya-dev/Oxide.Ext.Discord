using System;
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
        public static void Timestamp(StringBuilder builder, PlaceholderState state, long timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void ShortTime(StringBuilder builder, PlaceholderState state, long timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortTime));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void Longtime(StringBuilder builder, PlaceholderState state, long timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongTime));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void ShortDate(StringBuilder builder, PlaceholderState state, long timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDate));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void LongDate(StringBuilder builder, PlaceholderState state, long timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDate));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void ShortDateTime(StringBuilder builder, PlaceholderState state, long timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void LongDateTime(StringBuilder builder, PlaceholderState state, long timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDateTime));
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(ulong,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static void RelativeTime(StringBuilder builder, PlaceholderState state, long timestamp) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.RelativeTime));

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "timestamp");
            RegisterPlaceholders(DiscordExtensionCore.Instance);
        }

        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = TimestampName)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<long>(plugin, $"{placeholderPrefix}", dataKey, Timestamp);
            placeholders.RegisterPlaceholder<long>(plugin, $"{placeholderPrefix}.shortime", dataKey, ShortTime);
            placeholders.RegisterPlaceholder<long>(plugin, $"{placeholderPrefix}.longtime", dataKey, Longtime);
            placeholders.RegisterPlaceholder<long>(plugin, $"{placeholderPrefix}.shortdate", dataKey, ShortDate);
            placeholders.RegisterPlaceholder<long>(plugin, $"{placeholderPrefix}.longdate", dataKey, LongDate);
            placeholders.RegisterPlaceholder<long>(plugin, $"{placeholderPrefix}.shortdatetime", dataKey, ShortDateTime);
            placeholders.RegisterPlaceholder<long>(plugin, $"{placeholderPrefix}.longdatetime", dataKey, LongDateTime);
            placeholders.RegisterPlaceholder<long>(plugin, $"{placeholderPrefix}.relativetime", dataKey, RelativeTime);
        }

        private static void RegisterPlaceholders(Plugin plugin)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder(plugin, "timestamp.now", (builder, state) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow)));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.shortime", (builder, state) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.ShortTime)));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.longtime", (builder, state) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.LongTime)));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.shortdate", (builder, state) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.ShortDate)));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.longdate", (builder, state) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.LongDate)));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.shortdatetime", (builder, state) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.ShortDateTime)));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.longdatetime", (builder, state) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.LongDateTime)));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.relativetime", (builder, state) => PlaceholderFormatting.Replace(builder, state, DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.RelativeTime)));
        }
    }
}