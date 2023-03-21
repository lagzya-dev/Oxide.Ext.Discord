using System;
using System.Text;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="TimeSpan"/> placeholders
    /// </summary>
    public static class TimeSpanPlaceholders
    {
        /// <summary>
        /// <see cref="TimeSpan"/> placeholder
        /// </summary>
        public static void Time(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time);
        
        /// <summary>
        /// <see cref="TimeSpan.Days"/> placeholder
        /// </summary>
        public static void Days(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.Days);
        
        /// <summary>
        /// <see cref="TimeSpan.Hours"/> placeholder
        /// </summary>
        public static void Hours(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.Hours);
        
        /// <summary>
        /// <see cref="TimeSpan.Minutes"/> placeholder
        /// </summary>
        public static void Minutes(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.Minutes);
        
        /// <summary>
        /// <see cref="TimeSpan.Seconds"/> placeholder
        /// </summary>
        public static void Seconds(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.Seconds);
        
        /// <summary>
        /// <see cref="TimeSpan.Milliseconds"/> placeholder
        /// </summary>
        public static void Milliseconds(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.Milliseconds);
        
        /// <summary>
        /// <see cref="TimeSpan.TotalDays"/> placeholder
        /// </summary>
        public static void TotalDays(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.TotalDays);
        
        /// <summary>
        /// <see cref="TimeSpan.TotalHours"/> placeholder
        /// </summary>
        public static void TotalHours(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.TotalHours);
        
        /// <summary>
        /// <see cref="TimeSpan.TotalMinutes"/> placeholder
        /// </summary>
        public static void TotalMinutes(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.TotalMinutes);
        
        /// <summary>
        /// <see cref="TimeSpan.TotalSeconds"/> placeholder
        /// </summary>
        public static void TotalSeconds(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.TotalSeconds);
        
        /// <summary>
        /// <see cref="TimeSpan.TotalMilliseconds"/> placeholder
        /// </summary>
        public static void TotalMilliseconds(StringBuilder builder, PlaceholderState state, TimeSpan time) => PlaceholderFormatting.Replace(builder, state, time.TotalMilliseconds);

        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.time", dataKey, Time);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.days", dataKey, Days);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.hours", dataKey, Hours);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.minutes", dataKey, Minutes);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.seconds", dataKey, Seconds);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.milliseconds", dataKey, Milliseconds);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.total.days", dataKey, TotalDays);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.total.hours", dataKey, TotalHours);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.total.minutes", dataKey, TotalMinutes);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.total.seconds", dataKey, TotalSeconds);
            placeholders.RegisterPlaceholder<TimeSpan>(plugin, $"{placeholderPrefix}.total.milliseconds", dataKey, TotalMilliseconds);
        }
    }
}