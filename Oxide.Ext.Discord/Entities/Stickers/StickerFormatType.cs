using System.Runtime.Serialization;

namespace Oxide.Ext.Discord.Entities.Stickers
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/sticker#sticker-format-types">Sticker Format Types</a>
    /// </summary>
    public enum StickerFormatType
    {
        /// <summary>
        /// Sticker format type PNG
        /// </summary>
        [EnumMember(Value = "PNG")]
        Png = 1,
        
        /// <summary>
        /// Sticker format type APNG
        /// </summary>
        [EnumMember(Value = "APNG")]
        Apng = 2,
        
        /// <summary>
        /// Sticker format type LOTTIE
        /// </summary>
        [EnumMember(Value = "LOTTIE")]
        Lottie = 3
    }
}