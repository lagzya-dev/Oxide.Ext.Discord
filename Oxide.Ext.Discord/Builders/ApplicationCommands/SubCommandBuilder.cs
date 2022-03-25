using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
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
            parent.Add(new CommandOption
            {
                Name = name,
                Description = description,
                Type = CommandOptionType.SubCommand,
                Options = _options
            });
        }

        /// <summary>
        /// Adds a new option
        /// </summary>
        /// <param name="type">Option data type (Cannot be SubCommand or SubCommandGroup)</param>
        /// <param name="name">Name of the option</param>
        /// <param name="description">Description of the option</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CommandOptionBuilder<SubCommandBuilder> AddOption(CommandOptionType type, string name, string description)
        {
            return new CommandOptionBuilder<SubCommandBuilder>(_options, type, name, description, this);
        }
    }
}