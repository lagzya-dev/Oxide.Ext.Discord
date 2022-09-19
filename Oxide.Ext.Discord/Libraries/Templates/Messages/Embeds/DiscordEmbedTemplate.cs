using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Templates.Messages;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    /// <summary>
    /// Represents a template that can be used by the <see cref="DiscordEmbedTemplates"/>
    /// </summary>
    public class DiscordEmbedTemplate : BaseEmbedTemplate
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DiscordEmbedTemplate() : base(TemplateType.Embed, new TemplateVersion(1, 0, 0))
        {
            
        }

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

        /// <summary>
        /// Converts the template type {T} async
        /// </summary>
        /// <param name="data"></param>
        /// <param name="embed"></param>
        /// <returns></returns>
        public IDiscordAsyncCallback<DiscordEmbed> ToEmbedAsync(PlaceholderData data, DiscordEmbed embed = null)
        {
            return ToEmbedInternalAsync(data, embed, PluginAsyncCallback<DiscordEmbed>.Create());
        }
        
        internal IDiscordAsyncCallback<DiscordEmbed> ToEmbedInternalAsync(PlaceholderData data, DiscordEmbed embed = null, IDiscordAsyncCallback<DiscordEmbed> callback = null)
        {
            if (callback == null)
            {
                callback = InternalAsyncCallback<DiscordEmbed>.Create();
            }
            
            ToEmbedCallback.Start(this, data, embed, callback);
            return callback;
        }

        internal async Task HandleToEmbedAsync(PlaceholderData data, DiscordEmbed embed, IDiscordAsyncCallback<DiscordEmbed> callback)
        {
            DiscordEmbed result = await Task.FromResult(ToEmbed(data, embed)).ConfigureAwait(false); 
            callback.InvokeSuccess(result);
        }
    }
}