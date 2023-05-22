using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;

namespace Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents
{
    /// <summary>
    /// Represents an exception for select menu components
    /// </summary>
    public class InvalidSelectMenuComponentException : BaseDiscordException
    {
        private InvalidSelectMenuComponentException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidSelectMenuPlaceholder(string placeholder)
        {
            const int maxLength = 150;
            
            if (!string.IsNullOrEmpty(placeholder) && placeholder.Length > maxLength)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Placeholder cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuMinValues(int? minValues)
        {
            const int minMinValues = 0;
            const int maxMinValues = 25;
            
            if (!minValues.HasValue)
            {
                return;
            }
            
            if (minValues < minMinValues)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Min Values cannot be less than {minMinValues}");
            }
            
            if (minValues > maxMinValues)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Min Values cannot be more than {maxMinValues}");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuMaxValues(int? maxValues)
        {
            const int maxMaxValues = 25;
            
            if (maxValues > maxMaxValues)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Max Values cannot be more than {maxMaxValues}");
            }
        }

        internal static void ThrowIfInvalidSelectMenuValueRange(int? minValues, int? maxValues)
        {
            if (!minValues.HasValue && !maxValues.HasValue)
            {
                return;
            }
            
            if (maxValues < minValues)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Max Values {maxValues} cannot be less min values {minValues}");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionLabel(string label)
        {
            const int maxLength = 100;
            
            if (string.IsNullOrEmpty(label))
            {
                throw new InvalidSelectMenuComponentException("Select Menu Option Label cannot be less than 1 character");
            }
            
            if (label.Length > maxLength)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Option Label cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionValue(string value)
        {
            const int maxLength = 100;
            
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidSelectMenuComponentException("Select Menu Option Value cannot be less than 1 character");
            }
            
            if (value.Length > maxLength)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Option Value cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionDescription(string description)
        {
            const int maxLength = 100;
            
            if (!string.IsNullOrEmpty(description) && description.Length > maxLength)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Option Description cannot be more than {maxLength} characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionCount(int count)
        {
            const int maxOptions = 25;
            
            if (count > maxOptions)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Option Count cannot be more than {maxOptions}");
            }
        }

        internal static void ThrowIfInvalidComponentType(MessageComponentType type)
        {
            switch (type)
            {
                case MessageComponentType.ActionRow:
                case MessageComponentType.Button:
                case MessageComponentType.StringSelect:
                case MessageComponentType.InputText:
                    throw new InvalidSelectMenuComponentException($"'{type}' is not a valid type for a SelectMenu.");
            }
        }
        
        internal static void ThrowIfTypeCantAddOptions(MessageComponentType type)
        {
            if (type != MessageComponentType.StringSelect)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Type '{type}' is not allowed to add Options. Options can only be added on {nameof(MessageComponentType)}{MessageComponentType.StringSelect}");
            }
        }
        
        internal static void ThrowIfTypeCantAddChannelTypes(MessageComponentType type)
        {
            if (type != MessageComponentType.ChannelSelect)
            {
                throw new InvalidSelectMenuComponentException($"Select Menu Type '{type}' is not allowed to add Channel Types. Channel Types can only be added on {nameof(MessageComponentType)}{MessageComponentType.ChannelSelect}");
            }
        }
    }
}