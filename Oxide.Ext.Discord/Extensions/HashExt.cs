using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Hash extensions
    /// </summary>
    public static class HashExt
    {
        /// <summary>
        /// Remove all records from the hash with the given predicate fulter
        /// </summary>
        /// <param name="hash">Hash to have data removed from</param>
        /// <param name="predicate">Filter of which values to remove</param>
        /// <typeparam name="TKey">Key type of the hash</typeparam>
        /// <typeparam name="TValue">Value type of the hash</typeparam>
        public static void RemoveAll<TKey, TValue>(this Hash<TKey, TValue> hash, Func<TValue, bool> predicate)
        {
            if (hash == null)
            {
                return;
            }
            
            foreach (KeyValuePair<TKey, TValue> key in hash.Where(k => predicate(k.Value)).ToList())
            {
                hash.Remove(key.Key);
            }
        }
    }
}