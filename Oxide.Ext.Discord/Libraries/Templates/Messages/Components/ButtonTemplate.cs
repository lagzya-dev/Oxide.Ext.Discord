using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Emojis;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Components
{
    /// <summary>
    /// Template for Button Components
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class ButtonTemplate
    {
        /// <summary>
        /// If the button should be added to the message
        /// </summary>
        [JsonProperty("Button Enabled")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Display label for the button
        /// </summary>
        [JsonProperty("Button Label")]
        public string Label { get; set; } = "Button Label";

        /// <summary>
        /// Emoji for the button
        /// </summary>
        [JsonProperty("Button Emoji")]
        public DiscordEmojiTemplate Emoji { get; set; } = new DiscordEmojiTemplate();

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
        /// Converts the template to a <see cref="ButtonComponent"/>
        /// </summary>
        /// <returns></returns>
        public ButtonComponent ToButton()
        {
            ButtonComponent button = new ButtonComponent
            {
                Label = Label,
                Style = Style,
                Disabled = !Enabled,
                Emoji = Emoji?.ToEmoji()
            };

            if (Style == ButtonStyle.Link)
            {
                button.Url = Command;
            }
            else
            {
                button.CustomId = Command;
            }

            return button;
        }
    }
}