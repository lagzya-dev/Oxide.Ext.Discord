using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Templates;
using Oxide.Ext.Discord.Callbacks.Templates.Commands;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Exceptions.Libraries;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Commands
{
    /// <summary>
    /// Library for localizing <see cref="DiscordApplicationCommand"/>s
    /// </summary>
    public class DiscordCommandLocalizations : BaseTemplateLibrary
    {
        
        internal DiscordCommandLocalizations(ILogger logger) : base(Path.Combine(Interface.Oxide.InstanceDirectory, "discord", "commands"), logger) { }
        
        /// <summary>
        /// Registers Application Command Localization for a given language
        /// </summary>
        /// <param name="plugin">Plugin the for the command localization</param>
        /// <param name="fileNameSuffix">Suffix to be applied to the localization. IE DiscordExtension.{suffix}.json (optional)</param>
        /// <param name="localization">Localization to register</param>
        /// <param name="minVersion">Min supported registered version</param>
        /// <param name="language">Language to register</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordAsyncCallback RegisterCommandLocalization(Plugin plugin, string fileNameSuffix, DiscordCommandLocalization localization, TemplateVersion minVersion, string language = DiscordLang.DefaultOxideLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (localization == null) throw new ArgumentNullException(nameof(localization));

            IDiscordAsyncCallback callback = PluginAsyncCallback.Create();
            
            TemplateId id = new TemplateId(plugin, fileNameSuffix, language);
            RegisterTemplateCallback<DiscordCommandLocalization>.Start(this, id, localization, minVersion, callback);
            return callback;
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
        public IDiscordAsyncCallback BulkRegisterCommandLocalizations(Plugin plugin, string fileNameSuffix, List<BulkTemplateRegistration<DiscordCommandLocalization>> commands, TemplateVersion minVersion)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (commands == null) throw new ArgumentNullException(nameof(commands));

            IDiscordAsyncCallback callback = PluginAsyncCallback.Create();
            
            TemplateId id = new TemplateId(plugin, fileNameSuffix, null);
            BulkRegisterTemplateCallback<DiscordCommandLocalization>.Start(this, id, commands, minVersion, callback);
            return callback;
        }

        /// <summary>
        /// Applies Command Localizations Async
        /// </summary>
        /// <param name="plugin">Plugin the localizations are for</param>
        /// <param name="create">The command to apply the localizations to</param>
        /// <param name="fileNameSuffix">fileName suffix used when registering</param>
        /// <returns></returns>
        public IDiscordAsyncCallback ApplyCommandLocalizationsAsync(Plugin plugin, CommandCreate create, string fileNameSuffix)
        {
            return HandleApplyCommandLocalizations(plugin, fileNameSuffix, create, PluginAsyncCallback.Create());
        }
        
        private IDiscordAsyncCallback HandleApplyCommandLocalizations(Plugin plugin, string fileNameSuffix, CommandCreate create, IDiscordAsyncCallback callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));

            if (callback == null)
            {
                callback = InternalAsyncCallback.Create();
            }
            
            TemplateId id = new TemplateId(plugin, fileNameSuffix, null);
            ApplyCommandLocalizationsCallback.Start(id, create, callback);
            return callback;
        }

        internal async Task HandleApplyCommandLocalizations(TemplateId id, CommandCreate create, IDiscordAsyncCallback callback)
        {
            await HandlePrepareCommandLocalizationsAsync(create).ConfigureAwait(false);

            List<Task> tasks = DiscordPool.GetList<Task>();
            foreach (string dir in Directory.EnumerateDirectories(GetTemplateFolder(TemplateType.Command, id.PluginName)))
            {
                string lang = Path.GetFileName(dir);
                tasks.Add(HandleLoadAndApplyCommandLocalizations(id, create, lang));
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
            DiscordPool.FreeList(ref tasks);
            callback.InvokeSuccess();
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
        private Task HandlePrepareCommandLocalizationsAsync(CommandCreate create)
        {
            PrepareCommandLocalizations(create);
            return Task.CompletedTask;
        }

        private async Task HandleLoadAndApplyCommandLocalizations(TemplateId id, CommandCreate create, string lang)
        {
            DiscordCommandLocalization localization = await LoadTemplate<DiscordCommandLocalization>(TemplateType.Command, new TemplateId(id, lang)).ConfigureAwait(false);
            if (localization != null)
            {
                await localization.HandleApplyCommandLocalizationAsync(create, lang).ConfigureAwait(false);
            }
        }

        internal override string GetTemplateFolder(TemplateType type, string plugin)
        {
            return RootDir;
        }
        
        internal override string GetTemplatePath(TemplateType type, TemplateId id)
        {
            DiscordTemplateException.ThrowIfInvalidTemplateName(id.TemplateName);
            string fileName = !string.IsNullOrEmpty(id.TemplateName) ? $"{id.PluginName}.{id.TemplateName}.json" : $"{id.PluginName}.json";
            return Path.Combine(RootDir, id.Language, fileName);
        }
        
        internal override void OnPluginUnloaded(Plugin plugin)
        {
            string name = plugin.Name;
            RegisteredTemplates.RemoveWhere(rt => rt.PluginName == name);
        }
    }
}