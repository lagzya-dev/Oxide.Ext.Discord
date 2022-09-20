namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields
{
    /// <summary>
    /// Represents a template that can be used by the <see cref="DiscordEmbedFieldTemplates"/> Library
    /// </summary>
    public class DiscordEmbedFieldTemplate : BaseEmbedFieldTemplate
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DiscordEmbedFieldTemplate() : base(TemplateType.EmbedField, new TemplateVersion(1, 0, 0)) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="inline"></param>
        public DiscordEmbedFieldTemplate(string name, string value, bool inline = true) : this()
        {
            Name = name;
            Value = value;
            Inline = inline;
        }
    }
}