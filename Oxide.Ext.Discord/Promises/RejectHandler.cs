// Originally from: https://github.com/Real-Serious-Games/C-Sharp-Promise
// Modified by: MJSU

using System;
using Oxide.Ext.Discord.Interfaces.Promises;

namespace Oxide.Ext.Discord.Promises
{
    /// <summary>
    /// Represents a handler invoked when the promise is rejected.
    /// </summary>
    public struct RejectHandler
    {
        /// <summary>
        /// Callback fn.
        /// </summary>
        public readonly Action<Exception> Callback;

        /// <summary>
        /// The promise that is rejected when there is an error while invoking the handler.
        /// </summary>
        public readonly IRejectable Rejectable;
        
        public RejectHandler(Action<Exception> callback, IRejectable rejectable)
        {
            Callback = callback;
            Rejectable = rejectable;
        }

        public void Reject(Exception exception)
        {
            try
            {
                Callback(exception);
            }
            catch (Exception ex)
            {
                Rejectable.Reject(ex);
            }
        }
    }
}