using System;
using Oxide.Core;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Handles request that return data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Request<T> : Requests.Request
    {
        private readonly Action<T> _onSuccess;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method"></param>
        /// <param name="route"></param>
        /// <param name="data"></param>
        /// <param name="authHeader"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        /// <param name="logger"></param>
        public Request(RequestMethod method, string route, object data, string authHeader, Action<T> onSuccess, Action<RestError> onError, ILogger logger) : base(method, route, data, authHeader, onError, logger) 
        {
            _onSuccess = onSuccess;
        }

        /// <inheritdoc/>
        protected override void InvokeSuccess()
        {
            if (_onSuccess == null)
            {
                return;
            }
            
            T data = Response.ParseData<T>();
            Interface.Oxide.NextTick(() =>
            {
                try
                {
                    _onSuccess.Invoke(data);
                }
                catch (Exception ex)
                {
                    Logger.Exception("An exception occured during _onSuccess callback for request: [{0}] {1}", Method, RequestUrl, ex);
                }
            });
        }
    }
}