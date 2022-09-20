using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordEmbedFieldTemplates : BaseMessageTemplatesLibrary<DiscordEmbedFieldTemplate, EmbedField>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordEmbedFieldTemplates(ILogger logger) : base(TemplateType.EmbedField, logger) { }
    }
}