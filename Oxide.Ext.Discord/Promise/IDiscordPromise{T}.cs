using System;

namespace Oxide.Ext.Discord.Promise
{
    /// <summary>
    /// Interface for Discord Async Callbacks of {T}
    /// </summary>
    /// <typeparam name="TResult">Type to callback with</typeparam>
    public interface IDiscordPromise<TResult> : IDiscordPromise
    {
        /// <summary>
        /// Registers a Promise callback once the async call is completed with {T} type
        /// </summary>
        /// <param name="onResolved">Callback to register</param>
        /// <returns>This</returns>
        IDiscordPromise<TResult> Then(Action<TResult> onResolved);
        
        /// <summary>
        /// Callback to convert the promise results to the {TConvert} type
        /// </summary>
        /// <param name="onResolved">Callback to resolve</param>
        /// <typeparam name="TConvert">Type to convert to</typeparam>
        /// <returns>Promise with the {TConvert} type</returns>
        IDiscordPromise<TConvert> Then<TConvert>(Func<TResult, TConvert> onResolved);
        
        /// <summary>
        /// Callback to convert the promise results to the {TConvert} using a promise
        /// </summary>
        /// <param name="onResolved">Promise to resolve</param>
        /// <typeparam name="TConvert">Type to convert to</typeparam>
        /// <returns>Promise with the {TConvert} type</returns>
        IDiscordPromise<TConvert> Then<TConvert>(Func<TResult, IDiscordPromise<TConvert>> onResolved);

        /// <summary>
        /// Callbacks when the promise completes
        /// </summary>
        /// <param name="onResolved">Callback on promise success</param>
        /// <param name="onFail">Callback on promise fail</param>
        /// <returns>this</returns>
        IDiscordPromise<TResult> Done(Action<TResult> onResolved, Action<Exception> onFail);
        
        /// <summary>
        /// Resolves the promise
        /// </summary>
        /// <param name="data">Data to invoke with</param>
        void Resolve(TResult data);
    }
}