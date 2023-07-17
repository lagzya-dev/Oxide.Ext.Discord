using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Libraries.Placeholders.Keys;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="DateTime"/> placeholders
    /// </summary>
    public static class DateTimePlaceholders
    {
        /// <summary>
        /// <see cref="DateTime.Year"/> placeholder
        /// </summary>
        public static int Year(DateTime date) => date.Year;
        
        /// <summary>
        /// <see cref="DateTime.Month"/> placeholder
        /// </summary>
        public static int Month(DateTime date) => date.Month;
        
        /// <summary>
        /// <see cref="DateTime.Day"/> placeholder
        /// </summary>
        public static int Day(DateTime date) => date.Day;
        
        /// <summary>
        /// <see cref="DateTime.Hour"/> placeholder
        /// </summary>
        public static int Hour(DateTime date) => date.Hour;
        
        /// <summary>
        /// <see cref="DateTime.Minute"/> placeholder
        /// </summary>
        public static int Minute(DateTime date) => date.Minute;
        
        /// <summary>
        /// <see cref="DateTime.Second"/> placeholder
        /// </summary>
        public static int Second(DateTime date) => date.Second;
        
        /// <summary>
        /// <see cref="DateTime.Millisecond"/> placeholder
        /// </summary>
        public static int Millisecond(DateTime date) => date.Millisecond;

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, DefaultKeys.DateTime, new PlaceholderDataKey(nameof(DateTime)));
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="keys">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, DateTimeKeys keys, PlaceholderDataKey dataKey)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<DateTime>(plugin, keys.Date, dataKey);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, keys.Year, dataKey, Year);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, keys.Month, dataKey, Month);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, keys.Day, dataKey, Day);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, keys.Hour, dataKey, Hour);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, keys.Minute, dataKey, Minute);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, keys.Second, dataKey, Second);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, keys.Millisecond, dataKey, Millisecond);
        }
    }
}