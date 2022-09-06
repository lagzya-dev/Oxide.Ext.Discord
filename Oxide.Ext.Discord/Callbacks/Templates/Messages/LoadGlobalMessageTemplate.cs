using System.Threading.Tasks;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    internal class LoadGlobalMessageTemplate : BaseAsyncCallback
    {
        private readonly DiscordMessageTemplates _templates = DiscordExtension.DiscordMessageTemplates;
        private TemplateId _id;
        private IDiscordAsyncCallback<DiscordMessageTemplate> _callback;

        public static void Start(TemplateId id, IDiscordAsyncCallback<DiscordMessageTemplate> callback)
        {
            LoadGlobalMessageTemplate load = DiscordPool.Get<LoadGlobalMessageTemplate>();
            load.Init(id, callback);
            load.Run();
        }
        
        private void Init(TemplateId id, IDiscordAsyncCallback<DiscordMessageTemplate> callback)
        {
            _id = id;
            _callback = callback;
        }

        protected override Task HandleCallback()
        {
            return _templates.HandleGetGlobalMessageTemplate(_id, _callback);
        }

        protected override void EnterPool()
        {
            _id = default(TemplateId);
            _callback = null;
        }

        protected override void DisposeInternal()
        {
            _callback?.Dispose();
            DiscordPool.Free(this);
        }
    }
}