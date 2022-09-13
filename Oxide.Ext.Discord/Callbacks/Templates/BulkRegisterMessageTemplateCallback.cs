using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates
{
    internal class BulkRegisterTemplateCallback<T> : BaseAsyncCallback where T : BaseTemplate
    {
        private BaseTemplateLibrary _library;
        private TemplateId _id;
        private List<BulkTemplateRegistration<T>> _templates;
        private TemplateVersion _minVersion;
        private IDiscordAsyncCallback _callback;
        
        public static void Start(BaseTemplateLibrary library, TemplateId id, List<BulkTemplateRegistration<T>> templates, TemplateVersion minVersion, IDiscordAsyncCallback callback)
        {
            BulkRegisterTemplateCallback<T> register = DiscordPool.Get<BulkRegisterTemplateCallback<T>>();
            register.Init(library, id, templates, minVersion, callback);
            register.Run();
        }
        
        private void Init(BaseTemplateLibrary library, TemplateId id, List<BulkTemplateRegistration<T>> templates, TemplateVersion minVersion, IDiscordAsyncCallback callback)
        {
            _library = library;
            _id = id;
            _templates = templates;
            _minVersion = minVersion;
        }

        protected override  Task HandleCallback()
        {
            return _library.HandleBulkRegisterTemplate(_id, _templates, _minVersion, _callback);
        }

        protected override void EnterPool()
        {
            _library = null;
            _id = default(TemplateId);
            _templates = null;
            _minVersion = default(TemplateVersion);
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}