using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class IntegrationApplication
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("summary")]
        public string Summary { get; set; }
        
        [JsonProperty("bot")]
        public User Bot { get; set; }
    }
}