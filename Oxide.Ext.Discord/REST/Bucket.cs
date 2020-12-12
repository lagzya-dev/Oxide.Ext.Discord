using System.Collections.Generic;
using System.Threading;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.REST
{
    public class Bucket : List<Request>
    {
        public RequestMethod Method { get; }

        public string Route { get; }

        public int Limit { get; set; }

        public int Remaining { get; set; }

        public int Reset { get; set; }

        public bool Initialized { get; private set; }

        public bool Disposed { get; set; }

        public readonly string ApiKey;
        
        private Thread _thread;

        public Bucket(RequestMethod method, string route, string apiKey)
        {
            Method = method;
            Route = route;
            ApiKey = apiKey;

            _thread = new Thread(RunThread);
            _thread.Start();
        }

        public void Close()
        {
            _thread?.Abort();
            _thread = null;
        }

        public void Queue(Request request)
        {
            lock (this)
            {
                Add(request);
            }

            if (!Initialized)
            {
                Initialized = true;
            }
        }

        private void RunThread()
        {
            // 'Initialized' basically allows us to start the while
            // loop from the constructor even when this.Count = 0
            // (eg after the bucket is created, before requests are added)
            while (!Initialized || Count > 0)
            {
                if (Disposed)
                {
                    break;
                }

                if (!Initialized)
                {
                    continue;
                }

                FireRequests();
            }

            Disposed = true;
        }

        private void FireRequests()
        {
            ////this.CleanRequests();

            if (Remaining == 0 && Reset >= Time.TimeSinceEpoch())
            {
                Thread.Sleep(Reset - Time.TimeSinceEpoch());
                return;
            }
            
            BotRateLimit rateLimit;
            lock (RESTHandler.GlobalRateLimit)
            {
                rateLimit = RESTHandler.GlobalRateLimit[ApiKey];
            }

            if (rateLimit.HasReachedRateLimit)
            {
                Remaining = 0;
                Reset = rateLimit.NextBucketReset;
            }

            lock (this)
            {
                foreach (Request request in this)
                {
                    if (request.HasTimedOut())
                    {
                        request.Close(false);
                    }

                    if (request.InProgress)
                    {
                        return;
                    }
                }
            }
            
            rateLimit.FiredRequest();
            this[0].Fire(this);
        }

        ////private void CleanRequests()
        ////{
        ////    var requests = new List<Request>(this);

        ////    foreach (var req in requests.Where(x => x.HasTimedOut()))
        ////    {
        ////        Interface.Oxide.LogWarning($"[Discord Ext] Closing request (timed out): {req.Route + req.Endpoint} [{req.Method}]");
        ////        req.Close();
        ////    }
        ////}
    }
}
