using System.Text;
using Oxide.Ext.Discord.Extensions;
using Oxide.Plugins;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.REST
{
    using System;
    using System.Collections.Generic;

    public class RESTHandler
    {
        public static readonly Hash<string, BotRateLimit> GlobalRateLimit = new Hash<string, BotRateLimit>();
        
        private Hash<string, Bucket> buckets = new Hash<string, Bucket>();

        private string apiKey;

        private Dictionary<string, string> headers;

        private readonly LogLevel _logLevel;

        public RESTHandler(string apiKey, LogLevel logLevel)
        {
            this.apiKey = apiKey;
            _logLevel = logLevel;
            
            // Version
            string version = "";
            var exts = Oxide.Core.Interface.Oxide.GetAllExtensions();
            foreach (var ext in exts)
            {
                if (ext.Name != "Discord") continue;
                version = $"{ext.Version.Major}.{ext.Version.Minor}.{ext.Version.Patch}";
                break;
            }
            //-

            headers = new Dictionary<string, string>()
            {
                { "Authorization", $"Bot {this.apiKey}" },
                { "Content-Type", "application/json" },
                { "User-Agent", $"DiscordBot (https://github.com/Trickyyy/Oxide.Ext.Discord, {version})" }
            };

            lock (GlobalRateLimit)
            {
                if (!GlobalRateLimit.ContainsKey(apiKey))
                {
                    GlobalRateLimit[apiKey] = new BotRateLimit();
                }
            }
        }

        public void Shutdown()
        {
            foreach (Bucket bucket in buckets.Values)
            {
                bucket.Disposed = true;
                bucket.Close();
            }
        }

        public void DoRequest(string url, RequestMethod method, object data, Action callback)
        {
            CreateRequest(method, url, headers, data, response => callback?.Invoke());
        }

        public void DoRequest<T>(string url, RequestMethod method, object data, Action<T> callback)
        {
            CreateRequest(method, url, headers, data, response =>
            {
                callback?.Invoke(response.ParseData<T>());
            });
        }

        private void CreateRequest(RequestMethod method, string url, Dictionary<string, string> headers, object data, Action<RestResponse> callback)
        {
            var request = new Request(method, url, headers, data, callback, _logLevel);
            BucketRequest(request);
        }

        private string GetBucket(string route)
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

        private void BucketRequest(Request request)
        {
            buckets.RemoveAll(b => b.Disposed);

            string bucketId = GetBucket(request.Route);
            Bucket bucket = buckets[bucketId];
            if (bucket == null)
            {
                bucket = new Bucket(request.Method, request.Route, apiKey);
                buckets[bucketId] = bucket;
            }
            
            bucket.Queue(request);
        }
    }
}
