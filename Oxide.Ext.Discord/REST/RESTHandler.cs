using System;
using System.Collections.Generic;
using System.Text;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.REST
{
    /// <summary>
    /// Represents a REST handler for a bot
    /// </summary>
    public class RestHandler
    {
        /// <summary>
        /// Global Rate Limit for the bot
        /// </summary>
        public readonly BotGlobalRateLimit RateLimit = new BotGlobalRateLimit();
        
        /// <summary>
        /// The request buckets for the bot
        /// </summary>
        public readonly Hash<string, Bucket> Buckets = new Hash<string, Bucket>();

        private readonly Dictionary<string, string> _headers;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new REST handler for a bot client
        /// </summary>
        /// <param name="client">Client the request is for</param>
        /// <param name="logger">Logger from the client</param>
        public RestHandler(BotClient client, ILogger logger)
        {
            _logger = logger;

            _headers = new Dictionary<string, string>
            {
                ["Authorization"] = $"Bot {client.Settings.ApiToken}",
                ["Content-Type"] = "application/json",
                ["User-Agent"] = $"DiscordBot (https://github.com/Kirollos/Oxide.Ext.Discord, {DiscordExtension.GetExtensionVersion})"
            };
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Error callback if an error occurs</param>
        public void DoRequest(string url, RequestMethod method, object data, Action callback, Action<RestError> error)
        {
            CreateRequest(method, url, _headers, data, response => callback?.Invoke(), error);
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Error callback if an error occurs</param>
        /// <typeparam name="T">The type that is expected to be returned</typeparam>
        public void DoRequest<T>(string url, RequestMethod method, object data, Action<T> callback, Action<RestError> error)
        {
            CreateRequest(method, url, _headers, data, response =>
            {
                callback?.Invoke(response.ParseData<T>());
            }, error);
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="headers">Headers to be sent in the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Error callback if an error occurs</param>
        private void CreateRequest(RequestMethod method, string url, Dictionary<string, string> headers, object data, Action<RestResponse> callback, Action<RestError> error)
        {
            Request request = new Request(method, url, headers, data, callback, error, _logger);
            CleanupExpired();
            QueueRequest(request, _logger);
        }
        
        /// <summary>
        /// Removed buckets that are old and not being used
        /// </summary>
        public void CleanupExpired()
        {
            Buckets.RemoveAll(b => b.ShouldCleanup());
        }

        /// <summary>
        /// Queues the request
        /// </summary>
        /// <param name="request">Request to queue</param>
        /// <param name="logger">Logger to use</param>
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
        
        /// <summary>
        /// Shutdown the REST handler
        /// </summary>
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
            string previousSegment = routeSegments[0];
            for (int index = 1; index < routeSegments.Length; index++)
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
