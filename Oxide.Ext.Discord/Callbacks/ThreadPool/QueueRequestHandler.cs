using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.ThreadPool
{
    /// <summary>
    /// Callback for queuing a request using a thread pool
    /// </summary>
    public class QueueRequestHandler : BaseThreadPoolHandler
    {
        private BaseRequest _request;
        private RestHandler _rest;
        
        internal static QueueRequestHandler Create(BaseRequest request, RestHandler rest)
        {
            QueueRequestHandler callback = DiscordPool.Get<QueueRequestHandler>();
            callback.Init(request, rest);
            return callback;
        }
        
        private void Init(BaseRequest request, RestHandler rest)
        {
            _request = request;
            _rest = rest;
        }
        
        /// <summary>
        /// Calls the rest QueueBucket method with the request to be queued
        /// </summary>
        /// <param name="data"></param>
        protected override void HandleCallback(object data)
        {
            _rest.QueueBucket(_request);
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _request = null;
            _rest = null;
        }
    }
}