using System.Collections.Generic;
using Newtonsoft.Json;
namespace Oxide.Ext.Discord.Configuration
{
    /// <summary>
    /// Represents Discord Extension Logging Config
    /// </summary>
    internal class DiscordLoggingConfig
    {
        /// <summary>
        /// Discord Response Error codes that will not be logged
        /// </summary>
        [JsonProperty("Hide Discord Error Codes")]
        public HashSet<int> HideDiscordErrorCodes { get; set; }
    }
}