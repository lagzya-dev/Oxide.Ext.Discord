using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates
{
    internal class RegisterTemplateCallback<T> : BaseAsyncCallback where T : BaseTemplate
    {
        private BaseTemplateLibrary _library;
        private TemplateId _id;
        private T _template;
        private TemplateType _type;
        private TemplateVersion _minVersion;
        private DiscordAsyncCallback _callback;
        
        public static void Start(BaseTemplateLibrary library, TemplateId id, T template, TemplateType type, TemplateVersion minVersion, DiscordAsyncCallback callback)
        {
            RegisterTemplateCallback<T> register = DiscordPool.Get<RegisterTemplateCallback<T>>();
            register.Init(library, id, template, type, minVersion, callback);
            register.Run();
        }
        
        private void Init(BaseTemplateLibrary library, TemplateId id, T template, TemplateType type, TemplateVersion minVersion, DiscordAsyncCallback callback)
        {
            _library = library;
            _id = id;
            _template = template;
            _type = type;
            _minVersion = minVersion;
            _callback = callback;
        }

        protected override  Task HandleCallback()
        {
            return _library.HandleRegisterTemplate(_id, _template, _type, _minVersion, _callback);
        }

        protected override void EnterPool()
        {
            _library = null;
            _id = default(TemplateId);
            _template = null;
            _type = default(TemplateType);
            _minVersion = default(TemplateVersion);
            _callback = null;
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}