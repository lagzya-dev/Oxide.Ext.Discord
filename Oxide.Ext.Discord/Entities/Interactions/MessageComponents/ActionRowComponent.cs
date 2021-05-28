using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        [JsonProperty("components")]
        public List<ButtonComponent> Components { get; set; } = new List<ButtonComponent>();

        /// <summary>
        /// Constructor for ActionRowComponent
        /// Sets the Type to ActionRow
        /// </summary>
        public ActionRowComponent()
        {
            Type = MessageComponentType.ActionRow;
        }

        /// <summary>
        /// Add a message component to an action row
        /// </summary>
        /// <param name="component">Component to add</param>
        /// <exception cref="ArgumentNullException">Throw if component is null</exception>
        /// <exception cref="Exception">Throw if invalid message type is passed or components exceeds max of 5</exception>
        public void AddComponent(ButtonComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }
            
            if (component.Type == MessageComponentType.ActionRow)
            {
                throw new Exception("Cannot nest actions rows inside each other");
            }

            if (Components.Count >= 5)
            {
                throw new Exception("Cannot have more than 5 components in an action row");
            }
            
            Components.Add(component);
        }
    }
}