using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    /// <summary>
    /// Callback for parsing a <see cref="DiscordEmbedTemplate"/> to a <see cref="DiscordEmbed"/>
    /// </summary>
    public class ToEmbedCallback : BaseAsyncCallback
    {
        private DiscordEmbedTemplate _template;
        private PlaceholderData _data;
        private DiscordEmbed _embed;
        private IDiscordAsyncCallback<DiscordEmbed> _callback;

        /// <summary>
        /// Starts the callback
        /// </summary>
        /// <param name="template"></param>
        /// <param name="data"></param>
        /// <param name="embed"></param>
        /// <param name="callback"></param>
        public static void Start(DiscordEmbedTemplate template, PlaceholderData data, DiscordEmbed embed, IDiscordAsyncCallback<DiscordEmbed> callback)
        {
            ToEmbedCallback handler = DiscordPool.Get<ToEmbedCallback>();
            handler.Init(template, data, embed, callback);
            handler.Run();
        }
        
        private void Init(DiscordEmbedTemplate template, PlaceholderData data, DiscordEmbed embed, IDiscordAsyncCallback<DiscordEmbed> callback)
        {
            _template = template;
            _data = data;
            _embed = embed;
            _callback = callback;
        }
        
        ///<inheritdoc/>
        protected override Task HandleCallback()
        {
            return _template.HandleToEmbedAsync(_data, _embed, _callback);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _template = null;
            _data = null;
            _embed = null;
            _callback = null;
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}