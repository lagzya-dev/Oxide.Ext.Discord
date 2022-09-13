using System.Threading.Tasks;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Interfaces.Entities.Messages;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    /// <summary>
    /// Callback for parsing a message template to a message
    /// </summary>
    /// <typeparam name="T">Type of message to create</typeparam>
    public class ToMessageCallback<T> : BaseAsyncCallback where T : class, IDiscordTemplateMessage, new()
    {
        private DiscordMessageTemplate _template;
        private PlaceholderData _data;
        private T _message;
        private IDiscordAsyncCallback<T> _callback;

        /// <summary>
        /// Starts the callback
        /// </summary>
        /// <param name="template"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="callback"></param>
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
        
        ///<inheritdoc/>
        protected override Task HandleCallback()
        {
            return _template.HandleToMessageAsync(_data, _message, _callback);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _template = null;
            _data = null;
            _message = null;
            _callback = null;
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}