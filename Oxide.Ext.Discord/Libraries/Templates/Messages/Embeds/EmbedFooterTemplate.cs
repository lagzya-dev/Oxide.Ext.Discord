using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    /// <summary>
    /// Discord Template for Embed Footer
    /// </summary>
    public class EmbedFooterTemplate
    {
        /// <summary>
        /// Show Embed Footer
        /// </summary>
        [JsonProperty("Show Footer")]
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Embed Footer Text
        /// </summary>
        [JsonProperty("Footer Text")]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Embed Footer Icon
        /// </summary>
        [JsonProperty("Footer Icon URL")]
        public string IconUrl { get; set; } = string.Empty;
    }
}