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
            if (!string.IsNullOrEmpty(customId) && customId.Length > 100)
            {
                throw new InvalidMessageComponentException("CustomID cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidButtonLabel(string label)
        {
            if (!string.IsNullOrEmpty(label) && label.Length > 80)
            {
                throw new InvalidMessageComponentException("Button Label cannot be more than 80 characters");
            }
        }
        
        internal static void ThrowIfInvalidButtonUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new InvalidMessageComponentException("Button Url cannot be null or empty");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuPlaceholder(string placeholder)
        {
            if (!string.IsNullOrEmpty(placeholder) && placeholder.Length > 150)
            {
                throw new InvalidMessageComponentException("Select Menu Placeholder cannot be more than 150 characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuMinValues(int minValues)
        {
            if (minValues < 0)
            {
                throw new InvalidMessageComponentException("Select Menu Min Values cannot be less than 0");
            }
            
            if (minValues > 25)
            {
                throw new InvalidMessageComponentException("Select Menu Min Values cannot be more than 25");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuMaxValues(int maxValues, int minValues)
        {
            if (maxValues < minValues)
            {
                throw new InvalidMessageComponentException($"Select Menu Max Values {maxValues} cannot be less min values {minValues}");
            }
            
            if (maxValues > 25)
            {
                throw new InvalidMessageComponentException("Select Menu Max Values cannot be more than 25");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionLabel(string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                throw new InvalidMessageComponentException("Select Menu Option Label cannot be less than 1 character");
            }
            
            if (label.Length > 100)
            {
                throw new InvalidMessageComponentException("Select Menu Option Label cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidMessageComponentException("Select Menu Option Value cannot be less than 1 character");
            }
            
            if (value.Length > 100)
            {
                throw new InvalidMessageComponentException("Select Menu Option Value cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 100)
            {
                throw new InvalidMessageComponentException("Select Menu Option Description cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionCount(int count)
        {
            if (count > 25)
            {
                throw new InvalidMessageComponentException("Select Menu Option Count cannot be more than 25");
            }
        }
        
        internal static void ThrowIfInvalidTextInputLabel(string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                throw new InvalidMessageComponentException("Text Input Label cannot be less than 1 character");
            }
            
            if (label.Length > 45)
            {
                throw new InvalidMessageComponentException("Text Input Label cannot be more than 45 characters");
            }
        }
        
        internal static void ThrowIfInvalidTextInputValue(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 4000)
            {
                throw new InvalidMessageComponentException("Text Input Value cannot be more than 4000 characters");
            }
        }
        
        internal static void ThrowIfInvalidTextInputLength(int? minLength, int? maxLength)
        {
            if (!minLength.HasValue && !maxLength.HasValue)
            {
                return;
            }

            if (minLength < 0)
            {
                throw new InvalidMessageComponentException("Text Input Min Length cannot be less than 0");
            }

            if (minLength > 4000)
            {
                throw new InvalidMessageComponentException("Text Input Min Length cannot be greater than 4000");
            }

            if (maxLength < 1)
            {
                throw new InvalidMessageComponentException("Text Input Max Length cannot be less than 1");
            }
            
            if (maxLength > 4000)
            {
                throw new InvalidMessageComponentException("Text Input Max Length cannot be greater than 4000");
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