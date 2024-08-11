using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Types;

namespace Oxide.Ext.Discord.Connections;

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
        ReverseStringTokenizer tokenizer = new(webhookUrl, "/");
        tokenizer.MoveNext();
        WebhookToken = tokenizer.Current.ToString();
        tokenizer.MoveNext();
        WebhookId = new Snowflake(tokenizer.Current);
        LogLevel = logLevel;
    }
}