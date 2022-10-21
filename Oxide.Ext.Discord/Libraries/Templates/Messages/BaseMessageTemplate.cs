using Oxide.Ext.Discord.Callbacks.Templates.Messages;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Ext.Discord.Promise;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Represents the base class for Message Templates
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseMessageTemplate<TEntity> : BaseTemplate where TEntity : class
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="internalVersion"></param>
        protected BaseMessageTemplate(TemplateType type, TemplateVersion internalVersion) : base(type, internalVersion) { }

        /// <summary>
        /// Converts the template to the given entity
        /// </summary>
        /// <param name="data">(Optional) Placeholder Data to apply</param>
        /// <param name="entity">(Optional) Entity to apply to</param>
        /// <returns></returns>
        public abstract TEntity ToEntity(PlaceholderData data = null, TEntity entity = null);

        /// <summary>
        /// Converts the template to the given entity asynchronously 
        /// </summary>
        /// <param name="data">(Optional) Placeholder Data to apply</param>
        /// <param name="entity">(Optional) Entity to apply to</param>
        /// <returns></returns>
        public IDiscordPromise<TEntity> ToEntityAsync(PlaceholderData data = null, TEntity entity = null)
        {
            return ToEntityInternalAsync(data, entity, DiscordPromise<TEntity>.Create());
        }

        internal IDiscordPromise<TEntity> ToEntityInternalAsync(PlaceholderData data, TEntity message = null, IDiscordPromise<TEntity> promise = null)
        {
            if (promise == null)
            {
                promise = DiscordPromise<TEntity>.Create(true);
            }
            
            ToEntityCallback<BaseMessageTemplate<TEntity>, TEntity>.Start(this, data, message, promise);
            return promise;
        }
        
        internal void HandleToEntityAsync(PlaceholderData data, TEntity entity, IDiscordPromise<TEntity> promise)
        {
            promise.Resolve(ToEntity(data, entity));
        }
    }
}