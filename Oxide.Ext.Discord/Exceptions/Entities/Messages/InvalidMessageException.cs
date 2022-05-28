using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Messages;

namespace Oxide.Ext.Discord.Exceptions.Entities.Messages
{
    /// <summary>
    /// Represents an invalid message
    /// </summary>
    public class InvalidMessageException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        private InvalidMessageException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="create">Message Create that caused the error</param>
        private InvalidMessageException(string message, BaseMessageCreate create) : base($"{message}\nBody:{JsonConvert.SerializeObject(create, Formatting.Indented)}")
        {
            
        }

        internal static void ThrowIfMissingRequiredField(BaseMessageCreate create)
        {
            if (string.IsNullOrEmpty(create.Content) 
                && (create.Embeds == null || create.Embeds.Count == 0) 
                && (create.FileAttachments == null || create.FileAttachments.Count == 0)
                && (create.StickerIds == null || create.StickerIds.Count == 0))
            {
                throw new InvalidMessageException("Discord Messages require Either Content, An Embed, A Sticker, Or a File", create);
            }
        }
        
        internal static void ThrowIfInvalidContent(string content)
        {
            if (content != null && content.Length > 2000)
            {
                throw new InvalidMessageException("Content cannot be more than 2000 characters");
            }
        }

        internal static void ThrowIfMaxStickers(int? count)
        {
            if (count > 3)
            {
                throw new InvalidMessageException("Cannot have more than 3 stickers");
            }
        }

        internal static void ThrowIfInvalidFlags(MessageFlags? flags, MessageFlags allowedFlags, string message)
        {
            if (flags.HasValue && (flags & ~allowedFlags) != 0)
            {
                throw new InvalidMessageException(message);
            }
        }
    }
}