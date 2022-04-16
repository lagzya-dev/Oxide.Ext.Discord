using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Ext.Discord.Callbacks.Rest;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Multipart;
using RequestMethod = Oxide.Ext.Discord.Entities.Api.RequestMethod;
using Time = Oxide.Ext.Discord.Helpers.Time;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Represent a Discord API request
    /// </summary>
    public class Request : BasePoolable
    {
        /// <summary>
        /// HTTP request method
        /// </summary>
        public RequestMethod Method;

        /// <summary>
        /// Route on the API
        /// </summary>
        public string Route;

        /// <summary>
        /// Full Request URl to the API
        /// </summary>
        public string RequestUrl;

        /// <summary>
        /// Data to be sent with the request
        /// </summary>
        public object Data;
        
        /// <summary>
        /// Data serialized to bytes 
        /// </summary>
        public byte[] Contents { get; private set; }

        /// <summary>
        /// Required If Multipart Form Request
        /// </summary>
        public bool MultipartRequest;

        /// <summary>
        /// Multipart Boundary
        /// </summary>
        public string Boundary;
        
        /// <summary>
        /// Callback to call if the request completed successfully
        /// </summary>
        private Action _onSuccess;

        /// <summary>
        /// Callback to call if the request errored with the last error message
        /// </summary>
        private Action<RestError> _onError;

        /// <summary>
        /// The DateTime the request was started
        /// Used for request timeout
        /// </summary>
        public DateTime? StartTime { get; private set; }

        /// <summary>
        /// Returns if the request is currently in progress
        /// </summary>
        public bool InProgress { get; private set; }
        
        /// <summary>
        /// Discord Client making the request
        /// </summary>
        internal DiscordClient Client;

        internal Bucket Bucket;
        
        private const int TimeoutDuration = 15;
        
        /// <summary>
        /// Attachments for a request
        /// </summary>
        private List<IMultipartSection> _multipartSections;
        
        /// <summary>
        /// Response from the request
        /// </summary>
        internal RestResponse Response;

        private string _authHeader;
        private byte _retries;
        private HttpWebRequest _request;
        
        private RestError _lastError;
        private bool _success;

        /// <summary>
        /// Initializes a new request
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="method">HTTP method to call</param>
        /// <param name="route">Route to call on the API</param>
        /// <param name="data">Data for the request</param>
        /// <param name="authHeader">Authorization Header</param>
        /// <param name="onSuccess">Callback once the request completes successfully</param>
        /// <param name="onError">Callback when the request errors</param>
        public void Init(DiscordClient client, RequestMethod method, string route, object data, string authHeader, Action onSuccess, Action<RestError> onError)
        {
            Init(client, method, route, data,authHeader, onError);
            _onSuccess = onSuccess;
        }

        /// <summary>
        /// Creates a new request
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="method">HTTP method to call</param>
        /// <param name="route">Route to call on the API</param>
        /// <param name="data">Data for the request</param>
        /// <param name="authHeader">Authorization Header</param>
        /// <param name="onError">Callback when the request errors</param>
        protected void Init(DiscordClient client, RequestMethod method, string route, object data, string authHeader, Action<RestError> onError)
        {
            Client = client;
            Method = method;
            Route = route;
            RequestUrl = DiscordEndpoints.Rest.ApiUrl + route;
            Data = data;
            _authHeader = authHeader;
            _onError = onError;
            MultipartRequest = Data is IFileAttachments attachments && attachments.FileAttachments != null && attachments.FileAttachments.Count != 0;
        }

        /// <summary>
        /// Fires the request off
        /// </summary>
        public void Fire()
        {
            InProgress = true;
            StartTime = DateTime.UtcNow;
            
            try
            {
                //Can error during JSON serialization
                _request = CreateRequest();

                //Can timeout while writing request data
                WriteRequestData();

                using (HttpWebResponse response = _request.GetResponse() as HttpWebResponse)
                {
                    if (response != null)
                    {
                        ParseResponse(response);
                    }
                }

                _success = true;
                Close();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.RequestCanceled)
                {
                    Client.Logger.Debug("Client request cancelled. Plugin: {0}", Client.Plugin?.Name);
                    return;
                }
                
                using (HttpWebResponse httpResponse = ex.Response as HttpWebResponse)
                {
                    _lastError = new RestError(Client, Bucket, ex, RequestUrl, Method, _request.ContentType, Data, Contents);
                    if (httpResponse == null)
                    {
                        Bucket.ErrorDelayUntil = Time.TimeSinceEpoch() + 1;
                        _lastError.SetErrorMessage(RestRequestErrorType.Internal, DiscordLogLevel.Exception);
                        Close(false);
                        return;
                    }

                    int statusCode = (int)httpResponse.StatusCode;

                    string message = ParseResponse(ex.Response);
                    _lastError.SetResponseData(statusCode, message);
                    
                    if (statusCode == 429)
                    {
                        _lastError.SetErrorMessage(RestRequestErrorType.RateLimit, DiscordLogLevel.Warning);
                        Close(false);
                        return;
                    }
                    
                    DiscordApiError apiError = Response.ParseData<DiscordApiError>();
                    _lastError.SetApiError(apiError);
                    if (apiError != null && apiError.Code != 0)
                    {
                        _lastError.SetErrorMessage(RestRequestErrorType.ApiError, DiscordLogLevel.Error);
                    }
                    else
                    {
                        _lastError.SetErrorMessage(RestRequestErrorType.GenericWeb, DiscordLogLevel.Error);
                    }

                    Close();
                }
            }
            catch (JsonSerializationException ex)
            {
                Client.Logger.Exception("A JsonSerializationException occured for request. Plugin: {0} Method: {1} URL: {2} Data Type: {3}", Client.PluginName, Method, RequestUrl, Data?.GetType().Name ?? "None", ex);
                Close();
            }
            catch (Exception ex)
            {
                Client.Logger.Exception("An exception occured for request. Plugin: {0} Method: {1} URL: {2} Data Type: {3}", Client.PluginName, Method, RequestUrl, Data?.GetType().Name ?? "None", ex);
                Close();
            }
        }

        private HttpWebRequest CreateRequest()
        {
            SetRequestBody();
            
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(RequestUrl);
            req.Method = Method.ToString();
            req.UserAgent = $"DiscordBot (https://github.com/Kirollos/Oxide.Ext.Discord, {DiscordExtension.FullExtensionVersion})";
            req.Timeout = TimeoutDuration * 1000;
            req.ContentLength = 0;
            req.Headers.Set("Authorization", _authHeader);
            req.ContentType = MultipartRequest ? $"multipart/form-data;boundary=\"{Boundary}\"" : "application/json" ;

            return req;
        }

        private void SetRequestBody()
        {
            if (Data == null || Contents != null)
            {
                return;
            }
            
            if (MultipartRequest)
            {
                IFileAttachments attachments = (IFileAttachments)Data;
                _multipartSections = DiscordPool.GetList<IMultipartSection>();
                _multipartSections.Add(new MultipartFormSection("payload_json", Data, "application/json"));
                for (int index = 0; index < attachments.FileAttachments.Count; index++)
                {
                    MessageFileAttachment fileAttachment = attachments.FileAttachments[index];
                    _multipartSections.Add(new MultipartFileSection($"files[{(index + 1).ToString()}]", fileAttachment.FileName, fileAttachment.Data, fileAttachment.ContentType));
                }

                Boundary = Guid.NewGuid().ToString().Replace("-", "");
                Contents = MultipartHandler.GetMultipartFormData(Boundary, _multipartSections);
            }
            else
            {
                Contents = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Data, Client.Bot.ClientSerializerSettings));
            }
        }

        /// <summary>
        /// Closes the request and removes it from the bucket
        /// </summary>
        /// <param name="remove"></param>
        public void Close(bool remove = true)
        {
            _retries += 1;
            if (remove || _retries >= 3)
            {
                Bucket.DequeueRequest(this);
                
                if (_success)
                {
                    InvokeSuccess();
                }
                else
                {
                    InvokeError();
                }
            }
            else
            {
                InProgress = false;
                StartTime = null;
            }
        }

        /// <summary>
        /// Invokes the success callback
        /// </summary>
        protected virtual void InvokeSuccess()
        {
            if (_onSuccess == null)
            {
                DiscordPool.Free(this);
                return;
            }

            ApiCallback callback = DiscordPool.Get<ApiCallback>();
            callback.Init(this, _onSuccess, Client);
            Interface.Oxide.NextTick(callback.Callback);
        }

        private void InvokeError()
        {
            ApiErrorCallback callback = DiscordPool.Get<ApiErrorCallback>();
            callback.Init(this, _onError, _lastError, Client);
            Interface.Oxide.NextTick(callback.Callback);
        }
        
        /// <summary>
        /// Returns true if the request has timed out
        /// </summary>
        /// <returns></returns>
        public bool HasTimedOut()
        {
            if (!InProgress || StartTime == null)
            {
                return false;
            }

            return (DateTime.UtcNow - StartTime.Value).TotalSeconds > TimeoutDuration;
        }

        private void WriteRequestData()
        {
            if (Contents == null || Contents.Length == 0)
            {
                return;
            }

            _request.ContentLength = Contents.Length;

            using (Stream stream = _request.GetRequestStream())
            {
                stream.Write(Contents, 0, Contents.Length);
            }
        }

        private string ParseResponse(WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                if (stream == null)
                {
                    return null;
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string message = reader.ReadToEnd().Trim();
                    Response = DiscordPool.Get<RestResponse>();
                    Response.Init(Client.Bot, message);

                    ParseHeaders(response.Headers, Response);

                    return message;
                }
            }
        }

        private void ParseHeaders(WebHeaderCollection headers, RestResponse response)
        {
            string globalRetryAfterHeader = headers.Get("Retry-After");
            string isGlobalRateLimitHeader = headers.Get("X-RateLimit-Global");

            if (!string.IsNullOrEmpty(globalRetryAfterHeader) &&
                !string.IsNullOrEmpty(isGlobalRateLimitHeader) &&
                int.TryParse(globalRetryAfterHeader, out int globalRetryAfter) &&
                bool.TryParse(isGlobalRateLimitHeader, out bool isGlobalRateLimit) &&
                isGlobalRateLimit)
            {
                RateLimit limit = response.ParseData<RateLimit>();
                if (limit.Global)
                {
                    Bucket.Handler.RateLimit.ReachedRateLimit(globalRetryAfter);
                }
            }

            string bucketLimitHeader = headers.Get("X-RateLimit-Limit");
            string bucketRemainingHeader = headers.Get("X-RateLimit-Remaining");
            string bucketResetAfterHeader = headers.Get("X-RateLimit-Reset-After");
            string bucketNameHeader = headers.Get("X-RateLimit-Bucket");

            int bucketLimit = 0;
            int bucketRemaining = 0;
            double bucketResetAfter = 0;

            if (!string.IsNullOrEmpty(bucketLimitHeader))
            {
                int.TryParse(bucketLimitHeader, out bucketLimit);
            }

            if (!string.IsNullOrEmpty(bucketRemainingHeader))
            {
                int.TryParse(bucketRemainingHeader, out bucketRemaining);
            }
            
            if (!string.IsNullOrEmpty(bucketResetAfterHeader))
            {
                double.TryParse(bucketResetAfterHeader, out bucketResetAfter);
            }
            
            Bucket.UpdateFromRequest(bucketLimit, bucketRemaining, bucketResetAfter, bucketNameHeader);

            Client.Logger.Debug("Plugin: {0} Method: {1} Route: {2} Internal Bucket Id: {3} Limit: {4} Remaining: {5} Reset: {7} Time: {7} Bucket: {8}", Client.Plugin?.Name, Method, Route, Bucket.BucketId, Bucket.RateLimitTotalRequests, Bucket.RateLimitRemaining, Bucket.RateLimitReset, Time.TimeSinceEpoch(), bucketNameHeader);
        }

        /// <summary>
        /// Aborts a currently running request
        /// </summary>
        public void Abort()
        {
            _request.Abort();
        }

        /// <inheritdoc/>
        protected override void EnterPool()
        {
            Response?.Dispose();
            
            if (Data is BasePoolable poolable)
            {
                poolable.Dispose();
            }
            
            if (_multipartSections != null)
            {
                FreeList(ref _multipartSections);
            }
            
            Client = null;
            Method = default(RequestMethod);
            Route = null;
            RequestUrl = null;
            Data = null;
            Contents = null;
            MultipartRequest = false;
            Boundary = null;
            Response = null;
            _onSuccess = null;
            _onError = null;
            StartTime = null;
            InProgress = false;
            Bucket = null;
            _authHeader = null;
            _retries = 0;
            _lastError = null;
            _success = false;
        }
    }
}