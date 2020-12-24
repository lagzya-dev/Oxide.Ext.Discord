using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities
{
    public class RateLimit
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("retry_after")]
        public int RetryAfter { get; set; }

        [JsonProperty("global")]
        public bool Global { get; set; }
    }
}
