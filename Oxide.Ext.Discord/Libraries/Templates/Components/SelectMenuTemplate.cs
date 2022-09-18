using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Components
{
    /// <summary>
    /// Template for Select Menu Component
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SelectMenuTemplate : BaseComponentTemplate
    {
        /// <summary>
        /// Command for the Select Menu
        /// </summary>
        [JsonProperty("Select Menu ID")]
        public string CustomId { get; set; } = string.Empty;
        
        /// <summary>
        /// The choices in the select
        /// Max 25 options
        /// </summary>
        [JsonProperty("Select Menu Options")]
        public List<SelectMenuOptionTemplate> Options { get; set; }
        
        /// <summary>
        /// Custom placeholder text if nothing is selected
        /// Max 150 characters
        /// </summary>
        [JsonProperty("Select Menu Placeholder Text")]
        public string Placeholder { get; set; }

        /// <summary>
        /// the minimum number of items that must be chosen
        /// Default 1, Min 0, Max 25
        /// </summary>
        [JsonProperty("Select Menu Min Selected Values")]
        public int MinValues { get; set; } = 1;

        /// <summary>
        /// the maximum  number of items that must be chosen
        /// Default 1, Min 0, Max 25
        /// </summary>
        [JsonProperty("Select Menu Max Selected Values")]
        public int MaxValues { get; set; } = 1;

        /// <summary>
        /// If the Button is enabled
        /// </summary>
        [JsonProperty("Select Menu Enabled")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public SelectMenuTemplate()
        {
            Type = MessageComponentType.SelectMenu;
            Options = new List<SelectMenuOptionTemplate>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customId"></param>
        /// <param name="options"></param>
        /// <param name="placeholder"></param>
        /// <param name="minValues"></param>
        /// <param name="maxValues"></param>
        public SelectMenuTemplate(string customId, List<SelectMenuOptionTemplate> options, string placeholder = "", int minValues = 1, int maxValues = 1) : this()
        {
            CustomId = customId;
            Options = options;
            Placeholder = placeholder;
            MinValues = minValues;
            MaxValues = maxValues;
        }

        /// <summary>
        /// Converts the template to a <see cref="SelectMenuComponent"/>
        /// </summary>
        /// <returns></returns>
        public override BaseComponent ToComponent(PlaceholderData data)
        {
            SelectMenuComponent component = new SelectMenuComponent
            {
                CustomId = PlaceholderFormatting.ApplyPlaceholder(CustomId, data),
                Placeholder = PlaceholderFormatting.ApplyPlaceholder(Placeholder, data),
                MinValues = MinValues,
                MaxValues = MaxValues,
                Disabled = !Enabled,
            };

            for (int index = 0; index < Options.Count; index++)
            {
                component.Options.Add(Options[index].ToOption(data));
            }

            return component;
        }
    }
}