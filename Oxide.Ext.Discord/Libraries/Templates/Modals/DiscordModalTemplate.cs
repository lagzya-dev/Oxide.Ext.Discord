using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Templates.Modals;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Interactions.Response;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Components;

namespace Oxide.Ext.Discord.Libraries.Templates.Modals
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordModalTemplate : BaseTemplate
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
        
        public DiscordModalTemplate() : base(new TemplateVersion(1, 0, 0))
        {
            TemplateType = TemplateType.Modal;
        }

        public InteractionModalMessage ToModal(PlaceholderData data = null, InteractionModalMessage modal = null)
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
                        Components = { input.ToInputText(data) }
                    });
                }
            }

            return modal;
        }
        
        public IDiscordAsyncCallback<InteractionModalMessage> ToModalAsync(PlaceholderData data, InteractionModalMessage message = null)
        {
            return ToModalInternalAsync(data, message, DiscordAsyncCallback<InteractionModalMessage>.Create());
        }
        
        internal IDiscordAsyncCallback<InteractionModalMessage> ToModalInternalAsync(PlaceholderData data, InteractionModalMessage message = null, IDiscordAsyncCallback<InteractionModalMessage> callback = null)
        {
            if (callback == null)
            {
                callback = InternalAsyncCallback<InteractionModalMessage>.Create();
            }
            
            ToModalCallback.Start(this, data, message, callback);
            return callback;
        }
        
        internal async Task HandleToModalAsync(PlaceholderData data, InteractionModalMessage message, IDiscordAsyncCallback<InteractionModalMessage> callback)
        {
            InteractionModalMessage modal = await Task.FromResult(ToModal(data, message)).ConfigureAwait(false);
            callback.InvokeSuccess(modal);
        }
    }
}