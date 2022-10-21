using Oxide.Ext.Discord.Interfaces.Entities.Messages;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordMessageTemplates : BaseMessageTemplateLibrary<DiscordMessageTemplate, IDiscordMessageTemplate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordMessageTemplates(ILogger logger) : base(TemplateType.Message, logger) { }
    }
}