using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class BulkTemplateToEntityCallback<TTemplate, TEntity> : BaseAsyncCallback 
        where TTemplate : BaseMessageTemplate<TEntity>, new()
        where TEntity : class
    {
        private BaseMessageTemplatesLibrary<TTemplate, TEntity> _templates;
        private TemplateId _id;
        private DiscordInteraction _interaction;
        private BulkTemplateRequest _request;
        private DiscordPromise<List<TEntity>> _promise;

        public static void Start(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, BulkTemplateRequest request, DiscordPromise<List<TEntity>> promise)
        {
            BulkTemplateToEntityCallback<TTemplate, TEntity> load = DiscordPool.Get<BulkTemplateToEntityCallback<TTemplate, TEntity>>();
            load.Init(templates, id, null, request, promise);
            load.Run();
        }
        
        public static void Start(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, BulkTemplateRequest request, DiscordPromise<List<TEntity>> promise)
        {
            BulkTemplateToEntityCallback<TTemplate, TEntity> load = DiscordPool.Get<BulkTemplateToEntityCallback<TTemplate, TEntity>>();
            load.Init(templates, id, interaction, request, promise);
            load.Run();
        }
        
        private void Init(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, BulkTemplateRequest request, DiscordPromise<List<TEntity>> promise)
        {
            _templates = templates;
            _id = id;
            _interaction = interaction;
            _request = request;
            _promise = promise;
        }
        
        protected override Task HandleCallback()
        {
            return _templates.HandleGetLocalizedBulkEntityAsync(_id, _request, _interaction, _promise);
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