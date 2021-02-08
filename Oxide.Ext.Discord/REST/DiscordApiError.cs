using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Oxide.Ext.Discord.REST
{
    public class DiscordApiError
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
        
        [JsonProperty("errors")]
        public JObject Errors { get; set; }
    }
}