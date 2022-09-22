using System.Collections.Generic;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Callbacks.Async;
using Oxide.Ext.Discord.Libraries.Templates.Messages;
using Oxide.Ext.Discord.Libraries.Templates.Messages.Bulk;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Templates.Messages
{
    public class ToBulkEntityCallback<TTemplate, TEntity> : BaseAsyncCallback 
        where TTemplate : BaseMessageTemplate<TEntity> 
        where TEntity : class 
    {
        private TTemplate _template;
        private BulkEntityRequest<TEntity> _request;
        private DiscordAsyncCallback<List<TEntity>> _callback;

        /// <summary>
        /// Starts the callback
        /// </summary>
        /// <param name="template"></param>
        /// <param name="placeholder"></param>
        /// <param name="entities"></param>
        /// <param name="callback"></param>
        public static void Start(TTemplate template, BulkEntityRequest<TEntity> request, DiscordAsyncCallback<List<TEntity>> callback)
        {
            ToBulkEntityCallback<TTemplate, TEntity> handler = DiscordPool.Get<ToBulkEntityCallback<TTemplate, TEntity>>();
            handler.Init(template, request, callback);
            handler.Run();
        }
        
        private void Init(TTemplate template, BulkEntityRequest<TEntity> request, DiscordAsyncCallback<List<TEntity>> callback)
        {
            _template = template;
            _request = request;
            _callback = callback;
        }
        
        ///<inheritdoc/>
        protected override Task HandleCallback()
        {
            return _template.HandleBulkToEntityAsync(_request, _callback);
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            _template = null;
            _request = null;
            _callback = null;
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            _request.Dispose();
            DiscordPool.Free(this);
        }
    }
}