using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Configuration
{
    /// <summary>
    /// IP data config
    /// </summary>
    public class DiscordIpConfig
    {
        /// <summary>
        /// How many days to store IP data
        /// </summary>
        [JsonProperty("Save IP Data Duration (Days)")]
        public float StoreIpDuration { get; set; }
    }
}