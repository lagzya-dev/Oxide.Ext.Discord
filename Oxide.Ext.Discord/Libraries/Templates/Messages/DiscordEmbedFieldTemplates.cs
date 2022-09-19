using Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordEmbedFieldTemplates : BaseMessageTemplatesLibrary<DiscordEmbedFieldTemplate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordEmbedFieldTemplates(ILogger logger) : base(logger) { }
    }
}