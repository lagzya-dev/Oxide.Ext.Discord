using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
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
    public abstract class BaseTemplateLibrary : Library
    {
        /// <summary>
        /// Logger for the <see cref="BaseTemplateLibrary"/>
        /// </summary>
        protected readonly ILogger Logger;
        
        /// <summary>
        /// Root Directory for the library
        /// </summary>
        protected readonly string RootDir;
        
        internal readonly HashSet<TemplateId> RegisteredTemplates = new HashSet<TemplateId>();

        private static readonly JsonSerializer Serializer = JsonSerializer.Create(new JsonSerializerSettings{Formatting = Formatting.Indented});

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rootDir">Root Directory for the library</param>
        /// <param name="logger"></param>
        protected BaseTemplateLibrary(string rootDir, ILogger logger)
        {
            RootDir = rootDir;
            Logger = logger;
            if (!Directory.Exists(RootDir))
            {
                Directory.CreateDirectory(RootDir);
            }
        }
        
        internal async Task HandleRegisterTemplate<T>(TemplateId id, T template, TemplateType type, TemplateVersion minVersion, IDiscordPromise promise) where T : BaseTemplate
        {
            if (template.Version < minVersion)
            {
                Logger.Error("Failed to register for plugin: {0} Name: {1} Language: {2} because the template version {3} is less than the min supported version {4}", id.GetPluginName(), id.TemplateName, id.GetLanguageName(), template.Version, minVersion);
                return;
            }
            
            if (RegisteredTemplates.Contains(id))
            {
                Logger.Warning("Trying to register id {0} from plugin {1} with language {2} multiple times.", id.TemplateName, id.GetPluginName(), id.GetLanguageName());
            }
            
            string path = GetTemplatePath(type, id);
            if (File.Exists(path))
            {
                T existingTemplate = await LoadTemplate<T>(type, id).ConfigureAwait(false);
                if (existingTemplate != null && existingTemplate.Version >= minVersion)
                {
                    return;
                }

                await BackupTemplateFiles<T>(type, id, minVersion).ConfigureAwait(false);
            }
            
            await CreateFile(path, template).ConfigureAwait(false);
            RegisteredTemplates.Add(id);
            promise?.Resolve();
        }
        
        internal async Task HandleBulkRegisterTemplate<T>(TemplateId id, List<BulkTemplateRegistration<T>> templates, TemplateType type, TemplateVersion minVersion, IDiscordPromise promise) where T : BaseTemplate
        {
            foreach (BulkTemplateRegistration<T> registration in templates)
            {
                T template = registration.Template;
                await HandleRegisterTemplate(id.WithLanguage(registration.Language), template, type, minVersion, null).ConfigureAwait(false);
            }
            
            promise.Resolve();
        }

        internal async Task<T> LoadTemplate<T>(TemplateType type, TemplateId id) where T : BaseTemplate
        {
            string path = GetTemplatePath(type, id);
            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return await DiscordJsonReader.DeserializeFromAsync<T>(Serializer, stream).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Logger.Exception("Failed to load template from file: {0} Path: {1}", id.ToString(), path, ex);
                return null;
            }
        }

        internal Task<T> LoadTemplate<T>(TemplateType type, TemplateId id, string language) where T : BaseTemplate
        {
            return LoadTemplate<T>(type, id.WithLanguage(language));
        }

        private async Task CreateFile<T>(string path, T template) where T : BaseTemplate
        {
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            FileMode mode = File.Exists(path) ? FileMode.Truncate : FileMode.Create;

            FileStream stream = new FileStream(path, mode);
            await DiscordJsonWriter.WriteAndCopyAsync(Serializer, template, stream).ConfigureAwait(false);
            await stream.FlushAsync().ConfigureAwait(false);
            stream.Dispose();
        }

        private async Task BackupTemplateFiles<T>(TemplateType type, TemplateId id, TemplateVersion minVersion) where T : BaseTemplate
        {
            if (id.IsGlobal)
            {
                await BackupTemplate<T>(type, minVersion, id).ConfigureAwait(false);
                return;
            }

            string path = GetTemplateFolder(type, id.PluginName);
            if (Logger.IsLogging(DiscordLogLevel.Debug))
            {
                Logger.Debug("Backup Template files for: {0} Path: {1}", id.ToString(), path);
            }
            
            foreach (string dir in Directory.EnumerateDirectories(path))
            {
                string lang = Path.GetFileName(dir);
                Logger.Debug("Processing Directory: {0} Lang: {1}", dir, lang);
                await BackupTemplate<T>(type, minVersion, id.WithLanguage(lang)).ConfigureAwait(false);
            }
        }
        
        private async Task BackupTemplate<T>(TemplateType type, TemplateVersion minVersion, TemplateId langId) where T : BaseTemplate
        {
            string oldPath = GetTemplatePath(type, langId);
            if (!File.Exists(oldPath))
            {
                return;
            }

            T template = await LoadTemplate<T>(type, langId).ConfigureAwait(false);
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

        internal virtual string GetTemplateFolder(TemplateType type, string plugin)
        {
            return Path.Combine(RootDir, plugin, GetTemplateTypePath(type));
        }

        internal virtual string GetTemplatePath(TemplateType type, TemplateId id)
        {
            DiscordTemplateException.ThrowIfInvalidTemplateName(id.TemplateName);

            if (id.IsGlobal)
            {
                return Path.Combine(RootDir, id.PluginName, GetTemplateTypePath(type), $"{id.TemplateName}.json");
            }
            return Path.Combine(RootDir, id.PluginName, GetTemplateTypePath(type), id.Language, $"{id.TemplateName}.json");
        }

        private string GetRenamePath(string path, TemplateVersion version)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            return Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}.{version}.json");
        }

        /// <summary>
        /// Returns a pooled lowered string for the template type
        /// </summary>
        /// <param name="type"><see cref="TemplateType"/> to return the string for</param>
        /// <returns></returns>
        protected string GetTemplateTypePath(TemplateType type) => EnumCache<TemplateType>.ToLower(type);

        internal abstract void OnPluginUnloaded(Plugin plugin);
    }
}