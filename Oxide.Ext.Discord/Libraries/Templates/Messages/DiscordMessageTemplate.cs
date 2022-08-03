using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Interfaces.Entities.Messages;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Components;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Discord Message Template for sending localized Discord Messages
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordMessageTemplate : BaseTemplate
    {
        /// <summary>
        /// String contents of the message
        /// </summary>
        [JsonProperty("Message Content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Embeds for the message
        /// </summary>
        [JsonProperty("Message Embeds")]
        public List<DiscordEmbedTemplate> Embeds { get; set; } = new List<DiscordEmbedTemplate>();

        /// <summary>
        /// Buttons for the message
        /// </summary>
        [JsonProperty("Message Buttons")]
        public List<ButtonTemplate> Buttons { get; set; } = new List<ButtonTemplate>();

        /// <summary>
        /// Converts the <see cref="DiscordMessageTemplate"/> to a {T} message
        /// {T} supports all message types
        /// </summary>
        /// <param name="message">{T} message to use when creating the message; if null a new {T} will be created</param>
        /// <returns><see cref="DiscordMessageTemplate"/> converted to type {T} message</returns>
        public T ToMessage<T>(T message = null) where T : class, IDiscordTemplateMessage, new()
        {
            if (message == null)
            {
                message = new T();
            }
            
            message.Content = Content;
            if (Embeds != null && Embeds.Count != 0)
            {
                message.Embeds = new List<DiscordEmbed>();
                
                for (int index = 0; index < Embeds.Count; index++)
                {
                    DiscordEmbedTemplate embedTemplate = Embeds[index];
                    if (embedTemplate.Enabled)
                    {
                        message.Embeds.Add(CreateEmbed(embedTemplate));
                    }
                }
            }

            if (Buttons != null && Buttons.Count != 0)
            {
                message.Components = CreateButtons();
            }

            return message;
        }
        
        private static DiscordEmbed CreateEmbed(DiscordEmbedTemplate template)
        {
            DiscordEmbed embed = new DiscordEmbed
            {
                Title = template.Title,
                Url = template.Url,
                Description = template.Description,
                Color = !string.IsNullOrEmpty(template.Color) ? new DiscordColor(template.Color) : (DiscordColor?)null,
                Timestamp = template.TimeStamp ? DateTime.UtcNow : (DateTime?)null
            };
            if (!string.IsNullOrEmpty(template.ImageUrl))
            {
                embed.Image = new EmbedImage
                {
                    Url = template.Url
                };
            }
            if (!string.IsNullOrEmpty(template.ThumbnailUrl))
            {
                embed.Thumbnail = new EmbedThumbnail
                {
                    Url = template.ThumbnailUrl
                };
            }
            if (!string.IsNullOrEmpty(template.VideoUrl))
            {
                embed.Video = new EmbedVideo
                {
                    Url = template.ThumbnailUrl
                };
            }

            if (template.Fields != null && template.Fields.Count != 0)
            {
                embed.Fields = new List<EmbedField>();
                foreach (EmbedFieldTemplate field in template.Fields)
                {
                    embed.Fields.Add(new EmbedField(field.Name, field.Value, field.Inline));
                }
            }

            if (template.Footer != null && template.Footer.Enabled)
            {
                embed.Footer = new EmbedFooter(template.Footer.Text, template.Footer.IconUrl);
            }

            return embed;
        }

        private List<ActionRowComponent> CreateButtons()
        {
            List<ActionRowComponent> rows = new List<ActionRowComponent>();
            ActionRowComponent active = new ActionRowComponent();
            rows.Add(active);
            for (int index = 0; index < Buttons.Count; index++)
            {
                ButtonTemplate button = Buttons[index];
                if (index != 0 && !button.Inline || active.Components.Count == 5)
                {
                    InvalidMessageComponentException.ThrowIfInvalidMaxActionRows(rows.Count);
                    active = new ActionRowComponent();
                    rows.Add(active);
                }
                
                active.Components.Add(button.ToButton());
            }

            return rows;
        }
    }
}