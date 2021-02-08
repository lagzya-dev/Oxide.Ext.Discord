using System;

namespace Oxide.Ext.Discord.Extensions
{
    /// <summary>
    /// Extensions for enums
    /// </summary>
    public static class EnumExt
    {
        /// <summary>
        /// Try to parse the enum value from string value
        /// </summary>
        /// <param name="value">string to parse</param>
        /// <param name="result">Resulting enum value</param>
        /// <typeparam name="TEnum">Enum Type</typeparam>
        /// <returns>True if successfully parsed; false otherwise</returns>
        public static bool TryParse<TEnum>(this string value, out TEnum result)
            where TEnum : struct, IConvertible
        {
            bool retValue = value != null && Enum.IsDefined(typeof(TEnum), value);
            result = retValue ? (TEnum)Enum.Parse(typeof(TEnum), value) : default(TEnum);
            return retValue;
        }
    }
}