using System;
using Oxide.Core;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    /// <summary>
    /// Represents a async callback for a plugin
    /// </summary>
    public class PluginAsyncCallback : BaseDiscordAsyncCallback
    {
        private readonly Action _successCallback;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public PluginAsyncCallback()
        {
            _successCallback = InvokeSuccessInternal;
        }
        
        internal static PluginAsyncCallback Create()
        {
            return DiscordPool.Get<PluginAsyncCallback>();
        }

        /// <summary>
        /// Invoke the callback
        /// </summary>
        public override void InvokeSuccess()
        {
            Interface.Oxide.NextTick(_successCallback);
        }
        
        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}