using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#applicationcommandoption">ApplicationCommandOption</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandOption
    {
        /// <summary>
        /// They type of command option
        /// See <see cref="CommandOptionType"/>
        /// </summary>
        [JsonProperty("type")]
        public CommandOptionType Type { get; set; }
        
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
        /// See <see cref="CommandOption"/>
        /// </summary>
        [JsonProperty("choices")]
        public List<CommandOptionChoice> Choices { get; set; }
        
        /// <summary>
        /// If the option is a subcommand or subcommand group type, this nested options will be the parameters
        /// See <see cref="CommandOption"/>
        /// </summary>
        [JsonProperty("options")]
        public List<CommandOption> Options { get; set; }
    }
}