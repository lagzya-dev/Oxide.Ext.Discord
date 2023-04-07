using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Interfaces.Entities.Templates;
using Oxide.Ext.Discord.Json.Converters;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Components;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Discord Message Template for sending localized Discord Messages
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordMessageTemplate : BaseMessageTemplate<IDiscordMessageTemplate>
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
        public List<MessageEmbedTemplate> Embeds { get; set; } = new List<MessageEmbedTemplate>();

        /// <summary>
        /// Buttons for the message
        /// </summary>
        [JsonConverter(typeof(TemplateComponentsConverter))]
        [JsonProperty("Message Components")]
        public List<BaseComponentTemplate> Components { get; set; } = new List<BaseComponentTemplate>();

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public DiscordMessageTemplate() : base(new TemplateVersion(1, 0, 0)) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        public DiscordMessageTemplate(string content) : this()
        {
            Content = content ?? string.Empty;
        }

        /// <summary>
        /// Converts the <see cref="DiscordMessageTemplate"/> to a {T} message
        /// {T} supports all message types
        /// </summary>
        /// <param name="data">Placeholder Data for the template</param>
        /// <param name="message">{T} message to use when creating the message; if null a new {T} will be created</param>
        /// <returns><see cref="DiscordMessageTemplate"/> converted to type {T} message</returns>
        public T ToMessage<T>(PlaceholderData data = null, T message = null) where T : class, IDiscordMessageTemplate, new()
        {
            if (message == null)
            {
                message = new T();
            }

            ToEntity(data, message);
            return message;
        }

        /// <summary>
        /// Returns a <see cref="IDiscordMessageTemplate"/> entity for the given template
        /// </summary>
        /// <param name="data"><see cref="PlaceholderData"/> for the template</param>
        /// <param name="message">Entity to apply the template to</param>
        /// <returns></returns>
        public override IDiscordMessageTemplate ToEntity(PlaceholderData data, IDiscordMessageTemplate message)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                message.Content = PlaceholderFormatting.ApplyPlaceholder(Content, data);
            }

            if (Embeds != null && Embeds.Count != 0)
            {
                message.Embeds = CreateEmbeds(data);
            }

            if (Components != null && Components.Count != 0)
            {
                message.Components = CreateComponents(data);
            }

            return message;
        }

        private List<DiscordEmbed> CreateEmbeds(PlaceholderData data)
        {
            List<DiscordEmbed> embeds = new List<DiscordEmbed>();

            for (int index = 0; index < Embeds.Count; index++)
            {
                MessageEmbedTemplate template = Embeds[index];
                if (template.Enabled)
                {
                    embeds.Add(template.ToEntity(data));
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
                    active.Components.Add(button.ToComponent(data));
                    
                    if (!button.Inline || active.Components.Count == 5)
                    {
                        InvalidMessageComponentException.ThrowIfInvalidMaxActionRows(rows.Count);
                        active = new ActionRowComponent();
                        rows.Add(active);
                    }
                } 
                else if (component is SelectMenuTemplate selectMenu)
                {
                    active.Components.Add(selectMenu.ToComponent(data));
                    InvalidMessageComponentException.ThrowIfInvalidMaxActionRows(rows.Count);
                    active = new ActionRowComponent();
                    rows.Add(active);
                }
            }

            return rows;
        }
    }
}