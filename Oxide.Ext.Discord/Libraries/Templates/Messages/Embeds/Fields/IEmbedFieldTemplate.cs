using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds.Fields
{
    /// <summary>
    /// Embed Field Template
    /// </summary>
    public interface IEmbedFieldTemplate
    {
        /// <summary>
        /// Title of the field
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Value of the field
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Should the field be on the same row
        /// </summary>
        bool Inline { get; set; }
        
        /// <summary>
        /// Converts the template to an <see cref="EmbedField"/>
        /// </summary>
        /// <param name="data">Data to use</param>
        /// <param name="field">Initial field (Optional)</param>
        /// <returns></returns>
        EmbedField ToEntity(PlaceholderData data, EmbedField field = null);
    }
}