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

        /// <summary>
        /// Returns the Value of the option casted to T
        /// Supported Conversions
        /// CommandOptionType.String => string
        /// CommandOptionType.Integer => byte, sbyte, short, ushort, int, uint, long, ulong
        /// CommandOptionType.Number => float, double
        /// CommandOptionType.Boolean => bool
        /// CommandOptionType.User => ulong
        /// CommandOptionType.Channel => ulong
        /// CommandOptionType.Role => ulong
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if type isn't in a valid range for Type</exception>
        public T GetValue<T>() where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
        {
            switch (Type)
            {
                case CommandOptionType.String:
                case CommandOptionType.Integer:
                case CommandOptionType.Boolean:
                case CommandOptionType.Number:
                case CommandOptionType.User:
                case CommandOptionType.Channel:
                case CommandOptionType.Role:
                    return (T)Value;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Returns the value type
        /// </summary>
        /// <returns></returns>
        public Snowflake GetValueAsSnowflake()
        {
            return new Snowflake(GetValue<ulong>());
        }
        
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