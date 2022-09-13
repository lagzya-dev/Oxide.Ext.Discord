using System.Collections.Specialized;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Components
{
    public class InputTextTemplate : BaseComponentTemplate
    {
        /// <summary>
        /// Custom ID of the input text
        /// </summary>
        [JsonProperty("Input Text ID")]
        public string CustomId { get; set; } = string.Empty;
        
        /// <summary>
        /// The style of the input text
        /// </summary>
        [JsonConverter(typeof(StringEnumerator))]
        [JsonProperty("Input Text Style")]
        public InputTextStyles Style { get; set; } = InputTextStyles.Short;
        
        /// <summary>
        /// Text that appears on top of the input text field, max 80 characters
        /// </summary>
        [JsonProperty("Input Text Label")]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// The minimum length of the text input
        /// </summary>
        [JsonProperty("Input Text Min Length")]
        public int MinLength { get; set; } = 0;
        
        /// <summary>
        /// The maximum length of the text input
        /// </summary>
        [JsonProperty("Input Text Max Length")]
        public int MaxLength { get; set; } = 4000;
        
        /// <summary>
        /// The placeholder for the text input field
        /// </summary>
        [JsonProperty("Input Text Placeholder")]
        public string Placeholder { get; set; } = string.Empty;

        /// <summary>
        /// The pre-filled value for text input
        /// </summary>
        [JsonProperty("Input Text Value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Is the Input Text Required to be filled out
        /// </summary>
        [JsonProperty("Input Text Required")]
        public bool Required { get; set; } = false;

        [JsonConstructor]
        public InputTextTemplate()
        {
            Type = MessageComponentType.InputText;
        }
        
        public InputTextTemplate(string label, string customId, string value = "", InputTextStyles style = InputTextStyles.Short, bool required = false, string placeholder = "", int minLength = 0, int maxLength = 4000) : this()
        {
            Label = label;
            CustomId = customId;
            Value = value;
            Style = style;
            Required = required;
            Placeholder = placeholder;
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public InputTextComponent ToInputText(PlaceholderData data)
        {
            InputTextComponent input = new InputTextComponent
            {
                Label = PlaceholderFormatting.ApplyPlaceholder(Label, data),
                Placeholder = PlaceholderFormatting.ApplyPlaceholder(Placeholder, data),
                Value = PlaceholderFormatting.ApplyPlaceholder(Value, data),
                Required = Required,
                Style = Style,
                MinLength = MinLength,
                MaxLength = MaxLength,
                CustomId = CustomId
            };
            return input;
        }
    }
}