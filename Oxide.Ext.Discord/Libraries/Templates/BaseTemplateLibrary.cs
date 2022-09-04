using System;
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
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Oxide Library for Discord Templates
    /// </summary>
    public abstract class BaseTemplateLibrary : Library
    {
        private readonly string _rootDir = Path.Combine(Interface.Oxide.InstanceDirectory, "discord", "templates");
        private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();
        protected readonly ILogger Logger;

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
        
        internal async Task<T> LoadTemplate<T>(Plugin plugin, TemplateType type, string name, string language) where T : BaseTemplate
        {
            string path = GetTemplatePath(plugin, type, name, language);
            if (!File.Exists(path))
            {
                return null;
            }

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                DiscordJsonReader reader = DiscordPool.Get<DiscordJsonReader>();
                await reader.CopyFromAsync(stream).ConfigureAwait(false);
                T template = await reader.DeserializeAsync<T>(_serializer).ConfigureAwait(false);
                reader.Dispose();
                return template;
            }
        }
        
        internal Task CreateFile<T>(string path, T template) where T : BaseTemplate
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

            return Task.CompletedTask;
        }

        internal Task MoveFile(Plugin plugin, TemplateType type, string name, string language, TemplateVersion version)
        {
            string oldPath = GetTemplatePath(plugin, type, name, language);
            if (!File.Exists(oldPath))
            {
                return Task.CompletedTask;
            }

            string newPath = GetRenamePath(oldPath, version);
            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }
            
            File.Move(oldPath, newPath);
            return Task.CompletedTask;
        }
        
        internal string GetTemplatePath(Plugin plugin, TemplateType type, string name, string language)
        {
            DiscordTemplateException.ThrowIfInvalidTemplateName(name);

            if (string.IsNullOrEmpty(language))
            {
                return Path.Combine(_rootDir, plugin.Name, GetTemplateTypePath(type), $"{name}.json");
            }
            return Path.Combine(_rootDir, plugin.Name, GetTemplateTypePath(type), language, $"{name}.json");
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