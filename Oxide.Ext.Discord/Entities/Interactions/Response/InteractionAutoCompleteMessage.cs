using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Entities.Interactions.Response
{
    /// <summary>
    /// Interaction Auto Complete Response Message
    /// </summary>
    public class InteractionAutoCompleteMessage
    {
        /// <summary>
        /// Autocomplete choices (max of 25 choices)
        /// </summary>
        [JsonProperty("choices")]
        public List<CommandOptionChoice> Choices { get; set; }
    }
}