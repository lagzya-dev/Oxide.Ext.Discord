using System.Text;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Singleton;
using Oxide.Ext.Discord.Types;

namespace Oxide.Ext.Discord.Factory
{
    internal sealed class BucketIdFactory : Singleton<BucketIdFactory>
    {
        private const char SplitChar = '/';
        private const char QueryStringChar = '?';
        private const string IdReplacement = "id";
        private const string ReactionsRoute =  "reactions";

        private BucketIdFactory() { }
        
        /// <summary>
        /// Returns the Rate Limit Bucket for the given route
        /// https://discord.com/developers/docs/topics/rate-limits#rate-limits
        /// </summary>
        /// <param name="method">Request method for the request</param>
        /// <param name="route">API Route</param>
        /// <returns>Bucket ID for route</returns>
        internal BucketId GenerateId(RequestMethod method, string route)
        {
            int routeLength = route.LastIndexOf(QueryStringChar);
            if (routeLength == -1)
            {
                routeLength = route.Length;
            }
            
            StringTokenizer tokenizer = StringTokenizer.Create(route, SplitChar, routeLength);
            tokenizer.MoveNext();
            string previous = tokenizer.Current;
            
            StringBuilder bucket = DiscordPool.Internal.GetStringBuilder();
            bucket.Append(EnumCache<RequestMethod>.Instance.ToString(method));
            bucket.Append(':');
            bucket.Append(previous);
            
            while (tokenizer.MoveNext())
            {
                bucket.Append('/');

                string current = GetCurrent(tokenizer.Index, previous, tokenizer.Current);

                bucket.Append(current);
                if (current == ReactionsRoute)
                {
                    break;
                }
                
                previous = current;
            }

            return new BucketId(DiscordPool.Internal.FreeStringBuilderToString(bucket));
        }
        
        private static string GetCurrent(int index, string previous, string token)
        {
            //If previous is not a major ID we don't want to include the ID in the bucket ID so use "id" string instead
            return char.IsNumber(token[0]) && !IsMajorId(index, previous) ? IdReplacement : token;
        }

        private static bool IsMajorId(int index, string previous)
        {
            //We should only use Major ID if the previous segment name is the first segment and the ID is the second.
            if (index == 1)
            {
                switch (previous)
                {
                    case "guilds":
                    case "channels":
                    case "webhooks":
                        return true;
                }
            }

            return false;
        }
    }
}