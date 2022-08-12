using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Oxide.Core.Libraries.Covalence;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    public static class PlaceholderFormatting
    {
        private static readonly Regex GenericPositionRegex = new Regex(@"([xyz])(?::?([\d\.]*))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        public static void Replace(StringBuilder builder, PlaceholderMatch match, string value)
        {
            builder.Remove(match.Index, match.Length);
            builder.Insert(match.Index, value);
        }

        public static void Replace(StringBuilder builder, Match match, string value)
        {
            builder.Remove(match.Index, match.Length);
            builder.Insert(match.Index, value);
        }

        public static void Replace(StringBuilder builder, PlaceholderMatch match, Snowflake value)
        {
            Replace(builder, match, value.ToString());
        }

        public static void Replace(StringBuilder builder, PlaceholderMatch match, IFormattable value)
        {
            if (string.IsNullOrEmpty(match.Format))
            {
                Replace(builder, match, value.ToString(null, CultureInfo.CurrentCulture));
            }
            
            Replace(builder, match, value.ToString(match.Format, CultureInfo.CurrentCulture));
        }
        
        public static void Replace(StringBuilder builder, PlaceholderMatch placeholderMatch, GenericPosition position)
        {
            if (string.IsNullOrEmpty(placeholderMatch.Format))
            {
                Replace(builder, placeholderMatch, position.ToString());
                return;
            }

            StringBuilder sb = DiscordPool.GetStringBuilder();
            sb.Append(placeholderMatch.Format);
            MatchCollection matches = GenericPositionRegex.Matches(placeholderMatch.Format);
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
            
            Replace(builder, placeholderMatch, sb.ToString());
            DiscordPool.FreeStringBuilder(ref sb);
        }
    }
}