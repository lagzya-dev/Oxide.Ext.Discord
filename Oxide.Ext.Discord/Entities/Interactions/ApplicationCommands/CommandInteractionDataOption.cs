using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction-applicationcommandinteractiondataoption">Application Command Interaction Data Option</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandInteractionDataOption
    {
        /// <summary>
        /// The name of the parameter
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Value of ApplicationCommandOptionType
        /// </summary>
        [JsonProperty("type")]
        public int Type { get; set; }
        
        /// <summary>
        /// The value of the pair
        /// See <see cref="CommandOptionType"/>
        /// </summary>
        [JsonProperty("value")]
        public CommandOptionType Value { get; set; }
        
        /// <summary>
        /// Present if this option is a group or subcommand
        /// See <see cref="CommandInteractionDataOption"/>
        /// </summary>
        [JsonProperty("options")]
        public List<CommandInteractionDataOption> Options { get; set; }
    }
}