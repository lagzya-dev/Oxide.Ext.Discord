using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Templates.Modals
{
    /// <summary>
    /// Modal Templates Library
    /// </summary>
    public class DiscordModalTemplates : BaseMessageTemplateLibrary<DiscordModalTemplate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        internal DiscordModalTemplates(ILogger logger) : base(TemplateType.Modal, logger) { }
    }
}