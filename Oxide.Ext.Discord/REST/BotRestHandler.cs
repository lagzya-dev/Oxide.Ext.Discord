using System.Collections.Generic;
using System.Text;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.REST
{
    public class BotRestHandler
    {
        public readonly BotGlobalRateLimit RateLimit = new BotGlobalRateLimit();
        public readonly Hash<string, Bucket> Buckets = new Hash<string, Bucket>();
        private readonly List<DiscordClient> _clients = new List<DiscordClient>();

        public void AddClient(DiscordClient client)
        {
            _clients.Add(client);
        }

        public void RemoveClient(DiscordClient client)
        {
            _clients.RemoveAll(c => c == client);
        }

        public bool IsEmpty => _clients.Count == 0;
        
        public void CleanupExpired()
        {
            double timeSince = Time.TimeSinceEpoch();
            Buckets.RemoveAll(b => b.ShouldCleanup(timeSince));
        }

        public void QueueRequest(Request request, LogLevel logLevel)
        {
            string bucketId = GetBucketId(request.Route);
            Bucket bucket = Buckets[bucketId];
            if (bucket == null)
            {
                bucket = new Bucket(this, bucketId, logLevel);
                Buckets[bucketId] = bucket;
            }

            bucket.Queue(request);
        }
        
        public void Shutdown()
        {
            foreach (Bucket bucket in Buckets.Values)
            {
                bucket.Close();
            }
            Buckets.Clear();
            _clients.Clear();
            RateLimit.Shutdown();
        }
        
        private string GetBucketId(string route)
        {
            StringBuilder bucket = new StringBuilder();
            string[] routeSegments = route.Split('/');
            string previousSegment = null;
            for (int index = 0; index < routeSegments.Length; index++)
            {
                string segment = routeSegments[index];
                if (index != 0)
                {
                    switch (previousSegment)
                    {
                        // Reactions routes and sub-routes all share the same bucket
                        case "reactions":
                            return bucket.ToString();
                        
                        // Literal IDs should only be taken account if they are the Major ID (Channel ID / Guild ID / Webhook ID)
                        case "guilds":
                        case "channels": 
                        case "webhooks":
                            break;
                            
                        default:
                            if (ulong.TryParse(segment, out ulong _))
                            {
                                bucket.Append("id/");
                                previousSegment = segment;
                                continue;
                            }

                            break;
                    }
                }
                
                // All other parts of the route should be considered as part of the bucket identifier
                bucket.Append(previousSegment = segment);
                bucket.Append("/");
            }

            return bucket.ToString();
        }
    }
}