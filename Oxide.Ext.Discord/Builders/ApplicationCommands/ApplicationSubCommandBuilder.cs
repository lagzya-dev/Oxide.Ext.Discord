using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Builders.ApplicationCommands.BaseBuilders;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Exceptions.Builders;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Application Sub Command Builder
    /// </summary>
    public class ApplicationSubCommandBuilder : BaseSubCommandBuilder<ApplicationSubCommandBuilder, ApplicationCommandBuilder>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options for the sub command</param>
        /// <param name="name">Name of the sub command</param>
        /// <param name="description">Description of the sub command</param>
        /// <param name="parent">Parent for this builder</param>
        public ApplicationSubCommandBuilder(List<CommandOption> options, string name, string description, ApplicationCommandBuilder parent, string defaultLanguage) : base(options, name, description, parent, defaultLanguage) { }
        
        /// <summary>
        /// Adds a new option
        /// </summary>
        /// <param name="type">Option data type (Cannot be SubCommand or SubCommandGroup)</param>
        /// <param name="name">Name of the option</param>
        /// <param name="description">Description of the option</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ApplicationSubCommandOptionBuilder AddOption(CommandOptionType type, string name, string description)
        {
            ApplicationCommandBuilderException.ThrowIfInvalidCommandOptionType(type);
            return new ApplicationSubCommandOptionBuilder(SubCommand.Options, type, name, description, Builder, DefaultLanguage);
        }
    }
}