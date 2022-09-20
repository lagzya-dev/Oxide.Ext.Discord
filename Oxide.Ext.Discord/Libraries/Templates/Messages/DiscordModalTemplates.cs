using Oxide.Ext.Discord.Entities.Interactions.Response;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Modals;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordModalTemplates : BaseMessageTemplatesLibrary<DiscordModalTemplate, InteractionModalMessage>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordModalTemplates(ILogger logger) : base(TemplateType.Modal, logger) { }
    }
}