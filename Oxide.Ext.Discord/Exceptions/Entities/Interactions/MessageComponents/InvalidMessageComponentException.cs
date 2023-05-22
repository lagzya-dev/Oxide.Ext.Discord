namespace Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents
{
    /// <summary>
    /// Represents an invalid message component
    /// </summary>
    public class InvalidMessageComponentException : BaseDiscordException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        private InvalidMessageComponentException(string message) : base(message)
        {
            
        }
        
        internal static void ThrowIfInvalidCustomId(string customId)
        {
            const int maxLength = 100;
            
            if (!string.IsNullOrEmpty(customId) && customId.Length > maxLength)
            {
                throw new InvalidMessageComponentException($"CustomID cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidButtonLabel(string label)
        {
            const int maxLength = 80;
            if (!string.IsNullOrEmpty(label) && label.Length > 80)
            {
                throw new InvalidMessageComponentException($"Button Label cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidButtonUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new InvalidMessageComponentException("Button Url cannot be null or empty");
            }
        }

        internal static void ThrowIfInvalidTextInputLabel(string label)
        {
            const int maxLength = 45;
            
            if (string.IsNullOrEmpty(label))
            {
                throw new InvalidMessageComponentException("Text Input Label cannot be less than 1 character");
            }
            
            if (label.Length > maxLength)
            {
                throw new InvalidMessageComponentException($"Text Input Label cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidTextInputValue(string value)
        {
            const int maxLength = 4000;
            
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                throw new InvalidMessageComponentException($"Text Input Value cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidTextInputLength(int? minLength, int? maxLength)
        {
            const int minMinLength = 0;
            const int maxMinLength = 4000;
            const int minMaxLength = 1;
            const int maxMaxLength = 4000;
            
            if (!minLength.HasValue && !maxLength.HasValue)
            {
                return;
            }

            if (minLength < minMinLength)
            {
                throw new InvalidMessageComponentException($"Text Input Min Length cannot be less than {minMinLength}");
            }

            if (minLength > maxMinLength)
            {
                throw new InvalidMessageComponentException($"Text Input Min Length cannot be greater than {maxMinLength}");
            }

            if (maxLength < minMaxLength)
            {
                throw new InvalidMessageComponentException($"Text Input Max Length cannot be less than {minMaxLength}");
            }
            
            if (maxLength > maxMaxLength)
            {
                throw new InvalidMessageComponentException($"Text Input Max Length cannot be greater than {maxMaxLength}");
            }

            if (maxLength < minLength)
            {
                throw new InvalidMessageComponentException($"Text Input Max Length {maxLength} cannot be less than Min Length {minLength}");
            }
            
            if (minLength > maxLength)
            {
                throw new InvalidMessageComponentException($"Text Input Min Length {minLength} cannot be greater than Max Length {maxLength}");
            }
        }
        
        internal static void ThrowIfInvalidMaxActionRows(int count)
        {
            if (count > 5)
            {
                throw new InvalidMessageComponentException("Cannot have more than 5 action rows for message components");
            }
        }
        
        internal static void ThrowIfInvalidModalTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new InvalidMessageComponentException("Modal title cannot be less than 1 character");
            }

            if (title.Length > 45)
            {
                throw new InvalidMessageComponentException("Modal title cannot be more than 45 characters");
            }
        }
    }
}