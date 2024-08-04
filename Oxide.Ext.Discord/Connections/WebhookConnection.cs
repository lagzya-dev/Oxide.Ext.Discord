using System;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Connections
{
    /// <summary>
    /// Connection for a webhook
    /// </summary>
    public class WebhookConnection
    {
        /// <summary>
        /// API token for the bot
        /// </summary>
        public readonly Snowflake WebhookId;

        /// <summary>
        /// Token for the webhook
        /// </summary>
        public readonly string WebhookToken;
        
        /// <summary>
        /// Discord Extension Logging Level.
        /// See <see cref="DiscordLogLevel"/>
        /// </summary>
        public DiscordLogLevel LogLevel { get; set; }

        /// <summary>
        /// Constructor for a webhook connection
        /// </summary>
        /// <param name="webhookUrl">URL of the webhook</param>
        /// <param name="logLevel">Log level for the webhook</param>
        public WebhookConnection(string webhookUrl, DiscordLogLevel logLevel = DiscordLogLevel.Info)
        {
            ReadOnlySpan<char> span = webhookUrl.AsSpan();
            span = span.TrimEnd('/');
            int index = span.LastIndexOf('/');
            WebhookToken = span.Slice(index + 1).ToString();
            span = span.Slice(0, index);
            index = span.LastIndexOf('/');
            WebhookId = new Snowflake(span.Slice(index + 1));
            LogLevel = logLevel;
        }
    }
}