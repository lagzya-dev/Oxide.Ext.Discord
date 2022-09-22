using System;
using Oxide.Ext.Discord.Entities;

namespace Oxide.Ext.Discord.Interfaces.Callbacks.Async
{
    /// <summary>
    /// Interface for Discord Async Callbacks
    /// </summary>
    public interface IDiscordAsyncCallback : IDisposable
    {
        Snowflake Id { get; }
        bool IsCompleted { get; }
        
        /// <summary>
        /// Adds a callback when <see cref="InvokeSuccess"/> is called
        /// </summary>
        /// <param name="complete">Callback to call</param>
        /// <returns>this</returns>
        IDiscordAsyncCallback OnSuccess(Action complete);
    }
}