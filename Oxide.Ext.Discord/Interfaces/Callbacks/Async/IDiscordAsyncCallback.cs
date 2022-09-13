using System;

namespace Oxide.Ext.Discord.Interfaces.Callbacks.Async
{
    /// <summary>
    /// Interface for Discord Async Callbacks
    /// </summary>
    public interface IDiscordAsyncCallback : IDisposable
    {
        /// <summary>
        /// Adds a callback when <see cref="InvokeSuccess"/> is called
        /// </summary>
        /// <param name="complete">Callback to call</param>
        /// <returns>this</returns>
        IDiscordAsyncCallback OnSuccess(Action complete);
        
        /// <summary>
        /// Invokes the registered callbacks
        /// </summary>
        void InvokeSuccess();
    }
}