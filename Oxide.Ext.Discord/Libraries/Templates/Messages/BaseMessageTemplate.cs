using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Templates.Messages;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    public abstract class BaseMessageTemplate<TEntity> : BaseTemplate where TEntity : class
    {
        protected BaseMessageTemplate(TemplateType type, TemplateVersion internalVersion) : base(type, internalVersion) { }

        public abstract TEntity ToEntity(PlaceholderData data = null, TEntity entity = null);

        public IDiscordAsyncCallback<TEntity> ToEntityAsync(PlaceholderData data = null, TEntity entity = null)
        {
            return ToEntityInternalAsync(data, entity, PluginAsyncCallback<TEntity>.Create());
        }

        internal IDiscordAsyncCallback<TEntity> ToEntityInternalAsync(PlaceholderData data, TEntity message = null, IDiscordAsyncCallback<TEntity> callback = null)
        {
            if (callback == null)
            {
                callback = InternalAsyncCallback<TEntity>.Create();
            }
            
            ToEntityCallback<BaseMessageTemplate<TEntity>, TEntity>.Start(this, data, message, callback);
            return callback;
        }

        internal async Task HandleToEntityAsync(PlaceholderData data, TEntity entity, IDiscordAsyncCallback<TEntity> callback)
        {
            callback.InvokeSuccess(await HandleToEntityAsync(data, entity).ConfigureAwait(false));
        }
        
        internal Task<TEntity> HandleToEntityAsync(PlaceholderData data, TEntity entity)
        {
            return Task.FromResult(ToEntity(data, entity));
        }

        public List<TEntity> ToBulkEntity(List<PlaceholderData> placeholders, List<TEntity> entities = null)
        {
            if (entities == null)
            {
                entities = new List<TEntity>();
            }

            for (int index = 0; index < placeholders.Count; index++)
            {
                PlaceholderData data = placeholders[index];
                if (index < entities.Count)
                {
                    ToEntity(data, entities[index]);
                    continue;
                }
                
                entities.Add(ToEntity(data));
            }

            return entities;
        }
        
        public IDiscordAsyncCallback<List<TEntity>> ToBulkEntityAsync(List<PlaceholderData> placeholders, List<TEntity> entities = null)
        {
            return ToBulkEntityInternalAsync(placeholders, entities, PluginAsyncCallback<List<TEntity>>.Create());
        }

        internal IDiscordAsyncCallback<List<TEntity>> ToBulkEntityInternalAsync(List<PlaceholderData> placeholders, List<TEntity> entities = null, IDiscordAsyncCallback<List<TEntity>> callback = null)
        {
            if (callback == null)
            {
                callback = InternalAsyncCallback<List<TEntity>>.Create();
            }
            
            ToBulkEntityCallback<BaseMessageTemplate<TEntity>, TEntity>.Start(this, placeholders, entities, callback);
            return callback;
        }

        internal async Task HandleBulkToEntityAsync(List<PlaceholderData> placeholders, List<TEntity> entities, IDiscordAsyncCallback<List<TEntity>> callback)
        {
            callback.InvokeSuccess(await HandleBulkToEntityAsync(placeholders, entities).ConfigureAwait(false));
        }
        
        internal Task<List<TEntity>> HandleBulkToEntityAsync(List<PlaceholderData> placeholders, List<TEntity> entities)
        {
            return Task.FromResult(ToBulkEntity(placeholders, entities));
        }
    }
}