using Newtonsoft.Json;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class ClientStatus
    {
        [JsonProperty("desktop")]
        public string Desktop { get; set; }
        
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        
        [JsonProperty("web")]
        public string Web { get; set; }
    }
}