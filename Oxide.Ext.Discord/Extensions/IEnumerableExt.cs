using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Extensions
{
    public static class IEnumerableExt
    {
        public static List<TSource> ToPooledList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            List<TSource> list = DiscordPool.GetList<TSource>();
            list.AddRange(source);
            return list;
        }
    }
}