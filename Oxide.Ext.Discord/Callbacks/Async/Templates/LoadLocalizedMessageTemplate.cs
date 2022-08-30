using System.Threading.Tasks;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Callbacks.Async.Templates
{
    internal class LoadLocalizedMessageTemplate : BaseAsyncPoolableCallback
    {
        private readonly DiscordTemplates _templates = DiscordExtension.DiscordTemplates;
        private Plugin _plugin;
        private string _name;
        private string _language;
        private DiscordAsyncCallback<DiscordMessageTemplate> _callback;
        private ILogger _logger;

        public static LoadLocalizedMessageTemplate Create(Plugin plugin, string name, string language, DiscordAsyncCallback<DiscordMessageTemplate> callback, ILogger logger)
        {
            LoadLocalizedMessageTemplate load = DiscordPool.Get<LoadLocalizedMessageTemplate>();
            load.Init(plugin, name, language, callback, logger);
            return load;
        }

        private void Init(Plugin plugin, string name, string language, DiscordAsyncCallback<DiscordMessageTemplate> callback, ILogger logger)
        {
            _plugin = plugin;
            _name = name;
            _language = language;
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

            TemplateId templateId = new TemplateId(_name, null);
            if (pluginTemplates.TryGetValue(templateId, out DiscordMessageTemplate template))
            {
                _callback.InvokeSuccess(template);
                return;
            }

            template = await DiscordExtension.DiscordTemplates.LoadTemplate(_plugin, _name, _language).ConfigureAwait(false)
                       ?? await DiscordExtension.DiscordTemplates.LoadTemplate(_plugin, _name, DiscordLocale.GameServerLanguage).ConfigureAwait(false)
                       ?? await DiscordExtension.DiscordTemplates.LoadTemplate(_plugin, _name, DiscordLocale.DefaultOxideLanguage).ConfigureAwait(false);
            
            if (template == null)
            {
                _logger.Warning($"Plugin {{0}} is using the {nameof(DiscordTemplates)}.{nameof(DiscordTemplates.GetLocalizedMessageTemplate)} API but message template name '{{1}}' is not registered", _plugin.FullName(), _name);
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
            _language = null;
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