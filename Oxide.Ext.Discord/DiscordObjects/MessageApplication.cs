using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class MessageApplication
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("cover_image")]
        public string CoverImage { get; set; }      
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}