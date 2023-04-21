using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Libraries.Templates.Messages
{
    /// <summary>
    /// Represents the base class for Message Templates
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseMessageTemplate<TEntity> where TEntity : class
    {
        /// <summary>
        /// Converts the template to the given entity
        /// </summary>
        /// <param name="data">(Optional) Placeholder Data to apply</param>
        /// <param name="entity">(Optional) Entity to apply to</param>
        /// <returns></returns>
        public abstract TEntity ToEntity(PlaceholderData data = null, TEntity entity = null);
    }
}