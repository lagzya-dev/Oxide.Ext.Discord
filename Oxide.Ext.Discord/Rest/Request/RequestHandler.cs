using System.Collections.Generic;
using System.Text;
using System.Threading;
using Oxide.Ext.Discord.Logging;
namespace Oxide.Ext.Discord.Rest.Request
{
    /// <summary>
    /// Handles Assigning new requests to buckets
    /// </summary>
    public class RequestHandler
    {
        private readonly Thread _thread;
        private readonly ILogger _logger;
        private readonly RestHandler _rest;

        private readonly Queue<Request> _requests = new Queue<Request>();
        
        private readonly object _syncRoot = new object();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rest"></param>
        /// <param name="logger"></param>
        public RequestHandler(RestHandler rest, ILogger logger)
        {
            _logger = logger;
            _rest = rest;
            _thread = new Thread(ProcessQueue) {IsBackground = true, Name = "Request Handler"};
            _thread.Start();
        }

        /// <summary>
        /// Queues the following request to be added to a bucket
        /// </summary>
        /// <param name="request">Request to be added</param>
        public void QueueRequest(Request request)
        {
            AddRequest(request);
            if ((_thread.ThreadState & ThreadState.WaitSleepJoin) != 0)
            {
                _thread.Interrupt();
            }
        }

        private void ProcessQueue()
        {
            while (true)
            {
                while (HasRequests())
                {
                    QueueBucket();
                }

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                    
                }
            }
        }

        private void AddRequest(Request request)
        {
            lock (_syncRoot)
            {
                _requests.Enqueue(request);
            }
        }

        private Request GetNextRequest()
        {
            lock (_syncRoot)
            {
                return _requests.Dequeue();
            }
        }

        private bool HasRequests()
        {
            lock (_syncRoot)
            {
                return _requests.Count != 0;
            }
        }
        
        /// <summary>
        /// Queues the request for the bucket
        /// </summary>
        public void QueueBucket()
        {
            Request request = GetNextRequest();
            string bucketId = GetBucketId(request.Route);
            Bucket bucket = _rest.GetBucket(bucketId);
            bucket.QueueRequest(request);
            _rest.CleanupExpired();
        }
        
        /// <summary>
        /// Returns the Rate Limit Bucket for the given route
        /// https://discord.com/developers/docs/topics/rate-limits#rate-limits
        /// </summary>
        /// <param name="route">API Route</param>
        /// <returns>Bucket ID for route</returns>
        private static string GetBucketId(string route)
        {
            string[] routeSegments = route.Split('/');
            StringBuilder bucket = new StringBuilder(routeSegments[0]);
            bucket.Append('/');
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