using System;
using System.Threading;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.ThreadPool
{
    /// <summary>
    /// Represents a base callback to be used when needing a lambda callback so no delegate or class is generated
    /// This class is pooled to prevent allocations
    /// </summary>
    public abstract class BaseThreadPoolHandler : BasePoolable
    {
        /// <summary>
        /// The callback to be called by the delegate
        /// </summary>
        public readonly WaitCallback Callback;
        
        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseThreadPoolHandler()
        {
            Callback = CallbackInternal;
        }

        /// <summary>
        /// Overridden in the child class to handle the callback
        /// </summary>
        protected virtual void HandleCallback(object data)
        {
            
        }

        private void CallbackInternal(object data)
        {
            try
            {
                HandleCallback(data);
            }
            catch (Exception ex)
            {
                DiscordExtension.GlobalLogger.Exception("{0}.CallbackInternal had exception", GetType().Name, ex);
            }
            finally
            {
                Dispose();
            }
        }
    }
}