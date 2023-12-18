using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordEmbedFieldTemplates : BaseMessageTemplateLibrary<DiscordEmbedFieldTemplate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordEmbedFieldTemplates(ILogger logger) : base(TemplateType.EmbedField, logger) { }
    }
}