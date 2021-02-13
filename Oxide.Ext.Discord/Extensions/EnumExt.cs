using System;

namespace Oxide.Ext.Discord.Extensions
{
    public static class EnumExt
    {
        public static bool TryParse<TEnum>(this object value, out TEnum result) where TEnum : struct, IConvertible
        {
            bool retValue = value != null && Enum.IsDefined(typeof(TEnum), value);
            result = retValue ? (TEnum)Enum.Parse(typeof(TEnum), value.ToString()) : default(TEnum);
            return retValue;
        }
    }
}