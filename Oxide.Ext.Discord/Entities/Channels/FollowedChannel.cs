using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Channels
{
    public class FollowedChannel
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        
        [JsonProperty("webhook_id")]
        public string WebhookId { get; set; }
    }
}