using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Emojis;
using Oxide.Ext.Discord.Exceptions.Entities.Emojis;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Emojis
{
    /// <summary>
    /// Discord Template for Emoji
    /// </summary>
    public class EmojiTemplate
    {
        /// <summary>
        /// Emoji Name or ID
        /// </summary>
        [JsonProperty("Emoji Name Or ID")]
        public string Emoji { get; set; } = string.Empty;
        
        /// <summary>
        /// If the emoji is Animated
        /// </summary>
        [JsonProperty("For Custom Emojis Is The Emoji Animated")]
        public bool Animated { get; set; } = false;

        [JsonConstructor]
        public EmojiTemplate() { }

        public EmojiTemplate(string emoji, bool animated = false)
        {
            Emoji = emoji;
            Animated = animated;
        }
        
        /// <summary>
        /// Converts the <see cref="EmojiTemplate"/> to a <see cref="DiscordEmoji"/>
        /// </summary>
        /// <returns></returns>
        public DiscordEmoji ToEmoji()
        {
            if (string.IsNullOrEmpty(Emoji))
            {
                return null;
            }
            
            if (Snowflake.TryParse(Emoji, out Snowflake id))
            {
                return new DiscordEmoji
                {
                    EmojiId = id,
                    Animated = Animated
                };
            }
            
            InvalidEmojiException.ThrowIfInvalidEmojiString(Emoji);

            return new DiscordEmoji
            {
                Name = Emoji
            };
        }
    }
}