using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Guilds.Roles
{
    public class RoleTags
    {
        [JsonProperty("bot_id")]
        public string BotId { get; set; }
        
        [JsonProperty("integration_id")]
        public string IntegrationId { get; set; }
        
        [JsonProperty("premium_subscriber")]
        public bool PremiumSubscriber { get; set; }
    }
}