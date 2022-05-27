using System.Net;

namespace Oxide.Ext.Discord.Extensions
{
    internal static class WebHeaderCollectionExt
    {
        internal static bool GetBool(this WebHeaderCollection headers, string key)
        {
            string value = headers.Get(key);
            if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out bool result))
            {
                return default(bool);
            }

            return result;
        }
        
        internal static int GetInt(this WebHeaderCollection headers, string key)
        {
            string value = headers.Get(key);
            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out int result))
            {
                return default(int);
            }

            return result;
        }
        
        internal static double GetDouble(this WebHeaderCollection headers, string key)
        {
            string value = headers.Get(key);
            if (string.IsNullOrEmpty(value) || !double.TryParse(value, out double result))
            {
                return default(double);
            }

            return result;
        }
    }
}