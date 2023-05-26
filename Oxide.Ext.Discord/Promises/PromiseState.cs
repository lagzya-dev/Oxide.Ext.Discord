// Originally from: https://github.com/Real-Serious-Games/C-Sharp-Promise
// Modified by: MJSU

namespace Oxide.Ext.Discord.Promises
{
    /// <summary>
    /// Specifies the state of a promise.
    /// </summary>
    public enum PromiseState : byte
    {
        Pending,    // The promise is in-flight.
        Rejected,   // The promise has been rejected.
        Resolved    // The promise has been resolved.
    };
}