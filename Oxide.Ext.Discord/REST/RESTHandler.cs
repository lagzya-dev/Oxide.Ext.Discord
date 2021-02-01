using System;
using System.Collections.Generic;
using System.Text;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.REST
{
    public class RestHandler
    {
        public readonly BotGlobalRateLimit RateLimit = new BotGlobalRateLimit();
        public readonly Hash<string, Bucket> Buckets = new Hash<string, Bucket>();

        private readonly Dictionary<string, string> _headers;
        private readonly ILogger _logger;
        private readonly BotClient _client;

        public RestHandler(BotClient client, ILogger logger)
        {
            _client = client;
            _logger = new Logger(client.Settings.LogLevel);

            _headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bot {client.Settings.ApiToken}" },
                { "Content-Type", "application/json" },
                { "User-Agent", $"DiscordBot (https://github.com/Trickyyy/Oxide.Ext.Discord, {DiscordExtension.GetExtensionVersion})" }
            };
        }

        public void DoRequest(string url, RequestMethod method, object data, Action callback, Action<RestError> onError)
        {
            CreateRequest(method, url, _headers, data, response => callback?.Invoke(), onError);
        }

        public void DoRequest<T>(string url, RequestMethod method, object data, Action<T> callback, Action<RestError> onError)
        {
            CreateRequest(method, url, _headers, data, response =>
            {
                callback?.Invoke(response.ParseData<T>());
            }, onError);
        }

        private void CreateRequest(RequestMethod method, string url, Dictionary<string, string> headers, object data, Action<RestResponse> callback, Action<RestError> onError)
        {
            Request request = new Request(method, url, headers, data, callback, onError, _logger);
            CleanupExpired();
            QueueRequest(request, _logger);
        }
        
        public void CleanupExpired()
        {
            Buckets.RemoveAll(b => b.ShouldCleanup());
        }

        public void QueueRequest(Request request, ILogger logger)
        {
            string bucketId = GetBucketId(request.Route);
            Bucket bucket = Buckets[bucketId];
            if (bucket == null)
            {
                bucket = new Bucket(this, bucketId, logger);
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
            RateLimit.Shutdown();
        }
        
        private string GetBucketId(string route)
        {
            string[] routeSegments = route.Split('/');
            StringBuilder bucket = new StringBuilder(routeSegments[0]);
            bucket.Append("/");
            string previousSegment = routeSegments[0];
            for (int index = 0; index < routeSegments.Length; index++)
            {
                string segment = routeSegments[index];
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
                
                // All other parts of the route should be considered as part of the bucket identifier
                bucket.Append(previousSegment = segment);
                bucket.Append("/");
            }

            return bucket.ToString();
        }
    }
}
