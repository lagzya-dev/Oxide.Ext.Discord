using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-interaction-data-option-structure">Application Command Interaction Data Option</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class InteractionDataOption
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
        public CommandOptionType Type { get; set; }
        
        /// <summary>
        /// The value of the option resulting from user input
        /// Value can be string, integer, or double, or boolean type
        /// </summary>
        [JsonProperty("value")]
        private object Value { get; set; }

        public string GetString() => (string)Value;

        public int GetInt() => (int)Value;

        public bool GetBool() => (bool)Value;

        public double GetNumber() => (double)Value;

        public Snowflake GetSnowflake() => new Snowflake((ulong)Value);

        /// <summary>
        /// Present if this option is a group or subcommand
        /// See <see cref="InteractionDataOption"/>
        /// </summary>
        [JsonProperty("options")]
        public List<InteractionDataOption> Options { get; set; }
        
        /// <summary>
        /// True if this option is the currently focused option for autocomplete
        /// </summary>
        [JsonProperty("focused")]
        public bool? Focused { get; set; }
    }
}