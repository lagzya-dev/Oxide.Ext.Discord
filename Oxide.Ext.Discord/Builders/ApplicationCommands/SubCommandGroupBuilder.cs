using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Builder for Sub Command Groups
    /// </summary>
    public class SubCommandGroupBuilder
    {
        private readonly CommandOption _option;

        internal SubCommandGroupBuilder(string name, string description, ApplicationCommandBuilder builder)
        {
            _option = new CommandOption
            {
                Name = name,
                Description = description,
                Type = CommandOptionType.SubCommandGroup,
                Options = new List<CommandOption>()
            };
            builder.Command.Options.Add(_option);
        }
        
        /// <summary>
        /// Adds command name localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        public SubCommandGroupBuilder AddNameLocalizations(Plugin plugin, string langKey)
        {
            _option.NameLocalizations = LocaleConverter.GetCommandLocalization(plugin, langKey);
            return this;
        }
        
        /// <summary>
        /// Adds command description localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        public SubCommandGroupBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
        {
            _option.DescriptionLocalizations = LocaleConverter.GetCommandLocalization(plugin, langKey);
            return this;
        }

        /// <summary>
        /// Adds a sub command to this sub command group
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="description">Description of the command</param>
        /// <returns><see cref="SubCommandBuilder"/></returns>
        public SubCommandBuilder AddSubCommand(string name, string description)
        {
            return new SubCommandBuilder(_option.Options, name, description);
        }
    }
}