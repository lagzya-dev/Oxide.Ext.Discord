using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

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
        
        public static Hash<TKey, TElement> ToHash<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));
            
            Hash<TKey, TElement> hash = new Hash<TKey, TElement>();
            foreach (TSource element in source)
            {
                hash.Add(keySelector(element), elementSelector(element));
            }
            
            return hash;
        }
        
        public static Hash<TKey, TElement> ToPooledHash<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null) throw new ArgumentNullException(nameof(elementSelector));

            Hash<TKey, TElement> hash = DiscordPool.GetHash<TKey, TElement>();
            foreach (TSource element in source)
            {
                hash.Add(keySelector(element), elementSelector(element));
            }
            
            return hash;
        }
    }
}