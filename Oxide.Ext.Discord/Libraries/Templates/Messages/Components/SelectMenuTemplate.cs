using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents.SelectMenus;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Components.SelectMenus;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Components
{
    public class SelectMenuTemplate : BaseComponentTemplate
    {
        /// <summary>
        /// Command for the Select Menu
        /// </summary>
        [JsonProperty("Select Menu ID")]
        public string CustomId { get; set; } = string.Empty;
        
        /// <summary>
        /// Custom placeholder text if nothing is selected
        /// Max 150 characters
        /// </summary>
        [JsonProperty("Select Menu Placeholder Text")]
        public string Placeholder { get; set; }
        
        /// <summary>
        /// The choices in the select
        /// Max 25 options
        /// </summary>
        [JsonProperty("Select Menu Options")]
        public List<SelectMenuOptionTemplate> Options { get; set; }
        
        /// <summary>
        /// <see cref="MessageComponentType.ChannelSelect"/> <see cref="ChannelType"/> to show
        /// Max 25 options
        /// </summary>
        [JsonProperty("Select Menu Channel Types")]
        public List<ChannelType> ChannelTypes { get; set; }

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

        public SelectMenuTemplate(MessageComponentType type) : base(type) { }

        public override BaseComponent ToComponent(PlaceholderData data)
        {
            BaseSelectMenuComponent component = null;
            switch (Type)
            {
                case MessageComponentType.StringSelect:
                    var text = new StringSelectComponent();
                    component = text;
                    if (Options != null)
                    {
                        for (int index = 0; index < Options.Count; index++)
                        {
                            text.Options.Add(Options[index].ToOption(data));
                        }
                    }
                    break;
                
                case MessageComponentType.UserSelect:
                    component = new UserSelectComponent();
                    break;
                
                case MessageComponentType.RoleSelect:
                    component = new RoleSelectComponent();
                    break;
                
                case MessageComponentType.MentionableSelect:
                    component = new MentionableSelectComponent();
                    break;
                
                case MessageComponentType.ChannelSelect:
                    var channel = new ChannelSelectComponent();
                    component = channel;
                    channel.ChannelTypes = ChannelTypes = ChannelTypes != null && ChannelTypes.Count != 0 ? ChannelTypes : null;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            component.CustomId = PlaceholderFormatting.ApplyPlaceholder(CustomId, data);
            component.Placeholder = PlaceholderFormatting.ApplyPlaceholder(Placeholder, data);
            component.MinValues = MinValues;
            component.MaxValues = MaxValues;
            component.Disabled = !Enabled;
            
            return component;
        }

        private bool ShouldSerializeOptions() => Type == MessageComponentType.StringSelect;
        private bool ShouldSerializeChannelTypes() => Type == MessageComponentType.ChannelSelect;
    }
}