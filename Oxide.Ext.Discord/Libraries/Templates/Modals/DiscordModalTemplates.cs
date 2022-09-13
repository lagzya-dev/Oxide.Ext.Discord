using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Templates;
using Oxide.Ext.Discord.Callbacks.Templates.Modals;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Modals
{
    public class DiscordModalTemplates : BaseTemplateLibrary
    {
        private readonly Hash<TemplateId, DiscordModalTemplate> _templateCache = new Hash<TemplateId, DiscordModalTemplate>();

        public DiscordModalTemplates(ILogger logger) : base(Path.Combine(Interface.Oxide.InstanceDirectory, "discord", "templates"), logger) { }
        
        public void RegisterModalTemplate(Plugin plugin, string name, DiscordModalTemplate template, TemplateVersion minSupportedVersion, string language = DiscordLocale.DefaultOxideLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (template == null) throw new ArgumentNullException(nameof(template));

            TemplateId id = new TemplateId(plugin, name, language);
            RegisterTemplateCallback<DiscordModalTemplate>.Start(this, id, template, minSupportedVersion);
        }

        public IDiscordAsyncCallback<DiscordModalTemplate> GetModalTemplate(Plugin plugin, string name, DiscordInteraction interaction)
        {
            return GetModalTemplateInternal(plugin, name, interaction, DiscordAsyncCallback<DiscordModalTemplate>.Create());
        }
        
        internal IDiscordAsyncCallback<DiscordModalTemplate> GetModalTemplateInternal(Plugin plugin, string name, DiscordInteraction interaction, IDiscordAsyncCallback<DiscordModalTemplate> callback = null)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (callback == null)
            {
                callback = InternalAsyncCallback<DiscordModalTemplate>.Create();
            }
            
            string language = DiscordLocale.GetOxideLanguage(interaction.Locale);
            TemplateId id = new TemplateId(plugin, name, language);
            LoadModalTemplateCallback.Start(id, interaction, callback);
            return callback;
        }
        
        internal async Task HandleGetModalTemplate(TemplateId id, DiscordInteraction interaction, IDiscordAsyncCallback<DiscordModalTemplate> callback)
        {
            DiscordModalTemplate template = LoadFromCache(id);
            if (template != null)
            {
                callback.InvokeSuccess(template);
                return;
            }
            
            IPlayer player = interaction.User.Player;
            
            template = await LoadTemplate<DiscordModalTemplate>(TemplateType.Modal, id).ConfigureAwait(false)
                       ?? (player != null ? await LoadTemplate<DiscordModalTemplate>(TemplateType.Modal, id, DiscordLocale.GetPlayerLanguage(player)).ConfigureAwait(false) : null)
                       ?? await LoadTemplate<DiscordModalTemplate>(TemplateType.Modal, id, DiscordLocale.GetOxideLanguage(interaction.GuildLocale)).ConfigureAwait(false) 
                       ?? await LoadTemplate<DiscordModalTemplate>(TemplateType.Modal, id, DiscordLocale.GameServerLanguage).ConfigureAwait(false)
                       ?? await LoadTemplate<DiscordModalTemplate>(TemplateType.Modal, id, DiscordLocale.DefaultOxideLanguage).ConfigureAwait(false);

            if (template == null)
            {
                Logger.Warning($"Plugin {{0}} is using the {nameof(DiscordModalTemplates)}.{nameof(GetModalTemplate)} API but message template name '{{1}}' is not registered", id.GetPluginName(), id.TemplateName);
                callback.InvokeSuccess(new DiscordModalTemplate());
                return;
            }
            
            SetCache(id, template);
            callback.InvokeSuccess(template);
        }
        
        private DiscordModalTemplate LoadFromCache(TemplateId id) => _templateCache.TryGetValue(id, out DiscordModalTemplate template) ? template : null;

        private void SetCache(TemplateId id, DiscordModalTemplate template)
        {
            _templateCache[id] = template;
        }

        internal override void OnPluginUnloaded(Plugin plugin)
        {
            List<TemplateId> ids = DiscordPool.GetList<TemplateId>();
            foreach (TemplateId id in _templateCache.Keys)
            {
                if (plugin.Name == id.PluginName)
                {
                    ids.Add(id);
                }
            }

            for (int index = 0; index < ids.Count; index++)
            {
                TemplateId id = ids[index];
                RegisteredTemplates.Remove(id.TemplateName);
                _templateCache.Remove(id);
            }
            
            DiscordPool.FreeList(ref ids);
        }
    }
}