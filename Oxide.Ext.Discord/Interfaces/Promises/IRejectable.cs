// Originally from: https://github.com/Real-Serious-Games/C-Sharp-Promise
// Modified by: MJSU

using System;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Interfaces.Promises
{
    /// <summary>
    /// Interface for a promise that can be rejected.
    /// </summary>
    public interface IRejectable
    {
        /// <summary>
        /// ID of the promise, useful for debugging.
        /// </summary>
        Snowflake Id { get; }
        
        /// <summary>
        /// Name of the promise, when set, useful for debugging.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Reject the promise with an exception.
        /// </summary>
        void Reject(Exception ex);
    }
}