using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    /// <summary>
    /// Callback for parsing a <see cref="DiscordEmbedFieldTemplate"/> to a <see cref="EmbedField"/>
    /// </summary>
    public class ToEmbedFieldCallback : BaseAsyncCallback
    {
        private DiscordEmbedFieldTemplate _template;
        private PlaceholderData _data;
        private EmbedField _field;
        private IDiscordAsyncCallback<EmbedField> _callback;

        /// <summary>
        /// Starts the callback
        /// </summary>
        /// <param name="template"></param>
        /// <param name="data"></param>
        /// <param name="field"></param>
        /// <param name="callback"></param>
        public static void Start(DiscordEmbedFieldTemplate template, PlaceholderData data, EmbedField field, IDiscordAsyncCallback<EmbedField> callback)
        {
            ToEmbedFieldCallback handler = DiscordPool.Get<ToEmbedFieldCallback>();
            handler.Init(template, data, field, callback);
            handler.Run();
        }
        
        private void Init(DiscordEmbedFieldTemplate template, PlaceholderData data, EmbedField field, IDiscordAsyncCallback<EmbedField> callback)
        {
            _template = template;
            _data = data;
            _field = field;
            _callback = callback;
        }
        
        ///<inheritdoc/>
        protected override Task HandleCallback()
        {
            return _template.HandleToEmbedFieldAsync(_data, _field, _callback);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _template = null;
            _data = null;
            _field = null;
            _callback = null;
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}