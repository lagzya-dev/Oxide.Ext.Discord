using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class LoadInteractionMessageTemplate<TTemplate, TEntity> : BaseAsyncCallback 
        where TTemplate : BaseMessageTemplate<TEntity>, new()
        where TEntity : class
    {
        private BaseMessageTemplatesLibrary<TTemplate, TEntity> _templates;
        private TemplateId _id;
        private DiscordInteraction _interaction;
        private IDiscordAsyncCallback<TTemplate> _callback;

        public static void Start(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<TTemplate> callback)
        {
            LoadInteractionMessageTemplate<TTemplate, TEntity> load = DiscordPool.Get<LoadInteractionMessageTemplate<TTemplate, TEntity>>();
            load.Init(templates, id, interaction, callback);
            load.Run();
        }
        
        private void Init(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<TTemplate> callback)
        {
            _templates = templates;
            _id = id;
            _interaction = interaction;
            _callback = callback;
        }

        protected override Task HandleCallback()
        {
            return _templates.HandleGetMessageTemplateAsync(_id, _interaction, _callback);
        }

        protected override void EnterPool()
        {
            _templates = null;
            _id = default(TemplateId);
            _interaction = null;
            _callback = null;
        }

        protected override void DisposeInternal()
        {
            _callback?.Dispose();
            DiscordPool.Free(this);
        }
    }
}