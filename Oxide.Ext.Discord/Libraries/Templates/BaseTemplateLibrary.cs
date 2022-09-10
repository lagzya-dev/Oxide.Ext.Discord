using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Exceptions.Libraries;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Json.Serialization;
using Oxide.Ext.Discord.Logging;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Oxide Library for Discord Templates
    /// </summary>
    public abstract class BaseTemplateLibrary : Library
    {
        protected readonly ILogger Logger;
        protected readonly HashSet<string> RegisteredTemplates = new HashSet<string>();
        
        private readonly string _rootDir = Path.Combine(Interface.Oxide.InstanceDirectory, "discord", "templates");
        private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        protected BaseTemplateLibrary(ILogger logger)
        {
            Logger = logger;
            if (!Directory.Exists(_rootDir))
            {
                Directory.CreateDirectory(_rootDir);
            }
        }
        
        internal async Task HandleRegisterTemplate<T>(TemplateId id, T template, TemplateVersion minSupportedVersion) where T : BaseTemplate
        {
            string path = GetTemplatePath(TemplateType.Message, id);
            if (File.Exists(path))
            {
                T existingTemplate = await LoadTemplate<T>(TemplateType.Message, id).ConfigureAwait(false);
                if (existingTemplate.Version >= minSupportedVersion)
                {
                    return;
                }

                await BackupTemplateFiles<T>(TemplateType.Message, id, minSupportedVersion).ConfigureAwait(false);
            }
            
            await CreateFile(path, template).ConfigureAwait(false);
            RegisteredTemplates.Add(id.TemplateName);
        }

        internal async Task<T> LoadTemplate<T>(TemplateType type, TemplateId id) where T : BaseTemplate
        {
            string path = GetTemplatePath(type, id);
            if (!File.Exists(path))
            {
                return null;
            }

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return await DiscordJsonReader.DeserializeFromAsync<T>(_serializer, stream).ConfigureAwait(false);
            }
        }

        internal Task<T> LoadTemplate<T>(TemplateType type, TemplateId id, string language) where T : BaseTemplate
        {
            return LoadTemplate<T>(type, new TemplateId(id, language));
        }

        private async Task CreateFile<T>(string path, T template) where T : BaseTemplate
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FileMode mode = File.Exists(path) ? FileMode.Truncate : FileMode.Create;

            using (FileStream stream = new FileStream(path, mode))
            {
                DiscordJsonWriter writer = new DiscordJsonWriter();
                await writer.WriteAsync(_serializer, template).ConfigureAwait(false);
                await writer.Stream.CopyToPooledAsync(stream).ConfigureAwait(false);
                writer.Dispose();
            }
        }

        private async Task BackupTemplateFiles<T>(TemplateType type, TemplateId id, TemplateVersion minSupportedVersion) where T : BaseTemplate
        {
            if (id.IsGlobal)
            {
                await BackupTemplate<T>(type, minSupportedVersion, id).ConfigureAwait(false);
                return;
            }
            
            foreach (string dir in Directory.EnumerateDirectories(GetTemplateFolder(type, id.PluginName)))
            {
                string lang = Path.GetDirectoryName(dir);
                await BackupTemplate<T>(type, minSupportedVersion, new TemplateId(id, lang)).ConfigureAwait(false);
            }
        }
        
        private async Task BackupTemplate<T>(TemplateType type, TemplateVersion minSupportedVersion, TemplateId langId) where T : BaseTemplate
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

            if (template.Version >= minSupportedVersion)
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

        private string GetTemplateFolder(TemplateType type, string plugin)
        {
            return Path.Combine(_rootDir, plugin, GetTemplateTypePath(type));
        }

        private string GetTemplatePath(TemplateType type, TemplateId id)
        {
            DiscordTemplateException.ThrowIfInvalidTemplateName(id.TemplateName);

            if (id.IsGlobal)
            {
                return Path.Combine(_rootDir, id.PluginName, GetTemplateTypePath(type), $"{id.TemplateName}.json");
            }
            return Path.Combine(_rootDir, id.PluginName, GetTemplateTypePath(type), id.Language, $"{id.TemplateName}.json");
        }

        private string GetRenamePath(string path, TemplateVersion version)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            return Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}.{version}.json");
        }

        private string GetTemplateTypePath(TemplateType type) => EnumCache<TemplateType>.ToLower(type);

        internal abstract void OnPluginUnloaded(Plugin plugin);
    }
}