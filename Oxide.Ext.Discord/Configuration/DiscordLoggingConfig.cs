using System.Collections.Generic;
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
        public HashSet<int> HiddenDiscordErrorCodes { get; set; }
    }
}