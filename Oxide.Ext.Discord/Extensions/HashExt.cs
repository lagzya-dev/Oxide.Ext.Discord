using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Extensions
{
    internal static class HashExt
    {
        internal static void RemoveAll<TKey, TValue>(this Hash<TKey, TValue> dict, Func<TValue, bool> predicate)
        {
            if (dict == null)
            {
                return;
            }
            
            foreach (KeyValuePair<TKey, TValue> key in dict.Where(k => predicate(k.Value)).ToList())
            {
                dict.Remove(key.Key);
            }
        }
    }
}