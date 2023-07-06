using System;
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
        /// <see cref="DiscordFormatting.UnixTimestamp(long,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static string Timestamp(long timestamp) => DiscordFormatting.UnixTimestamp(timestamp);
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(long,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static string ShortTime(long timestamp) => DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortTime);
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(long,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static string Longtime(long timestamp) => DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongTime);
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(long,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static string ShortDate(long timestamp) => DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.ShortDate);
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(long,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static string LongDate(long timestamp) => DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDate);
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(long,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static string ShortDateTime(long timestamp) => DiscordFormatting.UnixTimestamp(timestamp);
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(long,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static string LongDateTime(long timestamp) => DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.LongDateTime);
        
        /// <summary>
        /// <see cref="DiscordFormatting.UnixTimestamp(long,Oxide.Ext.Discord.Helpers.TimestampStyles)"/> placeholder
        /// </summary>
        public static string RelativeTime(long timestamp) => DiscordFormatting.UnixTimestamp(timestamp, TimestampStyles.RelativeTime);

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
            placeholders.RegisterPlaceholder<long, string>(plugin, $"{placeholderPrefix}", dataKey, Timestamp);
            placeholders.RegisterPlaceholder<long, string>(plugin, $"{placeholderPrefix}.shorttime", dataKey, ShortTime);
            placeholders.RegisterPlaceholder<long, string>(plugin, $"{placeholderPrefix}.longtime", dataKey, Longtime);
            placeholders.RegisterPlaceholder<long, string>(plugin, $"{placeholderPrefix}.shortdate", dataKey, ShortDate);
            placeholders.RegisterPlaceholder<long, string>(plugin, $"{placeholderPrefix}.longdate", dataKey, LongDate);
            placeholders.RegisterPlaceholder<long, string>(plugin, $"{placeholderPrefix}.shortdatetime", dataKey, ShortDateTime);
            placeholders.RegisterPlaceholder<long, string>(plugin, $"{placeholderPrefix}.longdatetime", dataKey, LongDateTime);
            placeholders.RegisterPlaceholder<long, string>(plugin, $"{placeholderPrefix}.relativetime", dataKey, RelativeTime);
        }

        private static void RegisterPlaceholders(Plugin plugin)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder(plugin, "timestamp.now", () => DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.shorttime", () => DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.ShortTime));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.longtime", () => DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.LongTime));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.shortdate", () => DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.ShortDate));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.longdate", () => DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.LongDate));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.shortdatetime", () => DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.ShortDateTime));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.longdatetime", () => DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.LongDateTime));
            placeholders.RegisterPlaceholder(plugin, "timestamp.now.relativetime", () => DiscordFormatting.UnixTimestamp(DateTimeOffset.UtcNow, TimestampStyles.RelativeTime));
        }
    }
}