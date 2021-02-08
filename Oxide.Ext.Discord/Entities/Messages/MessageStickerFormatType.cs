using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#message-object-message-sticker-format-types">Message Sticker Format Types</a>
    /// </summary>
    public enum MessageStickerFormatType
    {
        /// <summary>
        /// Sticker format type PNG
        /// </summary>
        [Description("PNG")]
        Png = 1,
        
        /// <summary>
        /// Sticker format type APNG
        /// </summary>
        [Description("APNG")]
        Apng = 2,
        
        /// <summary>
        /// Sticker format type LOTTIE
        /// </summary>
        [Description("LOTTIE")]
        Lottie = 3
    }
}