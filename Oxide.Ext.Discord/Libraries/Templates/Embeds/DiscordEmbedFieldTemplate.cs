using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Callbacks.Templates;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Interfaces.Templates;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Promises;

namespace Oxide.Ext.Discord.Libraries.Templates.Embeds
{
    /// <summary>
    /// Discord Template for Embed Field
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordEmbedFieldTemplate : IBulkTemplate<EmbedField>
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
        [JsonConstructor]
        public DiscordEmbedFieldTemplate() { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="inline"></param>
        public DiscordEmbedFieldTemplate(string name, string value, bool inline = true)
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
        public EmbedField ToEntity(PlaceholderData data = null, EmbedField field = null)
        {
            if (field == null)
            {
                field = new EmbedField();
            }
            
            data?.IncrementDepth();

            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            field.Name = placeholders.ProcessPlaceholders(Name, data);
            field.Value = placeholders.ProcessPlaceholders(Value, data);
            field.Inline = Inline;

            data?.DecrementDepth();
            data?.AutoDispose();
            
            return field;
        }

        ///<inheritdoc/>
        public IPromise<List<EmbedField>> ToEntityBulk(List<PlaceholderData> data)
        {
            IPendingPromise<List<EmbedField>> promise = Promise<List<EmbedField>>.Create();
            BulkToEntityCallback<DiscordEmbedFieldTemplate, EmbedField>.Start(this, data, promise);
            return promise;
        }
    }
}