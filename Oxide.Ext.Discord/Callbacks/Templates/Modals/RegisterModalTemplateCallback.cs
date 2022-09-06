using System.Threading.Tasks;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Modals;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Modals
{
    internal class RegisterModalTemplateCallback : BaseAsyncCallback
    {
        private readonly DiscordModalTemplates _templates = DiscordExtension.DiscordModalTemplates;
        private TemplateId _id;
        private DiscordModalTemplate _template;
        private TemplateVersion _minSupportedVersion;
        
        public static void Start(TemplateId id, DiscordModalTemplate template, TemplateVersion minSupportedVersion)
        {
            RegisterModalTemplateCallback callback = DiscordPool.Get<RegisterModalTemplateCallback>();
            callback.Init(id, template, minSupportedVersion);
            callback.Run();
        }
        
        private void Init(TemplateId id, DiscordModalTemplate template, TemplateVersion minSupportedVersion)
        {
            _id = id;
            _template = template;
            _minSupportedVersion = minSupportedVersion;
        }

        protected override Task HandleCallback()
        {
            return _templates.HandleRegisterModalTemplate(_id, _template, _minSupportedVersion);
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