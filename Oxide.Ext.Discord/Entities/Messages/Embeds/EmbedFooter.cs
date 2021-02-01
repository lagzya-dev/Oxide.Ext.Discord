using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages.Embeds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-footer-structure">Embed Footer Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class EmbedFooter
    {
        /// <summary>
        /// Footer text
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Url of footer icon (only supports http(s) and attachments)
        /// </summary>
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// A proxied url of footer icon
        /// </summary>
        [JsonProperty("proxy_icon_url")]
        public string ProxyIconUrl { get; set; }
    }
}