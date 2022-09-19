using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Libraries.Placeholders;

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
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Embed Footer Icon
        /// </summary>
        [JsonProperty("Footer Icon URL")]
        public string IconUrl { get; set; } = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public EmbedFooterTemplate() {}

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

        /// <summary>
        /// Converts the template to a <see cref="EmbedFooter"/>
        /// </summary>
        /// <param name="data">Data to use</param>
        /// <returns><see cref="EmbedFooter"/></returns>
        public EmbedFooter ToFooter(PlaceholderData data)
        {
            return new EmbedFooter(PlaceholderFormatting.ApplyPlaceholder(Text, data), PlaceholderFormatting.ApplyPlaceholder(IconUrl, data));
        }
    }
}