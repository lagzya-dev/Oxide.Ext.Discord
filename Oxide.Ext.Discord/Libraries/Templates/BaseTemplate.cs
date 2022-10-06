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
        /// Constructor
        /// </summary>
        /// <param name="type">Type of the template</param>
        /// <param name="internalVersion">Internal Template Version</param>
        public BaseTemplate(TemplateType type, TemplateVersion internalVersion)
        {
            TemplateType = type;
            InternalVersion = internalVersion;
        }
        
        /// <summary>
        /// The type of the Template
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Template Type (DO NOT EDIT)", Order = 1000)]
        public TemplateType TemplateType { get; set; }
        
        /// <summary>
        /// The version of the Template
        /// Used when Registering templates to determine if we need to backup a template and create a new template for the given version
        /// </summary>
        [JsonProperty("Template Version (DO NOT EDIT)", Order = 1001)]
        public TemplateVersion Version { get; set; } = new TemplateVersion(1, 0, 0);
        
        /// <summary>
        /// Internal Version of the Template
        /// Reserved for future use
        /// </summary>
        [JsonProperty("Internal Template Version (DO NOT EDIT)", Order = 1002)]
        internal TemplateVersion InternalVersion { get; set; }
    }
}