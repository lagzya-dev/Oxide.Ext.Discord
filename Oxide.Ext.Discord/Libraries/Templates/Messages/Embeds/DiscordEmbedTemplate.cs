using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Exceptions.Entities.Messages;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    /// <summary>
    /// Discord Template for embed
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordEmbedTemplate : BaseMessageTemplate<DiscordEmbed>, IEmbedTemplate
    {
        ///<inheritdoc/>
        [JsonProperty("Show Embed")]
        public bool Enabled { get; set; } = true;

        ///<inheritdoc/>
        [JsonProperty("Embed Title")]
        public string Title { get; set; } = string.Empty;

        ///<inheritdoc/>
        [JsonProperty("Embed Title URL")]
        public string Url { get; set; } = string.Empty;

        ///<inheritdoc/>
        [JsonProperty("Embed Description")]
        public string Description { get; set; } = string.Empty;

        ///<inheritdoc/>
        [JsonProperty("Embed Hex Color")]
        public string Color { get; set; } = DiscordColor.Default.ToHex();

        ///<inheritdoc/>
        [JsonProperty("Embed Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        ///<inheritdoc/>
        [JsonProperty("Embed Thumbnail URL")]
        public string ThumbnailUrl { get; set; } = string.Empty;

        ///<inheritdoc/>
        [JsonProperty("Embed View Url")]
        public string VideoUrl { get; set; } = string.Empty;

        ///<inheritdoc/>
        [JsonProperty("Show Embed TimeStamp")]
        public bool TimeStamp { get; set; } = false;

        ///<inheritdoc/>
        [JsonProperty("Embed Fields")]
        public List<MessageEmbedFieldTemplate> Fields { get; set; } = new List<MessageEmbedFieldTemplate>();

        ///<inheritdoc/>
        [JsonProperty("Embed Footer")]
        public EmbedFooterTemplate Footer { get; set; } = new EmbedFooterTemplate();
        
        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public DiscordEmbedTemplate() : base(new TemplateVersion(1, 0, 0)) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="titleUrl"></param>
        public DiscordEmbedTemplate(string title, string description, string titleUrl = "") : this()
        {
            Title = title;
            Description = description;
            Url = titleUrl;
        }

        ///<inheritdoc cref="IEmbedTemplate.ToEntity"/>
        public override DiscordEmbed ToEntity(PlaceholderData data = null, DiscordEmbed entity = null)
        {
            return ToEntity(this, data, entity);
        }

        /// <summary>
        /// Converts the template to a <see cref="DiscordEmbed"/>
        /// </summary>
        /// <param name="template">Template to create the entity for</param>
        /// <param name="data">Data to use</param>
        /// <param name="embed">Initial embed to use</param>
        /// <returns></returns>
        internal static DiscordEmbed ToEntity(IEmbedTemplate template, PlaceholderData data, DiscordEmbed embed = null)
        {
            if (embed == null)
            {
                embed = new DiscordEmbed();
            }

            embed.Title = PlaceholderFormatting.ApplyPlaceholder(template.Title, data);
            embed.Url = PlaceholderFormatting.ApplyPlaceholder(template.Url, data);
            embed.Description = PlaceholderFormatting.ApplyPlaceholder(template.Description, data);
            embed.Color = !string.IsNullOrEmpty(template.Color) ? new DiscordColor(PlaceholderFormatting.ApplyPlaceholder(template.Color, data)) : (DiscordColor?)null;
            embed.Timestamp = template.TimeStamp ? DateTime.UtcNow : (DateTime?)null;

            if (!string.IsNullOrEmpty(template.ImageUrl))
            {
                embed.Image = new EmbedImage
                {
                    Url = PlaceholderFormatting.ApplyPlaceholder(template.Url, data)
                };
            }

            if (!string.IsNullOrEmpty(template.ThumbnailUrl))
            {
                embed.Thumbnail = new EmbedThumbnail
                {
                    Url = PlaceholderFormatting.ApplyPlaceholder(template.ThumbnailUrl, data)
                };
            }

            if (!string.IsNullOrEmpty(template.VideoUrl))
            {
                embed.Video = new EmbedVideo
                {
                    Url = PlaceholderFormatting.ApplyPlaceholder(template.ThumbnailUrl, data)
                };
            }

            if (template.Fields != null && template.Fields.Count != 0)
            {
                InvalidEmbedException.ThrowIfInvalidFieldCount(template.Fields.Count);
                embed.Fields = new List<EmbedField>();
                for (int index = 0; index < template.Fields.Count; index++)
                {
                    embed.Fields.Add(template.Fields[index].ToEntity(data));
                }
            }

            if (template.Footer != null && template.Footer.Enabled)
            {
                embed.Footer = template.Footer.ToFooter(data);
            }

            return embed;
        }
    }
}