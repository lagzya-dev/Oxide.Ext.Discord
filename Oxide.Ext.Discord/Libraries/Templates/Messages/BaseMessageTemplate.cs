using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Templates.Messages;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    public abstract class BaseMessageTemplate<TEntity> : BaseTemplate where TEntity : class
    {
        protected BaseMessageTemplate(TemplateType type, TemplateVersion internalVersion) : base(type, internalVersion) { }

        public abstract TEntity ToEntity(PlaceholderData data = null, TEntity entity = null);

        public IDiscordAsyncCallback<TEntity> ToEntityAsync(PlaceholderData data = null, TEntity entity = null)
        {
            return ToEntityInternalAsync(data, entity, DiscordAsyncCallback<TEntity>.Create());
        }

        internal DiscordAsyncCallback<TEntity> ToEntityInternalAsync(PlaceholderData data, TEntity message = null, DiscordAsyncCallback<TEntity> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordAsyncCallback<TEntity>.Create(true);
            }
            
            ToEntityCallback<BaseMessageTemplate<TEntity>, TEntity>.Start(this, data, message, callback);
            return callback;
        }

        internal async Task HandleToEntityAsync(PlaceholderData data, TEntity entity, DiscordAsyncCallback<TEntity> callback)
        {
            callback.InvokeSuccess(await HandleToEntityAsync(data, entity).ConfigureAwait(false));
        }
        
        internal Task<TEntity> HandleToEntityAsync(PlaceholderData data, TEntity entity)
        {
            return Task.FromResult(ToEntity(data, entity));
        }

        public List<TEntity> ToBulkEntity(BulkEntityRequest<TEntity> request)
        {
            List<TEntity> entities =  new List<TEntity>();
            List<BulkEntityItem<TEntity>> items = request.Items;
            for (int index = 0; index < items.Count; index++)
            {
                BulkEntityItem<TEntity> item = items[index];
                entities.Add(ToEntity(item.Data, item.Entity));
            }

            return entities;
        }
        
        public IDiscordAsyncCallback<List<TEntity>> ToBulkEntityAsync(BulkEntityRequest<TEntity> request)
        {
            return ToBulkEntityInternalAsync(request, DiscordAsyncCallback<List<TEntity>>.Create());
        }

        internal DiscordAsyncCallback<List<TEntity>> ToBulkEntityInternalAsync(BulkEntityRequest<TEntity> request, DiscordAsyncCallback<List<TEntity>> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordAsyncCallback<List<TEntity>>.Create(true);
            }
            
            ToBulkEntityCallback<BaseMessageTemplate<TEntity>, TEntity>.Start(this, request, callback);
            return callback;
        }

        internal async Task HandleBulkToEntityAsync(BulkEntityRequest<TEntity> request, DiscordAsyncCallback<List<TEntity>> callback)
        {
            callback.InvokeSuccess(await HandleBulkToEntityAsync(request).ConfigureAwait(false));
        }
        
        internal Task<List<TEntity>> HandleBulkToEntityAsync(BulkEntityRequest<TEntity> request)
        {
            return Task.FromResult(ToBulkEntity(request));
        }
    }
}