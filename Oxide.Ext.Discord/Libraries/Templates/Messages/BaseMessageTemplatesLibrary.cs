using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Templates;
using Oxide.Ext.Discord.Callbacks.Templates.Messages;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
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
        private readonly TemplateType _templateType;

        internal BaseMessageTemplatesLibrary(TemplateType type, ILogger logger) : base(Path.Combine(Interface.Oxide.InstanceDirectory, "discord", "templates"), logger)
        {
            _templateType = type;
        }
        
        /// <summary>
        /// Registers a global message template
        /// Global Message templates cannot be localized
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Unique name of the template</param>
        /// <param name="template">Template to register</param>
        /// <param name="minVersion">Min supported version for this template</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordAsyncCallback RegisterGlobalTemplateAsync(Plugin plugin, string templateName, TTemplate template, TemplateVersion minVersion)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            if (template == null) throw new ArgumentNullException(nameof(template));

            DiscordAsyncCallback callback = DiscordAsyncCallback.Create();
            
            TemplateId id = new TemplateId(plugin, templateName, null);
            RegisterTemplateCallback<TTemplate>.Start(this, id, template, _templateType, minVersion, callback);
            return callback;
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
        public IDiscordAsyncCallback RegisterLocalizedTemplateAsync(Plugin plugin, string templateName, TTemplate template, TemplateVersion minVersion, string language = DiscordLang.DefaultOxideLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            if (template == null) throw new ArgumentNullException(nameof(template));
            
            DiscordAsyncCallback callback = DiscordAsyncCallback.Create();

            TemplateId id = new TemplateId(plugin, templateName, language);
            RegisterTemplateCallback<TTemplate>.Start(this, id, template, _templateType, minVersion, callback);
            return callback;
        }

        /// <summary>
        /// Returns a global message template for the plugin with the given name
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <returns><see cref="DiscordMessageTemplate"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordAsyncCallback<TTemplate> GetGlobalTemplateAsync(Plugin plugin, string templateName) => GetGlobalTemplateInternalAsync(plugin, templateName, DiscordAsyncCallback<TTemplate>.Create());

        internal DiscordAsyncCallback<TTemplate> GetGlobalTemplateInternalAsync(Plugin plugin, string templateName, DiscordAsyncCallback<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            
            if (callback == null)
            {
                callback = DiscordAsyncCallback<TTemplate>.Create(true);
            }

            TemplateId id = new TemplateId(plugin, templateName, null);
            LoadMessageTemplateCallback<TTemplate, TEntity>.Start(this, id, callback);
            return callback;
        }

        public IDiscordAsyncCallback<TEntity> GetGlobalEntityAsync(Plugin plugin, string templateName, PlaceholderData data = null, TEntity entity = null) => GetGlobalEntityInternalAsync(plugin, templateName, data, entity, DiscordAsyncCallback<TEntity>.Create());

        internal DiscordAsyncCallback<TEntity> GetGlobalEntityInternalAsync(Plugin plugin, string templateName, PlaceholderData data = null, TEntity entity = null, DiscordAsyncCallback<TEntity> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordAsyncCallback<TEntity>.Create(true);
            }
            
            DiscordAsyncCallback<TTemplate> load = GetGlobalTemplateInternalAsync(plugin, templateName);
            load.OnSuccess(template => template.ToEntityInternalAsync(data, entity, callback));
            return callback;
        }
        
        public IDiscordAsyncCallback<List<TEntity>> GetGlobalBulkEntityAsync(Plugin plugin, BulkTemplateRequest<TEntity> request) => GetGlobalBulkEntityInternalAsync(plugin, request, DiscordAsyncCallback<List<TEntity>>.Create());

        internal DiscordAsyncCallback<List<TEntity>> GetGlobalBulkEntityInternalAsync(Plugin plugin, BulkTemplateRequest<TEntity> request, DiscordAsyncCallback<List<TEntity>> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordAsyncCallback<List<TEntity>>.Create(true);
            }

            TemplateId id = new TemplateId(plugin, string.Empty, null);
            BulkTemplateToEntityCallback<TTemplate, TEntity>.Start(this, id, request, callback);
            return callback;
        }
        
        /// <summary>
        /// Returns a message template for a given <see cref="IPlayer"/> player
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <param name="player">IPlayer for the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordAsyncCallback<TTemplate> GetTemplateForPlayerAsync(Plugin plugin, string templateName, IPlayer player) => GetTemplateForPlayerAsync(plugin, templateName, player?.Id);

        public IDiscordAsyncCallback<TEntity> GetEntityForPlayerAsync(Plugin plugin, string templateName, IPlayer player, PlaceholderData data = null, TEntity entity = null) => GetEntityForPlayerAsync(plugin, templateName, player?.Id, data, entity);
        public IDiscordAsyncCallback<List<TEntity>> GetBulkEntityForPlayerAsync(Plugin plugin, BulkTemplateRequest<TEntity> request, IPlayer player) => GetBulkEntityForPlayerAsync(plugin, request, player?.Id);

        /// <summary>
        /// Returns a message template for a given <see cref="IPlayer"/> player ID
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <param name="playerId">PlayerId for the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordAsyncCallback<TTemplate> GetTemplateForPlayerAsync(Plugin plugin, string templateName, string playerId) => GetLocalizedMessageTemplateAsync(plugin, templateName, DiscordExtension.DiscordLang.GetPlayerLanguage(playerId));

        public IDiscordAsyncCallback<TEntity> GetEntityForPlayerAsync(Plugin plugin, string templateName, string playerId, PlaceholderData data = null, TEntity entity = null) => GetLocalizedEntityAsync(plugin, templateName, DiscordExtension.DiscordLang.GetPlayerLanguage(playerId), data, entity);

        public IDiscordAsyncCallback<List<TEntity>> GetBulkEntityForPlayerAsync(Plugin plugin, BulkTemplateRequest<TEntity> request, string playerId) => GetLocalizedBulkEntityAsync(plugin, request, DiscordExtension.DiscordLang.GetPlayerLanguage(playerId));
        /// <summary>
        /// Returns a message template for a given language
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="templateName">Name of the template</param>
        /// <param name="language">Oxide language of the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordAsyncCallback<TTemplate> GetLocalizedMessageTemplateAsync(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage) => GetLocalizedMessageTemplateInternalAsync(plugin, templateName, language, DiscordAsyncCallback<TTemplate>.Create());

        internal DiscordAsyncCallback<TTemplate> GetLocalizedMessageTemplateInternalAsync(Plugin plugin, string templateName, string language, DiscordAsyncCallback<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));
            if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));

            if (callback == null)
            {
                callback = DiscordAsyncCallback<TTemplate>.Create(true);
            }
            
            TemplateId id = new TemplateId(plugin, templateName, language);
            LoadMessageTemplateCallback<TTemplate, TEntity>.Start(this, id, callback);
            return callback;
        }

        public IDiscordAsyncCallback<TEntity> GetLocalizedEntityAsync(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, PlaceholderData data = null, TEntity entity = null) => GetLocalizedEntityInternalAsync(plugin, templateName, language, data, entity, DiscordAsyncCallback<TEntity>.Create());

        internal DiscordAsyncCallback<TEntity> GetLocalizedEntityInternalAsync(Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, PlaceholderData data = null, TEntity entity = null, DiscordAsyncCallback<TEntity> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordAsyncCallback<TEntity>.Create(true);
            }
            
            DiscordAsyncCallback<TTemplate> load = GetLocalizedMessageTemplateInternalAsync(plugin, templateName, language);
            load.OnSuccess(template => template.ToEntityInternalAsync(data, entity, callback));
            return callback;
        }
        
        public IDiscordAsyncCallback<List<TEntity>> GetLocalizedBulkEntityAsync(Plugin plugin, BulkTemplateRequest<TEntity> request, string language = DiscordLang.DefaultOxideLanguage) => GetLocalizedBulkEntityInternalAsync(plugin, request, language, DiscordAsyncCallback<List<TEntity>>.Create());

        internal DiscordAsyncCallback<List<TEntity>> GetLocalizedBulkEntityInternalAsync(Plugin plugin, BulkTemplateRequest<TEntity> request, string language = DiscordLang.DefaultOxideLanguage, DiscordAsyncCallback<List<TEntity>> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordAsyncCallback<List<TEntity>>.Create(true);
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
        public IDiscordAsyncCallback<TTemplate> GetMessageTemplateAsync(Plugin plugin, string templateName, DiscordInteraction interaction) => GetMessageTemplateInternalAsync(plugin, templateName, interaction, DiscordAsyncCallback<TTemplate>.Create());

        internal DiscordAsyncCallback<TTemplate> GetMessageTemplateInternalAsync(Plugin plugin, string templateName, DiscordInteraction interaction, DiscordAsyncCallback<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(templateName)) throw new ArgumentNullException(nameof(templateName));

            if (callback == null)
            {
                callback = DiscordAsyncCallback<TTemplate>.Create(true);
            }
            
            TemplateId id = new TemplateId(plugin, templateName, DiscordExtension.DiscordLang.GetOxideLanguage(interaction.Locale));
            LoadMessageTemplateCallback<TTemplate, TEntity>.Start(this, id, interaction, callback);
            return callback;
        }
        
        internal async Task HandleGetMessageTemplateAsync(TemplateId id, DiscordInteraction interaction, DiscordAsyncCallback<TTemplate> callback)
        {
            callback.InvokeSuccess(await HandleGetMessageTemplateAsync(id, interaction).ConfigureAwait(false));
        }
        
        internal async Task<TTemplate> HandleGetMessageTemplateAsync(TemplateId id, DiscordInteraction interaction)
        {
            TTemplate template = LoadFromCache(id);
            if (template != null)
            {
                return template;
            }

            if (interaction != null)
            {
                IPlayer player = interaction.User.Player;
                template = await LoadTemplate<TTemplate>(_templateType, id).ConfigureAwait(false)
                           ?? (player != null ? await LoadTemplate<TTemplate>(_templateType, id, DiscordExtension.DiscordLang.GetPlayerLanguage(player)).ConfigureAwait(false) : null)
                           ?? await LoadTemplate<TTemplate>(_templateType, id, DiscordExtension.DiscordLang.GetOxideLanguage(interaction.GuildLocale)).ConfigureAwait(false) 
                           ?? await LoadTemplate<TTemplate>(_templateType, id, DiscordExtension.DiscordLang.GameServerLanguage).ConfigureAwait(false)
                           ?? await LoadTemplate<TTemplate>(_templateType, id, DiscordLang.DefaultOxideLanguage).ConfigureAwait(false);
            }
            else if (!id.IsGlobal)
            {
                template = await LoadTemplate<TTemplate>(_templateType, id).ConfigureAwait(false)
                           ?? await LoadTemplate<TTemplate>(_templateType, id, DiscordExtension.DiscordLang.GameServerLanguage).ConfigureAwait(false)
                           ?? await LoadTemplate<TTemplate>(_templateType, id, DiscordLang.DefaultOxideLanguage).ConfigureAwait(false);
            }
            else
            {
                template = await LoadTemplate<TTemplate>(_templateType, id).ConfigureAwait(false);
            }

            if (template == null)
            {
                Logger.Warning("Plugin {0} is using the {1} Template API but message template name '{2}' is not registered", id.GetPluginName(), this.GetType().Name, id.TemplateName);
                return new TTemplate();
            }
            
            SetCache(id, template);

            return template;
        }

        public IDiscordAsyncCallback<TEntity> GetEntityAsync(Plugin plugin, string templateName, DiscordInteraction interaction, PlaceholderData data = null, TEntity entity = null) => GetEntityInternalAsync(plugin, templateName, interaction, data, entity, DiscordAsyncCallback<TEntity>.Create());

        internal DiscordAsyncCallback<TEntity> GetEntityInternalAsync(Plugin plugin, string templateName, DiscordInteraction interaction, PlaceholderData data = null, TEntity entity = null, DiscordAsyncCallback<TEntity> callback = null)
        {
            if (callback == null)
            {
                callback = DiscordAsyncCallback<TEntity>.Create(true);
            }
            
            DiscordAsyncCallback<TTemplate> load = GetMessageTemplateInternalAsync(plugin, templateName, interaction);
            load.OnSuccess(template => template.ToEntityInternalAsync(data, entity, callback));
            return callback;
        }

        public IDiscordAsyncCallback<List<TEntity>> GetBulkEntityAsync(Plugin plugin, DiscordInteraction interaction, BulkTemplateRequest<TEntity> request) => GetBulkEntityAsync(plugin, interaction, request, DiscordAsyncCallback<List<TEntity>>.Create());

        internal DiscordAsyncCallback<List<TEntity>> GetBulkEntityAsync(Plugin plugin, DiscordInteraction interaction, BulkTemplateRequest<TEntity> request, DiscordAsyncCallback<List<TEntity>> callback)
        {
            if (callback == null)
            {
                callback = DiscordAsyncCallback<List<TEntity>>.Create(true);
            }

            TemplateId id = new TemplateId(plugin, string.Empty, DiscordExtension.DiscordLang.GetOxideLanguage(interaction.Locale));
            BulkTemplateToEntityCallback<TTemplate, TEntity>.Start(this, id, interaction, request, callback);
            return callback;
        }

        internal async Task HandleGetBulkEntityAsync(TemplateId id, BulkTemplateRequest<TEntity> request, DiscordInteraction interaction, DiscordAsyncCallback<List<TEntity>> callback)
        {
            List<TEntity> entities = await HandleGetBulkEntityAsync(id, request, interaction).ConfigureAwait(false);
            callback.InvokeSuccess(entities);
        }
        
        internal async Task<List<TEntity>> HandleGetBulkEntityAsync(TemplateId id, BulkTemplateRequest<TEntity> request, DiscordInteraction interaction)
        {
            List<TEntity> entities = new List<TEntity>();
            Hash<string, TTemplate> cache = DiscordPool.GetHash<string, TTemplate>();

            for (int index = 0; index < request.Items.Count; index++)
            {
                BulkTemplateItem<TEntity> item = request.Items[index];
                TTemplate template = cache[item.TemplateName];
                if (template == null)
                {
                    template = await HandleGetMessageTemplateAsync(id.WithName(item.TemplateName), interaction).ConfigureAwait(false);
                    cache[item.TemplateName] = template;
                }

                entities.Add(await template.HandleToEntityAsync(item.Data, item.Entity).ConfigureAwait(false));
            }
            
            DiscordPool.FreeHash(ref cache);

            return entities;
        }

        private TTemplate LoadFromCache(TemplateId id) => _templateCache.TryGetValue(id, out TTemplate template) ? template : null;

        private void SetCache(TemplateId id, TTemplate template)
        {
            _templateCache[id] = template;
        }

        internal override void OnPluginUnloaded(Plugin plugin)
        {
            string name = plugin.Name;
            _templateCache.RemoveAll(t => t.PluginName == name);
            RegisteredTemplates.RemoveWhere(rt => rt.PluginName == name);
        }
    }
}