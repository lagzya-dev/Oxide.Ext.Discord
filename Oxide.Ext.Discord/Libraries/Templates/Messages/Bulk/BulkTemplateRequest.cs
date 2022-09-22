using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk
{
    public class BulkTemplateRequest<TEntity> : BasePoolable
    {
        public List<BulkTemplateItem<TEntity>> Items = new List<BulkTemplateItem<TEntity>>();

        public static BulkTemplateRequest<TEntity> Create()
        {
            return DiscordPool.Get<BulkTemplateRequest<TEntity>>();
        }
        
        public void AddItem(string templateName, PlaceholderData data = null, TEntity entity = default(TEntity))
        {
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException();
            Items.Add(new BulkTemplateItem<TEntity>(templateName, data, entity));
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