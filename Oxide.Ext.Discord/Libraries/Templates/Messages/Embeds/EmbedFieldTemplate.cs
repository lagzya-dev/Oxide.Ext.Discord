using Newtonsoft.Json;
using Oxide.Ext.Discord.Builders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Embeds
{
    /// <summary>
    /// Discord Template for Embed Field
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class EmbedFieldTemplate
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
    }
}