using System.Threading.Tasks;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Interfaces.Entities.Messages;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async.Templates.Messages
{
    public class ToPlaceholderMessageCallback<T> : BaseAsyncCallback where T : class, IDiscordTemplateMessage, new()
    {
        private DiscordMessageTemplate _template;
        private PlaceholderData _data;
        private T _message;
        private IDiscordAsyncCallback<T> _callback;

        public static void Start(DiscordMessageTemplate template, PlaceholderData data, T message, IDiscordAsyncCallback<T> callback)
        {
            ToPlaceholderMessageCallback<T> handler = DiscordPool.Get<ToPlaceholderMessageCallback<T>>();
            handler.Init(template, data, message, callback);
            handler.Run();
        }
        
        private void Init(DiscordMessageTemplate template, PlaceholderData data, T message, IDiscordAsyncCallback<T> callback)
        {
            _template = template;
            _data = data;
            _message = message;
            _callback = callback;
        }
        
        protected override async Task HandleCallback()
        {
            _message = await _template.ToPlaceholderMessageInternalAsync(_data, _message).ConfigureAwait(false);
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