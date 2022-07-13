using System;
using System.Net;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities.Api;
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

        internal BaseRequest Request;
        
        private BaseRequestData _data;
        private RequestResponse _response;
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
            _logger = logger;
        }

        /// <summary>
        /// Fires the request off
        /// </summary>
        public void Run()
        {
            try
            {
                _response = RunInternal();
                Request.OnRequestCompleted(this, _response); 
            }
            catch (Exception ex)
            {
                _response = RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.Generic, DiscordLogLevel.Exception), RequestCompletedStatus.ErrorFatal);
                Request.OnRequestCompleted(this, _response); 
            }
        }
        
        private RequestResponse RunInternal()
        {
            Request.Status = RequestStatus.PendingCreateData;
            if (!CreateRequestData())
            {
                return _response;
            }
            
            _logger.Verbose($"{nameof(RequestHandler)}.{nameof(Run)} Starting REST Request. Request ID: {{0}} Method: {{1}} Url: {{2}} Contents:\n{{3}}", Request.Id, Request.Method, Request.Route, _data.StringContents ?? "No Contents");

            RequestResponse response = null;
            byte retries = 0;
            while(retries < 3) 
            {
                Request.Status = RequestStatus.PendingStart;
                Request.Bucket.WaitUntilBucketAvailable(this);
                Request.WaitUntilRequestCanStart();
                Request.Status = RequestStatus.InProgress;
                
                if (Request.IsCancelled)
                {
                    return RequestResponse.CreateCancelledResponse(Request.Client);
                }

                response = RunRequest();
                
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
            }
            
            return response;
        }

        private RequestResponse RunRequest()
        {
            try
            {
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
                RequestResponse response = HandleWebException(ex);
                Request.Client.Logger.Debug("Web Exception Occured. Type: {0} Request ID: {1} Plugin: {2} Method: {3} Route: {4} HTTP Code: {5} Message: {6} Exception: {7}", response.Error?.ErrorType, Request.Id, Request.Client.PluginName, Request.Method, Request.Route, response.Code, response.Error?.Message, ex);
                return response;
            }
            catch (JsonSerializationException ex)
            {
                Request.Client.Logger.Exception("A JsonSerializationException occured for request. ID: {0} Plugin: {1} Method: {2} URL: {3} Data Type: {4}", Request.Id, Request.Client.PluginName, Request.Method, Request.Route, Request.Data?.GetType().Name ?? "None", ex);
                return RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex), RequestCompletedStatus.ErrorFatal);
            }
            catch (Exception ex)
            {
                Request.Client.Logger.Exception("An exception occured for request. ID: {0} Plugin: {1} Method: {2} URL: {3} Data Type: {4}", Request.Id, Request.Client.PluginName, Request.Method, Request.Route, Request.Data?.GetType().Name ?? "None", ex);
                return RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex), RequestCompletedStatus.ErrorFatal);
            }
        }

        private RequestResponse HandleWebException(WebException ex)
        {
            if (ex.Status == WebExceptionStatus.RequestCanceled)
            {
                Request.Client.Logger.Debug("Client request cancelled. ID: {0} Plugin: {1}", Request.Id, Request.Client.PluginName);
                return RequestResponse.CreateCancelledResponse(Request.Client);
            }
            
            using (HttpWebResponse httpResponse = ex.Response as HttpWebResponse)
            {
                if (httpResponse == null)
                {
                    Request.OnRequestErrored();
                    return RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.Internal, DiscordLogLevel.Exception), RequestCompletedStatus.ErrorRetry);
                }

                int statusCode = (int)httpResponse.StatusCode;
                if (statusCode == 429)
                {
                    return RequestResponse.CreateWebExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.RateLimit, DiscordLogLevel.Warning), httpResponse, RequestCompletedStatus.ErrorRetry);
                }

                Request.OnRequestErrored();
                return RequestResponse.CreateWebExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.GenericWeb, DiscordLogLevel.Error), httpResponse, RequestCompletedStatus.ErrorFatal);
            }
        }

        private HttpWebRequest CreateRequest()
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(Request.RequestUrl);
            req.Method = EnumCache<RequestMethod>.ToString(Request.Method);
            req.UserAgent = $"DiscordBot (https://github.com/Kirollos/Oxide.Ext.Discord, {DiscordExtension.FullExtensionVersion})";
            req.Timeout = TimeoutDuration * 1000;
            req.ContentLength = 0;
            req.Headers.Set("Authorization", Request.AuthHeader);
            req.ContentType = _data.ContentType;
            return req;
        }
        
        private bool CreateRequestData()
        {
            try
            {
                _data = BaseRequestData.CreateRequestData(Request);
                return true;
            }
            catch (JsonSerializationException ex)
            {
                Request.Client.Logger.Exception("A JsonSerializationException occured for request. Request ID: {0} Plugin: {1} Method: {2} URL: {3} Data Type: {4}", Request.Id, Request.Client.PluginName, Request.Method, Request.Route, Request.Data?.GetType().Name ?? "None", ex);
                _response = RequestResponse.CreateExceptionResponse(Request.Client, GetRequestError(ex, RequestErrorType.Serialization, DiscordLogLevel.Error), RequestCompletedStatus.ErrorFatal);
                return false;
            }
        }

        /// <summary>
        /// Aborts a currently running request
        /// </summary>
        public void Abort()
        {
            Request.IsCancelled = true;
            _webRequest?.Abort();
        }

        private RequestError GetRequestError(Exception ex)
        {
            return new RequestError(Request, _data, ex);
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
            Request.Dispose();
            _data.Dispose();
            _response.Dispose();
            DiscordPool.Free(this);
        }
        
        /// <inheritdoc/>
        protected override void EnterPool()
        {
            _data = null;
            _logger = null;
        }
    }
}