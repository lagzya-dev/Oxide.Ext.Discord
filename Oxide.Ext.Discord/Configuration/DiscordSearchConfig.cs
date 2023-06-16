using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Configuration
{
    internal class DiscordSearchConfig
    {
        [JsonProperty("High Performance Player Search")]
        public bool HighPerformancePlayerSearchEnabled { get; set; }
    }
}