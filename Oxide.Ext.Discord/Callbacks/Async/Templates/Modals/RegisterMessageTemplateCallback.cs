using System.IO;
using System.Threading.Tasks;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Modals;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async.Templates.Modals
{
    internal class RegisterModalTemplateCallback : BaseAsyncPoolableCallback
    {
        private readonly DiscordModalTemplates _templates = DiscordExtension.DiscordModalTemplates;
        private Plugin _plugin;
        private string _name;
        private string _language;
        private DiscordModalTemplate _template;
        private TemplateVersion _minSupportedVersion;
        private ILogger _logger;

        public static RegisterModalTemplateCallback Create(Plugin plugin, string name, string language, DiscordModalTemplate template, TemplateVersion minSupportedVersion, ILogger logger)
        {
            RegisterModalTemplateCallback callback = DiscordPool.Get<RegisterModalTemplateCallback>();
            callback.Init(plugin, name, language, template, minSupportedVersion, logger);
            return callback;
        }
        
        private void Init(Plugin plugin, string name, string language, DiscordModalTemplate template, TemplateVersion minSupportedVersion, ILogger logger)
        {
            _plugin = plugin;
            _name = name;
            _language = language;
            _template = template;
            _minSupportedVersion = minSupportedVersion;
            _logger = logger;
        }
        
        protected override async Task HandleCallback()
        {
            string path = _templates.GetTemplatePath(_plugin, TemplateType.Message, _name, null);
            if (!File.Exists(path))
            {
                await _templates.CreateFile(path, _template).ConfigureAwait(false);
                return;
            }

            DiscordModalTemplate existingTemplate =  await _templates.LoadTemplate<DiscordModalTemplate>(_plugin, TemplateType.Modal, _name, _language).ConfigureAwait(false);
            if (existingTemplate.Version >= _minSupportedVersion)
            {
                return;
            }
            
            await _templates.MoveFile(_plugin, TemplateType.Message, _name, null, existingTemplate.Version).ConfigureAwait(false);
            await _templates.CreateFile(path, _template).ConfigureAwait(false);
        }

        protected override void EnterPool()
        {
            _plugin = null;
            _name = null;
            _language = null;
            _template = null;
            _minSupportedVersion = default(TemplateVersion);
            _logger = null;
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}