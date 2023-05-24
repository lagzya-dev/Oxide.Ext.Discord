using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Cache;
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
        internal Request Request;
        
        private Bucket _bucket;
        private RestHandler _rest;
        private DiscordJsonWriter _json;
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
        
        private static readonly Hash<RequestMethod, HttpMethod> HttpMethods = new Hash<RequestMethod, HttpMethod>
        {
            [RequestMethod.GET] = HttpMethod.Get,
            [RequestMethod.PUT] = HttpMethod.Put,
            [RequestMethod.POST] = HttpMethod.Post,
            [RequestMethod.PATCH] = new HttpMethod("PATCH"),
            [RequestMethod.DELETE] = HttpMethod.Delete,
        };

        /// <summary>
        /// Creates a new <see cref="RequestHandler"/>
        /// </summary>
        /// <param name="rest">Rest handler for the request</param>
        /// <param name="request">Request to be handled by this handler</param>
        public static void StartRequest(RestHandler rest, Request request)
        {
            RequestHandler handler = request.PluginPool.Get<RequestHandler>();
            handler.Init(rest, request, request.Client.Logger);
        }
        
        private void Init(RestHandler rest, Request request, ILogger logger)
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
                //_handler = RequestHandler.CreateRequestHandler(_request);
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
                _response = await RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(RequestErrorType.Generic, DiscordLogLevel.Exception).WithException(ex), null, RequestCompletedStatus.ErrorFatal).ConfigureAwait(false);
            }
            finally
            {
                Request.OnRequestCompleted(this, _response);
            }
        }
        
        private async Task<RequestResponse> FireRequestInternal()
        {
            _logger.Verbose($"{nameof(RequestHandler)}.{nameof(FireRequestInternal)} Starting REST Request. Request ID: {{0}} Method: {{1}} Url: {{2}} Contents:\n{{3}}", Request.Id, Request.Method, Request.Route, /*_data.StringContents ??*/ "No Contents");

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
            catch (TaskCanceledException)
            {
                return await RequestResponse.CreateCancelledResponse(Request.Client).ConfigureAwait(false);
            }
            catch (JsonSerializationException ex)
            {
                return await RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(RequestErrorType.Serialization, DiscordLogLevel.Error).WithException(ex), null, RequestCompletedStatus.ErrorFatal).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return await RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(RequestErrorType.Generic, DiscordLogLevel.Error).WithException(ex), null, RequestCompletedStatus.ErrorFatal).ConfigureAwait(false);
            }
        }

        private async Task<RequestResponse> HandleWebException(HttpRequestMessage request, HttpResponseMessage webResponse)
        {
            RequestResponse response;
            
            DiscordHttpStatusCode statusCode = (DiscordHttpStatusCode)webResponse.StatusCode;
            if (statusCode == DiscordHttpStatusCode.TooManyRequests)
            {
                response = await RequestResponse.CreateExceptionResponse(Request.Client, await GetRequestError(RequestErrorType.RateLimit, DiscordLogLevel.Warning).WithRequest(request).ConfigureAwait(false), webResponse, RequestCompletedStatus.ErrorRetry).ConfigureAwait(false);
            }
            else
            {
                response = await RequestResponse.CreateExceptionResponse(Request.Client, await GetRequestError(RequestErrorType.GenericWeb, DiscordLogLevel.Error).WithRequest(request).ConfigureAwait(false), webResponse, RequestCompletedStatus.ErrorFatal).ConfigureAwait(false);
            }
            
            Request.OnRequestErrored();

            if (Request.Client.Logger.IsLogging(DiscordLogLevel.Debug))
            {
                Request.Client.Logger.Debug("Web Exception Occured. Type: {0} Request ID: {1} Plugin: {2} Method: {3} Route: {4} HTTP Code: {5} Message: {6}", response.Error?.ErrorType, Request.Id, Request.Client.PluginName, Request.Method, Request.Route, response.Code, response.Error?.Message);
                Request.Client.Logger.Debug("Body:\n{0}", request.Content != null ? await request.Content.ReadAsStringAsync().ConfigureAwait(false) : "No Content");
            }

            return response;
        }

        private HttpRequestMessage CreateRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethods[Request.Method], Request.Route);
            object data = Request.Data;
            if (data != null)
            {
                if (data is IFileAttachments attachments && attachments.FileAttachments != null && attachments.FileAttachments.Count != 0)
                {
                    MultipartFormDataContent content = new MultipartFormDataContent();
                    
                    DiscordStreamContent json = GetJsonContent(data);
                    content.Add(json, "payload_json");

                    for (int index = 0; index < attachments.FileAttachments.Count; index++)
                    {
                        MessageFileAttachment fileAttachment = attachments.FileAttachments[index];
                
                        ByteArrayContent file = new ByteArrayContent(fileAttachment.Data);
                        content.Add(file, $"files[{(index + 1).ToString()}]", fileAttachment.FileName);
                        file.Headers.ContentType = MediaTypeHeaderCache.Instance.Get(fileAttachment.ContentType);
                    }

                    request.Content = content;
                }
                else
                {
                    request.Content = GetJsonContent(data);
                }
            }

            return request;
        }

        private DiscordStreamContent GetJsonContent(object data)
        {
            _json = DiscordJsonWriter.Get(PluginPool);
            _json.Write(Request.Client.Bot.JsonSerializer, data);
            
            if (Request.Client.Logger.IsLogging(DiscordLogLevel.Verbose))
            {
                _logger.Verbose($"{nameof(RequestHandler)}.{nameof(GetJsonContent)} Creating JSON Body: {{0}}", _json.ReadAsString());
            }

            DiscordStreamContent content = new DiscordStreamContent(_json.Stream);
            content.Headers.ContentType = MediaTypeHeaderCache.Instance.Get("application/json");
            return content;
        }

        /// <summary>
        /// Aborts a currently running request
        /// </summary>
        public void Abort()
        {
            Request.Source?.Cancel();
        }

        private ResponseError GetRequestError(RequestErrorType type, DiscordLogLevel log)
        {
            return new ResponseError(Request, type, log);
        }

        /// <inheritdoc/>
        protected override void EnterPool()
        {
            _json?.Dispose();
            Request.Dispose();
            _response.Dispose();
            _bucket = null;
            _rest = null;
            _json = null;
            Request = null;
            _response = null;
            _logger = null;
        }

        public void LogDebug(DebugLogger logger)
        {
            Request.LogDebug(logger);
        }
    }
}