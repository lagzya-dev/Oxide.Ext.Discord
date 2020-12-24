using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Gatway.Commands
{
    public class Identify
    {
        [JsonProperty("token")]
        public string Token;

        [JsonProperty("properties")]
        public Properties Properties;

        [JsonProperty("compress")]
        public bool? Compress;

        [JsonProperty("large_threshold")]
        public int? LargeThreshold;

        [JsonProperty("shard")]
        public List<int> Shard;

        [JsonProperty("presence")]
        public StatusUpdate StatusUpdate;
        
        [JsonProperty("guild_subscriptions")]
        public bool? GuildSubscriptions { get; set; }
        
        [JsonProperty("intents")]
        public BotIntents Intents { get; set; }
    }

    public class Properties
    {
        [JsonProperty("$os")]
        public string OS;

        [JsonProperty("$browser")]
        public string Browser;

        [JsonProperty("$device")]
        public string Device;
    }
}
