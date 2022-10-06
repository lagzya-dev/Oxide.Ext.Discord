using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields
{
    /// <summary>
    /// Represents a Embed Field template for <see cref="DiscordMessageTemplate"/>
    /// </summary>
    public class MessageEmbedFieldTemplate : IEmbedFieldTemplate
    {
        ///<inheritdoc/>
        public string Name { get; set; }
        
        ///<inheritdoc/>
        public string Value { get; set; }
        
        ///<inheritdoc/>
        public bool Inline { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public MessageEmbedFieldTemplate() {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="inline"></param>
        public MessageEmbedFieldTemplate(string name, string value, bool inline = true)
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
        public EmbedField ToEntity(PlaceholderData data, EmbedField field = null)
        {
            return DiscordEmbedFieldTemplate.ToEntityInternal(this, data, field);
        }
    }
}