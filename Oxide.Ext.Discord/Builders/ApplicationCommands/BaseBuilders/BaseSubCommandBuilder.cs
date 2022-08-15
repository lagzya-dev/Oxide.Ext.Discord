using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Helpers;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands.BaseBuilders
{
    public abstract class BaseSubCommandBuilder<TBuilder, TParent> where TBuilder : BaseSubCommandBuilder<TBuilder, TParent>
    {
        protected readonly TBuilder _builder;
        private readonly TParent _parent;
        protected readonly CommandOption _subCommand;

        protected BaseSubCommandBuilder(List<CommandOption> options, string name, string description, TParent parent)
         {
             _builder = (TBuilder)this;
            _parent = parent;
            _subCommand = new CommandOption(name, description, CommandOptionType.SubCommand, new List<CommandOption>());
            options.Add(_subCommand);
        }

         /// <summary>
        /// Adds command name localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        public TBuilder AddNameLocalizations(Plugin plugin, string langKey)
        {
            _subCommand.NameLocalizations = DiscordLocale.GetCommandLocalization(plugin, langKey);
            return _builder;
        }
        
        /// <summary>
        /// Adds command description localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        public TBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
        {
            _subCommand.DescriptionLocalizations = DiscordLocale.GetCommandLocalization(plugin, langKey);
            return _builder;
        }

        public TParent Build()
        {
            return _parent;
        }
    }
}