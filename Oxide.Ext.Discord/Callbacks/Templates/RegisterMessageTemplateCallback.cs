using System.Threading.Tasks;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Callbacks.Templates
{
    internal class RegisterTemplateCallback<T> : BaseAsyncCallback where T : BaseTemplate
    {
        private BaseTemplateLibrary _library;
        private TemplateId _id;
        private T _template;
        private TemplateVersion _minVersion;
        private IDiscordPromise _promise;
        
        public static void Start(BaseTemplateLibrary library, TemplateId id, T template, TemplateVersion minVersion, IDiscordPromise promise)
        {
            RegisterTemplateCallback<T> register = DiscordPool.Get<RegisterTemplateCallback<T>>();
            register.Init(library, id, template, minVersion, promise);
            register.Run();
        }
        
        private void Init(BaseTemplateLibrary library, TemplateId id, T template, TemplateVersion minVersion, IDiscordPromise promise)
        {
            _library = library;
            _id = id;
            _template = template;
            _minVersion = minVersion;
            _promise = promise;
        }

        protected override  Task HandleCallback()
        {
            return _library.HandleRegisterTemplate(_id, _template, _minVersion, _promise);
        }

        protected override string ExceptionData()
        {
            return $"Template ID: {_id.ToString()} Type: {_library.GetType().Name}";
        }

        protected override void EnterPool()
        {
            _library = null;
            _id = default(TemplateId);
            _template = null;
            _minVersion = default(TemplateVersion);
            _promise = null;
        }
    }
}