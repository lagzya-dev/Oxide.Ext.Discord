using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages.Embeds
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-thumbnail-structure">Embed Thumbnail Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class EmbedThumbnail
    {
        /// <summary>
        /// Source url of thumbnail (only supports http(s) and attachments)
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// A proxied url of the thumbnail
        /// </summary>
        [JsonProperty("proxy_url")]
        public string ProxyUrl { get; set; }

        /// <summary>
        /// Height of thumbnail
        /// </summary>
        [JsonProperty("height")]
        public int? Height { get; set; }

        /// <summary>
        /// Width of thumbnail
        /// </summary>
        [JsonProperty("width")]
        public int? Width { get; set; }
    }
}