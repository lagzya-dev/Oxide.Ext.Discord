using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    /// <summary>
    /// Discord Template for embed
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordEmbedTemplate
    {
        /// <summary>
        /// If this embed is enabled
        /// </summary>
        [JsonProperty("Show Embed")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// The Tile for the embed
        /// </summary>
        [JsonProperty("Embed Title")]
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// This Title Url for the embed
        /// </summary>
        [JsonProperty("Embed Title URL")]
        public string Url { get; set; } = string.Empty;
        
        /// <summary>
        /// The description of the embed
        /// </summary>
        [JsonProperty("Embed Description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The Hex Color for the embed
        /// </summary>
        [JsonProperty("Embed Hex Color")]
        public string Color { get; set; } = DiscordColor.Default.ToHex();
        
        /// <summary>
        /// Image URL to show in the embed
        /// </summary>
        [JsonProperty("Embed Image URL")]
        public string ImageUrl { get; set; } = string.Empty;
        
        /// <summary>
        /// Thumbnail url to show in the embed
        /// </summary>
        [JsonProperty("Embed Thumbnail URL")]
        public string ThumbnailUrl { get; set; } = string.Empty;
        
        /// <summary>
        /// Video url to show in the embed
        /// </summary>
        [JsonProperty("Embed View Url")]
        public string VideoUrl { get; set; } = string.Empty;

        /// <summary>
        /// Show timestamp in the embed
        /// </summary>
        [JsonProperty("Show Embed TimeStamp")]
        public bool TimeStamp { get; set; } = false;

        /// <summary>
        /// Fields for the embed
        /// </summary>
        [JsonProperty("Embed Fields")]
        public List<EmbedFieldTemplate> Fields { get; set; } = new List<EmbedFieldTemplate>();

        /// <summary>
        /// Footer for the embed
        /// </summary>
        [JsonProperty("Embed Footer")]
        public EmbedFooterTemplate Footer { get; set; } = new EmbedFooterTemplate();
    }
}