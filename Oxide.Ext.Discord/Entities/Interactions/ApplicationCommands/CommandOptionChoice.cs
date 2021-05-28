using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#applicationcommandoptionchoice">ApplicationCommandOptionChoice</a>
    /// If you specify choices for an option, they are the only valid values for a user to pick
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandOptionChoice
    {
        /// <summary>
        /// Choice name (1-100 characters)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Value of the choice
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}