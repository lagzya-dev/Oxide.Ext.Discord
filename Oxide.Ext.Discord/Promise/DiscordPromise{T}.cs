using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Exceptions.Promise;
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
        
        private readonly List<Action<TResult>> _success = new List<Action<TResult>>(0);

        internal new static IDiscordPromise<TResult> Create(bool isInternal = false)
        {
            DiscordPromise<TResult> promise = DiscordPool.Internal.Get<DiscordPromise<TResult>>();
            promise.IsInternal = isInternal;
            return promise;
        }

        internal static IDiscordPromise<TResult> FromResult(TResult result)
        {
            DiscordPromise<TResult> promise = DiscordPool.Internal.Get<DiscordPromise<TResult>>();
            promise.Resolve(result);
            return promise;
        }
        
        internal static IDiscordPromise<TResult> FromException(Exception ex)
        {
            DiscordPromise<TResult> promise = DiscordPool.Internal.Get<DiscordPromise<TResult>>();
            promise.Fail(ex);
            return promise;
        }
        
        internal static IDiscordPromise WhenAll(params IDiscordPromise<TResult>[] promises) => WhenAll((IList<IDiscordPromise<TResult>>)promises);

        internal static IDiscordPromise<TResult[]> WhenAll(IList<IDiscordPromise<TResult>> promises)
        {
            int numSuccess = 0;
            bool failure = false;
            DiscordPromise<TResult[]> allPromise = DiscordPool.Internal.Get<DiscordPromise<TResult[]>>();
            int count = promises.Count;
            TResult[] results = new TResult[count];
            for (int index = 0; index < promises.Count; index++)
            {
                IDiscordPromise<TResult> promise = promises[index];
                promise.Done(result =>
                {
                    if (failure)
                    {
                        return;
                    }

                    results[index] = result;
                    
                    if (++numSuccess == count)
                    {
                        allPromise.Resolve(results);
                    }
                }, error =>
                {
                    if (!failure)
                    {
                        failure = true;
                        allPromise.Fail(error);
                    }
                });
            }

            return allPromise;
        }

        ///<inheritdoc/>
        public IDiscordPromise<TResult> Then(Action<TResult> onResolved)
        {
            PromiseException.ThrowIfDisposed(this);
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
            PromiseException.ThrowIfDisposed(this);
            IDiscordPromise<TConvert> promise = DiscordPromise<TConvert>.Create(IsInternal);
            if (State == PromiseState.Resolved)
            {
                promise.Resolve(onResolved(_data));
                return promise;
            }
            
            Done(result => promise.Resolve(onResolved(result)), ex => promise.Fail(ex));
            IsInternal = true;
            return promise;
        }

        ///<inheritdoc/>
        public IDiscordPromise<TConvert> Then<TConvert>(Func<TResult, IDiscordPromise<TConvert>> onResolved)
        {
            PromiseException.ThrowIfDisposed(this);
            IDiscordPromise<TConvert> promise = DiscordPromise<TConvert>.Create(IsInternal);
            if (State == PromiseState.Resolved)
            {
                return onResolved(_data);
            }

            Done(result => onResolved(result).Then(r => promise.Resolve(r)), ex => promise.Fail(ex));
            IsInternal = true;
            return promise;
        }

        ///<inheritdoc/>
        public IDiscordPromise<TResult> Done(Action<TResult> onResolved, Action<Exception> onFail)
        {
            PromiseException.ThrowIfDisposed(this);
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
            PromiseException.ThrowIfDisposed(this);
            _data = data;
            base.Resolve();
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