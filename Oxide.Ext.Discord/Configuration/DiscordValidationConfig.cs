using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Configuration
{
    /// <summary>
    /// Discord Validation Config
    /// </summary>
    public class DiscordValidationConfig
    {
        [JsonProperty("Enable Request Validation")]
        public bool EnableValidation { get; set; }
    }
}