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
        /// Registers a callback of {T}
        /// </summary>
        /// <param name="onResolved">Callback to call</param>
        /// <returns>this</returns>
        IDiscordPromise<TResult> Then(Action<TResult> onResolved);
        IDiscordPromise<TConvert> Then<TConvert>(Func<TResult, TConvert> onResolved);
        IDiscordPromise<TConvert> Then<TConvert>(Func<TResult, IDiscordPromise<TConvert>> onResolved);

        new IDiscordPromise<TResult> Catch(Action<Exception> onFail);

        IDiscordPromise<TResult> Done(Action<TResult> onResolved, Action<Exception> onFail);
        
        void Resolve(TResult data);
    }
}