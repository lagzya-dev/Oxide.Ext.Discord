using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Interactions.Response;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Components;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages.Modals
{
    /// <summary>
    /// Template used for Modal Message Component
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordModalTemplate : BaseMessageTemplate<InteractionModalMessage>
    {
        /// <summary>
        /// Title of the modal
        /// </summary>
        [JsonProperty("Modal Title")]
        public string Title { get; set; }
        
        /// <summary>
        /// Title of the modal
        /// </summary>
        [JsonProperty("Modal ID")]
        public string CustomId { get; set; }

        /// <summary>
        /// Components of the Modal
        /// </summary>
        [JsonProperty("Modal Components")]
        private List<BaseComponentTemplate> Components { get; set; } = new List<BaseComponentTemplate>();
        
        /// <summary>
        /// Constructor
        /// </summary>
        [JsonConstructor]
        public DiscordModalTemplate() : base(new TemplateVersion(1, 0, 0)) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="customId"></param>
        public DiscordModalTemplate(string title, string customId) : this()
        {
            Title = title;
            CustomId = customId;
        }
        
        /// <summary>
        /// Converts the template to a <see cref="InteractionModalMessage"/>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modal"></param>
        /// <returns></returns>
        public override InteractionModalMessage ToEntity(PlaceholderData data = null, InteractionModalMessage modal = null)
        {
            if (modal == null)
            {
                modal = new InteractionModalMessage();
            }

            modal.Title = PlaceholderFormatting.ApplyPlaceholder(Title, data);
            modal.CustomId = PlaceholderFormatting.ApplyPlaceholder(CustomId, data);
            modal.Components = new List<ActionRowComponent>();

            for (int index = 0; index < Components.Count; index++)
            {
                BaseComponentTemplate component = Components[index];
                if (component is InputTextTemplate input)
                {
                    InvalidMessageComponentException.ThrowIfInvalidMaxActionRows(modal.Components.Count);
                    modal.Components.Add(new ActionRowComponent
                    {
                        Components = { input.ToComponent(data) }
                    });
                }
            }

            return modal;
        }
    }
}