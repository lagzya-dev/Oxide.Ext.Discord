using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Base Template used in Discord Templates
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class BaseTemplate
    {
        /// <summary>
        /// The type of the Template
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Template Type (Do Not Edit)", Order = 100)]
        public TemplateType TemplateType { get; set; }
        
        /// <summary>
        /// The version of the Template
        /// Used when Registering templates to determine if we need to backup a template and create a new template for the given version
        /// </summary>
        [JsonProperty("Template Version (Do Not Edit)", Order = 101)]
        public TemplateVersion Version { get; set; } = new TemplateVersion(1, 0, 0);
    }
}