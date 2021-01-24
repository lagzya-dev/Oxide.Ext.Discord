using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Webhooks
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Webhook
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        [JsonProperty("type")]
        public WebhookType Type { get; set; }
        
        [JsonProperty("guild_id")]
        public Snowflake GuildId { get; set; }
        
        [JsonProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        
        [JsonProperty("user")]
        public DiscordUser User { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }

        public static void CreateWebhook(DiscordClient client, string channelId, string name, string avatar, Action<Webhook> callback = null)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "name", name },
                { "avatar", avatar }
            };

            client.Bot.Rest.DoRequest($"/channels/{channelId}/webhooks", RequestMethod.POST, jsonObj, callback);
        }

        public static void GetChannelWebhooks(DiscordClient client, string channelId, Action<List<Webhook>> callback = null)
        {
            client.Bot.Rest.DoRequest($"/channels/{channelId}/webhooks", RequestMethod.GET, null, callback);
        }

        public static void GetGuildWebhooks(DiscordClient client, string guildId, Action<List<Webhook>> callback = null)
        {
            client.Bot.Rest.DoRequest($"/guilds/{guildId}/webhooks", RequestMethod.GET, null, callback);
        }

        public static void GetWebhook(DiscordClient client, string webhookId, Action<Webhook> callback = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{webhookId}", RequestMethod.GET, null, callback);
        }

        public static void GetWebhookWithToken(DiscordClient client, string webhookId, string webhookToken, Action<Webhook> callback = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{webhookId}/{webhookToken}", RequestMethod.GET, null, callback);
        }

        public void ModifyWebhook(DiscordClient client, string name, string avatar, Action<Webhook> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "name", name },
                { "avatar", avatar }
            };

            client.Bot.Rest.DoRequest($"/webhooks/{Id}", RequestMethod.POST, jsonObj, callback);
        }

        public void ModifyWebhookWithToken(DiscordClient client, string name, string avatar, Action<Webhook> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "name", name },
                { "avatar", avatar }
            };

            client.Bot.Rest.DoRequest<Webhook>($"/webhooks/{Id}/{Token}", RequestMethod.POST, jsonObj, callback);
        }

        public void DeleteWebhook(DiscordClient client, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}", RequestMethod.DELETE, null, callback);
        }

        public void DeleteWebhookWithToken(DiscordClient client, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}", RequestMethod.DELETE, null, callback);
        }

        public void ExecuteWebhook(DiscordClient client, WebhookCreateMessage payload, Action callback = null, WebhookSendType sendType = WebhookSendType.Default)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}{GetWebhookFormat(sendType)}", RequestMethod.POST, payload, callback);
        }
        
        public void ExecuteWebhook(DiscordClient client, WebhookCreateMessage payload, Action<Message> callback, WebhookSendType sendType = WebhookSendType.Default)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}{GetWebhookFormat(sendType)}?wait=true", RequestMethod.POST, payload, callback);
        }

        public void EditWebhookMessage(DiscordClient client, string messageId, WebhookEditMessage payload, Action<Message> callback = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}/messages/{messageId}", RequestMethod.PATCH, payload, callback);
        }
        
        public void DeleteWebhookMessage(DiscordClient client, string messageId, Action callback = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{Id}/{Token}/messages/{messageId}", RequestMethod.DELETE, null, callback);
        }

        private string GetWebhookFormat(WebhookSendType type)
        {
            switch (type)
            {
                case WebhookSendType.Default:
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
