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
    public class GroupSubCommandBuilder : BaseSubCommandBuilder<GroupSubCommandBuilder, SubCommandGroupBuilder>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">The options for the Sub Command Group</param>
        /// <param name="name">Name of the Sub Command Group</param>
        /// <param name="description">Description of the Sub Command Group</param>
        /// <param name="parent">The parent builder for this builder</param>
        /// <param name="defaultLanguage">Default language for the builder</param>
        internal GroupSubCommandBuilder(List<CommandOption> options, string name, string description, SubCommandGroupBuilder parent, string defaultLanguage) : base(options, name, description, parent, defaultLanguage) { }
        
        /// <summary>
        /// Adds a new option
        /// </summary>
        /// <param name="type">Option data type (Cannot be SubCommand or SubCommandGroup)</param>
        /// <param name="name">Name of the option</param>
        /// <param name="description">Description of the option</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GroupSubCommandOptionBuilder AddOption(CommandOptionType type, string name, string description)
        {
            ApplicationCommandBuilderException.ThrowIfInvalidCommandOptionType(type);
            return new GroupSubCommandOptionBuilder(SubCommand.Options, type, name, description, Builder, DefaultLanguage);
        }
    }
}