using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers.Converters;

namespace Oxide.Ext.Discord.Entities.Interactions.MessageComponents
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/message-components#actionrow">Action Row Component</a> within discord
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ActionRowComponent : BaseComponent
    {
        /// <summary>
        /// The components on the action row
        /// </summary>
        [JsonConverter(typeof(MessageComponentsConverter))]
        [JsonProperty("components")]
        public List<BaseComponent> Components { get; private set; }

        /// <summary>
        /// Constructor for ActionRowComponent
        /// Sets the Type to ActionRow
        /// </summary>
        public ActionRowComponent()
        {
            Type = MessageComponentType.ActionRow;
        }

        /// <summary>
        /// Add a button component to an action row
        /// </summary>
        /// <param name="component">Component to add</param>
        /// <exception cref="ArgumentNullException">Throw if component is null</exception>
        /// <exception cref="Exception">Throw if invalid message type is passed or components exceeds max of 5</exception>
        public void AddComponent(ButtonComponent component)
        {
            ValidateComponent(component);

            if (Components.Count >= 5)
            {
                throw new Exception("Cannot have more than 5 buttons in an action row");
            }

            for (int index = 0; index < Components.Count; index++)
            {
                BaseComponent existing = Components[index];
                if (existing is SelectMenuComponent)
                {
                    throw new Exception("Cannot have a ButtonComponent with a SelectMenuComponent");
                }
            }

            Components.Add(component);
        }

        /// <summary>
        /// Adds a <see cref="SelectMenuComponent"/> to an action row
        /// </summary>
        /// <param name="component">Select Menu Component to add</param>
        /// <exception cref="ArgumentNullException">Thrown if component is null</exception>
        /// <exception cref="Exception">Thrown if 1 or more components already exist</exception>
        public void AddComponent(SelectMenuComponent component)
        {
            ValidateComponent(component);
            
            if (Components.Count >= 1)
            {
                throw new Exception("Cannot have more than 1 select menu in an action row");
            }

            Components.Add(component);
        }

        private void ValidateComponent(BaseComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }
            
            if (Components == null)
            {
                Components = new List<BaseComponent>();
            }
        }
    }
}