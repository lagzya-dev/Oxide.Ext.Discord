using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields
{
    /// <summary>
    /// Discord Template for Embed Field
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordEmbedFieldTemplate : BaseMessageTemplate<EmbedField>, IEmbedFieldTemplate
    {
        ///<inheritdoc/>
        [JsonProperty("Field Title")]
        public string Name { get; set; } = string.Empty;

        ///<inheritdoc/>
        [JsonProperty("Field Value")]
        public string Value { get; set; } = string.Empty;

        ///<inheritdoc/>
        [JsonProperty("Keep Field On Same Row")]
        public bool Inline { get; set; } = true;

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public DiscordEmbedFieldTemplate() : base(TemplateType.EmbedField, new TemplateVersion(1, 0, 0)) { }
        
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

        ///<inheritdoc cref="IEmbedFieldTemplate.ToEntity"/>
        public override EmbedField ToEntity(PlaceholderData data, EmbedField field = null)
        {
            return ToEntityInternal(this, data, field);
        }

        internal static EmbedField ToEntityInternal(IEmbedFieldTemplate template, PlaceholderData data, EmbedField field = null)
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
    }
}