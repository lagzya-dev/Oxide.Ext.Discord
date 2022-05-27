using System;
using Oxide.Core;
using Oxide.Ext.Discord.Callbacks.Api;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Requests
{
    public class Request<T> : BaseRequest
    {
        /// <summary>
        /// Callback to call if the request completed successfully
        /// </summary>
        internal Action<T> OnSuccess;
        
        public void Init(DiscordClient client, RequestMethod method, string route, object data, Action<T> onSuccess, Action<RestError> onError)
        {
            base.Init(client, method, route, data, onError);
            OnSuccess = onSuccess;
        }
        
        /// <summary>
        /// Handles a completed request
        /// </summary>
        internal override void OnRequestCompleted(RequestHandler handler, RestResponse response)
        {
            base.OnRequestCompleted(handler, response);
            if (response.Status == RequestStatus.Success && OnSuccess == null)
            {
                Dispose();
                return;
            }
            
            ApiCallback<T> callback = DiscordPool.Get<ApiCallback<T>>();
            callback.Init(this, response);
            Interface.Oxide.NextTick(callback.Callback);
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        protected override void EnterPool()
        {
            base.EnterPool();
            OnSuccess = null;
        }
    }
}