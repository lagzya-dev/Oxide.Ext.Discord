using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields
{
    /// <summary>
    /// Discord Template for Embed Field
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class BaseEmbedFieldTemplate : BaseMessageTemplate<EmbedField>
    {
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
        /// <param name="type"></param>
        /// <param name="version"></param>
        [JsonConstructor]
        protected BaseEmbedFieldTemplate(TemplateType type, TemplateVersion version) : base(type, version) { }

        /// <summary>
        /// Converts the template to an <see cref="EmbedField"/>
        /// </summary>
        /// <param name="data">Data to use</param>
        /// <param name="field">Initial field (Optional)</param>
        /// <returns></returns>
        public override EmbedField ToEntity(PlaceholderData data, EmbedField field = null)
        {
            if (field == null)
            {
                field = new EmbedField();
            }

            field.Name = PlaceholderFormatting.ApplyPlaceholder(field.Name, data);
            field.Value = PlaceholderFormatting.ApplyPlaceholder(field.Value, data);
            field.Inline = Inline;

            return field;
        }
    }
}