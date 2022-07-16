using System;
using System.Threading.Tasks;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    /// <summary>
    /// Represents a base callback to be used when needing a lambda callback so no delegate or class is generated
    /// This class is pooled to prevent allocations
    /// </summary>
    public abstract class BaseAsyncPoolableCallback : BasePoolable
    {
        /// <summary>
        /// The callback to be called by the delegate
        /// </summary>
        private readonly Action _callback;
        
        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseAsyncPoolableCallback()
        {
            _callback = CallbackInternal;
        }

        /// <summary>
        /// Overridden in the child class to handle the callback
        /// </summary>
        protected virtual Task HandleCallback()
        {
            return Task.CompletedTask;
        }

        public void Run()
        {
            Task.Run(_callback);
        }

        private async void CallbackInternal()
        {
            try
            {
                await HandleCallback();
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