using System;
using System.IO;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Buckets;

namespace Oxide.Ext.Discord.Rest.Requests
{
    public abstract class BaseRequest : BasePoolable
    {
        public Snowflake RequestId;
        
        /// <summary>
        /// HTTP request method
        /// </summary>
        public RequestMethod Method;

        /// <summary>
        /// Route on the API
        /// </summary>
        public string Route;

        /// <summary>
        /// Data to be sent with the request
        /// </summary>
        public object Data;
        
        public string RequestUrl => DiscordEndpoints.Rest.ApiUrl + Route;
        
        /// <summary>
        /// Callback to call if the request errored with the last error message
        /// </summary>
        internal Action<RestError> OnError;
        
        /// <summary>
        /// Discord Client making the request
        /// </summary>
        internal DiscordClient Client;

        internal string AuthHeader;
        
        internal Bucket Bucket;

        /// <summary>
        /// How long to wait before retrying request since there was a web exception
        /// </summary>
        private DateTimeOffset _errorResetAt;

        protected void Init(DiscordClient client, RequestMethod method, string route, object data, Action<RestError> onError)
        {
            RequestId = new Snowflake(DateTimeOffset.UtcNow);
            Client = client;
            Method = method;
            Route = route;
            Data = data;
            AuthHeader = client.Bot.Rest.AuthHeader;
            OnError = onError;
        }

        internal DateTimeOffset GetRequestResetAt()
        {
            if (Bucket.RateLimit.HasReachedRateLimit)
            {
                return Bucket.RateLimit.NextReset();
            }
            
            return _errorResetAt > Bucket.ResetAt ? _errorResetAt : Bucket.ResetAt;
        }
        
        // internal bool CanStartRequest()
        // {
        //     if (Bucket.RateLimit.HasReachedRateLimit)
        //     {
        //         return false;
        //     }
        //
        //     if (_errorResetAt > DateTimeOffset.UtcNow)
        //     {
        //         return false;
        //     }
        //
        //     return Bucket.Remaining > 0 || Bucket.ResetAt >= DateTimeOffset.UtcNow;
        //
        //     // if(Bucket.ResetAt )
        //     //
        //     // Client.Logger.Debug($"BaseRequest.CanStartRequest? Bucket.Remaining > 0: {Bucket.Remaining > 0} && _errorResetAt <= DateTimeOffset.UtcNow: {_errorResetAt <= DateTimeOffset.UtcNow} && !Bucket.RateLimit.HasReachedRateLimit: {!Bucket.RateLimit.HasReachedRateLimit}");
        //     // return Bucket.Remaining > 0 && _errorResetAt <= DateTimeOffset.UtcNow && !Bucket.RateLimit.HasReachedRateLimit;
        // }

        internal virtual void OnRequestCompleted(RequestHandler handler, RestResponse response)
        {
            Bucket.OnRequestCompleted(handler, response);
        }

        internal void OnRequestErrored()
        {
            _errorResetAt = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(1); 
        }

        internal void OnRequestQueued(Bucket bucket)
        {
            Bucket = bucket;
        }

        protected override void EnterPool()
        {
            Method = default(RequestMethod);
            Route = null;
            Data = null;
            OnError = null;
            Client = null;
            Bucket = null;
        }
    }
}