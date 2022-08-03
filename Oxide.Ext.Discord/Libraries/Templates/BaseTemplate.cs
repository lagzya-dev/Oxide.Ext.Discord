using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Base Template used in Discord Templates
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class BaseTemplate
    {
        /// <summary>
        /// The version of the Template
        /// Used when Registering templates to determine if we need to backup a template and create a new template for the given version
        /// </summary>
        [JsonProperty("Template Version (Do not Edit)", Order = 100)]
        public TemplateVersion Version { get; set; } = new TemplateVersion(1, 0, 0);
    }
}