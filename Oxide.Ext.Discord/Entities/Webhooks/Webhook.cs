using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Users;
namespace Oxide.Ext.Discord.Entities.Webhooks
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/webhook#webhook-object">Webhook Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Webhook
    {
        /// <summary>
        /// The id of the webhook
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// The type of the webhook
        /// See <see cref="WebhookType"/>
        /// </summary>
        [JsonProperty("type")]
        public WebhookType Type { get; set; }
        
        /// <summary>
        /// The guild id this webhook is for
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }
        
        /// <summary>
        /// The channel id this webhook is for
        /// </summary>
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        
        /// <summary>
        /// The user this webhook was created by
        /// not returned when getting a webhook with its token
        /// </summary>
        [JsonProperty("user")]
        public DiscordUser User { get; set; }
        
        /// <summary>
        /// The default name of the webhook
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// the default user avatar hash of the webhook
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        
        /// <summary>
        /// The secure token of the webhook
        /// returned for Incoming Webhooks
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
        
        /// <summary>
        /// The bot/OAuth2 application that created this webhook
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
        
        /// <summary>
        /// The guild of the channel that this webhook is following (returned for Channel Follower Webhooks)
        /// </summary>
        [JsonProperty("source_guild")]
        public Guild SourceGuild { get; set; }
        
        /// <summary>
        /// The channel that this webhook is following (returned for Channel Follower Webhooks)
        /// </summary>
        [JsonProperty("source_channel")]
        public Snowflake SourceChannel { get; set; }

        /// <summary>
        /// Create a new webhook.
        /// Requires the MANAGE_WEBHOOKS permission.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#create-webhook">Create Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID for the webhook</param>
        /// <param name="name">Name of the webhook (1-80 characters)</param>
        /// <param name="avatar">Image for the default webhook avatar</param>
        /// <param name="callback">Callback with the completed webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void CreateWebhook(DiscordClient client, Snowflake channelId, string name, string avatar = null, Action<Webhook> callback = null, Action<RestError> error = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["name"] = name,
                ["avatar"] = avatar
            };

            client.Bot.Rest.DoRequest($"/channels/{channelId}/webhooks", RequestMethod.POST, data, callback, error);
        }

        /// <summary>
        /// Returns a list of channel webhook.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-channel-webhooks">Get Channel Webhooks</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to get webhooks for</param>
        /// <param name="callback">Callback with a list of channel webhooks</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetChannelWebhooks(DiscordClient client, Snowflake channelId, Action<List<Webhook>> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{channelId}/webhooks", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns a list of guild webhooks
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-guild-webhooks">Get Guild Webhooks</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to get webhooks for</param>
        /// <param name="callback">Callback with the list of guild webhooks</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetGuildWebhooks(DiscordClient client, Snowflake guildId, Action<List<Webhook>> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{guildId}/webhooks", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns the webhook with the given webhook ID
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-webhook">Get Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="webhookId">Webhook ID to get</param>
        /// <param name="callback">Callback with the webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetWebhook(DiscordClient client, Snowflake webhookId, Action<Webhook> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{webhookId}", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns the webhook with the given ID &amp; Token
        /// This call does not required authentication
        /// No user is returned in webhook object
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-webhook-with-token">Get Webhook with Token</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="webhookId">Webhook ID to get</param>
        /// <param name="webhookToken">Webhook Token</param>
        /// <param name="callback">Callback with the webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetWebhookWithToken(DiscordClient client, Snowflake webhookId, string webhookToken, Action<Webhook> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{webhookId}/{webhookToken}", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns the webhook with the given ID &amp; Token
        /// This call does not required authentication
        /// No user is returned in webhook object
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-webhook-with-token">Get Webhook with Token</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="webhookUrl">Returns the webhook for the specified URL</param>
        /// <param name="callback">Callback with the webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetWebhookWithUrl(DiscordClient client, string webhookUrl, Action<Webhook> callback = null, Action<RestError> error = null)
        {
            string[] webhookInfo = webhookUrl.Split('/');
            string id = webhookInfo[webhookInfo.Length - 2];
            string token = webhookInfo[webhookInfo.Length - 1];
            
            client.Bot.Rest.DoRequest($"/webhooks/{id}/{token}", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Modify a webhook.
        /// Requires the MANAGE_WEBHOOKS permission.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#modify-webhook">Modify Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="name">New webhook name</param>
        /// <param name="avatar">New avatar image</param>
        /// <param name="channelId">Channel to move the webhook to</param>
        /// <param name="callback">Callback with the updated webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ModifyWebhook(DiscordClient client, string name = null, string avatar = null, Snowflake? channelId = null, Action<Webhook> callback = null, Action<RestError> error = null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["name"] = name ,
                ["avatar"] = avatar,
                ["channel_id"] = channelId
            };

            client.Bot.Rest.DoRequest($"/webhooks/{Id}", RequestMethod.PATCH, data, callback, error);
        }

        /// <summary>
        /// Modify a webhook.
        /// Requires the MANAGE_WEBHOOKS permission.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#modify-webhook-with-token">Modify Webhook with Token</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="name">New webhook name</param>
        /// <param name="avatar">New avatar image</param>
        /// <param name="callback">Callback with the updated webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ModifyWebhookWithToken(DiscordClient client, string name = null, string avatar = null, Action<Webhook> callback = null, Action<RestError> error = null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["name"] = name,
                ["avatar"] = avatar
            };

            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}", RequestMethod.PATCH, data, callback, error);
        }

        /// <summary>
        /// Delete a webhook permanently.
        /// Requires the MANAGE_WEBHOOKS permission.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#delete-webhook">Delete Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteWebhook(DiscordClient client, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Delete a webhook permanently.
        /// Does not require authentication.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#delete-webhook-with-token">Delete Webhook with Token</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteWebhookWithToken(DiscordClient client, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Executes a webhook
        /// See <a href="https://discord.com/developers/docs/resources/webhook#execute-webhook">Execute Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="payload">Message data</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="sendType">Which type of webhook to execute</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ExecuteWebhook(DiscordClient client, WebhookCreateMessage payload, Action callback = null, Action<RestError> error = null, WebhookSendType sendType = WebhookSendType.Discord)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}{GetWebhookFormat(sendType)}", RequestMethod.POST, payload, callback, error);
        }
        
        /// <summary>
        /// Executes a webhook
        /// See <a href="https://discord.com/developers/docs/resources/webhook#execute-webhook">Execute Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="payload">Message data</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="sendType">Which type of webhook to execute</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ExecuteWebhook(DiscordClient client, WebhookCreateMessage payload, Action<DiscordMessage> callback, Action<RestError> error = null, WebhookSendType sendType = WebhookSendType.Discord)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}{GetWebhookFormat(sendType)}?wait=true", RequestMethod.POST, payload, callback, error);
        }

        /// <summary>
        /// Edits a previously-sent webhook message from the same token.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#edit-webhook-message">Edit Webhook Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID to edit</param>
        /// <param name="payload">The updated message</param>
        /// <param name="callback">Callback with the edited message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditWebhookMessage(DiscordClient client, Snowflake messageId, WebhookEditMessage payload, Action<DiscordMessage> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}/messages/{messageId}", RequestMethod.PATCH, payload, callback, error);
        }
        
        /// <summary>
        /// Deletes a message that was created by the webhook.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteWebhookMessage(DiscordClient client, Snowflake messageId, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}/messages/{messageId}", RequestMethod.DELETE, null, callback, error);
        }

        private string GetWebhookFormat(WebhookSendType type)
        {
            switch (type)
            {
                case WebhookSendType.Discord:
                    return string.Empty;
                case WebhookSendType.Slack:
                    return "/slack";
                case WebhookSendType.Github:
                    return "/github";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
