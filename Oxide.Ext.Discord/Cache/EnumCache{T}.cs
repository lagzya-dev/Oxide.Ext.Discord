using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Singleton;

namespace Oxide.Ext.Discord.Cache
{
    /// <summary>
    /// Represents a cache of enum strings
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    public class EnumCache<T> : Singleton<EnumCache<T>> where T : struct, IComparable, IFormattable, IConvertible
    {
        public readonly ReadOnlyCollection<T> Values;
        
        private readonly Dictionary<T, string> _cachedStrings = new Dictionary<T, string>();
        private readonly Dictionary<T, string> _loweredStrings = new Dictionary<T, string>();
        private readonly Dictionary<T, string> _numberString = new Dictionary<T, string>();
        private readonly Type _type;
        private readonly bool _isFlagsEnum;
        private readonly TypeCode _typeCode;

        /// <summary>
        /// Constructor
        /// </summary>
        private EnumCache()
        {
            _type = typeof(T);
            _isFlagsEnum = _type.HasAttribute<FlagsAttribute>(false);
            T[] values = Enum.GetValues(_type).Cast<T>().ToArray();
            _typeCode = Convert.GetTypeCode(values[0]);
            for (int index = 0; index < values.Length; index++)
            {
                T value = values[index];
                string enumString = value.ToString();
                _cachedStrings[value] = enumString;
                _loweredStrings[value] = enumString.ToLower();
            }
            Values = new ReadOnlyCollection<T>(values);
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

            str = _isFlagsEnum ? CreateFlagsString(value) : value.ToString();
            
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
                str = ToString(value).ToLower();
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

        private int GetTypeSize()
        {
            switch (_typeCode)
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                    return 8;
                case TypeCode.Int16:
                case TypeCode.UInt16:
                    return 16;
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return 32;
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return 64;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private string CreateFlagsString(T value)
        {
            StringBuilder sb = DiscordPool.Internal.GetStringBuilder();
            bool initial = true;
            int length = GetTypeSize();
            for (int index = 0; index < length; index++)
            {
                ulong enumValue = 1ul << index;
                if ((value.ToUInt64(null) & enumValue) != 0ul)
                {
                    if (!initial)
                    {
                        sb.Append(", ");
                    }

                    initial = false;

                    object converted = Convert.ChangeType(enumValue, _typeCode);
                    if (Enum.IsDefined(_type, converted))
                    {
                        sb.Append(Enum.GetName(_type, converted));
                    }
                    else
                    {
                        sb.Append("Unknown Value (1 << ");
                        sb.Append(index);
                        sb.Append(')');
                    }
                }
            }

            return DiscordPool.Internal.FreeStringBuilderToString(sb);
        }

    }
}