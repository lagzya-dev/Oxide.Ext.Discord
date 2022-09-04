using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Emojis;

namespace Oxide.Ext.Discord.Libraries.Templates.Components
{
    /// <summary>
    /// Template for Button Components
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ButtonTemplate : BaseComponentTemplate
    {
        /// <summary>
        /// Display label for the button
        /// </summary>
        [JsonProperty("Button Label")]
        public string Label { get; set; } = "Button Label";

        /// <summary>
        /// Emoji for the button
        /// </summary>
        [JsonProperty("Button Emoji")]
        public EmojiTemplate Emoji { get; set; } = new EmojiTemplate();

        /// <summary>
        /// <see cref="ButtonStyle"/> for the button
        /// </summary>
        [JsonProperty("Button Style")]
        public ButtonStyle Style { get; set; } = ButtonStyle.Primary;

        /// <summary>
        /// Command for the button. If <see cref="ButtonStyle.Link"/> then this will set the Url field; Else the CustomId field
        /// </summary>
        [JsonProperty("Button Command")]
        public string Command { get; set; } = "My Command";

        /// <summary>
        /// Should the button be on the same or new row
        /// </summary>
        [JsonProperty("Keep Button On Same Row")]
        public bool Inline { get; set; } = true;
        
        /// <summary>
        /// If the Button is enabled
        /// </summary>
        [JsonProperty("Button Enabled")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Default Constructor
        /// </summary>
        [JsonConstructor]
        public ButtonTemplate()
        {
            Type = MessageComponentType.Button;
        }

        /// <summary>
        /// Constructor without emoji
        /// </summary>
        /// <param name="label">Button Label</param>
        /// <param name="style"><see cref="ButtonStyle"/></param>
        /// <param name="command">Button Command</param>
        /// <param name="enabled">Is button enabled?</param>
        /// <param name="inline">Should the button be on the same row or a new row?</param>
        public ButtonTemplate(string label, ButtonStyle style, string command, bool enabled = true, bool inline = true) : this(label, style, command, null, enabled, inline) { }

        /// <summary>
        /// Constructor with emoji
        /// </summary>
        /// <param name="label">Button Label</param>
        /// <param name="style"><see cref="ButtonStyle"/></param>
        /// <param name="command">Button Command</param>
        /// <param name="emoji">Emoji for the button</param>
        /// <param name="enabled">Is button enabled?</param>
        /// <param name="inline">Should the button be on the same row or a new row?</param>
        public ButtonTemplate(string label, ButtonStyle style, string command, string emoji, bool enabled = true, bool inline = true)
        {
            Label = label;
            Style = style;
            Command = command;
            Emoji = new EmojiTemplate
            {
                Emoji = emoji
            };
            Enabled = enabled;
            Inline = inline;
        }

        /// <summary>
        /// Converts the template to a <see cref="ButtonComponent"/>
        /// </summary>
        /// <returns></returns>
        public ButtonComponent ToButton(PlaceholderData data)
        {
            ButtonComponent button = new ButtonComponent
            {
                Label = PlaceholderFormatting.ApplyPlaceholder(Label, data),
                Style = Style,
                Disabled = !Enabled,
                Emoji = Emoji?.ToEmoji()
            };

            if (Style == ButtonStyle.Link)
            {
                button.Url = PlaceholderFormatting.ApplyPlaceholder(Command, data);
            }
            else
            {
                button.CustomId = PlaceholderFormatting.ApplyPlaceholder(Command, data);
            }

            return button;
        }
    }
}