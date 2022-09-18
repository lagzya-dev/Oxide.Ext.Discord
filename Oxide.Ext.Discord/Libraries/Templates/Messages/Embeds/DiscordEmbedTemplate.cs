using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Exceptions.Entities.Messages;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Placeholders;

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

        [JsonConstructor]
        public DiscordEmbedTemplate() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="titleUrl"></param>
        public DiscordEmbedTemplate(string title = "", string description = "", string titleUrl = "")
        {
            Title = title;
            Description = description;
            Url = titleUrl;
        }

        public DiscordEmbed ToEmbed(PlaceholderData data)
        {
            DiscordEmbed embed = new DiscordEmbed
            {
                Title = PlaceholderFormatting.ApplyPlaceholder(Title, data),
                Url = PlaceholderFormatting.ApplyPlaceholder(Url, data),
                Description = PlaceholderFormatting.ApplyPlaceholder(Description, data),
                Color = !string.IsNullOrEmpty(Color) ? new DiscordColor(PlaceholderFormatting.ApplyPlaceholder(Color, data)) : (DiscordColor?)null,
                Timestamp = TimeStamp ? DateTime.UtcNow : (DateTime?)null
            };

            if (!string.IsNullOrEmpty(ImageUrl))
            {
                embed.Image = new EmbedImage
                {
                    Url = PlaceholderFormatting.ApplyPlaceholder(Url, data)
                };
            }

            if (!string.IsNullOrEmpty(ThumbnailUrl))
            {
                embed.Thumbnail = new EmbedThumbnail
                {
                    Url = PlaceholderFormatting.ApplyPlaceholder(ThumbnailUrl, data)
                };
            }

            if (!string.IsNullOrEmpty(VideoUrl))
            {
                embed.Video = new EmbedVideo
                {
                    Url = PlaceholderFormatting.ApplyPlaceholder(ThumbnailUrl, data)
                };
            }

            if (Fields != null && Fields.Count != 0)
            {
                InvalidEmbedException.ThrowIfInvalidFieldCount(Fields.Count);
                embed.Fields = new List<EmbedField>();
                for (int index = 0; index < Fields.Count; index++)
                {
                    EmbedFieldTemplate field = Fields[index];
                    embed.Fields.Add(new EmbedField(PlaceholderFormatting.ApplyPlaceholder(field.Name, data), PlaceholderFormatting.ApplyPlaceholder(field.Value, data), field.Inline));
                }
            }

            if (Footer != null && Footer.Enabled)
            {
                embed.Footer = new EmbedFooter(PlaceholderFormatting.ApplyPlaceholder(Footer.Text, data), PlaceholderFormatting.ApplyPlaceholder(Footer.IconUrl, data));
            }

            return embed;
        }
    }
}