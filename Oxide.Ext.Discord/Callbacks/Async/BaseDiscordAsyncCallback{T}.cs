using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Interfaces.Callbacks.Async;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Callbacks.Async
{
    /// <summary>
    /// Represents the base class for Discord Async Callbacks of {T}
    /// </summary>
    /// <typeparam name="T">Type to callback with</typeparam>
    public abstract class BaseDiscordAsyncCallback<T> : BasePoolable, IDiscordAsyncCallback<T>
    {
        private readonly List<Action<T>> _success = new List<Action<T>>();
        /// <summary>
        /// The data to callback with
        /// </summary>
        protected T Data;
        
        /// <summary>
        /// Registers a callback once the async call is completed with {T} type
        /// </summary>
        /// <param name="complete">Callback to register</param>
        /// <returns>This</returns>
        public IDiscordAsyncCallback<T> OnSuccess(Action<T> complete)
        {
            _success.Add(complete);
            return this;
        }
        
        /// <summary>
        /// Handles calling the callbacks and disposing of the object
        /// </summary>
        protected void InvokeSuccessInternal()
        {
            if (_success.Count != 0)
            {
                for (int index = 0; index < _success.Count; index++)
                {
                    Action<T> callback = _success[index];
                    callback.Invoke(Data);
                }
            }
            
            Dispose();
        }

        /// <summary>
        /// Invokes the <see cref="InvokeSuccessInternal"/> method
        /// </summary>
        /// <param name="data">Data to invoke with</param>
        public abstract void InvokeSuccess(T data);
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            Data = default(T);
            _success.Clear();
        }
    }
}