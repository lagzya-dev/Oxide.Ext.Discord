using System;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Extensions for enums
    /// </summary>
    internal static class EnumExt
    {
        /// <summary>
        /// Try to parse the enum value from string value
        /// </summary>
        /// <param name="value">string to parse</param>
        /// <param name="result">Resulting enum value</param>
        /// <typeparam name="TEnum">Enum Type</typeparam>
        /// <returns>True if successfully parsed; false otherwise</returns>
        public static bool TryParse<TEnum>(this object value, out TEnum result) where TEnum : struct, IConvertible
        {
            bool retValue = value != null && Enum.IsDefined(typeof(TEnum), value);
            result = retValue ? (TEnum)Enum.Parse(typeof(TEnum), value.ToString()) : default(TEnum);
            return retValue;
        }

        public static string GetEnumPropertyName<T>(this object value)
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            string enumValue = value.ToString();
            
            MemberInfo[] member = type.GetMember(value.ToString());
            object[] attributes = member[0].GetCustomAttributes(typeof(JsonProperty), false);
            if (attributes.Length != 0 && attributes[0] is JsonProperty attribute)
            {
                return attribute.PropertyName;
            }

            return null;
        }
    }
}