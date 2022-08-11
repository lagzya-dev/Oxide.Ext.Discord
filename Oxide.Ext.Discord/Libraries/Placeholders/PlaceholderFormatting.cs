using System;
using System.Globalization;
using System.Text;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    public static class PlaceholderFormatting
    {
        public static void Replace(StringBuilder builder, PlaceholderMatch match, string value)
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
    }
}