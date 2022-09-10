using System.Threading.Tasks;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates
{
    internal class RegisterTemplateCallback<T> : BaseAsyncCallback where T : BaseTemplate
    {
        private BaseTemplateLibrary _library;
        private TemplateId _id;
        private T _template;
        private TemplateVersion _minSupportedVersion;
        
        public static void Start(BaseTemplateLibrary library, TemplateId id, T template, TemplateVersion minSupportedVersion)
        {
            RegisterTemplateCallback<T> callback = DiscordPool.Get<RegisterTemplateCallback<T>>();
            callback.Init(library, id, template, minSupportedVersion);
            callback.Run();
        }
        
        private void Init(BaseTemplateLibrary library, TemplateId id, T template, TemplateVersion minSupportedVersion)
        {
            _library = library;
            _id = id;
            _template = template;
            _minSupportedVersion = minSupportedVersion;
        }

        protected override  Task HandleCallback()
        {
            return _library.HandleRegisterTemplate(_id, _template, _minSupportedVersion);
        }

        protected override void EnterPool()
        {
            _library = null;
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