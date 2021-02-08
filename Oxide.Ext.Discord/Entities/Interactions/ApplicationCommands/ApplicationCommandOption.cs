using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#applicationcommandoption">ApplicationCommandOption</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ApplicationCommandOption
    {
        /// <summary>
        /// They type of command option
        /// See <see cref="ApplicationCommandOptionType"/>
        /// </summary>
        [JsonProperty("type")]
        public ApplicationCommandOptionType Type { get; set; }
        
        /// <summary>
        /// Name of the command option (1-32 characters)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Description the command option (1-100 characters)
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// If the parameter is required or optional
        /// </summary>
        [JsonProperty("required")]
        public bool? Required { get; set; }
        
        /// <summary>
        /// Choices for string and int types for the user to pick from
        /// See <see cref="ApplicationCommandOption"/>
        /// </summary>
        [JsonProperty("choices")]
        public List<ApplicationCommandOptionChoice> Choices { get; set; }
        
        /// <summary>
        /// If the option is a subcommand or subcommand group type, this nested options will be the parameters
        /// See <see cref="ApplicationCommandOption"/>
        /// </summary>
        [JsonProperty("options")]
        public List<ApplicationCommandOption> Options { get; set; }
    }
}