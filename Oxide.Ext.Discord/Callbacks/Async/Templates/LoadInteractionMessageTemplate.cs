using System.Threading.Tasks;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Callbacks.Async.Templates
{
    internal class LoadInteractionMessageTemplate : BaseAsyncPoolableCallback
    {
        private readonly DiscordTemplates _templates = DiscordExtension.DiscordTemplates;
        private Plugin _plugin;
        private string _name;
        private DiscordInteraction _interaction;
        private DiscordAsyncCallback<DiscordMessageTemplate> _callback;
        private ILogger _logger;

        public static LoadInteractionMessageTemplate Create(Plugin plugin, string name, DiscordInteraction interaction, DiscordAsyncCallback<DiscordMessageTemplate> callback, ILogger logger)
        {
            LoadInteractionMessageTemplate load = DiscordPool.Get<LoadInteractionMessageTemplate>();
            load.Init(plugin, name, interaction, callback, logger);
            return load;
        }

        private void Init(Plugin plugin, string name, DiscordInteraction interaction, DiscordAsyncCallback<DiscordMessageTemplate> callback, ILogger logger)
        {
            _plugin = plugin;
            _name = name;
            _interaction = interaction;
            _callback = callback;
            _logger = logger;
        }

        protected override async Task HandleCallback()
        {
            Hash<TemplateId, DiscordMessageTemplate> pluginTemplates =  _templates.TemplateCache[_plugin.Name];
            if (pluginTemplates == null)
            {
                pluginTemplates = new Hash<TemplateId, DiscordMessageTemplate>();
                _templates.TemplateCache[_plugin.Name] = pluginTemplates;
            }

            string interactionLang = DiscordLocale.GetOxideLanguage(_interaction.Locale);
            TemplateId templateId = new TemplateId(_name, interactionLang);
            if (pluginTemplates.TryGetValue(templateId, out DiscordMessageTemplate template))
            {
                _callback.InvokeSuccess(template);
                return;
            }
            
            IPlayer player = _interaction.User.Player;

            template = await DiscordExtension.DiscordTemplates.LoadTemplate(_plugin, _name, interactionLang).ConfigureAwait(false)
                       ?? (player != null ? await DiscordExtension.DiscordTemplates.LoadTemplate(_plugin, _name, DiscordLocale.GetPlayerLanguage(player)).ConfigureAwait(false) : null)
                       ?? await DiscordExtension.DiscordTemplates.LoadTemplate(_plugin, _name, DiscordLocale.GetOxideLanguage(_interaction.GuildLocale)).ConfigureAwait(false) 
                       ?? await DiscordExtension.DiscordTemplates.LoadTemplate(_plugin, _name, DiscordLocale.GameServerLanguage).ConfigureAwait(false)
                       ?? await DiscordExtension.DiscordTemplates.LoadTemplate(_plugin, _name, DiscordLocale.DefaultOxideLanguage).ConfigureAwait(false);
            
            if (template == null)
            {
                _logger.Warning($"Plugin {{0}} is using the {nameof(DiscordTemplates)}.{nameof(DiscordTemplates.GetMessageTemplate)} API but message template name '{{1}}' is not registered", _plugin.FullName(), _name);
                _callback.InvokeSuccess(new DiscordMessageTemplate());
                return;
            }
            
            pluginTemplates[templateId] = template;
            _callback.InvokeSuccess(template);
        }

        protected override void EnterPool()
        {
            _plugin = null;
            _name = null;
            _interaction = null;
            _callback = null;
            _logger = null;
        }

        protected override void DisposeInternal()
        {
            _callback?.Dispose();
            DiscordPool.Free(this);
        }
    }
}