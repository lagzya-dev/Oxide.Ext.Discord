using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk
{
    public struct BulkTemplateItem<TEntity>
    {
        public string TemplateName;
        public PlaceholderData Data;
        public TEntity Entity;

        public BulkTemplateItem(string templateName, PlaceholderData data, TEntity entity)
        {
            TemplateName = templateName;
            Data = data;
            Entity = entity;
        }
    }
}