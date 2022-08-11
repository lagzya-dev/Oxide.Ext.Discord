using Oxide.Ext.Discord.Entities.Api;

namespace Oxide.Ext.Discord.Callbacks.Api.Entities
{
    /// <summary>
    /// Represents a base callback when a request is completed
    /// </summary>
    public abstract class BaseApiCompletedCallback : BaseCallback<RequestResponse>
    {
        /// <summary>
        /// <see cref="DiscordClient"/> for the Api Completed Callback
        /// </summary>
        protected DiscordClient Client;

        /// <summary>
        /// Initializes the <see cref="BaseApiCompletedCallback"/>
        /// </summary>
        /// <param name="client"></param>
        protected void Init(DiscordClient client)
        {
            Client = client;
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            Client = null;
        }
    }
}