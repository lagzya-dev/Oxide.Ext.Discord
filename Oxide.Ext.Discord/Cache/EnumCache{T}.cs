using System;
using System.Collections.Generic;
using System.Linq;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// Represents a cache of enum strings
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    public static class EnumCache<T> where T : struct, IConvertible
    {
        private static readonly Dictionary<T, string> CachedStrings = new Dictionary<T, string>();
        private static readonly Dictionary<T, string> LoweredStrings = new Dictionary<T, string>();
        private static readonly Dictionary<T, string> NumberString = new Dictionary<T, string>();
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
            if (CachedStrings.TryGetValue(value, out string str))
            {
                return str;
            }
            str = value.ToString();
            CachedStrings[value] = str;
            return str;
        }
        
        /// <summary>
        /// Returns the lowered string representation of the enum
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Enum value as lowered string</returns>
        public static string ToLower(T value)
        {
            if (LoweredStrings.TryGetValue(value, out string str))
            {
                return str;
            }
            str = value.ToString().ToLower();
            LoweredStrings[value] = str;
            return str;
        }

        public static string ToNumber(T value)
        {
            if (NumberString.TryGetValue(value, out string str))
            {
                return str;
            }
            
            str = value.ToType(Enum.GetUnderlyingType(typeof(T)), null).ToString();
            NumberString[value] = str;
            return str;
        }

        /// <summary>
        /// Returns a cached list of Enum Values
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyList<T> GetList()
        {
            return Values;
        }
    }
}