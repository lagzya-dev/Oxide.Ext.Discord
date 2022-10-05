using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Plugins;

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
        
        protected readonly string DefaultLanguage;
        
        private readonly TParent _parent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">The options for the sub command</param>
        /// <param name="name">Name of the sub command</param>
        /// <param name="description">Description of the sub command</param>
        /// <param name="parent">Parent for the sub command</param>
        protected BaseSubCommandBuilder(List<CommandOption> options, string name, string description, TParent parent, string defaultLanguage)
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
            SubCommand.NameLocalizations =  DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return Builder;
        }
         
         public TBuilder AddNameLocalization(string name, string lang)
         {
             if (DiscordExtension.DiscordLang.TryGetDiscordLocale(lang, out string discordLocale))
             {
                 lang = discordLocale;
             }
            
             SubCommand.NameLocalizations[lang] = name;
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
            SubCommand.DescriptionLocalizations =  DiscordExtension.DiscordLang.GetCommandLocalization(plugin, langKey);
            return Builder;
        }
        
        public TBuilder AddDescriptionLocalization(string name, string lang)
        {
            if (DiscordExtension.DiscordLang.TryGetDiscordLocale(lang, out string discordLocale))
            {
                lang = discordLocale;
            }
            
            SubCommand.DescriptionLocalizations[lang] = name;
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