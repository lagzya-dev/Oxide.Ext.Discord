using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class BulkTemplateToEntityCallback<TTemplate, TEntity> : BaseAsyncCallback 
        where TTemplate : BaseMessageTemplate<TEntity>, new()
        where TEntity : class
    {
        private BaseMessageTemplatesLibrary<TTemplate, TEntity> _templates;
        private TemplateId _id;
        private DiscordInteraction _interaction;
        private BulkTemplateRequest<TEntity> _request;
        private DiscordAsyncCallback<List<TEntity>> _callback;

        public static void Start(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, BulkTemplateRequest<TEntity> request, DiscordAsyncCallback<List<TEntity>> callback)
        {
            BulkTemplateToEntityCallback<TTemplate, TEntity> load = DiscordPool.Get<BulkTemplateToEntityCallback<TTemplate, TEntity>>();
            load.Init(templates, id, null, request, callback);
            load.Run();
        }
        
        public static void Start(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, BulkTemplateRequest<TEntity> request, DiscordAsyncCallback<List<TEntity>> callback)
        {
            BulkTemplateToEntityCallback<TTemplate, TEntity> load = DiscordPool.Get<BulkTemplateToEntityCallback<TTemplate, TEntity>>();
            load.Init(templates, id, interaction, request, callback);
            load.Run();
        }
        
        private void Init(BaseMessageTemplatesLibrary<TTemplate, TEntity> templates, TemplateId id, DiscordInteraction interaction, BulkTemplateRequest<TEntity> request, DiscordAsyncCallback<List<TEntity>> callback)
        {
            _templates = templates;
            _id = id;
            _interaction = interaction;
            _request = request;
            _callback = callback;
        }
        
        protected override Task HandleCallback()
        {
            return _templates.HandleGetBulkEntityAsync(_id, _request, _interaction, _callback);
        }

        protected override void EnterPool()
        {
            _templates = null;
            _id = default(TemplateId);
            _interaction = null;
            _request = null;
            _callback = null;
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}