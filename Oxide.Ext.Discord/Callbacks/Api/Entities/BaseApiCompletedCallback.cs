using Oxide.Ext.Discord.Entities.Api;

namespace Oxide.Ext.Discord.Callbacks.Api.Entities
{
    /// <summary>
    /// Represents a base callback when a request is completed
    /// </summary>
    public abstract class BaseApiCompletedCallback : BaseCallback<RequestResponse>
    {
        protected DiscordClient Client;

        protected void Init(DiscordClient client)
        {
            Client = client;
        }
        
        /// <summary>
        /// Runs the callback with the RequestResponse
        /// </summary>
        /// <param name="response">Response for the request</param>
        public sealed override void Run(RequestResponse response)
        {
            HandleCallback(response);
        }

        protected override void EnterPool()
        {
            Client = null;
        }
    }
}