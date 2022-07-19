using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Logging;
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
                _response = await RunInternal();
            }
            catch (Exception ex)
            {
                _response = await RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(RequestErrorType.Generic, DiscordLogLevel.Exception).WithException(ex), RequestCompletedStatus.ErrorFatal);
            }
            finally
            {
                Request.OnRequestCompleted(this, _response);
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
                await Request.Bucket.WaitUntilBucketAvailable(this, _token);
                await Request.WaitUntilRequestCanStart(_token);
                Request.Status = RequestStatus.InProgress;
                Request.Bucket.OnRequestStarted(this);
                
                if (Request.IsCancelled)
                {
                    return await RequestResponse.CreateCancelledResponse(Request.Client);
                }

                response = await RunRequest();
                
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
                using (HttpRequestMessage request = CreateRequest())
                {
                    using (HttpResponseMessage webResponse = await Request.HttpClient.SendAsync(request, _token))
                    {
                        if (webResponse.IsSuccessStatusCode)
                        {
                            return await RequestResponse.CreateSuccessResponse(Request.Client, webResponse);
                        }

                        RequestResponse response = await HandleWebException(request, webResponse);
                
                        return response;
                    }
                }
            }
            catch (TaskCanceledException)
            {
                return await RequestResponse.CreateCancelledResponse(Request.Client);
            }
            catch (JsonSerializationException ex)
            {
                Request.Client.Logger.Exception("A JsonSerializationException occured for request. ID: {0} Plugin: {1} Method: {2} URL: {3} Data Type: {4}", Request.Id, Request.Client.PluginName, Request.Method, Request.Route, Request.Data?.GetType().Name ?? "None", ex);
                return await RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(RequestErrorType.Serialization, DiscordLogLevel.Error).WithException(ex), RequestCompletedStatus.ErrorFatal);
            }
            catch (Exception ex)
            {
                Request.Client.Logger.Exception("An exception occured for request. ID: {0} Plugin: {1} Method: {2} URL: {3} Data Type: {4}", Request.Id, Request.Client.PluginName, Request.Method, Request.Route, Request.Data?.GetType().Name ?? "None", ex);
                return await RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(RequestErrorType.Generic, DiscordLogLevel.Error).WithException(ex), RequestCompletedStatus.ErrorFatal);
            }
        }

        private async Task<RequestResponse> HandleWebException(HttpRequestMessage request, HttpResponseMessage webResponse)
        {
            RequestResponse response;
            
            int statusCode = (int)webResponse.StatusCode;
            if (statusCode == 429)
            {
                response = await RequestResponse.CreateWebExceptionResponse(Request.Client, await GetRequestError(RequestErrorType.RateLimit, DiscordLogLevel.Warning).WithRequest(request), webResponse, RequestCompletedStatus.ErrorRetry);
            }
            else
            {
                response = await RequestResponse.CreateWebExceptionResponse(Request.Client, await GetRequestError(RequestErrorType.GenericWeb, DiscordLogLevel.Error).WithRequest(request), webResponse, RequestCompletedStatus.ErrorFatal);
            }
            
            Request.OnRequestErrored();

            if (Request.Client.Logger.IsLogging(DiscordLogLevel.Debug))
            {
                Request.Client.Logger.Debug("Web Exception Occured. Type: {0} Request ID: {1} Plugin: {2} Method: {3} Route: {4} HTTP Code: {5} Message: {6}", response.Error?.ErrorType, Request.Id, Request.Client.PluginName, Request.Method, Request.Route, response.Code, response.Error?.Message);
                Request.Client.Logger.Debug("Body:\n{0}", request.Content != null ? await request.Content.ReadAsStringAsync() : "No Content");
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
            
                    StringContent json = new StringContent(JsonConvert.SerializeObject(data, Request.Client.Bot.ClientSerializerSettings), Encoding.UTF8, "application/json");
                    content.Add(json, "payload_json");

                    for (int index = 0; index < attachments.FileAttachments.Count; index++)
                    {
                        MessageFileAttachment fileAttachment = attachments.FileAttachments[index];
                
                        ByteArrayContent file = new ByteArrayContent(fileAttachment.Data);
                        content.Add(file, $"files[{(index + 1).ToString()}]", fileAttachment.FileName);
                        file.Headers.ContentType = new MediaTypeHeaderValue(fileAttachment.ContentType);
                    }

                    request.Content = content;
                }
                else
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(data, Request.Client.Bot.ClientSerializerSettings), Encoding.UTF8, "application/json");
                }
            }

            return request;
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
            Request.Dispose();
            _response.Dispose();
            DiscordPool.Free(this);
        }
        
        /// <inheritdoc/>
        protected override void EnterPool()
        {
            _logger = null;
        }
    }
}