using System;
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
using Oxide.Ext.Discord.Logging;
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
        /// <param name="name">Unique name of the template</param>
        /// <param name="template">Template to register</param>
        /// <param name="minVersion">Min supported version for this template</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordAsyncCallback RegisterGlobalTemplateAsync(Plugin plugin, string name, TTemplate template, TemplateVersion minVersion)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (template == null) throw new ArgumentNullException(nameof(template));

            IDiscordAsyncCallback callback = PluginAsyncCallback.Create();
            
            TemplateId id = new TemplateId(plugin, name, null);
            RegisterTemplateCallback<TTemplate>.Start(this, id, template, _templateType, minVersion, callback);
            return callback;
        }
        
        /// <summary>
        /// Registers a message template with the given name and language
        /// </summary>
        /// <param name="plugin">Plugin the <see cref="DiscordMessageTemplate"/> is for</param>
        /// <param name="name">Name of the <see cref="DiscordMessageTemplate"/></param>
        /// <param name="template">Template to be registered</param>
        /// <param name="minVersion">
        /// The minimum supported template version.<br/>
        /// If an existing template exists and it's version is >=  the minimum supported version then we will use that template.<br/>
        /// If an existing template exists and it's version is &lt; the min supported version. The existing version is backed up and a new template is created 
        /// </param>
        /// <param name="language">Language for the template</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordAsyncCallback RegisterLocalizedTemplateAsync(Plugin plugin, string name, TTemplate template, TemplateVersion minVersion, string language = DiscordLang.DefaultOxideLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (template == null) throw new ArgumentNullException(nameof(template));
            
            IDiscordAsyncCallback callback = PluginAsyncCallback.Create();

            TemplateId id = new TemplateId(plugin, name, language);
            RegisterTemplateCallback<TTemplate>.Start(this, id, template, _templateType, minVersion, callback);
            return callback;
        }

        /// <summary>
        /// Returns a message template for a given <see cref="IPlayer"/> player
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <param name="player">IPlayer for the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordAsyncCallback<TTemplate> GetTemplateForPlayerAsync(Plugin plugin, string name, IPlayer player) => GetTemplateForPlayerAsync(plugin, name, player?.Id);

        /// <summary>
        /// Returns a message template for a given <see cref="IPlayer"/> player ID
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <param name="playerId">PlayerId for the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordAsyncCallback<TTemplate> GetTemplateForPlayerAsync(Plugin plugin, string name, string playerId)
        {
            return GetLocalizedMessageTemplateAsync(plugin, name, !string.IsNullOrEmpty(playerId) ? DiscordExtension.DiscordLang.GetPlayerLanguage(playerId) : DiscordExtension.DiscordLang.GameServerLanguage);
        }

        /// <summary>
        /// Returns a global message template for the plugin with the given name
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <returns><see cref="DiscordMessageTemplate"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordAsyncCallback<TTemplate> GetGlobalTemplateAsync(Plugin plugin, string name) => GetGlobalTemplateInternalAsync(plugin, name, PluginAsyncCallback<TTemplate>.Create());

        internal IDiscordAsyncCallback<TTemplate> GetGlobalTemplateInternalAsync(Plugin plugin, string name, IDiscordAsyncCallback<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            
            if (callback == null)
            {
                callback = InternalAsyncCallback<TTemplate>.Create();
            }

            TemplateId id = new TemplateId(plugin, name, null);
            LoadGlobalMessageTemplate<TTemplate, TEntity>.Start(this, id, callback);
            return callback;
        }
        
        internal async Task HandleGetGlobalTemplateAsync(TemplateId id, IDiscordAsyncCallback<TTemplate> callback)
        {
            callback.InvokeSuccess(await HandleGetGlobalTemplateAsync(id).ConfigureAwait(false));  
        }

        internal async Task<TTemplate> HandleGetGlobalTemplateAsync(TemplateId id)
        {
            TTemplate template = LoadFromCache(id);
            if (template != null)
            {
                return template;
            }
            
            template = await LoadTemplate<TTemplate>(TemplateType.Message, id).ConfigureAwait(false);
            if (template == null)
            {
                Logger.Warning($"Plugin {{0}} is using the {{1}}.{nameof(GetGlobalTemplateAsync)} API but message template name '{{2}}' is not registered", id.GetPluginName(), this.GetType().Name, id.TemplateName);
                return new TTemplate();
            }
            
            SetCache(id, template);
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
        public IDiscordAsyncCallback<TTemplate> GetLocalizedMessageTemplateAsync(Plugin plugin, string name, string language = DiscordLang.DefaultOxideLanguage) => GetLocalizedMessageTemplateInternalAsync(plugin, name, language, PluginAsyncCallback<TTemplate>.Create());

        internal IDiscordAsyncCallback<TTemplate> GetLocalizedMessageTemplateInternalAsync(Plugin plugin, string name, string language, IDiscordAsyncCallback<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));

            if (callback == null)
            {
                callback = InternalAsyncCallback<TTemplate>.Create();
            }
            
            TemplateId id = new TemplateId(plugin, name, language);
            LoadLocalizedMessageTemplate<TTemplate, TEntity>.Start(this, id, callback);
            return callback;
        }
        
        internal async Task HandleGetLocalizedMessageTemplateAsync(TemplateId id, IDiscordAsyncCallback<TTemplate> callback)
        {
            callback.InvokeSuccess(await HandleGetLocalizedMessageTemplateAsync(id).ConfigureAwait(false));
        }
        
        internal async Task<TTemplate> HandleGetLocalizedMessageTemplateAsync(TemplateId id)
        {
            TTemplate template = LoadFromCache(id);
            if (template != null)
            {
                return template;
            }
            
            template = await DiscordExtension.DiscordMessageTemplates.LoadTemplate<TTemplate>(TemplateType.Message, id).ConfigureAwait(false)
                       ?? await DiscordExtension.DiscordMessageTemplates.LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordExtension.DiscordLang.GameServerLanguage).ConfigureAwait(false)
                       ?? await DiscordExtension.DiscordMessageTemplates.LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordLang.DefaultOxideLanguage).ConfigureAwait(false);
            
            if (template == null)
            {
                Logger.Warning($"Plugin {{0}} is using the {{1}}.{nameof(GetLocalizedMessageTemplateAsync)} API but message template name '{{2}}' is not registered", id.GetPluginName(), this.GetType().Name, id.TemplateName);
                return new TTemplate();
            }
            
            SetCache(id, template);
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
        public IDiscordAsyncCallback<TTemplate> GetMessageTemplateAsync(Plugin plugin, string name, DiscordInteraction interaction) => GetMessageTemplateInternalAsync(plugin, name, interaction, PluginAsyncCallback<TTemplate>.Create());

        internal IDiscordAsyncCallback<TTemplate> GetMessageTemplateInternalAsync(Plugin plugin, string name, DiscordInteraction interaction, IDiscordAsyncCallback<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (callback == null)
            {
                callback = InternalAsyncCallback<TTemplate>.Create();
            }
            
            string language = DiscordExtension.DiscordLang.GetOxideLanguage(interaction.Locale);
            TemplateId id = new TemplateId(plugin, name, language);
            LoadInteractionMessageTemplate<TTemplate, TEntity>.Start(this, id, interaction, callback);
            return callback;
        }
        
        internal async Task HandleGetMessageTemplateAsync(TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<TTemplate> callback)
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
            
            IPlayer player = interaction.User.Player;
            
            template = await LoadTemplate<TTemplate>(TemplateType.Message, id).ConfigureAwait(false)
                       ?? (player != null ? await LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordExtension.DiscordLang.GetPlayerLanguage(player)).ConfigureAwait(false) : null)
                       ?? await LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordExtension.DiscordLang.GetOxideLanguage(interaction.GuildLocale)).ConfigureAwait(false) 
                       ?? await LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordExtension.DiscordLang.GameServerLanguage).ConfigureAwait(false)
                       ?? await LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordLang.DefaultOxideLanguage).ConfigureAwait(false);
            
            if (template == null)
            {
                Logger.Warning($"Plugin {{0}} is using the {{1}}.{nameof(GetMessageTemplateAsync)} API but message template name '{{2}}' is not registered", id.GetPluginName(), this.GetType().Name, id.TemplateName);
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

        internal override void OnPluginUnloaded(Plugin plugin)
        {
            string name = plugin.Name;
            _templateCache.RemoveAll(t => t.PluginName == name);
            RegisteredTemplates.RemoveWhere(rt => rt.PluginName == name);
        }
    }
}