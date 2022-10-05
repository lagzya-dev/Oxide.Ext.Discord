using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Callbacks.Templates
{
    internal class BulkRegisterTemplateCallback<T> : BaseAsyncCallback where T : BaseTemplate
    {
        private BaseTemplateLibrary _library;
        private TemplateId _id;
        private List<BulkTemplateRegistration<T>> _templates;
        private TemplateVersion _minVersion;
        private IDiscordPromise _promise;
        
        public static void Start(BaseTemplateLibrary library, TemplateId id, List<BulkTemplateRegistration<T>> templates, TemplateVersion minVersion, IDiscordPromise promise)
        {
            BulkRegisterTemplateCallback<T> register = DiscordPool.Get<BulkRegisterTemplateCallback<T>>();
            register.Init(library, id, templates, minVersion, promise);
            register.Run();
        }
        
        private void Init(BaseTemplateLibrary library, TemplateId id, List<BulkTemplateRegistration<T>> templates, TemplateVersion minVersion, IDiscordPromise promise)
        {
            _library = library;
            _id = id;
            _templates = templates;
            _minVersion = minVersion;
            _promise = promise;
        }

        protected override  Task HandleCallback()
        {
            return _library.HandleBulkRegisterTemplate(_id, _templates, _minVersion, _promise);
        }
        
        protected override string ExceptionData()
        {
            return $"Template ID: {_id.ToString()}  Type: {_library.GetType().Name}";
        }

        protected override void EnterPool()
        {
            _library = null;
            _id = default(TemplateId);
            _templates = null;
            _minVersion = default(TemplateVersion);
            _promise = null;
        }
    }
}