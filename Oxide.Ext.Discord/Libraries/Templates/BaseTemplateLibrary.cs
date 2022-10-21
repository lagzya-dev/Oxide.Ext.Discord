using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Exceptions.Libraries;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Promise;
using Oxide.Ext.Discord.Types;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Oxide Library for Discord Templates
    /// </summary>
    public abstract class BaseTemplateLibrary : BaseDiscordLibrary
    {
        /// <summary>
        /// Logger for the <see cref="BaseTemplateLibrary"/>
        /// </summary>
        protected readonly ILogger Logger;
        
        /// <summary>
        /// Root Directory for the library
        /// </summary>
        protected readonly string RootDirectory;

        protected readonly string TemplateTypeDirectory;
        
        /// <summary>
        /// The template type of this template library
        /// </summary>
        protected readonly TemplateType TemplateType;
        
        internal readonly DiscordConcurrentHashSet<TemplateId> RegisteredTemplates = new DiscordConcurrentHashSet<TemplateId>();
        private readonly Hash<string, string> _pluginTemplatePath = new Hash<string, string>();


        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {Formatting = Formatting.Indented};

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rootDirectory">Root Directory for the library</param>
        /// <param name="templateTypeDirectory">The directory for the template type</param>
        /// <param name="type">The template type of this library</param>
        /// <param name="logger"></param>
        protected BaseTemplateLibrary(TemplateType type, ILogger logger)
        {
            RootDirectory = Path.Combine(Interface.Oxide.InstanceDirectory, "discord");
            Logger = logger;
            TemplateType = type;
            TemplateTypeDirectory = EnumCache<TemplateType>.Instance.ToLower(TemplateType);

            if (!Directory.Exists(RootDirectory))
            {
                Directory.CreateDirectory(RootDirectory);
            }
        }
        
        internal void HandleRegisterTemplate<T>(TemplateId id, T template, TemplateVersion minVersion, IDiscordPromise promise) where T : BaseTemplate
        {
            if (template.Version < minVersion)
            {
                Logger.Error("Failed to register for plugin: {0} Name: {1} Language: {2} because the template version {3} is less than the min supported version {4}", id.GetPluginName(), id.TemplateName, id.GetLanguageName(), template.Version, minVersion);
                return;
            }
            
            if (!RegisteredTemplates.Add(id))
            {
                Logger.Warning("Trying to register template multiple times. Type: {0} {1}", TemplateType, id);
            }

            string path = GetTemplatePath(id);
            if (File.Exists(path))
            {
                T existingTemplate = LoadTemplate<T>(id);
                if (existingTemplate != null && existingTemplate.Version < minVersion)
                {
                    BackupTemplateFiles<T>(id, minVersion);
                    CreateFile(path, template);
                }
            }
            else
            {
                CreateFile(path, template);
            }
            
            promise.Resolve();
        }
        
        internal void HandleBulkRegisterTemplate<T>(TemplateId id, List<BulkTemplateRegistration<T>> templates, TemplateVersion minVersion, IDiscordPromise promise) where T : BaseTemplate
        {
            foreach (BulkTemplateRegistration<T> registration in templates)
            {
                T template = registration.Template;
                HandleRegisterTemplate(id.WithLanguage(registration.Language), template, minVersion, null);
            }
            
            promise.Resolve();
        }

        internal T LoadTemplate<T>(TemplateId id) where T : BaseTemplate
        {
            string path = GetTemplatePath(id);
            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(json, Settings);
            }
            catch (Exception ex)
            {
                Logger.Exception("Failed to load template from file: {0} Path: {1}", id.ToString(), path.Substring(Interface.Oxide.RootDirectory.Length), ex);
                return null;
            }
        }

        internal T LoadTemplate<T>(TemplateId id, string language) where T : BaseTemplate
        {
            return LoadTemplate<T>(id.WithLanguage(language));
        }

        private void CreateFile<T>(string path, T template) where T : BaseTemplate
        {
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonConvert.SerializeObject(template, Settings);
            File.WriteAllText(path, json);
        }

        private void BackupTemplateFiles<T>(TemplateId id, TemplateVersion minVersion) where T : BaseTemplate
        {
            if (id.IsGlobal)
            {
                BackupTemplate<T>(minVersion, id);
                return;
            }

            string path = GetTemplateFolder(id.PluginName);
            if (Logger.IsLogging(DiscordLogLevel.Debug))
            {
                Logger.Debug("Backup Template files for: {0} Path: {1}", id.ToString(), path);
            }
            
            foreach (string dir in Directory.EnumerateDirectories(path))
            {
                string lang = Path.GetFileName(dir);
                Logger.Debug("Processing Directory: {0} Lang: {1}", dir, lang);
                BackupTemplate<T>(minVersion, id.WithLanguage(lang));
            }
        }
        
        private void BackupTemplate<T>(TemplateVersion minVersion, TemplateId langId) where T : BaseTemplate
        {
            string oldPath = GetTemplatePath(langId);
            if (!File.Exists(oldPath))
            {
                return;
            }

            T template = LoadTemplate<T>(langId);
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

        protected string GetTemplateFolder(string plugin)
        {
            string folder = _pluginTemplatePath[plugin];
            if (string.IsNullOrEmpty(folder))
            {
                folder = Path.Combine(RootDirectory, plugin, TemplateTypeDirectory);
                _pluginTemplatePath[plugin] = folder;
            }
            
            return folder;
        }

        internal virtual string GetTemplatePath(TemplateId id)
        {
            DiscordTemplateException.ThrowIfInvalidTemplateName(id.TemplateName, TemplateType);

            if (id.IsGlobal)
            {
                return Path.Combine(GetTemplateFolder(id.PluginName), $"{id.TemplateName}.json");
            }
            return Path.Combine(GetTemplateFolder(id.PluginName), id.Language, $"{id.TemplateName}.json");
        }

        private string GetRenamePath(string path, TemplateVersion version)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            return Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}.{version}.json");
        }

        protected override void OnPluginUnloaded(Plugin plugin)
        {
            _pluginTemplatePath.Remove(plugin.Id());
        }
    }
}