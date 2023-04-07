using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Libraries.Pooling;

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

        internal new static DiscordPromise<TResult> Create(bool isInternal = false)
        {
            DiscordPromise<TResult> promise = DiscordPool.Internal.Get<DiscordPromise<TResult>>();
            promise.IsInternal = isInternal;
            return promise;
        }

        ///<inheritdoc/>
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

        ///<inheritdoc/>
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

        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public IDiscordPromise<TResult> Done(Action<TResult> onResolved, Action<Exception> onFail)
        {
            Then(onResolved);
            Catch(onFail);
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

        ///<inheritdoc/>
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