namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    /// <summary>
    /// Represents a Embed for <see cref="DiscordMessageTemplate"/>
    /// </summary>
    public class MessageEmbedTemplate : BaseEmbedTemplate
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MessageEmbedTemplate() : base(TemplateType.None, new TemplateVersion(1, 0, 0))
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="titleUrl"></param>
        public MessageEmbedTemplate(string title = "", string description = "", string titleUrl = "") : this()
        {
            Title = title;
            Description = description;
            Url = titleUrl;
        }
    }
}