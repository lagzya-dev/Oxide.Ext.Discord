using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// Represents a cache of enum strings
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    public class EnumCache<T> : Singleton<EnumCache<T>> where T : struct, IComparable, IFormattable, IConvertible
    {
        private readonly Dictionary<T, string> _cachedStrings = new Dictionary<T, string>();
        private readonly Dictionary<T, string> _loweredStrings = new Dictionary<T, string>();
        private readonly Dictionary<T, string> _numberString = new Dictionary<T, string>();
        private readonly Type _type;
        private readonly T[] _values;

        /// <summary>
        /// Constructor
        /// </summary>
        private EnumCache()
        {
            _type = typeof(T);
            _values = Enum.GetValues(_type).Cast<T>().ToArray();
            for (int index = 0; index < _values.Length; index++)
            {
                T value = _values[index];
                string enumString = value.ToString();
                _cachedStrings[value] = enumString;
                _loweredStrings[value] = enumString.ToLower();
            }
        }
        
        /// <summary>
        /// Returns the string representation of the enum
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Enum value as string</returns>
        public string ToString(T value)
        {
            if (_cachedStrings.TryGetValue(value, out string str))
            {
                return str;
            }
            str = value.ToString();
            _cachedStrings[value] = str;
            return str;
        }
        
        /// <summary>
        /// Returns the lowered string representation of the enum
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Enum value as lowered string</returns>
        public string ToLower(T value)
        {
            if (!_loweredStrings.TryGetValue(value, out string str))
            {
                str = value.ToString().ToLower();
                _loweredStrings[value] = str;
            }
            return str;
        }

        /// <summary>
        /// Converts the enum to it's number form as a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ToNumber(T value)
        {
            if (!_numberString.TryGetValue(value, out string str))
            {
                str = value.ToType(Enum.GetUnderlyingType(_type), null).ToString();
                _numberString[value] = str;
            }

            return str;
        }

        /// <summary>
        /// Returns a cached list of Enum Values
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<T> GetList()
        {
            return _values;
        }
    }
}