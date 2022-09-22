using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk
{
    public struct BulkEntityItem<TEntity>
    {
        public PlaceholderData Data;
        public TEntity Entity;

        public BulkEntityItem(PlaceholderData data, TEntity entity)
        {
            Data = data;
            Entity = entity;
        }
    }
}