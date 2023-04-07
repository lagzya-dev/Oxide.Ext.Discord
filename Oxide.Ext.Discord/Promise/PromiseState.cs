namespace Oxide.Ext.Discord.Promise
{
    /// <summary>
    /// Represents the current promise state
    /// </summary>
    public enum PromiseState : byte
    {
        /// <summary>
        /// Promise is waiting to be resolve or failed
        /// </summary>
        Pending,
        
        /// <summary>
        /// Promise was resolved
        /// </summary>
        Resolved,
        
        /// <summary>
        /// Promise failed
        /// </summary>
        Failed
    }
}