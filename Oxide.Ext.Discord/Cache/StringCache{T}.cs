using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// Caches strings from {T} ToString method
    /// </summary>
    /// <typeparam name="T">Type for the cache</typeparam>
    public class StringCache<T> : Singleton<StringCache<T>> where T : struct, IConvertible
    {
        private readonly Dictionary<T, string> _cachedStrings = new Dictionary<T, string>();
        
        /// <summary>
        /// Returns a cached ToString call of type {T}
        /// </summary>
        /// <param name="value">value to ToString</param>
        /// <returns></returns>
        public string ToString(T value)
        {
            if (!_cachedStrings.TryGetValue(value, out string str))
            {
                str = value.ToString();
                _cachedStrings[value] = str;
            }
            
            return str;
        }
    }
}