using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Exceptions.Builders;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Base Sub Command builder
    /// </summary>
    public class SubCommandBuilder
    {
        /// <summary>
        /// Options list to have options added to
        /// </summary>
        private readonly List<CommandOption> _options;

        internal SubCommandBuilder(List<CommandOption> parent, string name, string description)
        {
            _options = new List<CommandOption>();
            parent.Add(new CommandOption(name, description, CommandOptionType.SubCommand, _options));
        }

        /// <summary>
        /// Adds a new option
        /// </summary>
        /// <param name="type">Option data type (Cannot be SubCommand or SubCommandGroup)</param>
        /// <param name="name">Name of the option</param>
        /// <param name="description">Description of the option</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SubCommandOptionBuilder AddOption(CommandOptionType type, string name, string description)
        {
            ApplicationCommandBuilderException.ThrowIfInvalidCommandOptionType(type);
            
            return new SubCommandOptionBuilder(_options, type, name, description, this);
        }
    }
}