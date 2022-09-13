using Newtonsoft.Json;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Emojis;

namespace Oxide.Ext.Discord.Libraries.Templates.Components
{
    /// <summary>
    /// Template for Select Menu Options
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SelectMenuOptionTemplate
    {
        /// <summary>
        /// The user-facing name of the option,
        /// Max 100 characters
        /// </summary>
        [JsonProperty("Label")]
        public string Label { get; set; } = string.Empty;
        
        /// <summary>
        /// The dev-define value of the option,
        /// Max 100 characters
        /// </summary>
        [JsonProperty("Value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// An additional description of the option,
        /// Max 100 characters
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Emoji in the option
        /// </summary>
        [JsonProperty("Emoji")]
        public EmojiTemplate Emoji { get; set; }

        /// <summary>
        /// Will render this option as selected by default
        /// </summary>
        [JsonProperty("default")]
        public bool Default { get; set; } = false;

        [JsonConstructor]
        public SelectMenuOptionTemplate()
        {
            Emoji = new EmojiTemplate();
        }

        public SelectMenuOptionTemplate(string label, string value, string description = "", string emoji = "", bool @default = false)
        {
            Label = label;
            Value = value;
            Description = description;
            Emoji = new EmojiTemplate(emoji);
            Default = @default;
        }
    }
}