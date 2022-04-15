using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oxide.Ext.Discord.Logging;
namespace Oxide.Ext.Discord.Configuration
{
    /// <summary>
    /// Represents Discord Extension Logging Config
    /// </summary>
    internal class DiscordLoggingConfig
    {
        /// <summary>
        /// Server Console Log Level
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Server Console Log Level")]
        public DiscordLogLevel ConsoleLogLevel { get; set; }
        
        /// <summary>
        /// File Log Level
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("File Log Level")]
        public DiscordLogLevel FileLogLevel { get; set; }
        
        /// <summary>
        /// Discord Response Error codes that will not be logged
        /// </summary>
        [JsonProperty("Hide Discord Error Codes")]
        public HashSet<int> HideDiscordErrorCodes { get; set; }
    }
}