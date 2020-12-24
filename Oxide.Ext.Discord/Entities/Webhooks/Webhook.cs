using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.REST;

namespace Oxide.Ext.Discord.Entities.Webhooks
{
    public class Webhook
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("type")]
        public WebhookType Type { get; set; }
        
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        
        [JsonProperty("user")]
        public User User { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }

        public static void CreateWebhook(DiscordClient client, string channelId, string name, string avatar, Action<Webhook> callback = null)
        {
            var jsonObj = new Dictionary<string, string>()
            {
                { "name", name },
                { "avatar", avatar }
            };

            client.REST.DoRequest($"/channels/{channelId}/webhooks", RequestMethod.POST, jsonObj, callback);
        }

        public static void GetChannelWebhooks(DiscordClient client, string channelId, Action<List<Webhook>> callback = null)
        {
            client.REST.DoRequest($"/channels/{channelId}/webhooks", RequestMethod.GET, null, callback);
        }

        public static void GetGuildWebhooks(DiscordClient client, string guildId, Action<List<Webhook>> callback = null)
        {
            client.REST.DoRequest($"/guilds/{guildId}/webhooks", RequestMethod.GET, null, callback);
        }

        public static void GetWebhook(DiscordClient client, string webhookId, Action<Webhook> callback = null)
        {
            client.REST.DoRequest($"/webhooks/{webhookId}", RequestMethod.GET, null, callback);
        }

        public static void GetWebhookWithToken(DiscordClient client, string webhookId, string webhookToken, Action<Webhook> callback = null)
        {
            client.REST.DoRequest($"/webhooks/{webhookId}/{webhookToken}", RequestMethod.GET, null, callback);
        }

        public void ModifyWebhook(DiscordClient client, string name, string avatar, Action<Webhook> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "name", name },
                { "avatar", avatar }
            };

            client.REST.DoRequest($"/webhooks/{Id}", RequestMethod.POST, jsonObj, callback);
        }

        public void ModifyWebhookWithToken(DiscordClient client, string name, string avatar, Action<Webhook> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "name", name },
                { "avatar", avatar }
            };

            client.REST.DoRequest<Webhook>($"/webhooks/{Id}/{Token}", RequestMethod.POST, jsonObj, callback);
        }

        public void DeleteWebhook(DiscordClient client, Action callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}", RequestMethod.DELETE, null, callback);
        }

        public void DeleteWebhookWithToken(DiscordClient client, Action callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{Token}", RequestMethod.DELETE, null, callback);
        }

        public void ExecuteWebhook(DiscordClient client, WebhookPayload payload, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{Token}?wait=true", RequestMethod.POST, payload, callback);
        }

        public void ExecuteWebhookSlack(DiscordClient client, WebhookPayload payload, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{Token}/slack?wait=true", RequestMethod.POST, payload, callback);
        }

        public void ExecuteWebhookGitHub(DiscordClient client, WebhookPayload payload, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{Token}/github?wait=true", RequestMethod.POST, payload, callback);
        }

        public void EditWebhookMessage(DiscordClient client, string messageId, WebhookPayload payload, Action<Message> callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{Token}/messages/{messageId}", RequestMethod.PATCH, payload, callback);
        }
        
        public void DeleteWebhookMessage(DiscordClient client, string messageId, Action callback = null)
        {
            client.REST.DoRequest($"/webhooks/{Id}/{Token}/messages/{messageId}", RequestMethod.DELETE, null, callback);
        }
    }
}
