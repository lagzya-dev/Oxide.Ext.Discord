using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Async.Templates;
using Oxide.Ext.Discord.Callbacks.Async.Templates.Messages;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Interfaces.Entities.Messages;
using Oxide.Ext.Discord.Json.Converters;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Components;
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
        [JsonConverter(typeof(TemplateComponentsConverter))]
        [JsonProperty("Message Components")]
        public List<BaseComponentTemplate> Components { get; set; } = new List<BaseComponentTemplate>();

        /// <summary>
        /// Converts the <see cref="DiscordMessageTemplate"/> to a {T} message
        /// {T} supports all message types
        /// </summary>
        /// <param name="message">{T} message to use when creating the message; if null a new {T} will be created</param>
        /// <returns><see cref="DiscordMessageTemplate"/> converted to type {T} message</returns>
        public T ToMessage<T>(T message = null) where T : class, IDiscordTemplateMessage, new()
        {
            return ToPlaceholderMessage(null, message);
        }

        /// <summary>
        /// Converts the <see cref="DiscordMessageTemplate"/> to a {T} message
        /// {T} supports all message types
        /// </summary>
        /// <param name="data">Placeholder Data for the template</param>
        /// <param name="message">{T} message to use when creating the message; if null a new {T} will be created</param>
        /// <returns><see cref="DiscordMessageTemplate"/> converted to type {T} message</returns>
        public T ToPlaceholderMessage<T>(PlaceholderData data, T message = null) where T : class, IDiscordTemplateMessage, new()
        {
            if (message == null)
            {
                message = new T();
            }

            if (!string.IsNullOrEmpty(Content))
            {
                message.Content = ApplyPlaceholder(Content, data);
            }

            if (Embeds != null && Embeds.Count != 0)
            {
                message.Embeds = CreateEmbed(data);
            }

            if (Components != null && Components.Count != 0)
            {
                message.Components = CreateComponents(data);
            }

            return message;
        }

        public IDiscordAsyncCallback<T> ToPlaceholderMessageAsync<T>(PlaceholderData data, T message = null) where T : class, IDiscordTemplateMessage, new()
        {
            return ToPlaceholderMessageAsyncInternal(data, message, DiscordAsyncCallback<T>.Create());
        }
        
        internal IDiscordAsyncCallback<T> ToPlaceholderMessageAsyncInternal<T>(PlaceholderData data, T message = null, IDiscordAsyncCallback<T> callback = null) where T : class, IDiscordTemplateMessage, new()
        {
            if (callback == null)
            {
                callback = InternalAsyncCallback<T>.Create();
            }
            
            ToPlaceholderMessageCallback<T> handler = ToPlaceholderMessageCallback<T>.Create(this, data, message, callback);
            handler.Run();
            return callback;
        }

        internal Task<T> ToPlaceholderMessageInternalAsync<T>(PlaceholderData data, T message = null) where T : class, IDiscordTemplateMessage, new()
        {
            return Task.FromResult(ToPlaceholderMessage(data, message));
        }

        private List<DiscordEmbed> CreateEmbed(PlaceholderData data)
        {
            List<DiscordEmbed> embeds = new List<DiscordEmbed>();

            for (int index = 0; index < Embeds.Count; index++)
            {
                DiscordEmbedTemplate template = Embeds[index];
                if (!template.Enabled)
                {
                    continue;
                }

                DiscordEmbed embed = new DiscordEmbed
                {
                    Title = ApplyPlaceholder(template.Title, data),
                    Url = ApplyPlaceholder(template.Url, data),
                    Description = ApplyPlaceholder(template.Description, data),
                    Color = !string.IsNullOrEmpty(template.Color) ? new DiscordColor(ApplyPlaceholder(template.Color, data)) : (DiscordColor?)null,
                    Timestamp = template.TimeStamp ? DateTime.UtcNow : (DateTime?)null
                };
                
                embeds.Add(embed);
                
                if (!string.IsNullOrEmpty(template.ImageUrl))
                {
                    embed.Image = new EmbedImage
                    {
                        Url = ApplyPlaceholder(template.Url, data)
                    };
                }
                
                if (!string.IsNullOrEmpty(template.ThumbnailUrl))
                {
                    embed.Thumbnail = new EmbedThumbnail
                    {
                        Url = ApplyPlaceholder(template.ThumbnailUrl, data)
                    };
                }
                
                if (!string.IsNullOrEmpty(template.VideoUrl))
                {
                    embed.Video = new EmbedVideo
                    {
                        Url = ApplyPlaceholder(template.ThumbnailUrl, data)
                    };
                }

                if (template.Fields != null && template.Fields.Count != 0)
                {
                    embed.Fields = new List<EmbedField>();
                    foreach (EmbedFieldTemplate field in template.Fields)
                    {
                        embed.Fields.Add(new EmbedField(ApplyPlaceholder(field.Name, data), ApplyPlaceholder(field.Value, data), field.Inline));
                    }
                }

                if (template.Footer != null && template.Footer.Enabled)
                {
                    embed.Footer = new EmbedFooter(ApplyPlaceholder(template.Footer.Text, data), ApplyPlaceholder(template.Footer.IconUrl, data));
                }

            }

            return embeds;
        }

        private List<ActionRowComponent> CreateComponents(PlaceholderData data)
        {
            List<ActionRowComponent> rows = new List<ActionRowComponent>();
            ActionRowComponent active = new ActionRowComponent();
            rows.Add(active);
            for (int index = 0; index < Components.Count; index++)
            {
                BaseComponentTemplate component = Components[index];
                if (!component.Visible)
                {
                    continue;
                }

                if (component is ButtonTemplate button)
                {
                    active.Components.Add(button.ToButton(data));
                    
                    if (index != 0 && !button.Inline || active.Components.Count == 5)
                    {
                        InvalidMessageComponentException.ThrowIfInvalidMaxActionRows(rows.Count);
                        active = new ActionRowComponent();
                        rows.Add(active);
                    }
                } 
                else if (component is SelectMenuTemplate selectMenu)
                {
                    active.Components.Add(selectMenu.ToSelectMenu(data));
                    InvalidMessageComponentException.ThrowIfInvalidMaxActionRows(rows.Count);
                    active = new ActionRowComponent();
                    rows.Add(active);
                }
            }

            return rows;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string ApplyPlaceholder(string text, PlaceholderData value)
        {
            return value == null ? text : DiscordExtension.DiscordPlaceholders.ProcessPlaceholders(text, value);
        }
    }
}