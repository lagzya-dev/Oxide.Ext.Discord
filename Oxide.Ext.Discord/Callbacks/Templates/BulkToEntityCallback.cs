using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Interfaces.Templates;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates
{
    internal class BulkToEntityCallback<TTemplate, TEntity> : BaseAsyncCallback 
        where TTemplate : class, IBulkTemplate<TEntity> 
        where TEntity : class
    {
        private TTemplate _template;
        private List<PlaceholderData> _placeholders;
        private IPendingPromise<List<TEntity>> _promise;

        public static void Start(TTemplate template, List<PlaceholderData> data, IPendingPromise<List<TEntity>> promise)
        {
            BulkToEntityCallback<TTemplate, TEntity> callback = DiscordPool.Internal.Get<BulkToEntityCallback<TTemplate, TEntity>>();
            callback.Init(template, data, promise);
            callback.Run();
        }

        private void Init(TTemplate template, List<PlaceholderData> data, IPendingPromise<List<TEntity>> promise)
        {
            _template = template;
            _placeholders = data;
            _promise = promise;
        }
        
        protected override Task HandleCallback()
        {
            try
            {
                List<TEntity> entities = new List<TEntity>(_placeholders.Count);
                for (int index = 0; index < _placeholders.Count; index++)
                {
                    PlaceholderData data = _placeholders[index];
                    entities.Add(_template.ToEntity(data));
                }
                _promise.Resolve(entities);
            }
            catch (Exception ex)
            {
                _promise.Reject(ex);
            }
            
            return Task.CompletedTask;
        }

        protected override string GetExceptionMessage()
        {
            return $"Template: {_template.GetType().Name} Placeholders: {_placeholders?.Count} Promise: {_promise?.Id}";
        }

        protected override void EnterPool()
        {
            _template = null;
            _placeholders = null;
            _promise = null;
        }
    }
}