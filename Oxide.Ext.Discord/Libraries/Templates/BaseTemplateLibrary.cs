using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Exceptions.Libraries;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Json;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins;
using Oxide.Ext.Discord.Types;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Oxide Library for Discord Templates
    /// </summary>
    public abstract class BaseTemplateLibrary<TTemplate> : BaseDiscordLibrary where TTemplate : class
    {
        /// <summary>
        /// Logger for the <see cref="BaseTemplateLibrary{TTemplate}"/>
        /// </summary>
        protected readonly ILogger Logger;
        
        /// <summary>
        /// Root Directory for the library
        /// </summary>
        private readonly string _rootDirectory;

        /// <summary>
        /// Template Type Directory
        /// </summary>
        private readonly string _templateTypeDirectory;
        
        /// <summary>
        /// The template type of this template library
        /// </summary>
        protected readonly TemplateType TemplateType;
        
        internal readonly DiscordConcurrentSet<TemplateId> RegisteredTemplates = new DiscordConcurrentSet<TemplateId>();
        private readonly Hash<PluginId, string> _pluginTemplatePath = new Hash<PluginId, string>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">The template type of this library</param>
        /// <param name="logger"></param>
        protected BaseTemplateLibrary(TemplateType type, ILogger logger)
        {
            _rootDirectory = Path.Combine(Interface.Oxide.InstanceDirectory, "discord");
            Logger = logger;
            TemplateType = type;
            _templateTypeDirectory = EnumCache<TemplateType>.Instance.ToLower(TemplateType);

            if (!Directory.Exists(_rootDirectory))
            {
                Directory.CreateDirectory(_rootDirectory);
            }
        }
        
        internal void HandleRegisterTemplate(TemplateId id, TTemplate template, TemplateVersion version, TemplateVersion minVersion, IPendingPromise promise)
        {
            if (version < minVersion)
            {
                Logger.Error("Failed to register for plugin: {0} Name: {1} Language: {2} because the template version {3} is less than the min supported version {4}", id.GetPluginName(), id.TemplateName, id.GetLanguageName(), version, minVersion);
                return;
            }
            
            if (!RegisteredTemplates.Add(id))
            {
                Logger.Warning("Trying to register template multiple times. Type: {0} {1}", TemplateType, id);
            }

            DiscordTemplate<TTemplate> registeringTemplate = new DiscordTemplate<TTemplate>(template, version);
            string path = GetTemplatePath(id);
            if (File.Exists(path))
            {
                DiscordTemplate<TTemplate> existingTemplate = LoadTemplate(id);
                if (existingTemplate != null)
                {
                    if (existingTemplate.Version < minVersion)
                    {
                        BackupTemplateFiles(id, minVersion);
                        CreateFile(path, registeringTemplate);
                        OnTemplateRegistered(id, template);
                    }
                    else
                    {
                        OnTemplateRegistered(id, existingTemplate.Template);
                    }
                }
            }
            else
            {
                CreateFile(path, registeringTemplate);
                OnTemplateRegistered(id, template);
            }
            
            promise.Resolve();
        }
        
        internal void HandleBulkRegisterTemplate(TemplateId id, List<BulkTemplateRegistration<TTemplate>> templates, TemplateVersion minVersion, IPendingPromise promise)
        {
            foreach (BulkTemplateRegistration<TTemplate> registration in templates)
            {
                HandleRegisterTemplate(id.WithLanguage(registration.Language), registration.Template, registration.Version, minVersion, null);
            }
            
            promise.Resolve();
        }

        internal DiscordTemplate<TTemplate> LoadTemplate(TemplateId id)
        {
            string path = GetTemplatePath(id);
            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<DiscordTemplate<TTemplate>>(json, JsonSettings.Indented);
            }
            catch (Exception ex)
            {
                Logger.Exception("Failed to load template from file: {0} Path: {1}", id.ToString(), path.Substring(Interface.Oxide.RootDirectory.Length), ex);
                return null;
            }
        }

        internal DiscordTemplate<TTemplate> LoadTemplate(TemplateId id, string language)
        {
            return LoadTemplate(id.WithLanguage(language));
        }

        private void CreateFile(string path, DiscordTemplate<TTemplate> template)
        {
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonConvert.SerializeObject(template, JsonSettings.Indented);
            File.WriteAllText(path, json);
        }

        private void BackupTemplateFiles(TemplateId id, TemplateVersion minVersion)
        {
            if (id.IsGlobal)
            {
                BackupTemplate(minVersion, id);
                return;
            }

            string path = GetTemplateFolder(id.PluginId);
            if (Logger.IsLogging(DiscordLogLevel.Debug))
            {
                Logger.Debug("Backup Template files for: {0} Path: {1}", id.ToString(), path);
            }
            
            foreach (string dir in Directory.EnumerateDirectories(path))
            {
                string lang = Path.GetFileName(dir);
                Logger.Debug("Processing Directory: {0} Lang: {1}", dir, lang);
                BackupTemplate(minVersion, id.WithLanguage(lang));
            }
        }
        
        private void BackupTemplate(TemplateVersion minVersion, TemplateId langId)
        {
            string oldPath = GetTemplatePath(langId);
            if (!File.Exists(oldPath))
            {
                return;
            }

            DiscordTemplate<TTemplate> template = LoadTemplate(langId);
            if (template == null)
            {
                return;
            }

            if (template.Version >= minVersion)
            {
                return;
            }

            string newPath = GetRenamePath(oldPath, template.Version);
            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }

            File.Move(oldPath, newPath);
        }

        /// <summary>
        /// Returns the template folder for a given plugin
        /// </summary>
        /// <param name="plugin">Plugin Name the template is for</param>
        /// <returns></returns>
        protected string GetTemplateFolder(PluginId plugin)
        {
            string folder = _pluginTemplatePath[plugin];
            if (string.IsNullOrEmpty(folder))
            {
                folder = Path.Combine(_rootDirectory, plugin.PluginName(), _templateTypeDirectory);
                _pluginTemplatePath[plugin] = folder;
            }
            
            return folder;
        }

        internal virtual string GetTemplatePath(TemplateId id)
        {
            DiscordTemplateException.ThrowIfInvalidTemplateName(id.TemplateName, TemplateType);

            if (id.IsGlobal)
            {
                return Path.Combine(GetTemplateFolder(id.PluginId), $"{id.TemplateName}.json");
            }
            return Path.Combine(GetTemplateFolder(id.PluginId), id.Language, $"{id.TemplateName}.json");
        }

        private string GetRenamePath(string path, TemplateVersion version)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            return Path.Combine(Path.GetDirectoryName(path) ?? string.Empty, $"{Path.GetFileNameWithoutExtension(path)}.{version}.json");
        }

        internal virtual void OnTemplateRegistered(TemplateId id, TTemplate template) { }

        ///<inheritdoc/>
        protected override void OnPluginUnloaded(Plugin plugin)
        {
            PluginId id = plugin.Id();
            RegisteredTemplates.RemoveWhere(t => t.PluginId == id);
            _pluginTemplatePath.Remove(id);
        }
    }
}