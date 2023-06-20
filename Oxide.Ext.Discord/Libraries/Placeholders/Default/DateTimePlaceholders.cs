using System;
using Oxide.Core.Plugins;

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

        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<DateTime>(plugin, $"{placeholderPrefix}.date", dataKey);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, $"{placeholderPrefix}.year", dataKey, Year);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, $"{placeholderPrefix}.month", dataKey, Month);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, $"{placeholderPrefix}.day", dataKey, Day);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, $"{placeholderPrefix}.hour", dataKey, Hour);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, $"{placeholderPrefix}.minute", dataKey, Minute);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, $"{placeholderPrefix}.second", dataKey, Second);
            placeholders.RegisterPlaceholder<DateTime, int>(plugin, $"{placeholderPrefix}.millisecond", dataKey, Millisecond);
        }
    }
}