using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Extensions
{
    public static class HashExt
    {
        public static void RemoveAll<TKey, TValue>(this Hash<TKey, TValue> dict, Func<TValue, bool> predicate)
        {
            List<TKey> keys = dict.Keys.Where(k => predicate(dict[k])).ToList();
            foreach (TKey key in keys)
            {
                dict.Remove(key);
            }
        }

        public static Hash<TKey, TValue> ToHash<TKey, TValue>(this List<TValue> list, Func<TValue, TKey> key)
        {
            Hash<TKey, TValue> hash = new Hash<TKey, TValue>();
            for (int i = 0; i < list.Count; i++)
            {
                TValue value = list[i];
                hash[key(value)] = value;
            }

            return hash;
        }
    }
}