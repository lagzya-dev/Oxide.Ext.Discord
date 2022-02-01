using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Rest.Multipart;
using RequestMethod = Oxide.Ext.Discord.Entities.Api.RequestMethod;
using Time = Oxide.Ext.Discord.Helpers.Time;

namespace Oxide.Ext.Discord.Rest.Request
{
    /// <summary>
    /// Represent a Discord API request
    /// </summary>
    public class Request
    {
        /// <summary>
        /// HTTP request method
        /// </summary>
        public readonly RequestMethod Method;

        /// <summary>
        /// Route on the API
        /// </summary>
        public readonly string Route;

        /// <summary>
        /// Full Request URl to the API
        /// </summary>
        public string RequestUrl => BaseUrl + Route;

        /// <summary>
        /// Data to be sent with the request
        /// </summary>
        public readonly object Data;
        
        /// <summary>
        /// Data serialized to bytes 
        /// </summary>
        public byte[] Contents { get; private set; }

        /// <summary>
        /// Attachments for a request
        /// </summary>
        internal List<IMultipartSection> MultipartSections { get; private set; }

        /// <summary>
        /// Required If Multipart Form Request
        /// </summary>
        public readonly bool MultipartRequest;

        /// <summary>
        /// Multipart Boundary
        /// </summary>
        public string Boundary { get; set; }

        /// <summary>
        /// Response from the request
        /// </summary>
        public RestResponse Response { get; private set; }

        /// <summary>
        /// Callback to call if the request completed successfully
        /// </summary>
        private readonly Action _onSuccess;

        /// <summary>
        /// Callback to call if the request errored with the last error message
        /// </summary>
        public readonly Action<RestError> OnError;

        /// <summary>
        /// The DateTime the request was started
        /// Used for request timeout
        /// </summary>
        public DateTime? StartTime { get; private set; }

        /// <summary>
        /// Returns if the request is currently in progress
        /// </summary>
        public bool InProgress { get; private set; }

        internal Bucket Bucket;
        
        /// <summary>
        /// Base URL for Discord
        /// </summary>
        public const string UrlBase = "https://discord.com/api";
        
        /// <summary>
        /// API Version for Rest requests
        /// </summary>
        public const string ApiVersion = "v9";
        
        /// <summary>
        /// Base API Url
        /// </summary>
        public const string BaseUrl = UrlBase + "/" +ApiVersion;
        
        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger Logger;
        
        private const int TimeoutDuration = 15;

        private readonly string _authHeader;
        private byte _retries;
        
        private RestError _lastError;
        private bool _success;

        /// <summary>
        /// Creates a new request
        /// </summary>
        /// <param name="method">HTTP method to call</param>
        /// <param name="route">Route to call on the API</param>
        /// <param name="data">Data for the request</param>
        /// <param name="authHeader">Authorization Header</param>
        /// <param name="onSuccess">Callback once the request completes successfully</param>
        /// <param name="onError">Callback when the request errors</param>
        /// <param name="logger">Logger for the request</param>
        public Request(RequestMethod method, string route, object data, string authHeader, Action onSuccess, Action<RestError> onError, ILogger logger) : this(method, route, data,authHeader, onError, logger)
        {
            _onSuccess = onSuccess;
        }

        /// <summary>
        /// Creates a new request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="route"></param>
        /// <param name="data"></param>
        /// <param name="authHeader"></param>
        /// <param name="onError"></param>
        /// <param name="logger"></param>
        protected Request(RequestMethod method, string route, object data, string authHeader, Action<RestError> onError, ILogger logger)
        {
            Method = method;
            Route = route;
            Data = data;
            _authHeader = authHeader;
            OnError = onError;
            Logger = logger;
            MultipartRequest = Data is IFileAttachments attachments && attachments.FileAttachments != null && attachments.FileAttachments.Count != 0;
        }

        /// <summary>
        /// Fires the request off
        /// </summary>
        public void Fire()
        {
            InProgress = true;
            StartTime = DateTime.UtcNow;

            HttpWebRequest req = CreateRequest();
            
            try
            {
                //Can timeout while writing request data
                WriteRequestData(req);

                using (HttpWebResponse response = req.GetResponse() as HttpWebResponse)
                {
                    if (response != null)
                    {
                        ParseResponse(response);
                    }
                }

                _success = true;
                InvokeSuccess();
                Close();
            }
            catch (WebException ex)
            {
                using (HttpWebResponse httpResponse = ex.Response as HttpWebResponse)
                {
                    _lastError = new RestError(ex, req.RequestUri, Method, Data);
                    if (httpResponse == null)
                    {
                        Bucket.ErrorDelayUntil = Time.TimeSinceEpoch() + 1;
                        _lastError.SetLog(DiscordLogLevel.Exception, $"A web request exception occured (internal error) Request URL: [{req.Method}] {req.RequestUri}", ex);
                        Close(false);
                        return;
                    }

                    int statusCode = (int) httpResponse.StatusCode;
                    _lastError.HttpStatusCode = statusCode;
                        
                    string message = ParseResponse(ex.Response);
                    _lastError.Message = message;
                        
                    bool isRateLimit = statusCode == 429;
                    if (isRateLimit)
                    {
                        _lastError.SetLog(DiscordLogLevel.Warning, $"Discord rate limit reached. (Rate limit info: remaining: [{req.Method}] Route: {req.RequestUri} Content-Type: {req.ContentType} Remaining: {Bucket.RateLimitRemaining.ToString()} Limit: {Bucket.RateLimit.ToString()}, Reset In: {Bucket.RateLimitReset.ToString()}, Current Time: {Time.TimeSinceEpoch().ToString()}");
                        Close(false);
                        return;
                    }

                    DiscordApiError apiError = Response.ParseData<DiscordApiError>();
                    _lastError.DiscordError = apiError;
                    if (apiError != null && apiError.Code != 0)
                    {
                        _lastError.SetLog(DiscordLogLevel.Error, $"Discord API has returned error Discord Code: {apiError.Code.ToString()} Discord Error: {apiError.Message} Request: [{req.Method}] {req.RequestUri} (Response Code: {httpResponse.StatusCode.ToString()}) Content-Type: {req.ContentType}" +
                                                                 $"\nDiscord Errors: {apiError.Errors}" +
                                                                 $"\nRequest Body:\n{(Contents != null ? Encoding.UTF8.GetString(Contents) : "Contents is null")}");
                    }
                    else
                    {
                        _lastError.SetLog(DiscordLogLevel.Error, $"An error occured whilst submitting a request: Exception Status: {ex.Status.ToString()} Request: [{req.Method}] {req.RequestUri} (Response Code: {httpResponse.StatusCode.ToString()}): {message}");
                    }

                    Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Exception($"An exception occured for request: [{req.Method}] {req.RequestUri}", ex);
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
                MultipartSections = new List<IMultipartSection> {new MultipartFormSection("payload_json", Data, "application/json")};
                for (int index = 0; index < attachments.FileAttachments.Count; index++)
                {
                    MessageFileAttachment fileAttachment = attachments.FileAttachments[index];
                    MultipartSections.Add(new MultipartFileSection($"files[{(index + 1).ToString()}]", fileAttachment.FileName, fileAttachment.Data, fileAttachment.ContentType));
                }

                Boundary = Guid.NewGuid().ToString().Replace("-", "");
                Contents = MultipartHandler.GetMultipartFormData(Boundary, MultipartSections);
            }
            else
            {
                Contents = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Data, DiscordExtension.ExtensionSerializeSettings));
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
                if (!_success)
                {
                    InvokeError();
                }

                Bucket.DequeueRequest(this);
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
                return;
            }
            
            Interface.Oxide.NextTick(() =>
            {
                try
                {
                    _onSuccess.Invoke();
                }
                catch (Exception ex)
                {
                    Logger.Exception("An exception occured during Success callback for request: [{0}] {1}", Method, RequestUrl,  ex);
                }
            });
        }

        private void InvokeError()
        {
            Interface.Oxide.NextTick(() =>
            {
                if (OnError != null)
                {
                    try
                    {
                        OnError.Invoke(_lastError);
                    }
                    catch (Exception ex)
                    {
                        Logger.Exception("An exception occured during Error callback for request: [{0}] {1}", Method, RequestUrl,  ex);
                    }
                }

                if (_lastError.ShowErrorMessage && (_lastError.DiscordError == null || !DiscordExtension.DiscordConfig.Logging.HideDiscordErrorCodes.Contains(_lastError.DiscordError.Code)))
                {
                    Logger.Log(_lastError.LogLevel, _lastError.LogMessage, _lastError.Exception);
                }
            });
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

        private void WriteRequestData(WebRequest request)
        {
            if (Contents == null || Contents.Length == 0)
            {
                return;
            }

            request.ContentLength = Contents.Length;

            using (Stream stream = request.GetRequestStream())
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
                    Response = new RestResponse(message);

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

            if (!string.IsNullOrEmpty(bucketLimitHeader) &&
                int.TryParse(bucketLimitHeader, out int bucketLimit))
            {
                Bucket.RateLimit = bucketLimit;
            }

            if (!string.IsNullOrEmpty(bucketRemainingHeader) &&
                int.TryParse(bucketRemainingHeader, out int bucketRemaining))
            {
                Bucket.RateLimitRemaining = bucketRemaining;
            }

            double timeSince = Time.TimeSinceEpoch();
            if (!string.IsNullOrEmpty(bucketResetAfterHeader) &&
                double.TryParse(bucketResetAfterHeader, out double bucketResetAfter))
            {
                double resetTime = timeSince + bucketResetAfter;
                if (resetTime > Bucket.RateLimitReset)
                {
                    Bucket.RateLimitReset = resetTime;
                }
            }
            
            Logger.Debug("Method: {0} Route: {1} Internal Bucket Id: {2} Limit: {3} Remaining: {4} Reset: {5} Time: {6} Bucket: {7}", Method, Route, Bucket.BucketId, Bucket.RateLimit, Bucket.RateLimitRemaining, Bucket.RateLimitReset, Time.TimeSinceEpoch(), bucketNameHeader);
        }
    }
}