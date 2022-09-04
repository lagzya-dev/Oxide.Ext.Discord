using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Callbacks.Async.Templates.Modals;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates.Modals
{
    public class DiscordModalTemplates : BaseTemplateLibrary
    {
        internal readonly Hash<string, Hash<TemplateId, DiscordModalTemplate>> TemplateCache = new Hash<string, Hash<TemplateId, DiscordModalTemplate>>();

        public DiscordModalTemplates(ILogger logger) : base(logger) { }
        
        public void RegisterModalTemplate(Plugin plugin, string name, DiscordModalTemplate template, TemplateVersion minSupportedVersion, string language = DiscordLocale.DefaultOxideLanguage)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (template == null) throw new ArgumentNullException(nameof(template));

            RegisterModalTemplateCallback callback = RegisterModalTemplateCallback.Create(plugin, name, language, template, minSupportedVersion, Logger);
            callback.Run();
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
            
            LoadModalTemplate load = LoadModalTemplate.Create(plugin, name, interaction, callback, Logger);
            load.Run();
            return callback;
        }

        internal override void OnPluginUnloaded(Plugin plugin)
        {
            TemplateCache.Remove(plugin.Name);
        }
    }
}