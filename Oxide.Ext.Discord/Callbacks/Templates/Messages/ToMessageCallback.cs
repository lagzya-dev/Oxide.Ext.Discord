using System.Threading.Tasks;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Interfaces.Entities.Messages;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    public class ToMessageCallback<T> : BaseAsyncCallback where T : class, IDiscordTemplateMessage, new()
    {
        private DiscordMessageTemplate _template;
        private PlaceholderData _data;
        private T _message;
        private IDiscordAsyncCallback<T> _callback;

        public static void Start(DiscordMessageTemplate template, PlaceholderData data, T message, IDiscordAsyncCallback<T> callback)
        {
            ToMessageCallback<T> handler = DiscordPool.Get<ToMessageCallback<T>>();
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
        
        protected override Task HandleCallback()
        {
            return _template.HandleToMessageAsync(_data, _message, _callback);
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