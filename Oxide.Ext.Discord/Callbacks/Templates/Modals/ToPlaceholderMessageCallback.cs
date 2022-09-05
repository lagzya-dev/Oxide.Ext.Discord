using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async.Templates.Messages;
using Oxide.Ext.Discord.Entities.Interactions.Response;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Interfaces.Entities.Messages;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Libraries.Templates.Modals;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async.Templates.Modals
{
    public class ToPlaceholderModalCallback : BaseAsyncCallback
    {
        private DiscordModalTemplate _template;
        private PlaceholderData _data;
        private InteractionModalMessage _message;
        private IDiscordAsyncCallback<InteractionModalMessage> _callback;

        public static void Start(DiscordModalTemplate template, PlaceholderData data, InteractionModalMessage message, IDiscordAsyncCallback<InteractionModalMessage> callback)
        {
            ToPlaceholderModalCallback handler = DiscordPool.Get<ToPlaceholderModalCallback>();
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
        
        protected override async Task HandleCallback()
        {
            _message = await _template.ToPlaceholderModalInternalAsync(_data, _message).ConfigureAwait(false);
            _callback.InvokeSuccess(_message);
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