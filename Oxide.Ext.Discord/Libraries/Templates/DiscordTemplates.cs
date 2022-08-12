using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Exceptions.Libraries;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Json.Serialization;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Oxide Library for Discord Templates
    /// </summary>
    public class DiscordTemplates : Library
    {
        private readonly string _rootDir = Path.Combine(Interface.Oxide.InstanceDirectory, "discord", "messages");
        private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();
        private readonly Hash<string, Dictionary<TemplateId, DiscordMessageTemplate>> _pluginTemplates = new Hash<string, Dictionary<TemplateId, DiscordMessageTemplate>>();
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public DiscordTemplates(ILogger logger)
        {
            _logger = logger;
            if (!Directory.Exists(_rootDir))
            {
                Directory.CreateDirectory(_rootDir);
            }
        }

        /// <summary>
        /// Registers a global message template
        /// Global Message templates cannot be localized
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Unique name of the template</param>
        /// <param name="template">Template to register</param>
        /// <param name="minSupportedVersion">Min supported version for this template</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterGlobalMessageTemplate(Plugin plugin, string name, DiscordMessageTemplate template, TemplateVersion minSupportedVersion)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (template == null) throw new ArgumentNullException(nameof(template));

            string path = GetTemplatePath(plugin, name, null);
            if (!File.Exists(path))
            {
                CreateFile(path, template);
                return;
            }

            DiscordMessageTemplate existingTemplate = GetGlobalMessageTemplate(plugin, name);
            if (existingTemplate.Version >= minSupportedVersion)
            {
                return;
            }
            
            MoveFile(plugin, name, null, existingTemplate.Version);
            CreateFile(path, template);
        }
        
        /// <summary>
        /// Registers a message template with the given name and language
        /// </summary>
        /// <param name="plugin">Plugin the <see cref="DiscordMessageTemplate"/> is for</param>
        /// <param name="name">Name of the <see cref="DiscordMessageTemplate"/></param>
        /// <param name="template">Template to be registered</param>
        /// <param name="minSupportedVersion">
        /// The minimum supported template version.<br/>
        /// If an existing template exists and it's version is >=  the minimum supported version then we will use that template.<br/>
        /// If an existing template exists and it's version is &lt; the min supported version. The existing version is backed up and a new template is created 
        /// </param>
        /// <param name="language">Language for the template</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterLocalizedMessageTemplate(Plugin plugin, string name, DiscordMessageTemplate template, TemplateVersion minSupportedVersion, string language = DiscordLocale.DefaultOxideLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (template == null) throw new ArgumentNullException(nameof(template));

            string path = GetTemplatePath(plugin, name, language);
            if (!File.Exists(path))
            {
                CreateFile(path, template);
                return;
            }

            DiscordMessageTemplate existingTemplate = GetLocalizedMessageTemplate(plugin, name, language);
            if (existingTemplate.Version >= minSupportedVersion)
            {
                return;
            }
            
            MoveFile(plugin, name, language, existingTemplate.Version);
            CreateFile(path, template);
        }

        /// <summary>
        /// Returns a message template for a given <see cref="IPlayer"/> player
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <param name="player">IPlayer for the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public DiscordMessageTemplate GetMessageTemplateForPlayer(Plugin plugin, string name, IPlayer player)
        {
            string locale = player != null ? DiscordLocale.GetPlayerLanguage(player) : DiscordLocale.GameServerLanguage;
            return GetLocalizedMessageTemplate(plugin, name, locale);
        }
        
        /// <summary>
        /// Returns a message template for a given <see cref="IPlayer"/> player ID
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <param name="playerId">PlayerId for the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public DiscordMessageTemplate GetMessageTemplateForPlayer(Plugin plugin, string name, string playerId)
        {
            string locale = !string.IsNullOrEmpty(playerId) ? DiscordLocale.GetPlayerLanguage(playerId) : DiscordLocale.GameServerLanguage;
            return GetLocalizedMessageTemplate(plugin, name, locale);
        }

        /// <summary>
        /// Returns a global message template for the plugin with the given name
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <returns><see cref="DiscordMessageTemplate"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public DiscordMessageTemplate GetGlobalMessageTemplate(Plugin plugin, string name)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            Dictionary<TemplateId, DiscordMessageTemplate> templates = _pluginTemplates[plugin.Name];
            if (templates == null)
            {
                templates = new Dictionary<TemplateId, DiscordMessageTemplate>(TemplateId.TemplateIdComparer);
                _pluginTemplates[plugin.Name] = templates;
            }

            TemplateId templateId = new TemplateId(name, null);
            if (templates.TryGetValue(templateId, out DiscordMessageTemplate template))
            {
                return template;
            }

            template = DiscordExtension.DiscordTemplates.LoadTemplate(plugin, name, null);
            templates[templateId] = template;

            return template;
        }
        
        /// <summary>
        /// Returns a message template for a given language
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <param name="language">Oxide language of the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public DiscordMessageTemplate GetLocalizedMessageTemplate(Plugin plugin, string name, string language = DiscordLocale.DefaultOxideLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));

            Dictionary<TemplateId, DiscordMessageTemplate> templates = _pluginTemplates[plugin.Name];
            if (templates == null)
            {
                templates = new Dictionary<TemplateId, DiscordMessageTemplate>(TemplateId.TemplateIdComparer);
                _pluginTemplates[plugin.Name] = templates;
            }

            TemplateId templateId = new TemplateId(name, language);
            if (templates.TryGetValue(templateId, out DiscordMessageTemplate template))
            {
                return template;
            }
            
            template = DiscordExtension.DiscordTemplates.LoadTemplate(plugin, name, language)
                                          ?? DiscordExtension.DiscordTemplates.LoadTemplate(plugin, name, DiscordLocale.GameServerLanguage)
                                          ?? DiscordExtension.DiscordTemplates.LoadTemplate(plugin, name, DiscordLocale.DefaultOxideLanguage);
            
            templates[templateId] = template;

            return template;
        }
        
        /// <summary>
        /// Returns a message template for a given language
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <param name="interaction">Interaction to get the template for</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public DiscordMessageTemplate GetMessageTemplate(Plugin plugin, string name, DiscordInteraction interaction)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            Dictionary<TemplateId, DiscordMessageTemplate> templates = _pluginTemplates[plugin.Name];
            if (templates == null)
            {
                templates = new Dictionary<TemplateId, DiscordMessageTemplate>(TemplateId.TemplateIdComparer);
                _pluginTemplates[plugin.Name] = templates;
            }

            string interactionLang = DiscordLocale.GetOxideLanguage(interaction.Locale);
            TemplateId templateId = new TemplateId(name, interactionLang);
            if (templates.TryGetValue(templateId, out DiscordMessageTemplate template))
            {
                return template;
            }
            
            IPlayer player = interaction.User.Player;
            
            template = DiscordExtension.DiscordTemplates.LoadTemplate(plugin, name, interactionLang)
                                              ?? (player != null ? DiscordExtension.DiscordTemplates.LoadTemplate(plugin, name, DiscordLocale.GetPlayerLanguage(player)) : null)
                                              ?? DiscordExtension.DiscordTemplates.LoadTemplate(plugin, name, DiscordLocale.GetOxideLanguage(interaction.GuildLocale)) 
                                              ?? DiscordExtension.DiscordTemplates.LoadTemplate(plugin, name, DiscordLocale.GameServerLanguage)
                                              ?? DiscordExtension.DiscordTemplates.LoadTemplate(plugin, name, DiscordLocale.DefaultOxideLanguage);
            
            templates[templateId] = template;

            return template;
        }

        private DiscordMessageTemplate LoadTemplate(Plugin plugin, string name, string language)
        {
            string path = GetTemplatePath(plugin, name, language);
            if (!File.Exists(path))
            {
                return null;
            }

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                DiscordJsonReader reader = DiscordPool.Get<DiscordJsonReader>();
                reader.CopyFrom(stream);
                DiscordMessageTemplate template = reader.Deserialize<DiscordMessageTemplate>(_serializer);
                reader.Dispose();
                return template;
            }
        }

        private void CreateFile(string path, DiscordMessageTemplate template)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            FileMode mode = File.Exists(path) ? FileMode.Truncate : FileMode.Create;

            using (FileStream stream = new FileStream(path, mode))
            {
                DiscordJsonWriter writer = new DiscordJsonWriter();
                writer.Write(_serializer, template);
                writer.Stream.CopyToPooled(stream);
                writer.Dispose();
            }
        }

        private void MoveFile(Plugin plugin, string name, string language, TemplateVersion version)
        {
            string oldPath = GetTemplatePath(plugin, name, language);
            if (!File.Exists(oldPath))
            {
                return;
            }

            string newPath = GetRenamePath(plugin, name, language, version);
            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }
            
            File.Move(oldPath, newPath);
        }
        
        private string GetTemplatePath(Plugin plugin, string name, string language)
        {
            DiscordTemplateException.ThrowIfInvalidTemplateName(name);
            if (string.IsNullOrEmpty(language))
            {
                return Path.Combine(_rootDir, plugin.Name, $"{name}.json");
            }
            return Path.Combine(_rootDir, plugin.Name, language, $"{name}.json");
        }
        
        private string GetRenamePath(Plugin plugin, string name, string language, TemplateVersion version)
        {
            return Path.Combine(_rootDir, plugin.Name, language, $"{name}.{version.ToString()}.json");
        }

        internal void OnPluginUnloaded(Plugin plugin)
        {
            _pluginTemplates.Remove(plugin.Name);
        }
    }
}