using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Templates.Embeds
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordEmbedTemplates : BaseMessageTemplateLibrary<DiscordEmbedTemplate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordEmbedTemplates(ILogger logger) : base(TemplateType.Embed, logger) { }
    }
}