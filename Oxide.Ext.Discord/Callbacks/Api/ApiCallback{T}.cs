using System;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal class ApiCallback<T> : BaseApiCallback<Request<T>>
    {
        private T _data;

        public override void Init(Request<T> request, RequestResponse response)
        {
            base.Init(request, response);
            _data = response.ParseData<T>();
        }

        protected override void OnSuccess()
        {
            try
            {
                Request.OnSuccess.Invoke(_data);
            }
            catch (Exception ex)
            {
                Client.Logger.Exception("An exception occured during Success callback for request {0] [{1}] {2}", Client.PluginName, Request.Method, Request.Route, ex);
            }
            finally
            {
                Dispose();
            }
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            _data = default(T);
        }
    }
}