using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk
{
    /// <summary>
    /// Represents an item in a bulk template request
    /// </summary>
    public struct BulkTemplateItem
    {
        /// <summary>
        /// Name of the template
        /// </summary>
        public readonly string TemplateName;
        
        /// <summary>
        /// <see cref="PlaceholderData"/> for the template
        /// </summary>
        public readonly PlaceholderData Data;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        public BulkTemplateItem(string templateName, PlaceholderData data)
        {
            TemplateName = templateName;
            Data = data;
        }
    }
}