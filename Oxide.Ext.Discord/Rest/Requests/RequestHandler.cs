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
using Oxide.Ext.Discord.Json.Serialization;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Network;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Represent a Discord API request
    /// </summary>
    public class RequestHandler : BasePoolable
    {
        internal BaseRequest Request;

        private DiscordJsonWriter _json;
        private RequestResponse _response;
        private CancellationToken _token;
        private ILogger _logger;

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
        /// <param name="request">Request to be handled by this handler</param>
        public static RequestHandler CreateRequestHandler(BaseRequest request)
        {
            RequestHandler handler = DiscordPool.Get<RequestHandler>();
            handler.Init(request, request.Client.Logger);
            return handler;
        }
        
        /// <summary>
        /// Initializes a new request
        /// </summary>
        /// <param name="request">Request to be handled by this handler</param>
        /// <param name="logger">Logger for the request</param>
        private void Init(BaseRequest request, ILogger logger)
        {
            Request = request;
            _token = Request.Source.Token;
            _logger = logger;
        }

        /// <summary>
        /// Fires the request off
        /// </summary>
        public async Task Run()
        {
            try
            {
                _response = await RunInternal().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _response = await RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(RequestErrorType.Generic, DiscordLogLevel.Exception).WithException(ex), null, RequestCompletedStatus.ErrorFatal).ConfigureAwait(false);
            }
            finally
            {
                await Request.OnRequestCompleted(this, _response).ConfigureAwait(false);
            }
        }
        
        private async Task<RequestResponse> RunInternal()
        {
            _logger.Verbose($"{nameof(RequestHandler)}.{nameof(RunInternal)} Starting REST Request. Request ID: {{0}} Method: {{1}} Url: {{2}} Contents:\n{{3}}", Request.Id, Request.Method, Request.Route, /*_data.StringContents ??*/ "No Contents");

            RequestResponse response = null;
            byte retries = 0;
            byte retries429 = 0;
            while(retries < 3 && retries429 < 6) 
            {
                Request.Status = RequestStatus.PendingStart;
                await Request.Bucket.WaitUntilBucketAvailable(this, _token).ConfigureAwait(false);
                await Request.WaitUntilRequestCanStart(_token).ConfigureAwait(false);
                Request.Status = RequestStatus.InProgress;
                Request.Bucket.OnRequestStarted(this);
                
                if (Request.IsCancelled)
                {
                    return await RequestResponse.CreateCancelledResponse(Request.Client).ConfigureAwait(false);
                }

                response = await RunRequest().ConfigureAwait(false);
                
                Request.Bucket.UpdateRateLimits(this, response);
                
                switch (response.Status)
                {
                    case RequestCompletedStatus.Success:
                    case RequestCompletedStatus.Cancelled:
                    case RequestCompletedStatus.ErrorFatal:
                        return response;
                }

                if (response.Code != 429)
                {
                    retries++;
                }
                else
                {
                    retries429++;
                }
            }
            
            return response;
        }

        private async Task<RequestResponse> RunRequest()
        {
            try
            {
                using (HttpRequestMessage request = await CreateRequest().ConfigureAwait(false))
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
            
            int statusCode = (int)webResponse.StatusCode;
            if (statusCode == 429)
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

        private async Task<HttpRequestMessage> CreateRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethods[Request.Method], Request.Route);
            object data = Request.Data;
            if (data != null)
            {
                if (data is IFileAttachments attachments && attachments.FileAttachments != null && attachments.FileAttachments.Count != 0)
                {
                    MultipartFormDataContent content = new MultipartFormDataContent();
                    
                    DiscordStreamContent json = await GetJsonContent(data).ConfigureAwait(false);
                    content.Add(json, "payload_json");

                    for (int index = 0; index < attachments.FileAttachments.Count; index++)
                    {
                        MessageFileAttachment fileAttachment = attachments.FileAttachments[index];
                
                        ByteArrayContent file = new ByteArrayContent(fileAttachment.Data);
                        content.Add(file, $"files[{(index + 1).ToString()}]", fileAttachment.FileName);
                        file.Headers.ContentType = MediaTypeHeaderCache.Get(fileAttachment.ContentType);
                    }

                    request.Content = content;
                }
                else
                {
                    request.Content = await GetJsonContent(data).ConfigureAwait(false);
                }
            }

            return request;
        }

        private async Task<DiscordStreamContent> GetJsonContent(object data)
        {
            _json = DiscordPool.Get<DiscordJsonWriter>();
            await _json.WriteAsync(Request.Client.Bot.JsonSerializer, data).ConfigureAwait(false);
            
            if (Request.Client.Logger.IsLogging(DiscordLogLevel.Verbose))
            {
                _logger.Verbose($"{nameof(RequestHandler)}.{nameof(GetJsonContent)} Creating JSON Body: {{0}}", await _json.ReadAsStringAsync().ConfigureAwait(false));
            }

            DiscordStreamContent content = new DiscordStreamContent(_json.Stream);
            content.Headers.ContentType = MediaTypeHeaderCache.Get("application/json");
            return content;
        }

        /// <summary>
        /// Aborts a currently running request
        /// </summary>
        public void Abort()
        {
            Request.Source?.Cancel();
        }

        private RequestError GetRequestError(RequestErrorType type, DiscordLogLevel log)
        {
            return new RequestError(Request, type, log);
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            _json?.Dispose();
            Request.Dispose();
            _response.Dispose();
            DiscordPool.Free(this);
        }
        
        /// <inheritdoc/>
        protected override void EnterPool()
        {
            _json = null;
            Request = null;
            _response = null;
            _logger = null;
        }
    }
}