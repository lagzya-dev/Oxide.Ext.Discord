using System;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Rest.Requests;
using Oxide.Ext.Discord.Threading;

namespace Oxide.Ext.Discord.Callbacks.ThreadPool
{
    /// <summary>
    /// Thread Pool Handler for Rest Requests
    /// </summary>
    public class RestRequestHandler : BaseThreadPoolHandler
    {
        private RequestHandler _handler;
        private ILogger _logger;

        /// <summary>
        /// Creates a thread pooled rest request
        /// </summary>
        /// <param name="handler">Request Handler for the request</param>
        /// <param name="logger">Logger</param>
        /// <returns><see cref="RestRequestHandler"/></returns>
        public static RestRequestHandler CreateRequestCallback(RequestHandler handler, ILogger logger)
        {
            RestRequestHandler request = DiscordPool.Get<RestRequestHandler>();
            request.Init(handler, logger);
            return request;
        }
        
        private void Init(RequestHandler handler, ILogger logger)
        {
            _handler = handler;
            _logger = logger;
        }
        
        ///<inheritdoc/>
        protected override void HandleCallback(object data)
        {
            string bucketId = GetBucketId();
            AdjustableSemaphore semaphore = GetSemaphore();
            
            _logger.Debug("Waiting for bucket availability for Method: {0} Route: {1}", _handler.Request.Method, _handler.Request.Route);

            try
            {
                semaphore.WaitOne();
                if (bucketId != GetBucketId())
                {
                    _logger.Debug("Bucket ID Changed. Waiting for bucket availability again for Method: {0} Route: {1}", _handler.Request.Method, _handler.Request.Route);
                    semaphore.Release();
                    semaphore = GetSemaphore();
                    semaphore.WaitOne();
                }

                if (IsBucketShutdown() || _handler.Cancelled)
                {
                    semaphore.Release();
                    return;
                }

                _logger.Debug("Starting request callback for Method: {0} Route: {1}", _handler.Request.Method, _handler.Request.Route);
                _handler.Run();
            }
            catch (Exception ex)
            {
                _logger.Exception("Request Callback threw exception Method: {0} Route: {1}", _handler.Request.Method, _handler.Request.Route, ex);
            }
            finally
            {
                semaphore.Release();
                _logger.Debug("Request callback completed for Method: {0} Route: {1}", _handler.Request.Method, _handler.Request.Route);
            }
        }

        private Bucket GetBucket()
        {
            return _handler.Request.Bucket;
        }
        
        private string GetBucketId()
        {
            return GetBucket().BucketId;
        }

        private AdjustableSemaphore GetSemaphore()
        {
            return GetBucket().Semaphore;
        }
        
        private bool IsBucketShutdown()
        {
            return GetBucket().IsShutdown;
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _handler = null;
            _logger = null;
        }
    }
}