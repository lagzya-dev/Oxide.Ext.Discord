using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Helpers.Interfaces
{
    /// <summary>
    /// Interface used to get the entity ID from an entity
    /// </summary>
    public interface IGetEntityId
    {
        /// <summary>
        /// Returns the unique ID for this entity
        /// </summary>
        /// <returns></returns>
        Snowflake GetEntityId();
    }
}