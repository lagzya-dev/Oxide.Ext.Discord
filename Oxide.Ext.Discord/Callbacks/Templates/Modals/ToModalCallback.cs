using System.Threading.Tasks;
using Oxide.Ext.Discord.Entities.Interactions.Response;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Modals;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Modals
{
    public class ToModalCallback : BaseAsyncCallback
    {
        private DiscordModalTemplate _template;
        private PlaceholderData _data;
        private InteractionModalMessage _message;
        private IDiscordAsyncCallback<InteractionModalMessage> _callback;

        public static void Start(DiscordModalTemplate template, PlaceholderData data, InteractionModalMessage message, IDiscordAsyncCallback<InteractionModalMessage> callback)
        {
            ToModalCallback handler = DiscordPool.Get<ToModalCallback>();
            handler.Init(template, data, message, callback);
            handler.Run();
        }
        
        private void Init(DiscordModalTemplate template, PlaceholderData data, InteractionModalMessage message, IDiscordAsyncCallback<InteractionModalMessage> callback)
        {
            _template = template;
            _data = data;
            _message = message;
            _callback = callback;
        }
        
        protected override Task HandleCallback()
        {
            return _template.HandleToModalAsync(_data, _message, _callback);
        }

        protected override void EnterPool()
        {
            _template = null;
            _data = null;
            _message = null;
            _callback = null;
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}