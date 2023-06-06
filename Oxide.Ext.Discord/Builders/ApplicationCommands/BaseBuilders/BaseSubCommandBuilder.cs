using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Libraries.Locale;

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
        
        /// <summary>
        /// Default language being built
        /// </summary>
        protected readonly ServerLocale DefaultLanguage;
        
        private readonly TParent _parent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">The options for the sub command</param>
        /// <param name="name">Name of the sub command</param>
        /// <param name="description">Description of the sub command</param>
        /// <param name="parent">Parent for the sub command</param>
        /// <param name="defaultLanguage">Default language being built</param>
        protected BaseSubCommandBuilder(List<CommandOption> options, string name, string description, TParent parent, ServerLocale defaultLanguage)
        {
            Builder = (TBuilder)this;
            _parent = parent;
            SubCommand = new CommandOption(name, description, CommandOptionType.SubCommand, new List<CommandOption>());
            options.Add(SubCommand);
            DefaultLanguage = defaultLanguage;
            AddNameLocalization(name, DefaultLanguage);
            AddDescriptionLocalization(description, DefaultLanguage);
        }

         /// <summary>
        /// Adds command name localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddNameLocalizations(Plugin plugin, string langKey) has been deprecated and will be removed in the future. Please use AddNameLocalization(string name, string lang) instead.")]
        public TBuilder AddNameLocalizations(Plugin plugin, string langKey)
        {
            SubCommand.NameLocalizations = DiscordLocales.Instance.GetDiscordLocalizations(plugin, langKey);
            return Builder;
        }
         
         /// <summary>
         /// Adds Application Sub Command Name Localization
         /// </summary>
         /// <param name="name">Localized name value</param>
         /// <param name="serverLocale">Oxide lang the value is in</param>
         /// <returns>This</returns>
         public TBuilder AddNameLocalization(string name, ServerLocale serverLocale)
         {
             DiscordLocale discordLocale = serverLocale.GetDiscordLocale();
             if (discordLocale.IsValid)
             {
                 SubCommand.NameLocalizations[discordLocale.Id] = name;
             }
             
             return Builder;
         }
        
        /// <summary>
        /// Adds command description localizations for a given plugin and lang key
        /// </summary>
        /// <param name="plugin">Plugin containing the localizations</param>
        /// <param name="langKey">Lang Key containing the localized text</param>
        /// <returns></returns>
        [Obsolete("AddDescriptionLocalizations(Plugin plugin, string langKey) has been deprecated and will be removed in the future. Please use AddDescriptionLocalization(string name, string lang) instead.")]
        public TBuilder AddDescriptionLocalizations(Plugin plugin, string langKey)
        {
            SubCommand.DescriptionLocalizations = DiscordLocales.Instance.GetDiscordLocalizations(plugin, langKey);
            return Builder;
        }
        
        /// <summary>
        /// Adds Application Sub Command Description Localizations
        /// </summary>
        /// <param name="description">Localized description value</param>
        /// <param name="serverLocale">Oxide lang the value is in</param>
        /// <returns>This</returns>
        public TBuilder AddDescriptionLocalization(string description, ServerLocale serverLocale)
        {
            DiscordLocale discordLocale = serverLocale.GetDiscordLocale();
            if (discordLocale.IsValid)
            { 
                SubCommand.DescriptionLocalizations[discordLocale.Id] = description;
            }
            
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