using Newtonsoft.Json;

namespace Oxide.Ext.Discord.REST
{
    public class DiscordApiError
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}