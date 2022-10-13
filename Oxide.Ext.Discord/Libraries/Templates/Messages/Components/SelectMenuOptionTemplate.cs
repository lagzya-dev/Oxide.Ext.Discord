using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents.SelectMenus;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Emojis;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Components.SelectMenus
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
        public bool Default { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public SelectMenuOptionTemplate()
        {
            Emoji = new EmojiTemplate();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <param name="emoji"></param>
        /// <param name="default"></param>
        public SelectMenuOptionTemplate(string label, string value, string description = "", string emoji = "", bool @default = false)
        {
            Label = label;
            Value = value;
            Description = description;
            Emoji = new EmojiTemplate(emoji);
            Default = @default;
        }

        /// <summary>
        /// Converts the template to <see cref="SelectMenuOption"/>
        /// </summary>
        /// <param name="data"><see cref="PlaceholderData"/> to use</param>
        /// <returns><see cref="SelectMenuOption"/></returns>
        public SelectMenuOption ToOption(PlaceholderData data)
        {
            return new SelectMenuOption
            {
                Label = PlaceholderFormatting.ApplyPlaceholder(Label, data),
                Value = PlaceholderFormatting.ApplyPlaceholder(Value, data),
                Description = PlaceholderFormatting.ApplyPlaceholder(Description, data),
                Emoji = Emoji.ToEmoji(),
                Default = Default
            };
        }
    }
}