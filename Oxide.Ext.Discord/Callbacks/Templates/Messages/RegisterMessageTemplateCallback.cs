using System.Threading.Tasks;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class RegisterMessageTemplateCallback : BaseAsyncCallback
    {
        private readonly DiscordMessageTemplates _templates = DiscordExtension.DiscordMessageTemplates;
        private TemplateId _id;
        private DiscordMessageTemplate _template;
        private TemplateVersion _minSupportedVersion;
        
        public static void Start(TemplateId id, DiscordMessageTemplate template, TemplateVersion minSupportedVersion, ILogger logger)
        {
            RegisterMessageTemplateCallback callback = DiscordPool.Get<RegisterMessageTemplateCallback>();
            callback.Init(id, template, minSupportedVersion);
            callback.Run();
        }
        
        private void Init(TemplateId id, DiscordMessageTemplate template, TemplateVersion minSupportedVersion)
        {
            _id = id;
            _template = template;
            _minSupportedVersion = minSupportedVersion;
        }

        protected override  Task HandleCallback()
        {
            return _templates.HandleRegisterMessageTemplate(_id, _template, _minSupportedVersion);
        }

        protected override void EnterPool()
        {
            _id = default(TemplateId);
            _template = null;
            _minSupportedVersion = default(TemplateVersion);
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}