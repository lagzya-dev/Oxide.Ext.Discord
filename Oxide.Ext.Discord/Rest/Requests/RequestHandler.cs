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
        /// Initializes a new request
        /// </summary>
        /// <param name="request">Request to be handled by this handler</param>
        public void Init(BaseRequest request, ILogger logger)
        {
            Request = request;
            _requestUrl = request.RequestUrl;
            _logger = logger;
            _logger.Debug($"RequestHandler.Init Request URL: {_requestUrl}");
        }

        /// <summary>
        /// Fires the request off
        /// </summary>
        public void Run()
        {
            while (true)
            {
                DateTimeOffset reset = Request.GetRequestResetAt();
                while (reset > DateTimeOffset.UtcNow)
                { 
                    //_logger.Debug($"Can't Start Request: Waiting For: {(Request.GetRequestResetAt() - DateTimeOffset.UtcNow).TotalSeconds} Seconds");
                   ThreadExt.SleepUntil(reset);
                   reset = Request.GetRequestResetAt();
                }

                RestResponse response = RunRequest();

                if (response.Status == RequestStatus.Success)
                {
                    Request.OnRequestCompleted(this, response);
                    break;
                }

                if(response.Status == RequestStatus.ErrorFatal || ++_retries > 3)
                {
                    Request.OnRequestCompleted(this, response);
                    break;
                }
                
                response.Dispose();
                
                if (response.Status == RequestStatus.Cancelled)
                {
                    break;
                }
            }
        }

        private RestResponse RunRequest()
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
                    return RestResponse.CreateSuccessResponse(Request.Client, webResponse);
                }
            }
            catch (WebException ex)
            {
                return HandleWebException(ex);
            }
            catch (JsonSerializationException ex)
            {
                Request.Client.Logger.Exception("A JsonSerializationException occured for request. Plugin: {0} Method: {1} URL: {2} Data Type: {3}", Request.Client.PluginName, Request.Method, _requestUrl, Request.Data?.GetType().Name ?? "None", ex);
                return RestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex), RequestStatus.ErrorFatal);
            }
            catch (Exception ex)
            {
                Request.Client.Logger.Exception("An exception occured for request. Plugin: {0} Method: {1} URL: {2} Data Type: {3}", Request.Client.PluginName, Request.Method, _requestUrl, Request.Data?.GetType().Name ?? "None", ex);
                return RestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex), RequestStatus.ErrorFatal);
            }
        }
        
        private RestResponse HandleWebException(WebException ex)
        {
            if (ex.Status == WebExceptionStatus.RequestCanceled)
            {
                Request.Client.Logger.Debug("Client request cancelled. Plugin: {0}", Request.Client.PluginName);
                return RestResponse.CreateCancelledResponse(Request.Client);
            }
            
            Request.OnRequestErrored();

            using (HttpWebResponse httpResponse = ex.Response as HttpWebResponse)
            {
                if (httpResponse == null)
                {
                    return RestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.Internal, DiscordLogLevel.Exception), RequestStatus.ErrorRetry);
                }

                int statusCode = (int)httpResponse.StatusCode;
                if (statusCode == 429)
                {
                    return RestResponse.CreateWebExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.RateLimit, DiscordLogLevel.Warning), httpResponse, RequestStatus.ErrorRetry);
                }

                return RestResponse.CreateWebExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.GenericWeb, DiscordLogLevel.Error), httpResponse, RequestStatus.ErrorFatal);
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

        private RestError GetRequestError(Exception ex)
        {
            return new RestError(Request.Client, Request.Bucket, ex, _requestUrl, Request.Method, _webRequest.ContentType, Request.Data, _data?.Contents);
        }
        
        private RestError GetRequestError(Exception ex, RequestErrorType type, DiscordLogLevel logLevel)
        {
            RestError error = GetRequestError(ex);
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