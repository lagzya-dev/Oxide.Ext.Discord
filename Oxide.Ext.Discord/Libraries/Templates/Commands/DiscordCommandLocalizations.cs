using System;
using System.Collections.Generic;
using System.IO;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Templates;
using Oxide.Ext.Discord.Callbacks.Templates.Commands;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Exceptions.Libraries;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Libraries.Locale;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Promises;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    /// <summary>
    /// Library for localizing <see cref="DiscordApplicationCommand"/>s
    /// </summary>
    public class DiscordCommandLocalizations : BaseTemplateLibrary<DiscordCommandLocalization>
    {
        internal DiscordCommandLocalizations(ILogger logger) : base(TemplateType.Command, logger) { }

        /// <summary>
        /// Registers Application Command Localization for a given language
        /// </summary>
        /// <param name="plugin">Plugin the for the command localization</param>
        /// <param name="fileNameSuffix">Suffix to be applied to the localization. IE DiscordExtension.{suffix}.json (optional)</param>
        /// <param name="localization">Localization to register</param>
        /// <param name="version">Version of the template</param>
        /// <param name="minVersion">Min supported registered version</param>
        /// <param name="language">Language to register</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IPromise RegisterCommandLocalizationAsync(Plugin plugin, string fileNameSuffix, DiscordCommandLocalization localization, TemplateVersion version, TemplateVersion minVersion, string language = DiscordLocales.DefaultServerLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (localization == null) throw new ArgumentNullException(nameof(localization));

            IPendingPromise promise = Promise.Create();
            
            TemplateId id = TemplateId.CreateLocalized(plugin, fileNameSuffix, ServerLocale.Parse(language));
            RegisterTemplateCallback<DiscordCommandLocalization>.Start(this, id, localization, version, minVersion, promise);
            return promise;
        }
        
        /// <summary>
        /// Registers multiple command localizations
        /// </summary>
        /// <param name="plugin">Plugin the for the command localization</param>
        /// <param name="fileNameSuffix">Suffix to be applied to the localization. IE DiscordExtension.{suffix}.json (optional)</param>
        /// <param name="commands">List of <see cref="DiscordCommandLocalization"/> to bulk register</param>
        /// <param name="minVersion">Min supported registered version</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IPromise BulkRegisterCommandLocalizationsAsync(Plugin plugin, string fileNameSuffix, List<BulkTemplateRegistration<DiscordCommandLocalization>> commands, TemplateVersion minVersion)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (commands == null) throw new ArgumentNullException(nameof(commands));

            IPendingPromise promise = Promise.Create();
            
            TemplateId id = TemplateId.CreateGlobal(plugin, fileNameSuffix);
            BulkRegisterTemplateCallback<DiscordCommandLocalization>.Start(this, id, commands, minVersion, promise);
            return promise;
        }

        /// <summary>
        /// Applies Command Localizations Async
        /// </summary>
        /// <param name="plugin">Plugin the localizations are for</param>
        /// <param name="create">The command to apply the localizations to</param>
        /// <param name="fileNameSuffix">fileName suffix used when registering</param>
        /// <returns></returns>
        public IPromise ApplyCommandLocalizationsAsync(Plugin plugin, CommandCreate create, string fileNameSuffix)
        {
            return HandleApplyCommandLocalizationsAsync(plugin, fileNameSuffix, create);
        }
        
        private IPromise HandleApplyCommandLocalizationsAsync(Plugin plugin, string fileNameSuffix, CommandCreate create, IPendingPromise promise = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));

            if (promise == null)
            {
                promise = Promise.Create();
            }
            
            TemplateId id = TemplateId.CreateGlobal(plugin, fileNameSuffix);
            ApplyCommandLocalizationsCallback.Start(id, create, promise);
            return promise;
        }

        internal void HandleApplyCommandLocalizationsAsync(TemplateId id, CommandCreate create, IPendingPromise promise)
        {
            HandleApplyCommandLocalizationsAsync(id, create);
            promise.Resolve();
        }

        private void HandleApplyCommandLocalizationsAsync(TemplateId id, CommandCreate create)
        {
            PrepareCommandLocalizations(create);
            
            foreach (string dir in Directory.EnumerateDirectories(GetTemplateFolder(id.PluginId)))
            {
                ServerLocale lang = ServerLocale.Parse(Path.GetFileName(dir));
                HandleLoadAndApplyCommandLocalizationsAsync(id, create, lang);
            }
        }
        
        private void PrepareCommandLocalizations(CommandCreate create)
        {
            if (create.NameLocalizations == null)
            {
                create.NameLocalizations = new Hash<string, string>();
            }

            if (create.DescriptionLocalizations == null)
            {
                create.DescriptionLocalizations = new Hash<string, string>();
            }

            if (create.Options == null)
            {
                return;
            }

            for (int index = 0; index < create.Options.Count; index++)
            {
                PrepareOptionLocalizations(create.Options[index]);
            }
        }
        
        private void PrepareOptionLocalizations(CommandOption opt)
        {
            if (opt.NameLocalizations == null)
            {
                opt.NameLocalizations = new Hash<string, string>();
            }

            if (opt.DescriptionLocalizations == null)
            {
                opt.DescriptionLocalizations = new Hash<string, string>();
            }

            if (opt.Options != null)
            {
                for (int index = 0; index < opt.Options.Count; index++)
                {
                    PrepareOptionLocalizations(opt.Options[index]);
                }
            }

            if (opt.Choices != null)
            {
                for (int i = 0; i < opt.Choices.Count; i++)
                {
                    CommandOptionChoice choice = opt.Choices[i];
                    if (choice.NameLocalizations == null)
                    {
                        choice.NameLocalizations = new Hash<string, string>();
                    }
                }
            }
        }

        private void HandleLoadAndApplyCommandLocalizationsAsync(TemplateId id, CommandCreate create, ServerLocale locale)
        {
            DiscordLocale discordLocale = locale.GetDiscordLocale();
            if (discordLocale.IsValid)
            {
                DiscordTemplate<DiscordCommandLocalization> localization = LoadTemplate(id.WithLanguage(locale));
                localization?.Template.ApplyCommandLocalization(create, discordLocale);
            }
        }

        internal override string GetTemplatePath(TemplateId id)
        {
            DiscordTemplateException.ThrowIfInvalidTemplateName(id.TemplateName, TemplateType);
            string fileName = !string.IsNullOrEmpty(id.TemplateName) ? $"{id.PluginId}.{id.TemplateName}.json" : $"{id.PluginId}.json";
            return Path.Combine(GetTemplateFolder(id.PluginId), id.Language.Id, fileName);
        }

        ///<inheritdoc/>
        protected override void OnPluginLoaded(Plugin plugin) { }
    }
}