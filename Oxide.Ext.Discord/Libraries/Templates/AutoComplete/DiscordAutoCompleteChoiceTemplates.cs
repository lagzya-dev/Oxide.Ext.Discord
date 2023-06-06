using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Templates;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Libraries.Locale;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;
using Oxide.Ext.Discord.Promises;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.AutoComplete
{
    public class DiscordAutoCompleteChoiceTemplates : BaseTemplateLibrary<DiscordAutoCompleteChoiceTemplate>
    {
        private readonly ConcurrentDictionary<TemplateId, DiscordAutoCompleteChoiceTemplate> _globalCache = new ConcurrentDictionary<TemplateId, DiscordAutoCompleteChoiceTemplate>();
        private readonly ConcurrentDictionary<TemplateId, List<LocalizedTemplate>> _localizedCache = new ConcurrentDictionary<TemplateId, List<LocalizedTemplate>>();

        public DiscordAutoCompleteChoiceTemplates(ILogger logger) : base(TemplateType.AutoCompleteChoice, logger) { }

        public IPromise RegisterGlobalTemplate(Plugin plugin, string templateName, DiscordAutoCompleteChoiceTemplate template, TemplateVersion version, TemplateVersion minVersion)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));

            TemplateId id = TemplateId.CreateGlobal(plugin, templateName);
            return RegisterTemplate(id, template, version, minVersion);
        }
        
        public IPromise RegisterLocalizedTemplate(Plugin plugin, string templateName, DiscordAutoCompleteChoiceTemplate template, TemplateVersion version, TemplateVersion minVersion, string language = DiscordLocales.DefaultServerLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));

            TemplateId id = TemplateId.CreateLocalized(plugin, templateName, ServerLocale.Parse(language));
            return RegisterTemplate(id, template, version, minVersion);
        }

        private IPromise RegisterTemplate(TemplateId id, DiscordAutoCompleteChoiceTemplate template, TemplateVersion version, TemplateVersion minVersion)
        {
            if (template == null) throw new ArgumentNullException(nameof(template));
            IPendingPromise promise = Promise.Create();
            RegisterTemplateCallback<DiscordAutoCompleteChoiceTemplate>.Start(this, id, template, version, minVersion, promise);
            return promise;
        }

        public DiscordAutoCompleteChoiceTemplate GetGlobalTemplate(Plugin plugin, string templateName)
        {
            TemplateId id = TemplateId.CreateGlobal(plugin, templateName);
            if (_globalCache.TryGetValue(id, out DiscordAutoCompleteChoiceTemplate cached))
            {
                return cached;
            }

            DiscordTemplate<DiscordAutoCompleteChoiceTemplate> template = LoadTemplate(id);
            
            if (template == null)
            {
                Logger.Error("Plugin {0} is using the {1} Template API but message template name '{2}' is not registered", id.GetPluginName(), GetType().Name, id.TemplateName);
                return new DiscordAutoCompleteChoiceTemplate();
            }
            
            SetCache(id, template.Template);

            return template.Template;
        }

        public CommandOptionChoice ApplyGlobal(Plugin plugin, string templateName, CommandOptionChoice choice = null, PlaceholderData placeholders = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            
            if (choice == null)
            {
                choice = new CommandOptionChoice();
            }
            
            TemplateId id = TemplateId.CreateGlobal(plugin, templateName);
            ApplyGlobal(id, choice, placeholders);
            return choice;
        }

        private void ApplyGlobal(TemplateId id, CommandOptionChoice choice, PlaceholderData placeholders)
        {
            LoadTemplate(id)?.Template.ApplyName(choice, placeholders);
        }

        public CommandOptionChoice ApplyLocalized(Plugin plugin, string templateName, CommandOptionChoice choice = null, PlaceholderData placeholders = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));

            if (choice == null)
            {
                choice = new CommandOptionChoice();
            }
            
            TemplateId id = TemplateId.CreateGlobal(plugin, templateName);
            ApplyLocalizations(id, choice, placeholders);
            return choice;
        }

        private void ApplyLocalizations(TemplateId id, CommandOptionChoice choice, PlaceholderData placeholders)
        {
            placeholders?.ManualPool();
            
            if (choice.NameLocalizations == null)
            {
                choice.NameLocalizations = new Hash<string, string>();
            }
            
            //Apply global template to the name field if it exists
            ApplyGlobal(id, choice, placeholders);

            List<LocalizedTemplate> list = GetLocalizedTemplates(id);
            for (int index = 0; index < list.Count; index++)
            {
                LocalizedTemplate template = list[index];
                template.Template.ApplyLocalization(template.Locale, choice, placeholders);
            }

            placeholders?.Dispose();
        }

        private List<LocalizedTemplate> GetLocalizedTemplates(TemplateId id)
        {
            if(_localizedCache.TryGetValue(id, out List<LocalizedTemplate> templates))
            {
                return templates;
            }

            templates = new List<LocalizedTemplate>();

            foreach (string dir in Directory.EnumerateDirectories(GetTemplateFolder(id.PluginId)))
            {
                ServerLocale locale = ServerLocale.Parse(Path.GetFileName(dir));
                DiscordLocale discordLocale = locale.GetDiscordLocale();
                if (!discordLocale.IsValid)
                {
                    continue;
                }
                
                DiscordAutoCompleteChoiceTemplate template = LoadTemplate(id.WithLanguage(locale))?.Template;
                if (template != null)
                {
                    templates.Add(new LocalizedTemplate(discordLocale, template));    
                }
            }

            _localizedCache[id] = templates;

            return templates;
        }
        
        private void SetCache(TemplateId id, DiscordAutoCompleteChoiceTemplate template)
        {
            if (id.IsGlobal)
            {
                _globalCache[id] = template;
            }
        }

        internal override void OnTemplateRegistered(TemplateId id, DiscordAutoCompleteChoiceTemplate template)
        {
            if (id.IsGlobal)
            {
                _globalCache[id] = template;
            }
        }
        
        protected override void OnPluginUnloaded(Plugin plugin)
        {
            base.OnPluginUnloaded(plugin);
            PluginId pluginId = plugin.Id();
            _globalCache.RemoveAll(t => t.PluginId == pluginId);
            _localizedCache.RemoveAll(t => t.PluginId == pluginId);
        }
        
        private struct LocalizedTemplate
        {
            public readonly DiscordLocale Locale;
            public readonly DiscordAutoCompleteChoiceTemplate Template;

            public LocalizedTemplate(DiscordLocale locale, DiscordAutoCompleteChoiceTemplate template)
            {
                Locale = locale;
                Template = template;
            }
        }
    }
}