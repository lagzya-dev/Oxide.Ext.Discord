using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Promise
{
    /// <summary>
    /// Represents a DiscordPromise {TResult}
    /// </summary>
    /// <typeparam name="TResult">Callback type of the promise</typeparam>
    public class DiscordPromise<TResult> : DiscordPromise, IDiscordPromise<TResult>
    {
        /// <summary>
        /// The data to callback with
        /// </summary>
        private TResult _data;
        
        private readonly List<Action<TResult>> _success = new List<Action<TResult>>();

        internal static DiscordPromise<TResult> Create(bool isInternal = false)
        {
            DiscordPromise<TResult> promise = DiscordPool.Get<DiscordPromise<TResult>>();
            promise.IsInternal = isInternal;
            return promise;
        }
        
        /// <summary>
        /// Registers a Promise callback once the async call is completed with {T} type
        /// </summary>
        /// <param name="onResolved">Callback to register</param>
        /// <returns>This</returns>
        public IDiscordPromise<TResult> Then(Action<TResult> onResolved)
        {
            if (State == PromiseState.Resolved)
            {
                onResolved(_data);
                return this;
            }
            
            _success.Add(onResolved);
            return this;
        }
        
        public IDiscordPromise<TConvert> Then<TConvert>(Func<TResult, TConvert> onResolved)
        {
            DiscordPromise<TConvert> promise = DiscordPromise<TConvert>.Create(IsInternal);
            if (State == PromiseState.Resolved)
            {
                promise.Resolve(onResolved(_data));
                return promise;
            }
            
            Then(result => promise.Resolve(onResolved(result)));
            AddChild(promise);
            IsInternal = true;
            return promise;
        }
        
        public IDiscordPromise<TConvert> Then<TConvert>(Func<TResult, IDiscordPromise<TConvert>> onResolved)
        {
            DiscordPromise<TConvert> promise = DiscordPromise<TConvert>.Create(IsInternal);
            if (State == PromiseState.Resolved)
            {
                return onResolved(_data);
            }
            
            Then(result => onResolved(result).Then(r => promise.Resolve(r)));
            AddChild(promise);
            IsInternal = true;
            return promise;
        }
        
        public IDiscordPromise<TResult> Fail(Action<Exception> onFail)
        {
            if (State == PromiseState.Failed)
            {
                onFail(Exception);
                return this;
            }

            Fails.Add(onFail);
            return this;
        }
        
        public IDiscordPromise<TResult> Done(Action<TResult> onResolved, Action<Exception> onFail)
        {
            Then(onResolved);
            Fail(onFail);
            return this;
        }
        
        /// <summary>
        /// Handles calling the callbacks and disposing of the object
        /// </summary>
        protected override void InvokeResolveInternal()
        {
            for (int index = 0; index < _success.Count; index++)
            {
                Action<TResult> callback = _success[index];
                callback.Invoke(_data);
            }
            
            base.InvokeResolveInternal();
        }

        /// <summary>
        /// Invokes the <see cref="InvokeResolveInternal"/> method
        /// </summary>
        /// <param name="data">Data to invoke with</param>
        public void Resolve(TResult data)
        {
            SetState(PromiseState.Resolved);
            _data = data;
            InvokeResolveInternal();
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            _data = default(TResult);
            _success.Clear();
        }
    }
}