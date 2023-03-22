using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk
{
    public struct BulkTemplateItem
    {
        public readonly string TemplateName;
        public readonly PlaceholderData Data;

        public BulkTemplateItem(string templateName, PlaceholderData data)
        {
            TemplateName = templateName;
            Data = data;
        }
    }
}