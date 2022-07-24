using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
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
        internal Stream Content;
        
        private DiscordClient _client;

        /// <summary>
        /// Create new REST response with the given data
        /// </summary>
        /// <param name="client">BotClient for the response</param>
        /// <param name="response">The Web Response for the request</param>
        /// <param name="status">The status of the request indicating if it was successful</param>
        /// <param name="error">If the request had an error the error created from the request</param>
        private async Task Init(DiscordClient client, HttpResponseMessage response, RequestCompletedStatus status, RequestError error = null)
        {
            _client = client;
            Status = status;
            Error = error;

            if (response != null)
            {
                Code = (int)response.StatusCode;
                Content = await response.Content.ReadAsStreamAsync();
                RateLimit = DiscordPool.Get<RateLimitResponse>();
                RateLimit.Init(response.Headers, _client.Logger);
                if (error != null)
                {
                    await error.SetResponseData((int)response.StatusCode, Content);
                }
            }
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
        /// Creates a Web Exception REST API response
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="error">Rest Error that occured</param>
        /// <param name="httpResponse">Web Response for the request</param>
        /// <param name="status">The request status containing the fail reason</param>
        /// <returns>A web exception <see cref="RequestResponse"/></returns>
        public static async Task<RequestResponse> CreateExceptionResponse(DiscordClient client, RequestError error, HttpResponseMessage httpResponse, RequestCompletedStatus status)
        {
            RequestResponse response = DiscordPool.Get<RequestResponse>();
            await response.Init(client, httpResponse, status, error);
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
            Content = null;
            RateLimit = null;
            Error = null;
            Code = 0;
            _client = null;
            Content = null;
        }
    }
}
