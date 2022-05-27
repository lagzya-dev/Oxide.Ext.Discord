using System;
using System.Net;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests.Data;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Represent a Discord API request
    /// </summary>
    public class RequestHandler : BasePoolable
    {
        private const int TimeoutDuration = 15;
        
        /// <summary>
        /// Returns if the request is currently in progress
        /// </summary>
        public bool InProgress { get; private set; }

        internal BaseRequest Request;
        
        /// <summary>
        /// Full Request URl to the API
        /// </summary>
        private string _requestUrl;
        private byte _retries;
        private RequestData _data;
        private HttpWebRequest _webRequest;
        private ILogger _logger;

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
            _requestUrl = request.RequestUrl;
            _logger = logger;
        }

        /// <summary>
        /// Fires the request off
        /// </summary>
        public void Run()
        {
            while (true)
            {
                while (!Request.CanStartRequest())
                { 
                    //_logger.Debug($"Can't Start Request: Waiting For: {(Request.GetRequestResetAt() - DateTimeOffset.UtcNow).TotalSeconds} Seconds");
                   ThreadExt.SleepUntil(Request.GetResetAt());
                }
                
                Request.Bucket.FireRequest();

                RequestResponse response = RunRequest();

                if (response.Status == RequestCompletedStatus.Success)
                {
                    Request.OnRequestCompleted(this, response);
                    break;
                }

                if(response.Status == RequestCompletedStatus.ErrorFatal || ++_retries > 3)
                {
                    Request.OnRequestCompleted(this, response);
                    break;
                }
                
                response.Dispose();
                
                if (response.Status == RequestCompletedStatus.Cancelled)
                {
                    break;
                }
            }
        }

        private RequestResponse RunRequest()
        {
            try
            {
                if (_data == null)
                {
                    _data = RequestData.CreateRequestData(Request);
                }
                
                //Can error during JSON serialization
                _webRequest = CreateRequest();

                //Can timeout while writing request data
                _data.WriteRequestData(_webRequest);
                using (HttpWebResponse webResponse = _webRequest.GetResponse() as HttpWebResponse)
                {
                    return RequestResponse.CreateSuccessResponse(Request.Client, webResponse);
                }
            }
            catch (WebException ex)
            {
                return HandleWebException(ex);
            }
            catch (JsonSerializationException ex)
            {
                Request.Client.Logger.Exception("A JsonSerializationException occured for request. Plugin: {0} Method: {1} URL: {2} Data Type: {3}", Request.Client.PluginName, Request.Method, _requestUrl, Request.Data?.GetType().Name ?? "None", ex);
                return RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex), RequestCompletedStatus.ErrorFatal);
            }
            catch (Exception ex)
            {
                Request.Client.Logger.Exception("An exception occured for request. Plugin: {0} Method: {1} URL: {2} Data Type: {3}", Request.Client.PluginName, Request.Method, _requestUrl, Request.Data?.GetType().Name ?? "None", ex);
                return RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex), RequestCompletedStatus.ErrorFatal);
            }
        }
        
        private RequestResponse HandleWebException(WebException ex)
        {
            if (ex.Status == WebExceptionStatus.RequestCanceled)
            {
                Request.Client.Logger.Debug("Client request cancelled. Plugin: {0}", Request.Client.PluginName);
                return RequestResponse.CreateCancelledResponse(Request.Client);
            }
            
            Request.OnRequestErrored();

            using (HttpWebResponse httpResponse = ex.Response as HttpWebResponse)
            {
                if (httpResponse == null)
                {
                    return RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.Internal, DiscordLogLevel.Exception), RequestCompletedStatus.ErrorRetry);
                }

                int statusCode = (int)httpResponse.StatusCode;
                if (statusCode == 429)
                {
                    return RequestResponse.CreateWebExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.RateLimit, DiscordLogLevel.Warning), httpResponse, RequestCompletedStatus.ErrorRetry);
                }

                return RequestResponse.CreateWebExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.GenericWeb, DiscordLogLevel.Error), httpResponse, RequestCompletedStatus.ErrorFatal);
            }
        }

        private HttpWebRequest CreateRequest()
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(_requestUrl);
            req.Method = Request.Method.ToString();
            req.UserAgent = $"DiscordBot (https://github.com/Kirollos/Oxide.Ext.Discord, {DiscordExtension.FullExtensionVersion})";
            req.Timeout = TimeoutDuration * 1000;
            req.ContentLength = 0;
            req.Headers.Set("Authorization", Request.AuthHeader);
            req.ContentType = _data.ContentType;
            return req;
        }

        /// <summary>
        /// Aborts a currently running request
        /// </summary>
        public void Abort()
        {
            _webRequest.Abort();
        }

        private RequestError GetRequestError(Exception ex)
        {
            return new RequestError(Request.Client, Request.Bucket, ex, _requestUrl, Request.Method, _webRequest.ContentType, Request.Data, _data?.Contents);
        }
        
        private RequestError GetRequestError(Exception ex, RequestErrorType type, DiscordLogLevel logLevel)
        {
            RequestError error = GetRequestError(ex);
            error.SetErrorMessage(type, logLevel);
            return error;
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        /// <inheritdoc/>
        protected override void EnterPool()
        {
            _data?.Dispose();
            _data = null;
            _requestUrl = null;
            InProgress = false;
            _retries = 0;
            _logger = null;
        }
    }
}