using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Builder for Sub Command Groups
    /// </summary>
    public class SubCommandGroupBuilder
    {
        private readonly ApplicationCommandBuilder _builder;
        private readonly CommandOption _option;

        internal SubCommandGroupBuilder(string name, string description, ApplicationCommandBuilder builder)
        {
            _option = new CommandOption(name, description, CommandOptionType.SubCommandGroup, new List<CommandOption>());
            _builder = builder;
            builder.Command.Options.Add(_option);
        }
        
        /// <summary>
        /// Adds command name localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddDescriptionLocalizations has been deprecated and will be removed in the future. Please upgrade to DiscordCommandLocalizations for Application Command localization")]
        public SubCommandGroupBuilder AddNameLocalizations(Plugin plugin, string langKey)
        {
            _option.NameLocalizations =  DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return this;
        }
        
        /// <summary>
        /// Adds command description localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddDescriptionLocalizations has been deprecated and will be removed in the future. Please upgrade to DiscordCommandLocalizations for Application Command localization")]
        public SubCommandGroupBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
        {
            _option.DescriptionLocalizations =  DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return this;
        }

        /// <summary>
        /// Adds a sub command to this sub command group
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="description">Description of the command</param>
        /// <returns><see cref="ApplicationSubCommandBuilder"/></returns>
        public GroupSubCommandBuilder AddSubCommand(string name, string description)
        {
            return new GroupSubCommandBuilder(_option.Options, name, description, this);
        }

        /// <summary>
        /// Returns the built <see cref="DiscordApplicationCommand"/>
        /// </summary>
        /// <returns></returns>
        public ApplicationCommandBuilder Build()
        {
            return _builder;
        }
    }
}