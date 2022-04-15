using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Rest
{
    /// <summary>
    /// Callback for queuing a request using a thread pool
    /// </summary>
    public class QueueRequestCallback : BaseThreadPoolCallback
    {
        private Request _request;
        private RestHandler _rest;

        private void Init(Request request, RestHandler rest)
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
        protected override void EnterPool()
        {
            _request = null;
            _rest = null;
        }
        
        internal static QueueRequestCallback Create(Request request, RestHandler rest)
        {
            QueueRequestCallback callback = DiscordPool.Get<QueueRequestCallback>();
            callback.Init(request, rest);
            return callback;
        }
    }
}