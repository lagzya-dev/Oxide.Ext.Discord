using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    /// <summary>
    /// Formatting Helpers for Placeholders
    /// </summary>
    public static class PlaceholderFormatting
    {
        private static readonly Regex GenericPositionRegex = new Regex(@"([xyz])(?::?([\d\.]*))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        /// <summary>
        /// Replace the <see cref="PlaceholderState"/> with the the string value
        /// </summary>
        /// <param name="builder"><see cref="StringBuilder"/> for the placeholder</param>
        /// <param name="state"><see cref="PlaceholderState"/> for the placeholder</param>
        /// <param name="value">Placeholder value to replace</param>
        public static void Replace(StringBuilder builder, PlaceholderState state, string value)
        {
            builder.Remove(state.Index, state.Length);
            builder.Insert(state.Index, value);
        }

        /// <summary>
        /// Replace the <see cref="Match"/> with the the string value
        /// </summary>
        /// <param name="builder"><see cref="StringBuilder"/> for the placeholder</param>
        /// <param name="match"><see cref="Match"/> for the placeholder</param>
        /// <param name="value">Placeholder value to replace</param>
        public static void Replace(StringBuilder builder, Match match, string value)
        {
            builder.Remove(match.Index, match.Length);
            builder.Insert(match.Index, value);
        }

        /// <summary>
        /// Replace the <see cref="Match"/> with the the string value
        /// </summary>
        /// <param name="builder"><see cref="StringBuilder"/> for the placeholder</param>
        /// <param name="state"><see cref="Match"/> for the placeholder</param>
        /// <param name="value">Snowflake value to replace</param>
        public static void Replace(StringBuilder builder, PlaceholderState state, Snowflake value)
        {
            Replace(builder, state, value.ToString());
        }

        /// <summary>
        /// Replace the <see cref="Match"/> with the the string value
        /// </summary>
        /// <param name="builder"><see cref="StringBuilder"/> for the placeholder</param>
        /// <param name="state"><see cref="Match"/> for the placeholder</param>
        /// <param name="value"><see cref="IFormattable"/> value to use with formatting</param>
        public static void Replace(StringBuilder builder, PlaceholderState state, IFormattable value)
        {
            if (string.IsNullOrEmpty(state.Format))
            {
                Replace(builder, state, value.ToString(null, CultureInfo.CurrentCulture));
            }
            
            Replace(builder, state, value.ToString(state.Format, CultureInfo.CurrentCulture));
        }
        
        /// <summary>
        /// Replace the <see cref="PlaceholderState"/> with the formatted position
        /// </summary>
        /// <param name="builder"><see cref="StringBuilder"/> for the placeholder</param>
        /// <param name="placeholderState"><see cref="PlaceholderState"/> for the placeholder</param>
        /// <param name="position"><see cref="GenericPosition"/> position to format and replace</param>
        public static void Replace(StringBuilder builder, PlaceholderState placeholderState, GenericPosition position)
        {
            if (string.IsNullOrEmpty(placeholderState.Format))
            {
                Replace(builder, placeholderState, position.ToString());
                return;
            }

            StringBuilder sb = DiscordPool.GetStringBuilder();
            sb.Append(placeholderState.Format);
            MatchCollection matches = GenericPositionRegex.Matches(placeholderState.Format);
            for (int index = matches.Count - 1; index >= 0; index--)
            {
                Match match = matches[index];
                switch (match.Groups[1].Value)
                {
                    case "x":
                        Replace(sb, match, position.X.ToString(match.Groups[2].Value));
                        break;
                    case "y":
                        Replace(sb, match, position.Y.ToString(match.Groups[2].Value));
                        break;
                    case "z":
                        Replace(sb, match, position.Z.ToString(match.Groups[2].Value));
                        break;
                }
            }
            
            Replace(builder, placeholderState, sb.ToString());
            DiscordPool.FreeStringBuilder(ref sb);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ApplyPlaceholder(string text, PlaceholderData value)
        {
            return value == null ? text : DiscordExtension.DiscordPlaceholders.ProcessPlaceholders(text, value);
        }
    }
}