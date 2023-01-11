using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Cache
{
    public class StringCache<T> : Singleton<StringCache<T>> where T : struct, IConvertible
    {
        private readonly Dictionary<T, string> _cachedStrings = new Dictionary<T, string>();
        
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