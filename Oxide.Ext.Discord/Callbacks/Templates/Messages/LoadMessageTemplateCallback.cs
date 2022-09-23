using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class LoadMessageTemplateCallback<TTemplate, TEntity> : BaseAsyncCallback 
        where TTemplate : BaseMessageTemplate<TEntity>, new()
        where TEntity : class
    {
        private BaseMessageTemplatesLibrary<TTemplate, TEntity> _templates;
        private TemplateId _id;
        private DiscordInteraction _interaction;
        private DiscordAsyncCallback<TTemplate> _callback;

        public static void Start(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordAsyncCallback<TTemplate> callback)
        {
            LoadMessageTemplateCallback<TTemplate, TEntity> load = DiscordPool.Get<LoadMessageTemplateCallback<TTemplate, TEntity>>();
            load.Init(templates, id, null, callback);
            load.Run();
        }
        
        public static void Start(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id,  DiscordInteraction interaction, DiscordAsyncCallback<TTemplate> callback)
        {
            LoadMessageTemplateCallback<TTemplate, TEntity> load = DiscordPool.Get<LoadMessageTemplateCallback<TTemplate, TEntity>>();
            load.Init(templates, id, interaction, callback);
            load.Run();
        }
        
        private void Init(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, DiscordAsyncCallback<TTemplate> callback)
        {
            _templates = templates;
            _id = id;
            _interaction = interaction;
            _callback = callback;
        }

        protected override Task HandleCallback()
        {
            return _templates.HandleGetLocalizedTemplateAsync(_id, _interaction, _callback);
        }

        protected override void EnterPool()
        {
            _templates = null;
            _id = default(TemplateId);
            _callback = null;
        }

        protected override void DisposeInternal()
        {
            _callback?.Dispose();
            DiscordPool.Free(this);
        }
    }
}