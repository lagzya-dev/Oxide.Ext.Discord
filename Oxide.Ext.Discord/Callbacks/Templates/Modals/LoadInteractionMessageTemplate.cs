using System.Threading.Tasks;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Modals;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Callbacks.Templates.Modals
{
    internal class LoadModalTemplateCallback : BaseAsyncCallback
    {
        private readonly DiscordModalTemplates _templates = DiscordExtension.DiscordModalTemplates;
        private Plugin _plugin;
        private string _name;
        private DiscordInteraction _interaction;
        private IDiscordAsyncCallback<DiscordModalTemplate> _callback;
        private ILogger _logger;

        public static void Start(Plugin plugin, string name, DiscordInteraction interaction, IDiscordAsyncCallback<DiscordModalTemplate> callback, ILogger logger)
        {
            LoadModalTemplateCallback load = DiscordPool.Get<LoadModalTemplateCallback>();
            load.Init(plugin, name, interaction, callback, logger);
            load.Run();
        }

        private void Init(Plugin plugin, string name, DiscordInteraction interaction, IDiscordAsyncCallback<DiscordModalTemplate> callback, ILogger logger)
        {
            _plugin = plugin;
            _name = name;
            _interaction = interaction;
            _callback = callback;
            _logger = logger;
        }

        protected override async Task HandleCallback()
        {
            Hash<TemplateId, DiscordModalTemplate> pluginTemplates =  _templates.TemplateCache[_plugin.Name];
            if (pluginTemplates == null)
            {
                pluginTemplates = new Hash<TemplateId, DiscordModalTemplate>();
                _templates.TemplateCache[_plugin.Name] = pluginTemplates;
            }

            string interactionLang = DiscordLocale.GetOxideLanguage(_interaction.Locale);
            TemplateId templateId = new TemplateId(_name, interactionLang);
            if (pluginTemplates.TryGetValue(templateId, out DiscordModalTemplate template))
            {
                _callback.InvokeSuccess(template);
                return;
            }
            
            IPlayer player = _interaction.User.Player;

            template = await _templates.LoadTemplate<DiscordModalTemplate>(_plugin, TemplateType.Modal, _name, interactionLang).ConfigureAwait(false)
                       ?? (player != null ? await _templates.LoadTemplate<DiscordModalTemplate>(_plugin, TemplateType.Modal, _name, DiscordLocale.GetPlayerLanguage(player)).ConfigureAwait(false) : null)
                       ?? await _templates.LoadTemplate<DiscordModalTemplate>(_plugin, TemplateType.Modal, _name, DiscordLocale.GetOxideLanguage(_interaction.GuildLocale)).ConfigureAwait(false) 
                       ?? await _templates.LoadTemplate<DiscordModalTemplate>(_plugin, TemplateType.Modal, _name, DiscordLocale.GameServerLanguage).ConfigureAwait(false)
                       ?? await _templates.LoadTemplate<DiscordModalTemplate>(_plugin, TemplateType.Modal, _name, DiscordLocale.DefaultOxideLanguage).ConfigureAwait(false);
            
            if (template == null)
            {
                _logger.Warning($"Plugin {{0}} is using the {nameof(DiscordModalTemplates)}.{nameof(DiscordModalTemplates.LoadTemplate)} API but message template name '{{1}}' is not registered", _plugin.FullName(), _name);
                _callback.InvokeSuccess(new DiscordModalTemplate());
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