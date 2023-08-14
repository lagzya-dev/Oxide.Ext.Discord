using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Interfaces.Logging;
using Oxide.Ext.Discord.Json.Serialization;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Network;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Threading;
using Oxide.Plugins;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Represent a Discord API request
    /// </summary>
    public class RequestHandler : BasePoolable, IDebugLoggable
    {
        internal BaseRequest Request;
        
        private Bucket _bucket;
        private RestHandler _rest;
        private RequestResponse _response;
        private CancellationToken _token;
        private ILogger _logger;
        private readonly Func<Task> _runner;

        /// <summary>
        /// Constructor
        /// </summary>
        public RequestHandler()
        {
            _runner = StartRequest;
        }

        /// <summary>
        /// Creates a new <see cref="RequestHandler"/>
        /// </summary>
        /// <param name="rest">Rest handler for the request</param>
        /// <param name="request">Request to be handled by this handler</param>
        public static void StartRequest(RestHandler rest, BaseRequest request)
        {
            RequestHandler handler = request.PluginPool.Get<RequestHandler>();
            handler.Init(rest, request, request.Client.Logger);
        }
        
        private void Init(RestHandler rest, BaseRequest request, ILogger logger)
        {
            _rest = rest;
            Request = request;
            _token = Request.Source.Token;
            _logger = logger;
            Task.Run(_runner, _token);
        }

        private async Task StartRequest()
        {
            AdjustableSemaphore semaphore = null;
            try
            {
                _bucket = _rest.QueueBucket(this, Request);
            
                BucketId bucketId = _bucket.Id;
                semaphore = _bucket.Semaphore;
                Request.Status = RequestStatus.PendingBucket;
            
                _logger.Debug("Waiting for bucket availability Bucket ID: {0} Request ID: {1}", _bucket.Id, Request.Id);
                
                await semaphore.WaitOneAsync().ConfigureAwait(false);
                if (bucketId != _bucket.Id)
                {
                    _logger.Debug("Bucket ID Changed. Waiting for bucket availability again for ID: {0} Old Bucket ID: {1} New Bucket ID: {2}", Request.Id, bucketId, _bucket.Id);
                    semaphore.Release();
                    semaphore = _bucket.Semaphore;
                    await semaphore.WaitOneAsync().ConfigureAwait(false);
                }

                _logger.Debug("Request callback started for Bucket ID: {0} Request ID: {1}", _bucket.Id, Request.Id);
                await FireRequest().ConfigureAwait(false);
                _logger.Debug("Request callback completed successfully for Bucket ID: {0} Request ID: {1}", _bucket.Id, Request.Id);
            }
            catch (Exception ex)
            {
                _logger.Exception("Request callback threw exception for Bucket ID: {0} Request ID: {1}", Request.Id, Request.Id, ex);
            }
            finally
            {
                semaphore?.Release();
            }
            
            Dispose();
        }
        
        /// <summary>
        /// Fires the request off
        /// </summary>
        private async Task FireRequest()
        {
            try
            {
                _response = await FireRequestInternal().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _response = await RequestResponse.CreateExceptionResponse(Request.Client, GetResponseError(RequestErrorType.Generic, DiscordLogLevel.Exception).WithException(ex), null, RequestCompletedStatus.ErrorFatal).ConfigureAwait(false);
            }
            finally
            {
                Request.OnRequestCompleted(this, _response);
            }
        }
        
        private async Task<RequestResponse> FireRequestInternal()
        {
            _logger.Verbose($"{nameof(RequestHandler)}.{nameof(FireRequestInternal)} Starting REST Request. Request ID: {{0}} Method: {{1}} Url: {{2}}", Request.Id, Request.Method, Request.Route);

            RequestResponse response = null;
            byte retries = 0;
            while(CanSendRequest(response, retries)) 
            {
                Request.Status = RequestStatus.PendingStart;
                await _bucket.WaitUntilBucketAvailable(this, _token).ConfigureAwait(false);
                await Request.WaitUntilRequestCanStart(_token).ConfigureAwait(false);
                Request.Status = RequestStatus.InProgress;
                _bucket.OnRequestStarted(this);
                
                if (Request.IsCancelled)
                {
                    _logger.Verbose($"{nameof(RequestHandler)}.{nameof(FireRequestInternal)} Cancel REST Request. Request ID: {{0}} Method: {{1}} Url: {{2}}", Request.Id, Request.Method, Request.Route);
                    return await RequestResponse.CreateCancelledResponse(Request.Client).ConfigureAwait(false);
                }

                response = await SendRequest().ConfigureAwait(false);
                
                _bucket.UpdateRateLimits(this, response);
                
                switch (response.Status)
                {
                    case RequestCompletedStatus.Success:
                    case RequestCompletedStatus.Cancelled:
                    case RequestCompletedStatus.ErrorFatal:
                        return response;
                }

                if (response.Code == DiscordHttpStatusCode.BadRequest)
                {
                    return response;
                }
                
                retries++;
            }
            
            return response;
        }

        private bool CanSendRequest(RequestResponse response, byte retries)
        {
            if (retries >= 6)
            {
                return false;
            }
            
            if (response != null && response.Code != DiscordHttpStatusCode.TooManyRequests)
            {
                return retries < 3;
            }
            
            return true;
        }

        private async Task<RequestResponse> SendRequest()
        {
            try
            {
                using (HttpRequestMessage request = CreateRequest())
                {
                    using (HttpResponseMessage webResponse = await Request.HttpClient.SendAsync(request, _token).ConfigureAwait(false))
                    {
                        if (webResponse.IsSuccessStatusCode)
                        {
                            return await RequestResponse.CreateSuccessResponse(Request.Client, webResponse).ConfigureAwait(false);
                        }

                        return await HandleWebException(request, webResponse).ConfigureAwait(false);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                return await RequestResponse.CreateCancelledResponse(Request.Client).ConfigureAwait(false);
            }
            catch (JsonSerializationException ex)
            {
                return await RequestResponse.CreateExceptionResponse(Request.Client, GetResponseError(RequestErrorType.Serialization, DiscordLogLevel.Error).WithException(ex), null, RequestCompletedStatus.ErrorFatal).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return await RequestResponse.CreateExceptionResponse(Request.Client, GetResponseError(RequestErrorType.Generic, DiscordLogLevel.Error).WithException(ex), null, RequestCompletedStatus.ErrorFatal).ConfigureAwait(false);
            }
        }

        private async Task<RequestResponse> HandleWebException(HttpRequestMessage request, HttpResponseMessage webResponse)
        {
            RequestResponse response;
            
            DiscordHttpStatusCode statusCode = (DiscordHttpStatusCode)webResponse.StatusCode;
            if (statusCode == DiscordHttpStatusCode.TooManyRequests)
            {
                response = await RequestResponse.CreateExceptionResponse(Request.Client, await GetResponseError(RequestErrorType.RateLimit, DiscordLogLevel.Warning).WithRequest(request).ConfigureAwait(false), webResponse, RequestCompletedStatus.ErrorRetry).ConfigureAwait(false);
            }
            else
            {
                response = await RequestResponse.CreateExceptionResponse(Request.Client, await GetResponseError(RequestErrorType.GenericWeb, DiscordLogLevel.Error).WithRequest(request).ConfigureAwait(false), webResponse, RequestCompletedStatus.ErrorFatal).ConfigureAwait(false);
            }
            
            Request.OnRequestErrored();

            if (Request.Client.Logger.IsLogging(DiscordLogLevel.Debug))
            {
                Request.Client.Logger.Debug("Web Exception Occured. Type: {0} Request ID: {1} Plugin: {2} Method: {3} Route: {4} HTTP Code: {5} Message: {6}", response.Error?.ErrorType, Request.Id, Request.Client.PluginName, Request.Method, Request.Route, response.Code, response.Error?.ResponseMessage);
                Request.Client.Logger.Debug("Body:\n{0}", request.Content != null ? await request.Content.ReadAsStringAsync().ConfigureAwait(false) : "No Content");
            }

            return response;
        }

        private HttpRequestMessage CreateRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage(DiscordHttpMethods.GetMethod(Request.Method), Request.Route);
            CreateContent(request);
            return request;
        }
        
        private void CreateContent(HttpRequestMessage request)
        {
            object data = Request.Data;
            if (data == null)
            {
                return;
            }
            
            if (data is IFileAttachments attachments && attachments.FileAttachments != null && attachments.FileAttachments.Count != 0)
            {
                MultipartFormDataContent content = new MultipartFormDataContent();

                HttpContent json = GetJsonContent(data);
                content.Add(json, "payload_json");

                for (int index = 0; index < attachments.FileAttachments.Count; index++)
                {
                    MessageFileAttachment fileAttachment = attachments.FileAttachments[index];
                    ByteArrayContent file = new ByteArrayContent(fileAttachment.Data);
                    content.Add(file, FileAttachmentCache.Instance.GetName(index), fileAttachment.FileName);
                    file.Headers.ContentType = MediaTypeHeaderCache.Instance.Get(fileAttachment.ContentType);
                }

                request.Content = content;
            }
            else
            {
                request.Content = GetJsonContent(data);
            }
        }

        private StringContent GetJsonContent(object data)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(data, Request.Client.Bot.JsonSettings));
            content.Headers.ContentType = MediaTypeHeaderCache.Instance.Get("application/json");
            return content;
        }

        /// <summary>
        /// Aborts a currently running request
        /// </summary>
        public void Abort()
        {
            Request.Client.Logger.Debug($"{nameof(RequestHandler)}.{nameof(Abort)} Abort Request Bucket ID: {{0}} Request ID: {{1}} Plugin: {{2}} Method: {{3}} Route: {{4}}", _bucket.Id, Request.Id, Request.Client.PluginName, Request.Method, Request.Route);
            Request.Abort();
        }

        private ResponseError GetResponseError(RequestErrorType type, DiscordLogLevel log) => new ResponseError(Request, type, log);

        /// <inheritdoc/>
        protected override void EnterPool()
        {
            Request.Dispose();
            _response.Dispose();
            _bucket = null;
            _rest = null;
            Request = null;
            _response = null;
            _logger = null;
        }

        ///<inheritdoc/>
        public void LogDebug(DebugLogger logger)
        {
            Request.LogDebug(logger);
        }
    }
}