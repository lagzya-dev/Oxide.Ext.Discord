using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Configuration
{
    /// <summary>
    /// Discord Validation Config
    /// </summary>
    public class DiscordValidationConfig
    {
        /// <summary>
        /// Enables request validation
        /// </summary>
        [JsonProperty("Enable Request Validation")]
        public bool EnableValidation { get; set; }
    }
}