using System.Threading.Tasks;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    public class ToEntityCallback<TTemplate, TEntity> : BaseAsyncCallback 
        where TTemplate : BaseMessageTemplate<TEntity> 
        where TEntity : class 
    {
        private TTemplate _template;
        private PlaceholderData _data;
        private TEntity _entity;
        private IDiscordAsyncCallback<TEntity> _callback;

        /// <summary>
        /// Starts the callback
        /// </summary>
        /// <param name="template"></param>
        /// <param name="data"></param>
        /// <param name="entity"></param>
        /// <param name="callback"></param>
        public static void Start(TTemplate template, PlaceholderData data, TEntity entity, IDiscordAsyncCallback<TEntity> callback)
        {
            ToEntityCallback<TTemplate, TEntity> handler = DiscordPool.Get<ToEntityCallback<TTemplate, TEntity>>();
            handler.Init(template, data, entity, callback);
            handler.Run();
        }
        
        private void Init(TTemplate template, PlaceholderData data, TEntity entity, IDiscordAsyncCallback<TEntity> callback)
        {
            _template = template;
            _data = data;
            _entity = entity;
            _callback = callback;
        }
        
        ///<inheritdoc/>
        protected override Task HandleCallback()
        {
            return _template.HandleToEntityAsync(_data, _entity, _callback);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _template = null;
            _data = null;
            _entity = null;
            _callback = null;
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}