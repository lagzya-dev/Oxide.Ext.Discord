using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Exceptions.Libraries;
using Oxide.Ext.Discord.Json.Serialization;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Promise;

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
        protected readonly string RootDir;
        
        protected readonly TemplateType TemplateType;
        
        internal readonly Types.ConcurrentHashSet<TemplateId> RegisteredTemplates = new Types.ConcurrentHashSet<TemplateId>();

        private static readonly JsonSerializer Serializer = JsonSerializer.Create(new JsonSerializerSettings{Formatting = Formatting.Indented});

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rootDir">Root Directory for the library</param>
        /// <param name="logger"></param>
        protected BaseTemplateLibrary(string rootDir, TemplateType type, ILogger logger)
        {
            RootDir = rootDir;
            Logger = logger;
            TemplateType = type;
            if (!Directory.Exists(RootDir))
            {
                Directory.CreateDirectory(RootDir);
            }
        }
        
        internal async Task HandleRegisterTemplate<T>(TemplateId id, T template, TemplateVersion minVersion, IDiscordPromise promise) where T : BaseTemplate
        {
            if (template.Version < minVersion)
            {
                Logger.Error("Failed to register for plugin: {0} Name: {1} Language: {2} because the template version {3} is less than the min supported version {4}", id.GetPluginName(), id.TemplateName, id.GetLanguageName(), template.Version, minVersion);
                return;
            }
            
            if (RegisteredTemplates.Contains(id))
            {
                Logger.Warning("Trying to register template multiple times. Type: {0} {1}", TemplateType, id);
            }
            
            RegisteredTemplates.Add(id);
            
            string path = GetTemplatePath(id);
            if (File.Exists(path))
            {
                T existingTemplate = await LoadTemplate<T>(id).ConfigureAwait(false);
                if (existingTemplate != null && existingTemplate.Version < minVersion)
                {
                    await BackupTemplateFiles<T>(id, minVersion).ConfigureAwait(false);
                    await CreateFile(path, template).ConfigureAwait(false);
                }
            }
            else
            {
                await CreateFile(path, template).ConfigureAwait(false);
            }
            
            promise.Resolve();
        }
        
        internal async Task HandleBulkRegisterTemplate<T>(TemplateId id, List<BulkTemplateRegistration<T>> templates, TemplateVersion minVersion, IDiscordPromise promise) where T : BaseTemplate
        {
            foreach (BulkTemplateRegistration<T> registration in templates)
            {
                T template = registration.Template;
                await HandleRegisterTemplate(id.WithLanguage(registration.Language), template, minVersion, null).ConfigureAwait(false);
            }
            
            promise.Resolve();
        }

        internal async Task<T> LoadTemplate<T>(TemplateId id) where T : BaseTemplate
        {
            string path = GetTemplatePath(id);
            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous))
                {
                    return await DiscordJsonReader.DeserializeFromAsync<T>(Serializer, stream).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Logger.Exception("Failed to load template from file: {0} Path: {1}", id.ToString(), path.Substring(Interface.Oxide.RootDirectory.Length), ex);
                return null;
            }
        }

        internal Task<T> LoadTemplate<T>(TemplateId id, string language) where T : BaseTemplate
        {
            return LoadTemplate<T>(id.WithLanguage(language));
        }

        private async Task CreateFile<T>(string path, T template) where T : BaseTemplate
        {
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            FileMode mode = File.Exists(path) ? FileMode.Truncate : FileMode.Create;

            using (FileStream stream = new FileStream(path, mode, FileAccess.Write, FileShare.Write, 1024, FileOptions.Asynchronous))
            {
                await DiscordJsonWriter.WriteAndCopyAsync(Serializer, template, stream).ConfigureAwait(false);
                await stream.FlushAsync().ConfigureAwait(false);
            }
        }

        private async Task BackupTemplateFiles<T>(TemplateId id, TemplateVersion minVersion) where T : BaseTemplate
        {
            if (id.IsGlobal)
            {
                await BackupTemplate<T>(minVersion, id).ConfigureAwait(false);
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
                await BackupTemplate<T>(minVersion, id.WithLanguage(lang)).ConfigureAwait(false);
            }
        }
        
        private async Task BackupTemplate<T>(TemplateVersion minVersion, TemplateId langId) where T : BaseTemplate
        {
            string oldPath = GetTemplatePath(langId);
            if (!File.Exists(oldPath))
            {
                return;
            }

            T template = await LoadTemplate<T>(langId).ConfigureAwait(false);
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

        internal virtual string GetTemplateFolder(string plugin)
        {
            return Path.Combine(RootDir, plugin, GetTemplateTypePath());
        }

        internal virtual string GetTemplatePath(TemplateId id)
        {
            DiscordTemplateException.ThrowIfInvalidTemplateName(id.TemplateName, TemplateType);

            if (id.IsGlobal)
            {
                return Path.Combine(RootDir, id.PluginName, GetTemplateTypePath(), $"{id.TemplateName}.json");
            }
            return Path.Combine(RootDir, id.PluginName, GetTemplateTypePath(), id.Language, $"{id.TemplateName}.json");
        }

        private string GetRenamePath(string path, TemplateVersion version)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            return Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}.{version}.json");
        }
        
        private string GetTemplateTypePath() => EnumCache<TemplateType>.Instance.ToLower(TemplateType);
    }
}