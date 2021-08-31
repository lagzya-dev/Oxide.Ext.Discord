using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Base Sub Command builder
    /// </summary>
    public class SubCommandBuilder<T>
    {
        private readonly T _builder;
        /// <summary>
        /// Options list to have options added to
        /// </summary>
        private readonly List<CommandOption> _options;

        internal SubCommandBuilder(List<CommandOption> parent, string name, string description, T builder)
        {
            _builder = builder;
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
        public CommandOptionBuilder<SubCommandBuilder<T>> AddOption(CommandOptionType type, string name, string description)
        {
            return new CommandOptionBuilder<SubCommandBuilder<T>>(_options, type, name, description, this);
        }

        /// <summary>
        /// Returns the built sub command
        /// </summary>
        /// <returns></returns>
        public T Build()
        {
            return _builder;
        }
    }
}