using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class BulkTemplateToEntityCallback<TTemplate, TEntity, TTarget> : BaseAsyncCallback 
        where TTemplate : BaseMessageTemplate<TEntity>, new()
        where TEntity : class
        where TTarget : class, TEntity, new()
    {
        private BaseMessageTemplateLibrary<TTemplate, TEntity> _templates;
        private TemplateId _id;
        private DiscordInteraction _interaction;
        private BulkTemplateRequest _request;
        private List<TTarget> _entities;
        private IDiscordPromise<List<TTarget>> _promise;

        public static void Start(BaseMessageTemplateLibrary<TTemplate, TEntity> templates, TemplateId id, BulkTemplateRequest request, List<TTarget> entities, IDiscordPromise<List<TTarget>> promise)
        {
            BulkTemplateToEntityCallback<TTemplate, TEntity, TTarget> load = DiscordPool.Internal.Get<BulkTemplateToEntityCallback<TTemplate, TEntity, TTarget>>();
            load.Init(templates, id, null, request, entities, promise);
            load.Run();
        }
        
        public static void Start(BaseMessageTemplateLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, BulkTemplateRequest request, List<TTarget> entities, IDiscordPromise<List<TTarget>> promise)
        {
            BulkTemplateToEntityCallback<TTemplate, TEntity, TTarget> load = DiscordPool.Internal.Get<BulkTemplateToEntityCallback<TTemplate, TEntity, TTarget>>();
            load.Init(templates, id, interaction, request, entities, promise);
            load.Run();
        }
        
        private void Init(BaseMessageTemplateLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, BulkTemplateRequest request, List<TTarget> entities, IDiscordPromise<List<TTarget>> promise)
        {
            _templates = templates;
            _id = id;
            _interaction = interaction;
            _request = request;
            _entities = entities;
            _promise = promise;
        }
        
        protected override Task HandleCallback()
        {
            _templates.HandleGetLocalizedBulkEntity(_id, _request, _interaction, _entities, _promise);
            return Task.CompletedTask;
        }
        
        protected override string GetExceptionMessage()
        {
            return $"Template ID: {_id.ToString()} ";
        }

        protected override void EnterPool()
        {
            _templates = null;
            _id = default(TemplateId);
            _interaction = null;
            _request = null;
            _promise = null;
        }
    }
}