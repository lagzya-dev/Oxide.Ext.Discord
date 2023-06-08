namespace Oxide.Ext.Discord.Entities.Channels
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#channel-object-sort-order-types">Sort Order Types</a> in Discord
    /// </summary>
    public enum SortOrderType : byte
    {
        /// <summary>
        /// Sort forum posts by activity
        /// </summary>
        LatestActivity,
        
        /// <summary>
        /// Sort forum posts by creation time (from most recent to oldest)
        /// </summary>
        CreationDate
    }
}