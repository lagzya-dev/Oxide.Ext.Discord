using System.Threading.Tasks;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates
{
    internal class RegisterTemplateCallback<T> : BaseAsyncCallback where T : BaseTemplate
    {
        private BaseTemplateLibrary _library;
        private TemplateId _id;
        private T _template;
        private TemplateVersion _minVersion;
        private IDiscordAsyncCallback _callback;
        
        public static void Start(BaseTemplateLibrary library, TemplateId id, T template, TemplateVersion minVersion, IDiscordAsyncCallback callback)
        {
            RegisterTemplateCallback<T> register = DiscordPool.Get<RegisterTemplateCallback<T>>();
            register.Init(library, id, template, minVersion, callback);
            register.Run();
        }
        
        private void Init(BaseTemplateLibrary library, TemplateId id, T template, TemplateVersion minVersion, IDiscordAsyncCallback callback)
        {
            _library = library;
            _id = id;
            _template = template;
            _minVersion = minVersion;
            _callback = callback;
        }

        protected override  Task HandleCallback()
        {
            return _library.HandleRegisterTemplate(_id, _template, _minVersion, _callback);
        }

        protected override void EnterPool()
        {
            _library = null;
            _id = default(TemplateId);
            _template = null;
            _minVersion = default(TemplateVersion);
            _callback = null;
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}