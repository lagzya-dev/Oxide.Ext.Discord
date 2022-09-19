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
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Library for Discord Message templates
    /// </summary>
    public abstract class BaseMessageTemplatesLibrary<TTemplate> : BaseTemplateLibrary where TTemplate : BaseTemplate, new()
    {
        private readonly Hash<TemplateId, TTemplate> _templateCache = new Hash<TemplateId, TTemplate>();

        internal BaseMessageTemplatesLibrary(ILogger logger) : base(Path.Combine(Interface.Oxide.InstanceDirectory, "discord", "templates"), logger) { }
        
        /// <summary>
        /// Registers a global message template
        /// Global Message templates cannot be localized
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Unique name of the template</param>
        /// <param name="template">Template to register</param>
        /// <param name="minVersion">Min supported version for this template</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordAsyncCallback RegisterGlobalTemplate(Plugin plugin, string name, TTemplate template, TemplateVersion minVersion)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (template == null) throw new ArgumentNullException(nameof(template));

            IDiscordAsyncCallback callback = PluginAsyncCallback.Create();
            
            TemplateId id = new TemplateId(plugin, name, null);
            RegisterTemplateCallback<TTemplate>.Start(this, id, template, minVersion, callback);
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
        public IDiscordAsyncCallback RegisterLocalizedTemplate(Plugin plugin, string name, TTemplate template, TemplateVersion minVersion, string language = DiscordLang.DefaultOxideLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (template == null) throw new ArgumentNullException(nameof(template));
            
            IDiscordAsyncCallback callback = PluginAsyncCallback.Create();

            TemplateId id = new TemplateId(plugin, name, language);
            RegisterTemplateCallback<TTemplate>.Start(this, id, template, minVersion, callback);
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
        public IDiscordAsyncCallback<TTemplate> GetTemplateForPlayer(Plugin plugin, string name, IPlayer player) => GetTemplateForPlayer(plugin, name, player?.Id);

        /// <summary>
        /// Returns a message template for a given <see cref="IPlayer"/> player ID
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <param name="playerId">PlayerId for the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordAsyncCallback<TTemplate> GetTemplateForPlayer(Plugin plugin, string name, string playerId)
        {
            return GetLocalizedMessageTemplate(plugin, name, !string.IsNullOrEmpty(playerId) ? DiscordExtension.DiscordLang.GetPlayerLanguage(playerId) : DiscordExtension.DiscordLang.GameServerLanguage);
        }

        /// <summary>
        /// Returns a global message template for the plugin with the given name
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <returns><see cref="DiscordMessageTemplate"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IDiscordAsyncCallback<TTemplate> GetGlobalTemplate(Plugin plugin, string name) => GetGlobalTemplateInternal(plugin, name, PluginAsyncCallback<TTemplate>.Create());

        internal IDiscordAsyncCallback<TTemplate> GetGlobalTemplateInternal(Plugin plugin, string name, IDiscordAsyncCallback<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            
            if (callback == null)
            {
                callback = InternalAsyncCallback<TTemplate>.Create();
            }

            TemplateId id = new TemplateId(plugin, name, null);
            LoadGlobalMessageTemplate<TTemplate>.Start(this, id, callback);
            return callback;
        }
        
        internal async Task HandleGetGlobalTemplate(TemplateId id, IDiscordAsyncCallback<TTemplate> callback)
        {
            TTemplate template = LoadFromCache(id);
            if (template != null)
            {
                callback.InvokeSuccess(template);
                return;
            }
            
            template = await LoadTemplate<TTemplate>(TemplateType.Message, id).ConfigureAwait(false);
            if (template == null)
            {
                Logger.Warning($"Plugin {{0}} is using the {{1}}.{nameof(GetGlobalTemplate)} API but message template name '{{2}}' is not registered", id.GetPluginName(), this.GetType().Name, id.TemplateName);
                callback.InvokeSuccess(new TTemplate());
                return;
            }
            
            SetCache(id, template);
            callback.InvokeSuccess(template);
        }
        
        /// <summary>
        /// Returns a message template for a given language
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <param name="language">Oxide language of the template</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordAsyncCallback<TTemplate> GetLocalizedMessageTemplate(Plugin plugin, string name, string language = DiscordLang.DefaultOxideLanguage) => GetLocalizedMessageTemplateInternal(plugin, name, language, PluginAsyncCallback<TTemplate>.Create());

        internal IDiscordAsyncCallback<TTemplate> GetLocalizedMessageTemplateInternal(Plugin plugin, string name, string language, IDiscordAsyncCallback<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));

            if (callback == null)
            {
                callback = InternalAsyncCallback<TTemplate>.Create();
            }
            
            TemplateId id = new TemplateId(plugin, name, language);
            LoadLocalizedMessageTemplate<TTemplate>.Start(this, id, callback);
            return callback;
        }
        
        internal async Task HandleGetLocalizedMessageTemplate(TemplateId id, IDiscordAsyncCallback<TTemplate> callback)
        {
            TTemplate template = LoadFromCache(id);
            if (template != null)
            {
                callback.InvokeSuccess(template);
                return;
            }
            
            template = await DiscordExtension.DiscordMessageTemplates.LoadTemplate<TTemplate>(TemplateType.Message, id).ConfigureAwait(false)
                       ?? await DiscordExtension.DiscordMessageTemplates.LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordExtension.DiscordLang.GameServerLanguage).ConfigureAwait(false)
                       ?? await DiscordExtension.DiscordMessageTemplates.LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordLang.DefaultOxideLanguage).ConfigureAwait(false);
            
            if (template == null)
            {
                Logger.Warning($"Plugin {{0}} is using the {{1}}.{nameof(GetLocalizedMessageTemplate)} API but message template name '{{2}}' is not registered", id.GetPluginName(), this.GetType().Name, id.TemplateName);
                callback.InvokeSuccess(new TTemplate());
                return;
            }
            
            SetCache(id, template);
            callback.InvokeSuccess(template);
        }
        
        /// <summary>
        /// Returns a message template for a given language
        /// </summary>
        /// <param name="plugin">Plugin the template is for</param>
        /// <param name="name">Name of the template</param>
        /// <param name="interaction">Interaction to get the template for</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if Plugin is null or name / language is null or empty</exception>
        public IDiscordAsyncCallback<TTemplate> GetMessageTemplate(Plugin plugin, string name, DiscordInteraction interaction)
        {
            return GetMessageTemplateInternal(plugin, name, interaction, PluginAsyncCallback<TTemplate>.Create());
        }
        
        internal IDiscordAsyncCallback<TTemplate> GetMessageTemplateInternal(Plugin plugin, string name, DiscordInteraction interaction, IDiscordAsyncCallback<TTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (callback == null)
            {
                callback = InternalAsyncCallback<TTemplate>.Create();
            }
            
            string language = DiscordExtension.DiscordLang.GetOxideLanguage(interaction.Locale);
            TemplateId id = new TemplateId(plugin, name, language);
            LoadInteractionMessageTemplate<TTemplate>.Start(this, id, interaction, callback);
            return callback;
        }
        
        internal async Task HandleGetMessageTemplate(TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<TTemplate> callback)
        {
            TTemplate template = LoadFromCache(id);
            if (template != null)
            {
                callback.InvokeSuccess(template);
                return;
            }
            
            IPlayer player = interaction.User.Player;
            
            template = await LoadTemplate<TTemplate>(TemplateType.Message, id).ConfigureAwait(false)
                       ?? (player != null ? await LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordExtension.DiscordLang.GetPlayerLanguage(player)).ConfigureAwait(false) : null)
                       ?? await LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordExtension.DiscordLang.GetOxideLanguage(interaction.GuildLocale)).ConfigureAwait(false) 
                       ?? await LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordExtension.DiscordLang.GameServerLanguage).ConfigureAwait(false)
                       ?? await LoadTemplate<TTemplate>(TemplateType.Message, id, DiscordLang.DefaultOxideLanguage).ConfigureAwait(false);
            
            if (template == null)
            {
                Logger.Warning($"Plugin {{0}} is using the {{1}}.{nameof(GetMessageTemplate)} API but message template name '{{2}}' is not registered", id.GetPluginName(), this.GetType().Name, id.TemplateName);
                callback.InvokeSuccess(new TTemplate());
                return;
            }
            
            SetCache(id, template);
            callback.InvokeSuccess(template);
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