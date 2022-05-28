using System;
using System.Text;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Rest.Requests;
using Oxide.Ext.Discord.Rest.Requests.Data;

namespace Oxide.Ext.Discord.Entities.Api
{
    /// <summary>
    /// Error object that is returned to the caller when a request fails
    /// </summary>
    public class RequestError
    {
        /// <summary>
        /// The request method that was called
        /// </summary>
        public readonly RequestMethod RequestMethod;

        /// <summary>
        /// The web exception from the request
        /// </summary>
        public readonly Exception Exception;

        /// <summary>
        /// The URI that was called
        /// </summary>
        public readonly string Url;

        /// <summary>
        /// What data was passed to the request
        /// </summary>
        public readonly object Data;

        /// <summary>
        /// HTTP Content Type for the request
        /// </summary>
        public readonly string ContentType;

        /// <summary>
        /// How long it has been since the discord epoch in seconds
        /// </summary>
        public readonly double TimeSinceEpoch;

        /// <summary>
        /// The byte[] contents of the request
        /// </summary>
        public readonly byte[] Contents;

        /// <summary>
        /// HTTP Status code
        /// </summary>
        public int HttpStatusCode { get; private set; }

        /// <summary>
        /// If discord returned an error this will contain that error message
        /// </summary>
        public DiscordApiError DiscordError { get; private set; }

        /// <summary>
        /// Full string response if we received one
        /// </summary>
        public string Message { get; private set; }

        private DiscordLogLevel _logLevel;
        private RequestErrorType _errorType;
        private readonly DiscordClient _client;
        private readonly Bucket _bucket;

        /// <summary>
        /// Should we display the error message
        /// </summary>
        private bool _showErrorMessage = true;

        /// <summary>
        /// Creates a new rest error
        /// </summary>
        /// <param name="request">Request the error is for</param>
        /// <param name="exception">The web exception we received</param>
        /// <param name="data">Data passed to the request</param>
        internal RequestError(BaseRequest request, RequestData data, Exception exception)
        {
            _client = request.Client;
            _bucket = request.Bucket;
            Url = request.Route;
            RequestMethod = request.Method;
            ContentType = data.ContentType;
            Data = data;
            Contents = data.Contents;
            TimeSinceEpoch = TimeHelpers.TimeSinceEpoch();
            Exception = exception;
        }

        /// <summary>
        /// Suppresses the error message from being logged
        /// </summary>
        public void SuppressErrorMessage()
        {
            _showErrorMessage = false;
        }

        /// <summary>
        /// Sets the log information for the error
        /// </summary>
        /// <param name="type">The type of rest error that has occured</param>
        /// <param name="logLevel">Error log level</param>
        internal void SetErrorMessage(RequestErrorType type, DiscordLogLevel logLevel)
        {
            _errorType = type;
            _logLevel = logLevel;
        }

        /// <summary>
        /// Sets the HTTP Response data
        /// </summary>
        /// <param name="code">HTTP Response Code</param>
        /// <param name="message">HTTP Response Body Text</param>
        internal void SetResponseData(int code, string message)
        {
            HttpStatusCode = code;
            Message = message;
        }

        /// <summary>
        /// Sets the <see cref="DiscordApiError"/> if one occured
        /// </summary>
        /// <param name="error">Discord API Error to be set</param>
        internal void SetApiError(DiscordApiError error)
        {
            DiscordError = error;
        }

        /// <summary>
        /// Performs the error logging for the request
        /// </summary>
        internal void LogError()
        {
            if (!_client.Logger.IsLogging(_logLevel))
            {
                return;
            }

            if (!_showErrorMessage || DiscordError != null && DiscordExtension.DiscordConfig.Logging.HideDiscordErrorCodes.Contains(DiscordError.Code))
            {
                return;
            }

            switch (_errorType)
            {
                case RequestErrorType.Internal:
                    _client.Logger.Error("Rest Request Exception (Internal Error) Plugin: {0} Request URL: [{1}] {2}", _client.PluginName, RequestMethod, Url);
                    break;
                
                case RequestErrorType.RateLimit:
                    _client.Logger.Warning("Rest Request Exception (Rate Limit) Plugin: {0} Request URL:  [{1}] {2} Content-Type: {3} Remaining: {4} Limit: {5} Reset At: {6} Current Time: {7}",
                        _client.PluginName, RequestMethod, Url, ContentType, _bucket.Remaining, _bucket.Limit, _bucket.ResetAt, TimeSinceEpoch.ToDateTimeOffsetFromSeconds());
                    break;

                case RequestErrorType.ApiError:
                    _client.Logger.Error("Rest Request Exception (Discord API Error). Plugin: {0} Request URL: [{1}] {2} Content-Type: {3} Http Response Code: {4} Discord Error Code: {5} Discord Error: {6}\nDiscord Errors: {7}Request Body:\n{8}",
                        _client.PluginName, RequestMethod, Url, ContentType, HttpStatusCode, DiscordError.Code, DiscordError.Message, DiscordError.Errors, Contents != null ? Encoding.UTF8.GetString(Contents) : "No Contents");
                    break;

                case RequestErrorType.GenericWeb:
                    _client.Logger.Error("Rest Request Exception (Web Error). Plugin: {0} Request URL: [{1}] {2} Content-Type: {3} Http Response Code: {4} Message: {5}", _client.PluginName, RequestMethod, Url, ContentType, HttpStatusCode, Message);
                    break;


                case RequestErrorType.Serialization:
                    _client.Logger.Exception("Rest Request Exception (JSON Serialization). Plugin: {0} Method: {1} URL: {2} Data Type: {3}", _client.PluginName, RequestMethod, Url, Data?.GetType().Name ?? "None", Exception);
                    break;

                case RequestErrorType.Generic:
                    _client.Logger.Exception("Rest Request Exception (Generic Error). Plugin: {0} Method: {1} URL: {2} Data Type: {3}", _client.PluginName, RequestMethod, Url, Data?.GetType().Name ?? "None", Exception);
                    break;
            }
        }
    }
}