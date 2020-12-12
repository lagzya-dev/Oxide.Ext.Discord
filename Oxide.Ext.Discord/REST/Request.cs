using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.DiscordObjects;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.REST
{
    public class Request
    {
        public RequestMethod Method { get; }

        public string Route { get; }

        public string Endpoint { get; }

        public string RequestUrl => UrlBase + Route + Endpoint;

        public Dictionary<string, string> Headers { get; }

        public object Data { get; }

        public RestResponse Response { get; private set; }

        public Action<RestResponse> Callback { get; }

        public DateTime? StartTime { get; private set; }

        public bool InProgress { get; set; }

        private Bucket _bucket;

        private byte _retries;
        
        private const string UrlBase = "https://discordapp.com/api";

        private const int RequestMaxLength = 30;

        private readonly ILogger _logger;
        
        private static readonly JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public Request(RequestMethod method, string route, string endpoint, Dictionary<string, string> headers, object data, Action<RestResponse> callback, LogLevel logLevel)
        {
            this.Method = method;
            this.Route = route;
            this.Endpoint = endpoint;
            this.Headers = headers;
            this.Data = data;
            this.Callback = callback;
            _logger = new Logger<Request>(logLevel);
        }

        public void Fire(Bucket bucket)
        {
            _bucket = bucket;
            InProgress = true;
            StartTime = DateTime.UtcNow;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(RequestUrl);
            req.Method = Method.ToString();
            req.ContentType = "application/json";
            req.Timeout = RequestMaxLength * 1000;
            req.ContentLength = 0;

            if (Headers != null)
            {
                req.SetRawHeaders(Headers);
            }
            
            try
            {
                //Can timeout while writing request data
                if (Data != null)
                {
                    WriteRequestData(req, Data);
                }
                
                using HttpWebResponse response = req.GetResponse() as HttpWebResponse;
                if (response != null)
                {
                    ParseResponse(response);
                }

                Callback?.Invoke(Response);
                Close();
            }
            catch (WebException ex)
            {
                using HttpWebResponse httpResponse = ex.Response as HttpWebResponse;
                if (httpResponse == null)
                {
                    _logger.LogException($"[Discord Extension] A web request exception occured (internal error) [RETRY={_retries}/3].", ex);
                    _logger.LogError($"[Discord Extension] Request URL: [{Method.ToString()}] {RequestUrl}");

                    Close(false);
                    return;
                }
                
                string message = ParseResponse(ex.Response);
                
                bool isRateLimit = (int) httpResponse.StatusCode == 429;
                if (isRateLimit)
                {
                    _logger.LogInfo($"Discord ratelimit reached. (Ratelimit info: remaining: {bucket.Remaining}, limit: {bucket.Limit}, reset: {bucket.Reset}, time now: {Helpers.Time.TimeSinceEpoch()}");
                }
                else
                {
                    DiscordApiError apiError = Response.ParseData<DiscordApiError>();
                    if (!string.IsNullOrEmpty(apiError.Code))
                    {
                        _logger.LogWarning($"[Discord Extension] Discord has returned error Code - {apiError.Code}: {apiError.Message} - {req.RequestUri} (code {httpResponse.StatusCode})");
                    }
                    else
                    {
                        _logger.LogWarning($"[Discord Extension] An error occured whilst submitting a request to {req.RequestUri} (code {httpResponse.StatusCode}): {message}");
                    }
                }
                
                Close(!isRateLimit);
            }
            catch (Exception ex)
            {
                _logger.LogException("[Discord Extension] Request callback raised an exception", ex);
                Close();
            }
        }

        public void Close(bool remove = true)
        {
            if (remove || _retries >= 3)
            {
                lock (_bucket)
                {
                    _bucket.Remove(this);
                }
            }
            else
            {
                _retries += 1;
                InProgress = false;
                StartTime = null;
            }
        }

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
            string contents = JsonConvert.SerializeObject(data, DefaultSerializerSettings);

            byte[] bytes = Encoding.UTF8.GetBytes(contents);
            request.ContentLength = bytes.Length;

            using Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
        }

        private string ParseResponse(WebResponse response)
        {
            using Stream stream = response.GetResponseStream();
            if (stream == null)
            {
                return null;
            }
            
            using StreamReader reader = new StreamReader(stream);
            string message = reader.ReadToEnd().Trim();

            Response = new RestResponse(message);

            ParseHeaders(response.Headers, Response);

            return message;
        }

        private void ParseHeaders(WebHeaderCollection headers, RestResponse response)
        {
            string rateRetryAfterHeader = headers.Get("Retry-After");
            string rateLimitGlobalHeader = headers.Get("X-RateLimit-Global");

            if (!string.IsNullOrEmpty(rateRetryAfterHeader) &&
                !string.IsNullOrEmpty(rateLimitGlobalHeader) &&
                int.TryParse(rateRetryAfterHeader, out int rateRetryAfter) &&
                bool.TryParse(rateLimitGlobalHeader, out bool rateLimitGlobal) &&
                rateLimitGlobal)
            {
                RateLimit limit = response.ParseData<RateLimit>();
                if (limit.global)
                {
                    GlobalRateLimit.Reached(rateRetryAfter);
                }
            }

            string rateLimitHeader = headers.Get("X-RateLimit-Limit");
            string rateRemainingHeader = headers.Get("X-RateLimit-Remaining");
            string rateResetHeader = headers.Get("X-RateLimit-Reset");

            if (!string.IsNullOrEmpty(rateLimitHeader) &&
                int.TryParse(rateLimitHeader, out int rateLimit))
            {
                _bucket.Limit = rateLimit;
            }

            if (!string.IsNullOrEmpty(rateRemainingHeader) &&
                int.TryParse(rateRemainingHeader, out int rateRemaining))
            {
                _bucket.Remaining = rateRemaining;
            }

            if (!string.IsNullOrEmpty(rateResetHeader) &&
                int.TryParse(rateResetHeader, out int rateReset))
            {
                _bucket.Reset = rateReset;
            }

            ////_logger.LogInfo($"Recieved ratelimit deets: {bucket.Limit}, {bucket.Remaining}, {bucket.Reset}, time now: {bucket.TimeSinceEpoch()}");
            ////_logger.LogInfo($"Time until reset: {(bucket.Reset - (int)bucket.TimeSinceEpoch())}");
        }
    }
}
