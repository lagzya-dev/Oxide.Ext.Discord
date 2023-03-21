using System;
using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="DateTime"/> placeholders
    /// </summary>
    public static class DateTimePlaceholders
    {
        /// <summary>
        /// <see cref="DateTime"/> placeholder
        /// </summary>
        public static void Date(StringBuilder builder, PlaceholderState state, DateTime date) => PlaceholderFormatting.Replace(builder, state, date);
        
        /// <summary>
        /// <see cref="DateTime.Year"/> placeholder
        /// </summary>
        public static void Year(StringBuilder builder, PlaceholderState state, DateTime date) => PlaceholderFormatting.Replace(builder, state, date.Year);
        
        /// <summary>
        /// <see cref="DateTime.Month"/> placeholder
        /// </summary>
        public static void Month(StringBuilder builder, PlaceholderState state, DateTime date) => PlaceholderFormatting.Replace(builder, state, date.Month);
        
        /// <summary>
        /// <see cref="DateTime.Day"/> placeholder
        /// </summary>
        public static void Day(StringBuilder builder, PlaceholderState state, DateTime date) => PlaceholderFormatting.Replace(builder, state, date.Day);
        
        /// <summary>
        /// <see cref="DateTime.Hour"/> placeholder
        /// </summary>
        public static void Hour(StringBuilder builder, PlaceholderState state, DateTime date) => PlaceholderFormatting.Replace(builder, state, date.Hour);
        
        /// <summary>
        /// <see cref="DateTime.Minute"/> placeholder
        /// </summary>
        public static void Minute(StringBuilder builder, PlaceholderState state, DateTime date) => PlaceholderFormatting.Replace(builder, state, date.Minute);
        
        /// <summary>
        /// <see cref="DateTime.Second"/> placeholder
        /// </summary>
        public static void Second(StringBuilder builder, PlaceholderState state, DateTime date) => PlaceholderFormatting.Replace(builder, state, date.Second);
        
        /// <summary>
        /// <see cref="DateTime.Millisecond"/> placeholder
        /// </summary>
        public static void Millisecond(StringBuilder builder, PlaceholderState state, DateTime date) => PlaceholderFormatting.Replace(builder, state, date.Millisecond);

        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<DateTime>(plugin, $"{placeholderPrefix}.date", dataKey, Date);
            placeholders.RegisterPlaceholder<DateTime>(plugin, $"{placeholderPrefix}.year", dataKey, Year);
            placeholders.RegisterPlaceholder<DateTime>(plugin, $"{placeholderPrefix}.month", dataKey, Month);
            placeholders.RegisterPlaceholder<DateTime>(plugin, $"{placeholderPrefix}.day", dataKey, Day);
            placeholders.RegisterPlaceholder<DateTime>(plugin, $"{placeholderPrefix}.hour", dataKey, Hour);
            placeholders.RegisterPlaceholder<DateTime>(plugin, $"{placeholderPrefix}.minute", dataKey, Minute);
            placeholders.RegisterPlaceholder<DateTime>(plugin, $"{placeholderPrefix}.second", dataKey, Second);
            placeholders.RegisterPlaceholder<DateTime>(plugin, $"{placeholderPrefix}.millisecond", dataKey, Millisecond);
        }
    }
}