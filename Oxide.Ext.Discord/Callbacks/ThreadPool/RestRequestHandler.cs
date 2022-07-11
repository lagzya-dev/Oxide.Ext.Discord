using System;
using Oxide.Ext.Discord.Entities.Api;
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
        private Bucket _bucket;
        private BaseRequest _request; 
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
            _request = handler.Request;
            _bucket = _request.Bucket;
            _logger = logger;
        }
        
        ///<inheritdoc/>
        protected override void HandleCallback(object data)
        {
            string bucketId = _bucket.Id;
            AdjustableSemaphore semaphore = _bucket.Semaphore;
            _request.Status = RequestStatus.PendingBucket;
            
            _logger.Debug("Waiting for bucket availability for Bucket ID: {0} Request ID: {1}", _bucket.Id, _request.Id);

            try
            {
                semaphore.WaitOne();
                if (bucketId != _bucket.Id)
                {
                    _logger.Debug("Bucket ID Changed. Waiting for bucket availability again for ID: {0} Old Bucket ID: {1} New Bucket ID: {2}", _request.Id, bucketId, _bucket.Id);
                    semaphore.Release();
                    semaphore = _bucket.Semaphore;
                    semaphore.WaitOne();
                }

                _logger.Debug("Request callback started for Bucket ID: {0} Request ID: {1}", _bucket.Id, _request.Id);
                RequestResponse response = _handler.Run();
                _request.OnRequestCompleted(_handler, response);
                _logger.Debug("Request callback completed for Bucket ID: {0} Request ID: {1}", _bucket.Id, _request.Id);
                _request.Dispose();
            }
            catch (Exception ex)
            {
                _request.OnRequestCompleted(_handler, RequestResponse.CreateUnhandledExceptionResponse(_handler));
                _logger.Exception("Request Callback threw exception for Bucket ID: {0} Request ID: {1}", _bucket.Id, _request.Id, ex);
                _request.Dispose();
            }
            finally
            {
                semaphore.Release();
            }
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
            _bucket = null;
            _request = null;
            _logger = null;
        }
    }
}