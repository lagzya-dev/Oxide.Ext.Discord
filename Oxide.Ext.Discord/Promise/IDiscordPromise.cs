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

        IDiscordPromise Catch(Action<Exception> onFail);

        IDiscordPromise Done(Action onResolved, Action<Exception> onFail);
        
        void Resolve();

        void Fail(Exception ex);
    }
}