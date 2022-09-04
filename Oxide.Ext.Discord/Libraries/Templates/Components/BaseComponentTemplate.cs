using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;

namespace Oxide.Ext.Discord.Libraries.Templates.Components
{
    public abstract class BaseComponentTemplate
    {
        /// <summary>
        /// If the component should be added to the message
        /// </summary>
        [JsonProperty("Show Component")]
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Type of the component
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Type")]
        public MessageComponentType Type { get; set; }
    }
}