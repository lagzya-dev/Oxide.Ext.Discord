using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Hash extensions
    /// </summary>
    internal static class HashExt
    {
        /// <summary>
        /// Remove all records from the hash with the given predicate filter
        /// </summary>
        /// <param name="hash">Hash to have data removed from</param>
        /// <param name="predicate">Filter of which values to remove</param>
        /// <typeparam name="TKey">Key type of the hash</typeparam>
        /// <typeparam name="TValue">Value type of the hash</typeparam>
        internal static void RemoveAll<TKey, TValue>(this Hash<TKey, TValue> hash, Func<TKey, bool> predicate)
        {
            if (hash == null) throw new ArgumentNullException(nameof(hash));

            List<TKey> removeKeys = DiscordPool.GetList<TKey>();
            foreach (KeyValuePair<TKey, TValue> key in hash)
            {
                if (predicate(key.Key))
                {
                    removeKeys.Add(key.Key);
                }
            }

            foreach (TKey key in removeKeys)
            {
                hash.Remove(key);
            }
            
            DiscordPool.FreeList(removeKeys);
        }
        
        /// <summary>
        /// Remove all records from the hash with the given predicate filter
        /// </summary>
        /// <param name="hash">Hash to have data removed from</param>
        /// <param name="predicate">Filter of which values to remove</param>
        /// <param name="onRemove">Action to call when an element is removed</param>
        /// <typeparam name="TKey">Key type of the hash</typeparam>
        /// <typeparam name="TValue">Value type of the hash</typeparam>
        internal static void RemoveAll<TKey, TValue>(this Hash<TKey, TValue> hash, Func<TValue, bool> predicate, Action<TValue> onRemove = null)
        {
            if (hash == null) throw new ArgumentNullException(nameof(hash));

            List<TKey> removeKeys = DiscordPool.GetList<TKey>();
            foreach (KeyValuePair<TKey, TValue> key in hash)
            {
                if (predicate(key.Value))
                {
                    removeKeys.Add(key.Key);
                    onRemove?.Invoke(key.Value);
                }
            }

            foreach (TKey key in removeKeys)
            {
                hash.Remove(key);
            }
            
            DiscordPool.FreeList(removeKeys);
        }

        /// <summary>
        /// Creates a clone of a hash with it's current key value pairs
        /// </summary>
        /// <param name="hash">Hash to be copied</param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns>Copied Hash</returns>
        internal static Hash<TKey, TValue> Clone<TKey, TValue>(this Hash<TKey, TValue> hash)
        {
            Hash<TKey, TValue> copy = new Hash<TKey, TValue>();
            CopyTo(hash, copy);
            return copy;
        }

        internal static void CopyTo<TKey, TValue>(this Hash<TKey, TValue> hash, Hash<TKey, TValue> target)
        {
            foreach (KeyValuePair<TKey, TValue> value in hash)
            {
                target[value.Key] = value.Value;
            }
        }
    }
}