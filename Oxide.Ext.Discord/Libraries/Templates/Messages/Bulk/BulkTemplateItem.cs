using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk
{
    public struct BulkTemplateItem
    {
        public string TemplateName;
        public PlaceholderData Data;

        public BulkTemplateItem(string templateName, PlaceholderData data)
        {
            TemplateName = templateName;
            Data = data;
        }
    }
}