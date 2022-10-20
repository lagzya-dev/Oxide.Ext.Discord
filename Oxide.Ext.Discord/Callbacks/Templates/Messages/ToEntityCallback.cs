using System.Threading.Tasks;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    public class ToEntityCallback<TTemplate, TEntity> : BaseAsyncCallback 
        where TTemplate : BaseMessageTemplate<TEntity> 
        where TEntity : class 
    {
        private TTemplate _template;
        private PlaceholderData _data;
        private TEntity _entity;
        private IDiscordPromise<TEntity> _promise;

        /// <summary>
        /// Starts the callback
        /// </summary>
        /// <param name="template"></param>
        /// <param name="data"></param>
        /// <param name="entity"></param>
        /// <param name="promise"></param>
        public static void Start(TTemplate template, PlaceholderData data, TEntity entity, IDiscordPromise<TEntity> promise)
        {
            ToEntityCallback<TTemplate, TEntity> handler = DiscordPool.Get<ToEntityCallback<TTemplate, TEntity>>();
            handler.Init(template, data, entity, promise);
            handler.Run();
        }
        
        private void Init(TTemplate template, PlaceholderData data, TEntity entity, IDiscordPromise<TEntity> promise)
        {
            _template = template;
            _data = data;
            _entity = entity;
            _promise = promise;
        }
        
        ///<inheritdoc/>
        protected override Task HandleCallback()
        {
            _template.HandleToEntityAsync(_data, _entity, _promise);
            return Task.CompletedTask;
        }
        
        protected override string ExceptionData()
        {
            return $"Type: {typeof(TEntity).Name} Placeholders: {_data.GetKeys()}";
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _template = null;
            _data = null;
            _entity = null;
            _promise = null;
        }
    }
}