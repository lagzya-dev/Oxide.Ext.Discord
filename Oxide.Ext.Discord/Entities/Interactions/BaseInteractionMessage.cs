using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Exceptions.Entities.Messages;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents a Base Message for an interaction
    /// </summary>
    public class BaseInteractionMessage : BaseMessageCreate
    {
        /// <summary>
        /// A developer-defined identifier for the interactable form
        /// Max 100 characters
        /// </summary>
        [JsonProperty("custom_id")]
        public string CustomId { get; set; }
        
        /// <summary>
        /// Title of the modal if Modal Response
        /// Max 45 characters
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        
        /// <summary>
        /// Autocomplete choices (max of 25 choices)
        /// </summary>
        [JsonProperty("choices")]
        public List<CommandOptionChoice> Choices { get; set; }

        ///<inheritdoc/>
        protected override void ValidateRequiredFields()
        {
            
        }

        ///<inheritdoc/>
        protected override void ValidateFlags()
        {
            InvalidMessageException.ThrowIfInvalidFlags(Flags, MessageFlags.SuppressEmbeds | MessageFlags.Ephemeral, "Invalid Message Flags Used for Channel Message. Only supported flags are MessageFlags.SuppressEmbeds and MessageFlags.Ephemeral");
        }
    }
}