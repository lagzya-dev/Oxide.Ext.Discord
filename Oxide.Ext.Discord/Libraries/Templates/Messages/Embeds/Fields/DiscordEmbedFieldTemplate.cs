using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Templates.Messages;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields
{
    /// <summary>
    /// Represents a template that can be used by the <see cref="DiscordEmbedFieldTemplates"/> Library
    /// </summary>
    public class DiscordEmbedFieldTemplate : BaseEmbedFieldTemplate
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DiscordEmbedFieldTemplate() : base(TemplateType.EmbedField, new TemplateVersion(1, 0, 0)) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="inline"></param>
        public DiscordEmbedFieldTemplate(string name, string value, bool inline = true) : this()
        {
            Name = name;
            Value = value;
            Inline = inline;
        }

        /// <summary>
        /// Converts the template type {T} async
        /// </summary>
        /// <param name="data"></param>
        /// <param name="embed"></param>
        /// <returns></returns>
        public IDiscordAsyncCallback<EmbedField> ToEmbedFieldAsync(PlaceholderData data, EmbedField embed = null)
        {
            return ToEmbedFieldInternalAsync(data, embed, PluginAsyncCallback<EmbedField>.Create());
        }
        
        internal IDiscordAsyncCallback<EmbedField> ToEmbedFieldInternalAsync(PlaceholderData data, EmbedField embed = null, IDiscordAsyncCallback<EmbedField> callback = null)
        {
            if (callback == null)
            {
                callback = InternalAsyncCallback<EmbedField>.Create();
            }
            
            ToEmbedFieldCallback.Start(this, data, embed, callback);
            return callback;
        }

        internal async Task HandleToEmbedFieldAsync(PlaceholderData data, EmbedField embed, IDiscordAsyncCallback<EmbedField> callback)
        {
            EmbedField result = await Task.FromResult(ToField(data, embed)).ConfigureAwait(false); 
            callback.InvokeSuccess(result);
        }
    }
}