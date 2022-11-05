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
        /// Converts the template to the given entity
        /// </summary>
        /// <param name="data">(Optional) Placeholder Data to apply</param>
        /// <param name="entity">(Optional) Entity to apply to</param>
        /// <returns></returns>
        public T ToEntity<T>(PlaceholderData data = null, T entity = null) where T : class, TEntity => ToEntity(data, entity);
    }
}