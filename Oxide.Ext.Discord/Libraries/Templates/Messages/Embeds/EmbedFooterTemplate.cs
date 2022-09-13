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
        public bool Enabled { get; set; }

        /// <summary>
        /// Embed Footer Text
        /// </summary>
        [JsonProperty("Footer Text")]
        public string Text { get; set; }

        /// <summary>
        /// Embed Footer Icon
        /// </summary>
        [JsonProperty("Footer Icon URL")]
        public string IconUrl { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="iconUrl"></param>
        /// <param name="enabled"></param>
        public EmbedFooterTemplate(string text = "", string iconUrl = "", bool enabled = false)
        {
            Text = text;
            IconUrl = iconUrl;
            Enabled = enabled;
        }
    }
}