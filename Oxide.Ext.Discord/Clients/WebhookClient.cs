using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Factory;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Rest;

namespace Oxide.Ext.Discord.Clients
{
    /// <summary>
    /// A client that can connect to a webhook
    /// </summary>
    public class WebhookClient : BaseClient, IDebugLoggable
    {
        /// <summary>
        /// Webhook that has been connected to
        /// </summary>
        public DiscordWebhook Webhook { get; private set; }

        internal readonly WebhookConnection Connection;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection">Connection for the webhook</param>
        public WebhookClient(WebhookConnection connection)
        {
            Connection = connection;
            Initialized = true;
            Rest = new RestHandler(Logger);
        }

        internal override void HandleConnect()
        {
            DiscordWebhook.GetWebhookWithToken(_clients[0], Connection.WebhookId, Connection.WebhookToken)
                .Then(webhook => Webhook = webhook)
                .Catch(ex => Logger.Exception("An error occured connecting the webhook", ex));
        }

        internal override void HandleShutdown()
        {
            Logger.Debug($"{nameof(WebhookClient)}.{nameof(HandleShutdown)} Shutting down the webhook");
            Initialized = false;
            WebhookClientFactory.Instance.RemoveWebhook(this);
            ShutdownRest();
        }
        
        internal override void ResetRestApi()
        {
            try
            {
                Rest?.Shutdown();
            }
            finally
            {
                Rest = new RestHandler(Logger);
            }
        }
        
        ///<inheritdoc/>
        public void LogDebug(DebugLogger logger)
        {
            logger.AppendField("Webhook ID", Connection.WebhookId);
            logger.AppendField("Initialized", Initialized);
            logger.AppendFieldEnum("Log Level", Logger.LogLevel);
            logger.AppendField("Plugins", GetClientPluginList());
            logger.AppendObject("REST API", Rest);
        }
    }
}