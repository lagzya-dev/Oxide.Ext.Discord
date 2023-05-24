using System;

namespace Oxide.Ext.Discord.Promise
{
    /// <summary>
    /// Interface for Discord Async Callbacks
    /// </summary>
    public interface IDiscordPromise : IDisposable
    {
        /// <summary>
        /// Adds a callback to be called when completed
        /// </summary>
        /// <param name="onResolved">Callback to call</param>
        /// <returns>this</returns>
        IDiscordPromise Then(Action onResolved);

        /// <summary>
        /// Adds exception callback to the promise
        /// </summary>
        /// <param name="onFail">Exception callback to add</param>
        /// <returns>This</returns>
        IDiscordPromise Catch(Action<Exception> onFail);
        
        /// <summary>
        /// Adds exception callback to the promise
        /// </summary>
        /// <param name="onFail">Exception callback to add</param>
        /// <returns>This</returns>
        IDiscordPromise Catch<TException>(Action<TException> onFail) where TException : Exception;

        /// <summary>
        /// Adds Then and Catch callbacks to the promise
        /// </summary>
        /// <param name="onResolved">Callback when the promise is completed</param>
        /// <param name="onFail">Callback when an exception occurs</param>
        /// <returns>This</returns>
        IDiscordPromise Done(Action onResolved, Action<Exception> onFail);
        
        /// <summary>
        /// Resolves the promise
        /// </summary>
        void Resolve();

        /// <summary>
        /// Fails the promise with the given exception
        /// </summary>
        /// <param name="ex">Exception to fail with</param>
        void Fail(Exception ex);
    }
}