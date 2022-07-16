using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Entities.Api
{
    /// <summary>
    /// Represents a REST response from discord
    /// </summary>
    public class RequestResponse : BasePoolable
    {
        internal RequestCompletedStatus Status;
        internal RateLimitResponse RateLimit;
        internal RequestError Error;
        internal int Code;
        
        /// <summary>
        /// Data discord sent us
        /// </summary>
        private string _message;
        private DiscordClient _client;
        
        /// <summary>
        /// Create new REST response with the given data
        /// </summary>
        /// <param name="client">BotClient for the response</param>
        /// <param name="response">The Web Response for the request</param>
        /// <param name="status">The status of the request indicating if it was successful</param>
        private async Task Init(DiscordClient client, HttpResponseMessage response, RequestCompletedStatus status)
        {
            _client = client;
            RateLimit = DiscordPool.Get<RateLimitResponse>();
            Status = status;

            if (response == null)
            {
                return;
            }
            
            Code = (int)response.StatusCode;
            RateLimit.Init(response.Headers, _client.Logger);

            _message = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Creates a success REST API response
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="httpResponse">The Web Response for the request</param>
        /// <returns>A success <see cref="RequestResponse"/></returns>
        public static async Task<RequestResponse> CreateSuccessResponse(DiscordClient client, HttpResponseMessage httpResponse)
        {
            RequestResponse response = DiscordPool.Get<RequestResponse>();
            await response.Init(client, httpResponse, RequestCompletedStatus.Success);
            return response;
        }
        
        /// <summary>
        /// Creates an exception REST API response
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="error">The Rest Error the occured</param>
        /// <param name="status">The request status containing the fail reason</param>
        /// <returns>An exception <see cref="RequestResponse"/></returns>
        public static async Task<RequestResponse> CreateExceptionResponse(DiscordClient client, RequestError error, RequestCompletedStatus status)
        {
            RequestResponse response = DiscordPool.Get<RequestResponse>();
            await response.Init(client, null, status);
            response.Error = error;
            
            RequestErrorMessage apiError = response.ParseData<RequestErrorMessage>();
            error.SetApiError(apiError);
            if (apiError != null && apiError.Code != 0)
            {
                error.SetErrorMessage(RequestErrorType.ApiError, DiscordLogLevel.Error);
            }

            return response;
        }
        
        /// <summary>
        /// Creates a Web Exception REST API response
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="error">Rest Error that occured</param>
        /// <param name="httpResponse">Web Response for the request</param>
        /// <param name="status">The request status containing the fail reason</param>
        /// <returns>A web exception <see cref="RequestResponse"/></returns>
        public static async Task<RequestResponse> CreateWebExceptionResponse(DiscordClient client, RequestError error, HttpResponseMessage httpResponse, RequestCompletedStatus status)
        {
            RequestResponse response = DiscordPool.Get<RequestResponse>();
            await response.Init(client, httpResponse, status);
            error.SetResponseData((int)httpResponse.StatusCode, response._message);
            response.Error = error;
            
            RequestErrorMessage apiError = response.ParseData<RequestErrorMessage>();
            error.SetApiError(apiError);
            if (apiError != null && apiError.Code != 0)
            {
                error.SetErrorMessage(RequestErrorType.ApiError, DiscordLogLevel.Error);
            }

            return response;
        }
        
        /// <summary>
        /// Creates a REST API response for a cancelled request
        /// </summary>
        /// <param name="client">Client the request was for</param>
        /// <returns>A cancelled <see cref="RequestResponse"/></returns>
        public static async Task<RequestResponse> CreateCancelledResponse(DiscordClient client)
        {
            RequestResponse response = DiscordPool.Get<RequestResponse>();
            await response.Init(client, null, RequestCompletedStatus.Cancelled);
            return response;
        }

        /// <summary>
        /// Parse the data to it's given object
        /// </summary>
        /// <typeparam name="T">Type to be parsed as</typeparam>
        /// <returns>Data string JSON parsed to object</returns>
        public T ParseData<T>() => !string.IsNullOrEmpty(_message) ? JsonConvert.DeserializeObject<T>(_message, _client.Bot.ClientSerializerSettings) : default(T);

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            RateLimit?.Dispose();
            DiscordPool.Free(this);
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            Status = default(RequestCompletedStatus);
            RateLimit = null;
            Error = null;
            Code = 0;
            _message = null;
            _client = null;
        }
    }
}
