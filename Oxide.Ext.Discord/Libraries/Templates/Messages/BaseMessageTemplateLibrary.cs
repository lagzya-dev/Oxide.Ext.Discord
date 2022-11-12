using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Templates;
using Oxide.Ext.Discord.Callbacks.Templates.Messages;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Langs;
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
    public abstract class BaseMessageTemplateLibrary<TTemplate, TEntity> : BaseTemplateLibrary<TTemplate>
        where TTemplate : BaseMessageTemplate<TEntity>, new()
        where TEntity : class
    {
        private readonly ConcurrentDictionary<TemplateId, TTemplate> _templateCache = new ConcurrentDictionary<TemplateId, TTemplate>();

        internal BaseMessageTemplateLibrary(TemplateType type, ILogger logger) : base(type, logger) { }
        
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
            
            TemplateId id = TemplateId.CreateGlobal(plugin, templateName);
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

            TemplateId id = TemplateId.CreateLocalized(plugin, templateName, language);
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
        public TTemplate GetGlobalTemplate(Plugin plugin, string templateName) => HandleGetLocalizedTemplate(TemplateId.CreateGlobal(plugin, templateName), null);
        
        internal IDiscordPromise<List<T>> GetGlobalBulkEntityInternalAsync<T>(Plugin plugin, BulkTemplateRequest request, List<T> entities = null, IDiscordPromise<List<T>> promise = null) where T : class, TEntity, new()
        {
            if (promise == null)
            {
                promise = DiscordPromise<List<T>>.Create(true);
            }

            TemplateId id = TemplateId.CreateGlobalBulk(plugin);
            BulkTemplateToEntityCallback<TTemplate, TEntity, T>.Start(this, id, request, entities, promise);
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
        public TTemplate GetPlayerTemplate(Plugin plugin, string templateName, IPlayer player) => GetPlayerTemplate(plugin, templateName, player?.Id);
        public TTemplate GetPlayerTemplate(Plugin plugin, string templateName, string playerId) => HandleGetLocalizedTemplate(TemplateId.CreatePlayer(plugin, templateName, playerId), null);

        /// <summary>
        /// Returns a message template for a given language
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <param name="language">Oxide language of the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public TTemplate GetLocalizedTemplate(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage) => HandleGetLocalizedTemplate(TemplateId.CreateLocalized(plugin, templateName, language), null);

        internal IDiscordPromise<List<T>> GetLocalizedBulkEntityInternalAsync<T>(Plugin plugin, BulkTemplateRequest request, List<T> entities = null, string language = DiscordLang.DefaultOxideLanguage, IDiscordPromise<List<T>> callback = null) where T : class, TEntity, new()
        {
            if (callback == null)
            {
                callback = DiscordPromise<List<T>>.Create(true);
            }
        
            TemplateId id = TemplateId.CreateLocalizedBulk(plugin, language);
            BulkTemplateToEntityCallback<TTemplate, TEntity, T>.Start(this, id, request, entities, callback);
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
        public TTemplate GetLocalizedTemplate(Plugin plugin, string templateName, DiscordInteraction interaction) => HandleGetLocalizedTemplate(TemplateId.CreateInteraction(plugin, templateName, interaction), interaction);
        
        internal IDiscordPromise<List<T>> GetLocalizedBulkEntityInternalAsync<T>(Plugin plugin, DiscordInteraction interaction, BulkTemplateRequest request, List<T> entities, IDiscordPromise<List<T>> callback) where T : class, TEntity, new()
        {
            if (callback == null)
            {
                callback = DiscordPromise<List<T>>.Create(true);
            }
        
            TemplateId id = TemplateId.CreateInteractionBulk(plugin, interaction);
            BulkTemplateToEntityCallback<TTemplate, TEntity, T>.Start(this, id, interaction, request, entities, callback);
            return callback;
        }

        internal void HandleGetLocalizedBulkEntity<T>(TemplateId id, BulkTemplateRequest request, DiscordInteraction interaction, List<T> entities, IDiscordPromise<List<T>> callback) where T : class, TEntity, new()
        {
            callback.Resolve(HandleGetLocalizedBulkEntity(id, request, interaction, entities));
        }
        
        internal List<T> HandleGetLocalizedBulkEntity<T>(TemplateId id, BulkTemplateRequest request, DiscordInteraction interaction, List<T> entities) where T : class, TEntity, new()
        {
            if (entities == null)
            {
                entities = new List<T>();
            }
            
            Hash<string, TTemplate> cache = DiscordPool.GetHash<string, TTemplate>();
            for (int index = 0; index < request.Items.Count; index++)
            {
                BulkTemplateItem item = request.Items[index];
                TTemplate template = cache[item.TemplateName];
                if (template == null)
                {
                    template = HandleGetLocalizedTemplate(id.WithName(item.TemplateName), interaction);
                    cache[item.TemplateName] = template;
                }
                
                entities.Add((T)template.ToEntity(item.Data));
            }
            
            DiscordPool.FreeHash(cache);
        
            return entities;
        }

        internal TTemplate HandleGetLocalizedTemplate(TemplateId id, DiscordInteraction interaction)
        {
            TTemplate template = LoadFromCache(id);
            if (template != null)
            {
                return template;
            }

            if (interaction != null)
            {
                IPlayer player = interaction.User.Player;
                template = LoadTemplate(id)
                           ?? (player != null ? LoadTemplate(id, DiscordExtension.DiscordLang.GetPlayerLanguage(player)) : null)
                           ?? LoadTemplate(id, DiscordExtension.DiscordLang.GetOxideLanguage(interaction.GuildLocale))
                           ?? LoadTemplate(id, DiscordExtension.DiscordLang.GameServerLanguage)
                           ?? LoadTemplate(id, DiscordLang.DefaultOxideLanguage);
            }
            else if (!id.IsGlobal)
            {
                template = LoadTemplate(id)
                           ?? LoadTemplate(id, DiscordExtension.DiscordLang.GameServerLanguage)
                           ?? LoadTemplate(id, DiscordLang.DefaultOxideLanguage);
            }
            else
            {
                template = LoadTemplate(id);
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

        internal override void OnTemplateRegistered(TemplateId id, TTemplate template)
        {
            SetCache(id, template);
        }
        
        protected override void OnPluginLoaded(Plugin plugin) { }

        protected override void OnPluginUnloaded(Plugin plugin)
        {
            base.OnPluginUnloaded(plugin);
            string name = plugin.Name;
            _templateCache.RemoveAll(t => t.PluginName == name);
            RegisteredTemplates.RemoveWhere(rt => rt.PluginName == name);
        }
    }
}