using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;

namespace Oxide.Ext.Discord.Entities.Interactions.MessageComponents.SelectMenus
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/interactions/message-components#select-menus">Select Menus Component</a> within discord.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class TextSelectComponent : BaseSelectMenuComponent
    {
        /// <summary>
        /// The choices in the select
        /// Max 25 options
        /// </summary>
        [JsonProperty("options")]
        public List<SelectMenuOption> Options { get; } = new List<SelectMenuOption>();

        /// <summary>
        /// Select Menu Component Constructor
        /// </summary>
        public TextSelectComponent() : base(MessageComponentType.TextSelect) { }

        public override void Validate()
        {
            base.Validate();
            if (Options != null)
            {
                InvalidSelectMenuComponentException.ThrowIfInvalidSelectMenuOptionCount(Options.Count);
                for (int index = 0; index < Options.Count; index++)
                {
                    SelectMenuOption option = Options[index];
                    option.Validate();
                }
            }
        }
    }
}