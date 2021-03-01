using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Logging;
using RequestMethod = Oxide.Ext.Discord.Entities.Api.RequestMethod;
using Time = Oxide.Ext.Discord.Helpers.Time;

namespace Oxide.Ext.Discord.Rest
{
    /// <summary>
    /// Represent a Discord API request
    /// </summary>
    public class Request
    {
        /// <summary>
        /// HTTP request method
        /// </summary>
        public RequestMethod Method { get; }

        /// <summary>
        /// Route on the API
        /// </summary>
        public string Route { get; }

        /// <summary>
        /// Full Request URl to the API
        /// </summary>
        public string RequestUrl => UrlBase + "/" + ApiVersion + Route;

        /// <summary>
        /// Data to be sent with the request
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// Response from the request
        /// </summary>
        public RestResponse Response { get; private set; }

        /// <summary>
        /// Callback to call if the request completed successfully
        /// </summary>
        public Action<RestResponse> Callback { get; }
        
        /// <summary>
        /// Callback to call if the request errored with the last error message
        /// </summary>
        public Action<RestError> OnError { get; }

        /// <summary>
        /// The DateTime the request was started
        /// Used for request timeout
        /// </summary>
        public DateTime? StartTime { get; private set; }

        /// <summary>
        /// Returns if the request is currently in progress
        /// </summary>
        public bool InProgress { get; set; }

        internal Bucket Bucket;
        
        private const string UrlBase = "https://discord.com/api";
        private const string ApiVersion = "v8";
        private const int RequestMaxLength = 15;

        private readonly string _authHeader;
        private byte _retries;
        
        private readonly ILogger _logger;
        private RestError _lastError;

        /// <summary>
        /// Creates a new request
        /// </summary>
        /// <param name="method">HTTP method to call</param>
        /// <param name="route">Route to call on the API</param>
        /// <param name="data">Data for the request</param>
        /// <param name="authHeader">Authorization Header</param>
        /// <param name="callback">Callback once the request completes successfully</param>
        /// <param name="onError">Callback when the request errors</param>
        /// <param name="logger">Logger for the request</param>
        public Request(RequestMethod method, string route, object data, string authHeader, Action<RestResponse> callback, Action<RestError> onError, ILogger logger)
        {
            Method = method;
            Route = route;
            Data = data;
            _authHeader = authHeader;
            Callback = callback;
            OnError = onError;
            _logger = logger;
        }

        /// <summary>
        /// Fires the request off
        /// </summary>
        public void Fire()
        {
            InProgress = true;
            StartTime = DateTime.UtcNow;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(RequestUrl);
            req.Method = Method.ToString();
            req.ContentType = "application/json";
            req.UserAgent = $"DiscordBot (https://github.com/Kirollos/Oxide.Ext.Discord, {DiscordExtension.GetExtensionVersion})";
            req.Timeout = RequestMaxLength * 1000;
            req.ContentLength = 0;
            req.Headers.Set("Authorization", _authHeader);

            try
            {
                //Can timeout while writing request data
                if (Data != null)
                {
                    WriteRequestData(req, Data);
                }

                using (HttpWebResponse response = req.GetResponse() as HttpWebResponse)
                {
                    if (response != null)
                    {
                        ParseResponse(response);
                    }
                }

                Callback?.Invoke(Response);
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
                        Close(false);
                        _logger.Exception($"A web request exception occured (internal error) [RETRY={_retries}/3].\nRequest URL: [{Method.ToString()}] {req.RequestUri}", ex);
                        return;
                    }

                    int statusCode = (int) httpResponse.StatusCode;
                    _lastError.HttpStatusCode = statusCode;
                        
                    string message = ParseResponse(ex.Response);
                    _lastError.Message = message;
                        
                    bool isRateLimit = statusCode == 429;
                    if (isRateLimit)
                    {
                        _logger.Warning($"Discord rate limit reached. (Rate limit info: remaining: Route:{req.RequestUri}, {Bucket.RateLimitRemaining}, limit: {Bucket.RateLimit}, reset: {Bucket.RateLimitReset}, time now: {Time.TimeSinceEpoch()}");
                        Close(false);
                        return;
                    }

                    DiscordApiError apiError = Response.ParseData<DiscordApiError>();
                    _lastError.DiscordError = apiError;
                    if (!string.IsNullOrEmpty(apiError.Code))
                    {
                        _logger.Error($"Discord has returned error Code: {apiError.Code}: {apiError.Message}, {req.RequestUri} (code {httpResponse.StatusCode})\nErrors: {apiError.Errors}");
                    }
                    else
                    {
                        _logger.Error($"An error occured whilst submitting a request to {req.RequestUri} (code {httpResponse.StatusCode}): {message}");
                    }

                    Close();
                }
            }
            catch (Exception ex)
            {
                _logger.Exception("Request callback raised an exception", ex);
                Close();
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
                if (_retries >= 3)
                {
                    OnError?.Invoke(_lastError);
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
        /// Returns true if the request has timed out
        /// </summary>
        /// <returns></returns>
        public bool HasTimedOut()
        {
            if (!InProgress || StartTime == null)
            {
                return false;
            }

            return (DateTime.UtcNow - StartTime.Value).TotalSeconds > RequestMaxLength;
        }

        private void WriteRequestData(HttpWebRequest request, object data)
        {
            string contents = JsonConvert.SerializeObject(data, DiscordExtension.ExtensionSerializeSettings);

            byte[] bytes = Encoding.UTF8.GetBytes(contents);
            request.ContentLength = bytes.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
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
            
            _logger.Debug($"Method: {Method} Route: {Route} Internal Bucket Id: {Bucket.BucketId} Limit: {Bucket.RateLimit} Remaining: {Bucket.RateLimitRemaining} Reset: {Bucket.RateLimitReset} Time: {Time.TimeSinceEpoch()} Bucket: {bucketNameHeader}");
        }
    }
}
