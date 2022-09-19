namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields
{
    /// <summary>
    /// Represents a Embed Field template for <see cref="DiscordMessageTemplate"/>
    /// </summary>
    public class MessageEmbedFieldTemplate : BaseEmbedFieldTemplate
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MessageEmbedFieldTemplate() : base(TemplateType.None, new TemplateVersion(1, 0, 0)) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="inline"></param>
        public MessageEmbedFieldTemplate(string name, string value, bool inline = true) : this()
        {
            Name = name;
            Value = value;
            Inline = inline;
        }
    }
}