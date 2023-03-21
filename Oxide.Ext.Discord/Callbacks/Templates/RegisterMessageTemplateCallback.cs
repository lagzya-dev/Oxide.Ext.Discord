using System.Threading.Tasks;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Callbacks.Templates
{
    internal class RegisterTemplateCallback<TTemplate> : BaseAsyncCallback where TTemplate : BaseTemplate
    {
        private BaseTemplateLibrary<TTemplate> _library;
        private TemplateId _id;
        private TTemplate _template;
        private TemplateVersion _minVersion;
        private IDiscordPromise _promise;
        
        public static void Start(BaseTemplateLibrary<TTemplate> library, TemplateId id, TTemplate template, TemplateVersion minVersion, IDiscordPromise promise)
        {
            RegisterTemplateCallback<TTemplate> register = DiscordPool.Internal.Get<RegisterTemplateCallback<TTemplate>>();
            register.Init(library, id, template, minVersion, promise);
            register.Run();
        }
        
        private void Init(BaseTemplateLibrary<TTemplate> library, TemplateId id, TTemplate template, TemplateVersion minVersion, IDiscordPromise promise)
        {
            _library = library;
            _id = id;
            _template = template;
            _minVersion = minVersion;
            _promise = promise;
        }

        protected override  Task HandleCallback()
        {
            _library.HandleRegisterTemplate(_id, _template, _minVersion, _promise);
            return Task.CompletedTask;
        }

        protected override string GetExceptionMessage()
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