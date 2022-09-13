using System;

namespace Oxide.Ext.Discord.Interfaces.Callbacks.Async
{
    /// <summary>
    /// Interface for Discord Async Callbacks of {T}
    /// </summary>
    /// <typeparam name="T">Type to callback with</typeparam>
    public interface IDiscordAsyncCallback<T> : IDisposable
    {
        /// <summary>
        /// Registers a callback of {T}
        /// </summary>
        /// <param name="complete">Callback to call</param>
        /// <returns>this</returns>
        IDiscordAsyncCallback<T> OnSuccess(Action<T> complete);
        
        /// <summary>
        /// Invokes the registered callbacks with {T} data
        /// </summary>
        /// <param name="data">Data to callback with</param>
        void InvokeSuccess(T data);
    }
}