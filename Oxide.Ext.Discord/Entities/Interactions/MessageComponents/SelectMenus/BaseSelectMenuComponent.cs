using Newtonsoft.Json;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;

namespace Oxide.Ext.Discord.Entities.Interactions.MessageComponents.SelectMenus
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/interactions/message-components#select-menus">Select Menus Component</a> within discord.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class BaseSelectMenuComponent : BaseInteractableComponent
    {
        /// <summary>
        /// Custom placeholder text if nothing is selected
        /// Max 150 characters
        /// </summary>
        [JsonProperty("placeholder")]
        public string Placeholder { get; set; }

        /// <summary>
        /// the minimum number of items that must be chosen;
        /// Default 1, Min 0, Max 25
        /// </summary>
        [JsonProperty("min_values")]
        public int? MinValues { get; set; }
        
        /// <summary>
        /// the maximum  number of items that must be chosen;
        /// Default 1, Min 0, Max 25
        /// </summary>
        [JsonProperty("max_values")]
        public int? MaxValues { get; set; }
        
        /// <summary>
        /// Disable the select
        /// Default false
        /// </summary>
        [JsonProperty("disabled")]
        public bool? Disabled { get; set; }
        
        /// <summary>
        /// Select Menu Component Constructor
        /// </summary>
        protected BaseSelectMenuComponent(MessageComponentType type)
        {
            Type = type;
        }

        ///<inheritdoc />
        public override void Validate()
        {
            base.Validate();
            InvalidSelectMenuComponentException.ThrowIfInvalidSelectMenuPlaceholder(Placeholder);
            InvalidSelectMenuComponentException.ThrowIfInvalidSelectMenuMinValues(MinValues);
            InvalidSelectMenuComponentException.ThrowIfInvalidSelectMenuMaxValues(MaxValues);
            InvalidSelectMenuComponentException.ThrowIfInvalidSelectMenuValueRange(MinValues, MaxValues);
        }
    }
}