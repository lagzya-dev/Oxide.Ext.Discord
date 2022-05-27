using System.IO;
using System.Net;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Entities.Api
{
    /// <summary>
    /// Represents a REST response from discord
    /// </summary>
    public class RestResponse : BasePoolable
    {
        /// <summary>
        /// Data discord sent us
        /// </summary>
        internal string Message;

        private DiscordClient _client;

        internal RestError Error;

        internal RateLimitResponse RateLimit;
        
        internal RequestStatus Status;

        /// <summary>
        /// Create new REST response with the given data
        /// </summary>
        /// <param name="client">BotClient for the response</param>
        /// <param name="data"></param>
        public void Init(DiscordClient client, HttpWebResponse response, RequestStatus status)
        {
            _client = client;
            RateLimit = DiscordPool.Get<RateLimitResponse>();
            Status = status;
            if (response == null)
            {
                return;
            }

            RateLimit.Init(response.Headers, _client.Logger);

            using (Stream stream = response.GetResponseStream())
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Message = reader.ReadToEnd().Trim();
                    }
                }
            }
        }

        public static RestResponse CreateSuccessResponse(DiscordClient client, HttpWebResponse httpResponse)
        {
            RestResponse response = DiscordPool.Get<RestResponse>();
            response.Init(client, httpResponse, RequestStatus.Success);
            return response;
        }
        
        public static RestResponse CreateExceptionResponse(DiscordClient client, RestError error, RequestStatus status)
        {
            RestResponse response = DiscordPool.Get<RestResponse>();
            response.Init(client, null, status);
            response.Error = error;
            
            DiscordApiError apiError = response.ParseData<DiscordApiError>();
            error.SetApiError(apiError);
            if (apiError != null && apiError.Code != 0)
            {
                error.SetErrorMessage(RequestErrorType.ApiError, DiscordLogLevel.Error);
            }
            else
            {
                error.SetErrorMessage(RequestErrorType.GenericWeb, DiscordLogLevel.Error);
            }
            
            return response;
        }
        
        public static RestResponse CreateWebExceptionResponse(DiscordClient client, RestError error, HttpWebResponse httpResponse, RequestStatus status)
        {
            RestResponse response = DiscordPool.Get<RestResponse>();
            response.Init(client, httpResponse, status);
            error.SetResponseData((int)httpResponse.StatusCode, response.Message);
            response.Error = error;
            
            DiscordApiError apiError = response.ParseData<DiscordApiError>();
            error.SetApiError(apiError);
            if (apiError != null && apiError.Code != 0)
            {
                error.SetErrorMessage(RequestErrorType.ApiError, DiscordLogLevel.Error);
            }
            else
            {
                error.SetErrorMessage(RequestErrorType.GenericWeb, DiscordLogLevel.Error);
            }
            
            return response;
        }
        
        public static RestResponse CreateCancelledResponse(DiscordClient client)
        {
            RestResponse response = DiscordPool.Get<RestResponse>();
            response.Init(client, null, RequestStatus.Cancelled);
            return response;
        }

        /// <summary>
        /// Parse the data to it's given object
        /// </summary>
        /// <typeparam name="T">Type to be parsed as</typeparam>
        /// <returns>Data string JSON parsed to object</returns>
        public T ParseData<T>() => !string.IsNullOrEmpty(Message) ? JsonConvert.DeserializeObject<T>(Message, _client.Bot.ClientSerializerSettings) : default(T);

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            Message = null;
            _client = null;
            Error = null;
        }
    }
}
