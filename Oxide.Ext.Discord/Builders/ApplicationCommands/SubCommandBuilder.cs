using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Exceptions.Builders;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Base Sub Command builder
    /// </summary>
    public class SubCommandBuilder
    {
        private readonly CommandOption _subCommand;
        
        /// <summary>
        /// Options list to have options added to
        /// </summary>
        private readonly List<CommandOption> _options;

        internal SubCommandBuilder(List<CommandOption> parent, string name, string description)
        {
            _options = new List<CommandOption>();
            _subCommand = new CommandOption(name, description, CommandOptionType.SubCommand, _options);
            parent.Add(_subCommand);
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
        
        /// <summary>
        /// Adds command name localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        public SubCommandBuilder AddNameLocalizations(Plugin plugin, string langKey)
        {
            _subCommand.NameLocalizations = DiscordLocale.GetCommandLocalization(plugin, langKey);
            return this;
        }
        
        /// <summary>
        /// Adds command description localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        public SubCommandBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
        {
            _subCommand.DescriptionLocalizations = DiscordLocale.GetCommandLocalization(plugin, langKey);
            return this;
        }
    }
}