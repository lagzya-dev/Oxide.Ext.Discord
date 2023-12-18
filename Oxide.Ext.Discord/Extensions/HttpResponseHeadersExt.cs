using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Oxide.Ext.Discord.Rest;

namespace Oxide.Ext.Discord.Extensions
{
    internal static class HttpResponseHeadersExt
    {
        internal static string Get(this HttpResponseHeaders headers, string key)
        {
            return headers.TryGetValues(key, out IEnumerable<string> values) ? values.FirstOrDefault() : null;
        }
        
        internal static bool GetBool(this HttpResponseHeaders headers, string key)
        {
            string value = headers.Get(key);
            if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out bool result))
            {
                return default(bool);
            }

            return result;
        }
        
        internal static int GetInt(this HttpResponseHeaders headers, string key)
        {
            string value = headers.Get(key);
            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out int result))
            {
                return default(int);
            }

            return result;
        }
        
        internal static double GetDouble(this HttpResponseHeaders headers, string key)
        {
            string value = headers.Get(key);
            if (string.IsNullOrEmpty(value) || !double.TryParse(value, out double result))
            {
                return default(double);
            }

            return result;
        }
        
        internal static BucketId GetBucketId(this HttpResponseHeaders headers, string key)
        {
            string value = headers.Get(key);
            return !string.IsNullOrEmpty(value) ? new BucketId(value) : default(BucketId);
        }
    }
}