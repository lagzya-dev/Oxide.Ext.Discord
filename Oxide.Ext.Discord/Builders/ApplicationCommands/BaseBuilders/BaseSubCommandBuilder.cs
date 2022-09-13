using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands.BaseBuilders
{
    /// <summary>
    /// Base Subcommand Builder
    /// </summary>
    /// <typeparam name="TBuilder">Child Builder that inherits from this type</typeparam>
    /// <typeparam name="TParent">Parent for this builder</typeparam>
    public abstract class BaseSubCommandBuilder<TBuilder, TParent> where TBuilder : BaseSubCommandBuilder<TBuilder, TParent>
    {
        /// <summary>
        /// The child builder for this type
        /// </summary>
        protected readonly TBuilder Builder;

        /// <summary>
        /// The subcommand for the builder
        /// </summary>
        protected readonly CommandOption SubCommand;
        
        private readonly TParent _parent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">The options for the sub command</param>
        /// <param name="name">Name of the sub command</param>
        /// <param name="description">Description of the sub command</param>
        /// <param name="parent">Parent for the sub command</param>
        protected BaseSubCommandBuilder(List<CommandOption> options, string name, string description, TParent parent)
         {
             Builder = (TBuilder)this;
            _parent = parent;
            SubCommand = new CommandOption(name, description, CommandOptionType.SubCommand, new List<CommandOption>());
            options.Add(SubCommand);
        }

         /// <summary>
        /// Adds command name localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddNameLocalizations has been deprecated and will be removed in the future. Please upgrade to DiscordCommandLocalizations for Application Command localization")]
        public TBuilder AddNameLocalizations(Plugin plugin, string langKey)
        {
            SubCommand.NameLocalizations =  DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return Builder;
        }
        
        /// <summary>
        /// Adds command description localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddDescriptionLocalizations has been deprecated and will be removed in the future. Please upgrade to DiscordCommandLocalizations for Application Command localization")]
        public TBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
        {
            SubCommand.DescriptionLocalizations =  DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return Builder;
        }

        /// <summary>
        /// Builds the sub command and returns the parent builder
        /// </summary>
        /// <returns></returns>
        public TParent Build()
        {
            return _parent;
        }
    }
}