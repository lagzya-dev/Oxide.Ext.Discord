using System;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Rest.Requests;
using Oxide.Ext.Discord.Threading;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    /// <summary>
    /// Thread Pool Handler for Rest Requests
    /// </summary>
    public class RestRequestCallback : BaseAsyncPoolableCallback
    {
        private RestHandler _rest;
        private BaseRequest _request; 
        
        private RequestHandler _handler;
        private Bucket _bucket;

        private ILogger _logger;

        /// <summary>
        /// Creates a thread pooled rest request
        /// </summary>
        /// <param name="rest"><see cref="RestHandler"/> for the request</param>
        /// <param name="request">Request to be ran</param>
        /// <param name="logger">Logger</param>
        /// <returns><see cref="RestRequestCallback"/>Created Handler</returns>
        public static RestRequestCallback CreateRequestCallback(RestHandler rest, BaseRequest request, ILogger logger)
        {
            RestRequestCallback handler = DiscordPool.Get<RestRequestCallback>();
            handler.Init(rest, request, logger);
            return handler;
        }
        
        private void Init(RestHandler rest, BaseRequest handler, ILogger logger)
        {
            _rest = rest;
            _request = handler;
            _logger = logger;
        }
        
        ///<inheritdoc/>
        protected override async Task HandleCallback()
        {
            AdjustableSemaphore semaphore = null;
            try
            {
                _handler = RequestHandler.CreateRequestHandler(_request);
                _bucket = _rest.QueueBucket(_handler, _request);
            
                string bucketId = _bucket.Id;
                semaphore = _bucket.Semaphore;
                _request.Status = RequestStatus.PendingBucket;
            
                _logger.Debug("Waiting for bucket availability Bucket ID: {0} Request ID: {1}", _bucket.Id, _request.Id);
                
                await semaphore.WaitOneAsync();
                if (bucketId != _bucket.Id)
                {
                    _logger.Debug("Bucket ID Changed. Waiting for bucket availability again for ID: {0} Old Bucket ID: {1} New Bucket ID: {2}", _request.Id, bucketId, _bucket.Id);
                    semaphore.Release();
                    semaphore = _bucket.Semaphore;
                    await semaphore.WaitOneAsync();
                }

                _logger.Debug("Request callback started for Bucket ID: {0} Request ID: {1}", _bucket.Id, _request.Id);
                await _handler.Run();
                _logger.Debug("Request callback completed successfully for Bucket ID: {0} Request ID: {1}", _bucket.Id, _request.Id);
            }
            catch (Exception ex)
            {
                _logger.Exception("Request callback threw exception for Bucket ID: {0} Request ID: {1}", _bucket.Id, _request.Id, ex);
            }
            finally
            {
                semaphore?.Release();
            }
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            _handler.Dispose();
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