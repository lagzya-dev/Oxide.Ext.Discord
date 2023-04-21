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
            if (!string.IsNullOrEmpty(placeholder) && placeholder.Length > 150)
            {
                throw new InvalidSelectMenuComponentException("Select Menu Placeholder cannot be more than 150 characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuMinValues(int? minValues)
        {
            if (!minValues.HasValue)
            {
                return;
            }
            
            if (minValues < 0)
            {
                throw new InvalidSelectMenuComponentException("Select Menu Min Values cannot be less than 0");
            }
            
            if (minValues > 25)
            {
                throw new InvalidSelectMenuComponentException("Select Menu Min Values cannot be more than 25");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuMaxValues(int? maxValues)
        {
            if (maxValues > 25)
            {
                throw new InvalidSelectMenuComponentException("Select Menu Max Values cannot be more than 25");
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
            if (string.IsNullOrEmpty(label))
            {
                throw new InvalidSelectMenuComponentException("Select Menu Option Label cannot be less than 1 character");
            }
            
            if (label.Length > 100)
            {
                throw new InvalidSelectMenuComponentException("Select Menu Option Label cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidSelectMenuComponentException("Select Menu Option Value cannot be less than 1 character");
            }
            
            if (value.Length > 100)
            {
                throw new InvalidSelectMenuComponentException("Select Menu Option Value cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 100)
            {
                throw new InvalidSelectMenuComponentException("Select Menu Option Description cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidSelectMenuOptionCount(int count)
        {
            if (count > 25)
            {
                throw new InvalidSelectMenuComponentException("Select Menu Option Count cannot be more than 25");
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