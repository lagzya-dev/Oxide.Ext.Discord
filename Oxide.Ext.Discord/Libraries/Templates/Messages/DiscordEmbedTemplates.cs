using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordEmbedTemplates : BaseMessageTemplateLibrary<DiscordEmbedTemplate, DiscordEmbed>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordEmbedTemplates(ILogger logger) : base(TemplateType.Embed, logger) { }
    }
}