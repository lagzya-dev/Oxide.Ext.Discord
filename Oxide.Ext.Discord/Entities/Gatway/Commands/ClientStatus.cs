using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Commands
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