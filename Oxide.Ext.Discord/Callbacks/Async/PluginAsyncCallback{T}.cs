using System;
using Oxide.Core;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    /// <summary>
    /// Represents a plugin async callback of {T} data
    /// </summary>
    /// <typeparam name="T">Type to callbac kwith</typeparam>
    public class PluginAsyncCallback<T> : BaseDiscordAsyncCallback<T>
    {
        private readonly Action _successCallback;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public PluginAsyncCallback()
        {
            _successCallback = InvokeSuccessInternal;
        }
        
        internal static PluginAsyncCallback<T> Create()
        {
            return DiscordPool.Get<PluginAsyncCallback<T>>();
        }

        /// <summary>
        /// Invoke callbacks with {T} data
        /// </summary>
        /// <param name="data">Data to callback with</param>
        public override void InvokeSuccess(T data)
        {
            Data = data;
            Interface.Oxide.NextTick(_successCallback);
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}