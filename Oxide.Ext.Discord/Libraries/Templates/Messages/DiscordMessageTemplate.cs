using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Templates.Messages;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Interfaces.Entities.Messages;
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
    public class DiscordMessageTemplate : BaseTemplate
    {
        /// <summary>
        /// String contents of the message
        /// </summary>
        [JsonProperty("Message Content")]
        public string Content { get; set; }

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
        public DiscordMessageTemplate() : base(TemplateType.Message, new TemplateVersion(1, 0, 0)) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        public DiscordMessageTemplate(string content = "") : this()
        {
            Content = content;
        }

        /// <summary>
        /// Converts the <see cref="DiscordMessageTemplate"/> to a {T} message
        /// {T} supports all message types
        /// </summary>
        /// <param name="message">{T} message to use when creating the message; if null a new {T} will be created</param>
        /// <returns><see cref="DiscordMessageTemplate"/> converted to type {T} message</returns>
        public T ToMessage<T>(T message = null) where T : class, IDiscordTemplateMessage, new()
        {
            return ToMessage(null, message);
        }

        /// <summary>
        /// Converts the <see cref="DiscordMessageTemplate"/> to a {T} message
        /// {T} supports all message types
        /// </summary>
        /// <param name="data">Placeholder Data for the template</param>
        /// <param name="message">{T} message to use when creating the message; if null a new {T} will be created</param>
        /// <returns><see cref="DiscordMessageTemplate"/> converted to type {T} message</returns>
        public T ToMessage<T>(PlaceholderData data, T message = null) where T : class, IDiscordTemplateMessage, new()
        {
            if (message == null)
            {
                message = new T();
            }

            if (!string.IsNullOrEmpty(Content))
            {
                message.Content = PlaceholderFormatting.ApplyPlaceholder(Content, data);
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

        /// <summary>
        /// Converts the template type {T} async
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDiscordAsyncCallback<T> ToMessageAsync<T>(PlaceholderData data, T message = null) where T : class, IDiscordTemplateMessage, new()
        {
            return ToMessageInternalAsync(data, message, PluginAsyncCallback<T>.Create());
        }
        
        internal IDiscordAsyncCallback<T> ToMessageInternalAsync<T>(PlaceholderData data, T message = null, IDiscordAsyncCallback<T> callback = null) where T : class, IDiscordTemplateMessage, new()
        {
            if (callback == null)
            {
                callback = InternalAsyncCallback<T>.Create();
            }
            
            ToMessageCallback<T>.Start(this, data, message, callback);
            return callback;
        }

        internal async Task HandleToMessageAsync<T>(PlaceholderData data, T message, IDiscordAsyncCallback<T> callback) where T : class, IDiscordTemplateMessage, new()
        {
            T result = await Task.FromResult(ToMessage(data, message)).ConfigureAwait(false); 
            callback.InvokeSuccess(result);
        }

        private List<DiscordEmbed> CreateEmbed(PlaceholderData data)
        {
            List<DiscordEmbed> embeds = new List<DiscordEmbed>();

            for (int index = 0; index < Embeds.Count; index++)
            {
                BaseEmbedTemplate template = Embeds[index];
                if (template.Enabled)
                {
                    embeds.Add(template.ToEmbed(data));
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