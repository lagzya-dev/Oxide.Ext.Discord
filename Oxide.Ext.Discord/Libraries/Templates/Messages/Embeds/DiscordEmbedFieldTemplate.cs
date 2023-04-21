using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    /// <summary>
    /// Discord Template for Embed Field
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordEmbedFieldTemplate : BaseMessageTemplate<EmbedField>, IDiscordTemplate
    {
        private static readonly TemplateVersion InternalVersion = new TemplateVersion(1, 0, 0);
        
        /// <summary>
        /// Title of the field
        /// </summary>
        [JsonProperty("Field Title")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Value of the field
        /// </summary>
        [JsonProperty("Field Value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Should the field be on the same row
        /// </summary>
        [JsonProperty("Keep Field On Same Row")]
        public bool Inline { get; set; } = true;

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public DiscordEmbedFieldTemplate() { }
        
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

        /// <summary>
        /// Converts the template to an <see cref="EmbedField"/>
        /// </summary>
        /// <param name="data">Data to use</param>
        /// <param name="field">Initial field (Optional)</param>
        /// <returns></returns>
        public override EmbedField ToEntity(PlaceholderData data, EmbedField field = null)
        {
            return ToEntityInternal(this, data, field);
        }

        internal static EmbedField ToEntityInternal(DiscordEmbedFieldTemplate template, PlaceholderData data, EmbedField field = null)
        {
            if (field == null)
            {
                field = new EmbedField();
            }

            field.Name = PlaceholderFormatting.ApplyPlaceholder(template.Name, data);
            field.Value = PlaceholderFormatting.ApplyPlaceholder(template.Value, data);
            field.Inline = template.Inline;

            return field;
        }
        
        public TemplateVersion GetInternalVersion() => InternalVersion;
    }
}