using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class LoadMessageTemplateCallback<TTemplate, TEntity> : BaseAsyncCallback 
        where TTemplate : BaseMessageTemplate<TEntity>, new()
        where TEntity : class
    {
        private BaseMessageTemplatesLibrary<TTemplate, TEntity> _templates;
        private TemplateId _id;
        private DiscordInteraction _interaction;
        private IDiscordPromise<TTemplate> _promise;

        public static void Start(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, IDiscordPromise<TTemplate> promise)
        {
            LoadMessageTemplateCallback<TTemplate, TEntity> load = DiscordPool.Get<LoadMessageTemplateCallback<TTemplate, TEntity>>();
            load.Init(templates, id, null, promise);
            load.Run();
        }
        
        public static void Start(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id,  DiscordInteraction interaction, IDiscordPromise<TTemplate> promise)
        {
            LoadMessageTemplateCallback<TTemplate, TEntity> load = DiscordPool.Get<LoadMessageTemplateCallback<TTemplate, TEntity>>();
            load.Init(templates, id, interaction, promise);
            load.Run();
        }
        
        private void Init(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, IDiscordPromise<TTemplate> promise)
        {
            _templates = templates;
            _id = id;
            _interaction = interaction;
            _promise = promise;
        }

        protected override Task HandleCallback()
        {
            _templates.HandleGetLocalizedTemplateAsync(_id, _interaction, _promise);
            return Task.CompletedTask;
        }
        
        protected override string ExceptionData()
        {
            return $"Template ID: {_id.ToString()} ";
        }

        protected override void EnterPool()
        {
            _templates = null;
            _id = default(TemplateId);
            _promise = null;
        }
    }
}