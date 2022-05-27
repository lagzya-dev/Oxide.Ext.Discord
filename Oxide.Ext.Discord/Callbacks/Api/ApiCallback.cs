using System;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal class ApiCallback : BaseApiCallback<Request>
    {
        protected override void OnSuccess()
        {
            try
            {
                Request.OnSuccess.Invoke();
            }
            catch (Exception ex)
            {
                Client.Logger.Exception("An exception occured during Success callback for request {0] [{1}] {2}", Client.PluginName, Request.Method, Request.Route, ex);
            }
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}