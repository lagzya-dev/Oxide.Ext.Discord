using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates
{
    internal class BulkRegisterTemplateCallback<T> : BaseAsyncCallback where T : BaseTemplate
    {
        private BaseTemplateLibrary _library;
        private TemplateId _id;
        private List<BulkTemplateRegistration<T>> _templates;
        private TemplateType _type;
        private TemplateVersion _minVersion;
        private DiscordAsyncCallback _callback;
        
        public static void Start(BaseTemplateLibrary library, TemplateId id, List<BulkTemplateRegistration<T>> templates, TemplateType type, TemplateVersion minVersion, DiscordAsyncCallback callback)
        {
            BulkRegisterTemplateCallback<T> register = DiscordPool.Get<BulkRegisterTemplateCallback<T>>();
            register.Init(library, id, templates, type, minVersion, callback);
            register.Run();
        }
        
        private void Init(BaseTemplateLibrary library, TemplateId id, List<BulkTemplateRegistration<T>> templates, TemplateType type, TemplateVersion minVersion, DiscordAsyncCallback callback)
        {
            _library = library;
            _id = id;
            _templates = templates;
            _type = type;
            _minVersion = minVersion;
            _callback = callback;
        }

        protected override  Task HandleCallback()
        {
            return _library.HandleBulkRegisterTemplate(_id, _templates, _type, _minVersion, _callback);
        }

        protected override void EnterPool()
        {
            _library = null;
            _id = default(TemplateId);
            _templates = null;
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