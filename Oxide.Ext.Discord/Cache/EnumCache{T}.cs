using System;
using System.Collections.Generic;
using System.Linq;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// Represents a cache of enum strings
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    public static class EnumCache<T>
    {
        private static readonly Dictionary<T, string> CachedStrings = new Dictionary<T, string>();
        private static readonly Dictionary<T, string> LoweredStrings = new Dictionary<T, string>();
        private static readonly T[] Values; 

        static EnumCache()
        {
            Values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
            for (int index = 0; index < Values.Length; index++)
            {
                T value = Values[index];
                string enumString = value.ToString();
                CachedStrings[value] = enumString;
                LoweredStrings[value] = enumString.ToLower();
            }
        }
        
        /// <summary>
        /// Returns the string representation of the enum
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Enum value as string</returns>
        public static string ToString(T value)
        {
            return CachedStrings[value];
        }
        
        /// <summary>
        /// Returns the lowered string representation of the enum
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Enum value as lowered string</returns>
        public static string ToLower(T value)
        {
            return LoweredStrings[value];
        }

        public static IReadOnlyList<T> GetList()
        {
            return Values;
        }
    }
}