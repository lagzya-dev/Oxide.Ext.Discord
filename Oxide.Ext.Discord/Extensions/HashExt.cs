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
    }
}