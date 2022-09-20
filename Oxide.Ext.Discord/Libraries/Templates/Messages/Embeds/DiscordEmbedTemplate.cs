namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    /// <summary>
    /// Represents a template that can be used by the <see cref="DiscordEmbedTemplates"/>
    /// </summary>
    public class DiscordEmbedTemplate : BaseEmbedTemplate
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DiscordEmbedTemplate() : base(TemplateType.Embed, new TemplateVersion(1, 0, 0)) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="titleUrl"></param>
        public DiscordEmbedTemplate(string title, string description, string titleUrl = "") : this()
        {
            Title = title;
            Description = description;
            Url = titleUrl;
        }
    }
}