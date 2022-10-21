using System;
using System.Collections.Generic;
using System.IO;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Templates;
using Oxide.Ext.Discord.Callbacks.Templates.Messages;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Promise;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Library for Discord Message templates
    /// </summary>
    public abstract class BaseMessageTemplatesLibrary<TTemplate, TEntity> : BaseTemplateLibrary 
        where TTemplate : BaseMessageTemplate<TEntity>, new()
        where TEntity : class
    {
        private readonly Hash<TemplateId, TTemplate> _templateCache = new Hash<TemplateId, TTemplate>();

        internal BaseMessageTemplatesLibrary(TemplateType type, ILogger logger) : base(Path.Combine(Interface.Oxide.InstanceDirectory, "discord", "templates"), type, logger) { }
        
        /// <summary>
        /// Registers a global message template
        /// Global Message templates cannot be localized
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Unique name of the template</param>
        /// <param name="template">Template to register</param>
        /// <param name="minVersion">Min supported version for this template</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordPromise RegisterGlobalTemplateAsync(Plugin plugin, string templateName, TTemplate template, TemplateVersion minVersion)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            if (template == null) throw new ArgumentNullException(nameof(template));

            IDiscordPromise promise = DiscordPromise.Create();
            
            TemplateId id = new TemplateId(plugin, templateName, null);
            RegisterTemplateCallback<TTemplate>.Start(this, id, template, minVersion, promise);
            return promise;
        }
        
        /// <summary>
        /// Registers a message template with the given name and language
        /// </summary>
        /// <param name="plugin">Plugin the <see cref="DiscordMessageTemplate"/> is for</param>
        /// <param name="templateName">Name of the <see cref="DiscordMessageTemplate"/></param>
        /// <param name="template">Template to be registered</param>
        /// <param name="minVersion">
        /// The minimum supported template version.<br/>
        /// If an existing template exists and it's version is >=  the minimum supported version then we will use that template.<br/>
        /// If an existing template exists and it's version is &lt; the min supported version. The existing version is backed up and a new template is created 
        /// </param>
        /// <param name="language">Language for the template</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordPromise RegisterLocalizedTemplateAsync(Plugin plugin, string templateName, TTemplate template, TemplateVersion minVersion, string language = DiscordLang.DefaultOxideLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            if (template == null) throw new ArgumentNullException(nameof(template));
            
            IDiscordPromise promise = DiscordPromise.Create();

            TemplateId id = new TemplateId(plugin, templateName, language);
            RegisterTemplateCallback<TTemplate>.Start(this, id, template, minVersion, promise);
            return promise;
        }

        /// <summary>
        /// Returns a global message template for the plugin with the given name
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <returns><see cref="DiscordMessageTemplate"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordPromise<TTemplate> GetGlobalTemplateAsync(Plugin plugin, string templateName) => GetGlobalTemplateInternalAsync(plugin, templateName, DiscordPromise<TTemplate>.Create());

        internal IDiscordPromise<TTemplate> GetGlobalTemplateInternalAsync(Plugin plugin, string templateName, IDiscordPromise<TTemplate> promise = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            
            if (promise == null)
            {
                promise = DiscordPromise<TTemplate>.Create(true);
            }

            TemplateId id = new TemplateId(plugin, templateName, null);
            LoadMessageTemplateCallback<TTemplate, TEntity>.Start(this, id, promise);
            return promise;
        }

        public IDiscordPromise<T> GetGlobalEntityAsync<T>(Plugin plugin, string templateName, PlaceholderData data = null, T entity = null) where T : class, TEntity 
            => GetGlobalEntityInternalAsync(plugin, templateName, data, entity, DiscordPromise<TEntity>.Create()).Then(r => (T)r);

        public IDiscordPromise<TEntity> GetGlobalEntityAsync(Plugin plugin, string templateName, PlaceholderData data = null, TEntity entity = null) => GetGlobalEntityInternalAsync(plugin, templateName, data, entity, DiscordPromise<TEntity>.Create());

        internal IDiscordPromise<TEntity> GetGlobalEntityInternalAsync(Plugin plugin, string templateName, PlaceholderData data = null, TEntity entity = null, IDiscordPromise<TEntity> promise = null)
        {
            if (promise == null)
            {
                promise = DiscordPromise<TEntity>.Create(true);
            }
            
            IDiscordPromise<TTemplate> load = GetGlobalTemplateInternalAsync(plugin, templateName);
            load.Then(template => template.ToEntityInternalAsync(data, entity, promise));
            return promise;
        }
        
        public IDiscordPromise<List<T>> GetGlobalBulkEntityAsync<T>(Plugin plugin, BulkTemplateRequest request) where T : class, TEntity 
            => GetGlobalBulkEntityInternalAsync(plugin, request, DiscordPromise<List<TEntity>>.Create()).Then(r => r.ConvertAll(ConverterExt.Convert<T, TEntity>));

        public IDiscordPromise<List<TEntity>> GetGlobalBulkEntityAsync(Plugin plugin, BulkTemplateRequest request) => GetGlobalBulkEntityInternalAsync(plugin, request, DiscordPromise<List<TEntity>>.Create());

        internal IDiscordPromise<List<TEntity>> GetGlobalBulkEntityInternalAsync(Plugin plugin, BulkTemplateRequest request, IDiscordPromise<List<TEntity>> promise = null)
        {
            if (promise == null)
            {
                promise = DiscordPromise<List<TEntity>>.Create(true);
            }

            TemplateId id = new TemplateId(plugin, string.Empty, null);
            BulkTemplateToEntityCallback<TTemplate, TEntity>.Start(this, id, request, promise);
            return promise;
        }
        
        /// <summary>
        /// Returns a message template for a given <see cref="IPlayer"/> player
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <param name="player">IPlayer for the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordPromise<TTemplate> GetTemplateForPlayerAsync(Plugin plugin, string templateName, IPlayer player) => GetTemplateForPlayerAsync(plugin, templateName, player?.Id);
        public IDiscordPromise<TEntity> GetEntityForPlayerAsync(Plugin plugin, string templateName, IPlayer player, PlaceholderData data = null, TEntity entity = null) => GetEntityForPlayerAsync(plugin, templateName, player?.Id, data, entity);
        public IDiscordPromise<List<TEntity>> GetBulkEntityForPlayerAsync(Plugin plugin, BulkTemplateRequest request, IPlayer player) => GetBulkEntityForPlayerAsync(plugin, request, player?.Id);

        /// <summary>
        /// Returns a message template for a given <see cref="IPlayer"/> player ID
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <param name="playerId">PlayerId for the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordPromise<TTemplate> GetTemplateForPlayerAsync(Plugin plugin, string templateName, string playerId) => GetLocalizedTemplateAsync(plugin, templateName, DiscordExtension.DiscordLang.GetPlayerLanguage(playerId));

        public IDiscordPromise<TEntity> GetEntityForPlayerAsync(Plugin plugin, string templateName, string playerId, PlaceholderData data = null, TEntity entity = null) 
            => GetLocalizedEntityAsync(plugin, templateName, DiscordExtension.DiscordLang.GetPlayerLanguage(playerId), data, entity);

        public IDiscordPromise<List<TEntity>> GetBulkEntityForPlayerAsync(Plugin plugin, BulkTemplateRequest request, string playerId) => GetLocalizedBulkEntityAsync(plugin, request, DiscordExtension.DiscordLang.GetPlayerLanguage(playerId));
        /// <summary>
        /// Returns a message template for a given language
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <param name="language">Oxide language of the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordPromise<TTemplate> GetLocalizedTemplateAsync(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage) => GetLocalizedTemplateInternalAsync(plugin, templateName, language, DiscordPromise<TTemplate>.Create());

        internal IDiscordPromise<TTemplate> GetLocalizedTemplateInternalAsync(Plugin plugin, string templateName, string language, IDiscordPromise<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));

            if (callback == null)
            {
                callback = DiscordPromise<TTemplate>.Create(true);
            }
            
            TemplateId id = new TemplateId(plugin, templateName, language);
            LoadMessageTemplateCallback<TTemplate, TEntity>.Start(this, id, callback);
            return callback;
        }

        public IDiscordPromise<T> GetLocalizedEntityAsync<T>(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, PlaceholderData data = null, T entity = null) where T : class, TEntity 
            => GetLocalizedEntityInternalAsync(plugin, templateName, language, data, entity).Then(result => (T)result);

        public IDiscordPromise<TEntity> GetLocalizedEntityAsync(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, PlaceholderData data = null, TEntity entity = null) 
            => GetLocalizedEntityInternalAsync(plugin, templateName, language, data, entity, DiscordPromise<TEntity>.Create());

        internal IDiscordPromise<TEntity> GetLocalizedEntityInternalAsync(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, PlaceholderData data = null, TEntity entity = null, IDiscordPromise<TEntity> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordPromise<TEntity>.Create(true);
            }
            
            IDiscordPromise<TTemplate> load = GetLocalizedTemplateInternalAsync(plugin, templateName, language);
            load.Then(template => template.ToEntityInternalAsync(data, entity, callback));
            return callback;
        }
        
        public IDiscordPromise<List<T>> GetLocalizedBulkEntityAsync<T>(Plugin plugin, BulkTemplateRequest request, string language = DiscordLang.DefaultOxideLanguage) where T : class, TEntity
        {
            return GetLocalizedBulkEntityInternalAsync(plugin, request, language, DiscordPromise<List<TEntity>>.Create()).Then(r => r.ConvertAll(ConverterExt.Convert<T, TEntity>));
        }

        public IDiscordPromise<List<TEntity>> GetLocalizedBulkEntityAsync(Plugin plugin, BulkTemplateRequest request, string language = DiscordLang.DefaultOxideLanguage) 
            => GetLocalizedBulkEntityInternalAsync(plugin, request, language, DiscordPromise<List<TEntity>>.Create());

        internal IDiscordPromise<List<TEntity>> GetLocalizedBulkEntityInternalAsync(Plugin plugin, BulkTemplateRequest request, string language = DiscordLang.DefaultOxideLanguage, IDiscordPromise<List<TEntity>> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordPromise<List<TEntity>>.Create(true);
            }

            TemplateId id = new TemplateId(plugin, string.Empty, language);
            BulkTemplateToEntityCallback<TTemplate, TEntity>.Start(this, id, request, callback);
            return callback;
        }
        
        /// <summary>
        /// Returns a message template for a given language
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <param name="interaction">Interaction to get the template for</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordPromise<TTemplate> GetLocalizedTemplateAsync(Plugin plugin, string templateName, DiscordInteraction interaction) => GetLocalizedTemplateInternalAsync(plugin, templateName, interaction, DiscordPromise<TTemplate>.Create());

        internal IDiscordPromise<TTemplate> GetLocalizedTemplateInternalAsync(Plugin plugin, string templateName, DiscordInteraction interaction, IDiscordPromise<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));

            if (callback == null)
            {
                callback = DiscordPromise<TTemplate>.Create(true);
            }
            
            TemplateId id = new TemplateId(plugin, templateName, DiscordExtension.DiscordLang.GetOxideLanguage(interaction.Locale));
            LoadMessageTemplateCallback<TTemplate, TEntity>.Start(this, id, interaction, callback);
            return callback;
        }
        
        internal void HandleGetLocalizedTemplateAsync(TemplateId id, DiscordInteraction interaction, IDiscordPromise<TTemplate> callback)
        {
            callback.Resolve(HandleGetLocalizedTemplateAsync(id, interaction));
        }
        
        public IDiscordPromise<T> GetLocalizedEntityAsync<T>(Plugin plugin, string templateName, DiscordInteraction interaction, PlaceholderData data = null, T entity = null) where T : class, TEntity 
            => GetLocalizedEntityInternalAsync(plugin, templateName, interaction, data, entity, DiscordPromise<TEntity>.Create()).Then(r => (T)r);
        public IDiscordPromise<TEntity> GetLocalizedEntityAsync(Plugin plugin, string templateName, DiscordInteraction interaction, PlaceholderData data = null, TEntity entity = null) 
            => GetLocalizedEntityInternalAsync(plugin, templateName, interaction, data, entity, DiscordPromise<TEntity>.Create());

        internal IDiscordPromise<TEntity> GetLocalizedEntityInternalAsync(Plugin plugin, string templateName, DiscordInteraction interaction, PlaceholderData data = null, TEntity entity = null, IDiscordPromise<TEntity> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordPromise<TEntity>.Create(true);
            }
            
            IDiscordPromise<TTemplate> load = GetLocalizedTemplateInternalAsync(plugin, templateName, interaction);
            load.Then(template => template.ToEntityInternalAsync(data, entity, callback));
            return callback;
        }

        public IDiscordPromise<List<T>> GetLocalizedBulkEntityAsync<T>(Plugin plugin, DiscordInteraction interaction, BulkTemplateRequest request) where T : class, TEntity 
            => GetLocalizedBulkEntityInternalAsync(plugin, interaction, request, DiscordPromise<List<TEntity>>.Create()).Then(r => r.ConvertAll(ConverterExt.Convert<T, TEntity>));
        public IDiscordPromise<List<TEntity>> GetLocalizedBulkEntityAsync(Plugin plugin, DiscordInteraction interaction, BulkTemplateRequest request) => GetLocalizedBulkEntityInternalAsync(plugin, interaction, request, DiscordPromise<List<TEntity>>.Create());

        internal IDiscordPromise<List<TEntity>> GetLocalizedBulkEntityInternalAsync(Plugin plugin, DiscordInteraction interaction, BulkTemplateRequest request, IDiscordPromise<List<TEntity>> callback)
        {
            if (callback == null)
            {
                callback = DiscordPromise<List<TEntity>>.Create(true);
            }

            TemplateId id = new TemplateId(plugin, string.Empty, DiscordExtension.DiscordLang.GetOxideLanguage(interaction.Locale));
            BulkTemplateToEntityCallback<TTemplate, TEntity>.Start(this, id, interaction, request, callback);
            return callback;
        }

        internal void HandleGetLocalizedBulkEntityAsync(TemplateId id, BulkTemplateRequest request, DiscordInteraction interaction, IDiscordPromise<List<TEntity>> callback)
        {
            List<TEntity> entities = HandleGetLocalizedBulkEntityAsync(id, request, interaction);
            callback.Resolve(entities);
        }
        
        internal List<TEntity> HandleGetLocalizedBulkEntityAsync(TemplateId id, BulkTemplateRequest request, DiscordInteraction interaction)
        {
            List<TEntity> entities = new List<TEntity>();
            Hash<string, TTemplate> cache = DiscordPool.GetHash<string, TTemplate>();

            for (int index = 0; index < request.Items.Count; index++)
            {
                BulkTemplateItem item = request.Items[index];
                TTemplate template = cache[item.TemplateName];
                if (template == null)
                {
                    template = HandleGetLocalizedTemplateAsync(id.WithName(item.TemplateName), interaction);
                    cache[item.TemplateName] = template;
                }

                entities.Add(template.ToEntity(item.Data));
            }
            
            DiscordPool.FreeHash(cache);

            return entities;
        }

        private TTemplate HandleGetLocalizedTemplateAsync(TemplateId id, DiscordInteraction interaction)
        {
            TTemplate template = LoadFromCache(id);
            if (template != null)
            {
                return template;
            }

            if (interaction != null)
            {
                IPlayer player = interaction.User.Player;
                template = LoadTemplate<TTemplate>(id)
                           ?? (player != null ? LoadTemplate<TTemplate>(id, DiscordExtension.DiscordLang.GetPlayerLanguage(player)) : null)
                           ?? LoadTemplate<TTemplate>(id, DiscordExtension.DiscordLang.GetOxideLanguage(interaction.GuildLocale))
                           ?? LoadTemplate<TTemplate>(id, DiscordExtension.DiscordLang.GameServerLanguage)
                           ?? LoadTemplate<TTemplate>(id, DiscordLang.DefaultOxideLanguage);
            }
            else if (!id.IsGlobal)
            {
                template = LoadTemplate<TTemplate>(id)
                           ?? LoadTemplate<TTemplate>(id, DiscordExtension.DiscordLang.GameServerLanguage)
                           ?? LoadTemplate<TTemplate>(id, DiscordLang.DefaultOxideLanguage);
            }
            else
            {
                template = LoadTemplate<TTemplate>(id);
            }

            if (template == null)
            {
                Logger.Warning("Plugin {0} is using the {1} Template API but message template name '{2}' is not registered", id.GetPluginName(), this.GetType().Name, id.TemplateName);
                return new TTemplate();
            }
            
            SetCache(id, template);

            return template;
        }


        private TTemplate LoadFromCache(TemplateId id) => _templateCache.TryGetValue(id, out TTemplate template) ? template : null;

        private void SetCache(TemplateId id, TTemplate template)
        {
            _templateCache[id] = template;
        }

        protected override void OnPluginLoaded(Plugin plugin) { }

        protected override void OnPluginUnloaded(Plugin plugin)
        {
            string name = plugin.Name;
            _templateCache.RemoveAll(t => t.PluginName == name);
            RegisteredTemplates.RemoveWhere(rt => rt.PluginName == name);
        }
    }
}