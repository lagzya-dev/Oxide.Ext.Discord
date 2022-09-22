using System.Collections.Generic;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk
{
    public class BulkEntityRequest<TEntity> : BasePoolable
    {
        public List<BulkEntityItem<TEntity>> Items = new List<BulkEntityItem<TEntity>>();

        public static BulkEntityRequest<TEntity> Create()
        {
            return DiscordPool.Get<BulkEntityRequest<TEntity>>();
        }
        
        public void AddItem(PlaceholderData data = null, TEntity entity = default(TEntity))
        {
            Items.Add(new BulkEntityItem<TEntity>(data, entity));
        }
        
        protected override void EnterPool()
        {
            Items.Clear();
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}