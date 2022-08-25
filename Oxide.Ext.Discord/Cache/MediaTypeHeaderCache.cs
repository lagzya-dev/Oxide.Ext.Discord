using System.Net.Http.Headers;
using Oxide.Ext.Discord.Constants;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Cache
{
    internal static class MediaTypeHeaderCache
    {
        private static readonly Hash<string, MediaTypeHeaderValue> Cache = new Hash<string, MediaTypeHeaderValue>();
        private const string JsonHeader = "application/json";

        static MediaTypeHeaderCache()
        {
            MediaTypeHeaderValue header = MediaTypeHeaderValue.Parse(JsonHeader);
            header.CharSet = DiscordEncoding.Encoding.WebName;
            Cache[JsonHeader] = header;
        }

        public static MediaTypeHeaderValue Get(string value)
        {
            MediaTypeHeaderValue header = Cache[value];
            if (header == null)
            {
                header = MediaTypeHeaderValue.Parse(value);
                Cache[value] = header;
            }

            return header;
        }
    }
}