using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Ext.Discord.Exceptions.Promise;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Promise
{
    /// <summary>
    /// Represents the base class for Discord Async Callbacks
    /// </summary>
    public class DiscordPromise : BasePoolable, IDiscordPromise
    {
        /// <summary>
        /// If the promise is for an internal extension request
        /// </summary>
        protected bool IsInternal;
        
        /// <summary>
        /// The exception for the promise
        /// </summary>
        protected Exception Exception;

        internal PromiseState State { get; private set; }
        
        private readonly Action _resolve;
        private readonly Action _fail;
        private readonly Action _dispose;

        private readonly List<Action> _resolves = new List<Action>(0);
        private readonly List<Action<Exception>> _fails = new List<Action<Exception>>(0);

        /// <summary>
        /// Constructor
        /// </summary>
        public DiscordPromise()
        {
            _resolve = InvokeResolveInternal;
            _fail = InvokeFailInternal;
            _dispose = Dispose;
        }

        internal static IDiscordPromise Create(bool isInternal = false)
        {
            DiscordPromise promise = DiscordPool.Internal.Get<DiscordPromise>();
            promise.IsInternal = isInternal;
            return promise;
        }

        /// <summary>
        /// Registers a callback once the async call is completed
        /// </summary>
        /// <param name="onResolved">Callback to register</param>
        /// <returns>this</returns>
        public IDiscordPromise Then(Action onResolved)
        {
            PromiseException.ThrowIfDisposed(this);
            if (State == PromiseState.Resolved)
            {
                onResolved();
                return this;
            }
            
            _resolves.Add(onResolved);
            return this;
        }

        ///<inheritdoc/>
        public IDiscordPromise Catch(Action<Exception> onFail)
        {
            PromiseException.ThrowIfDisposed(this);
            if (State == PromiseState.Failed)
            {
                onFail(Exception);
                return this;
            }

            _fails.Add(onFail);
            return this;
        }

        public IDiscordPromise Catch<TException>(Action<TException> onFail) where TException : Exception
        {
            Catch(ex =>
            {
                if (ex is TException exception)
                {
                    onFail(exception);
                }
            });
            return this;
        }

        ///<inheritdoc/>
        public IDiscordPromise Done(Action onResolved, Action<Exception> onFail)
        {
            PromiseException.ThrowIfDisposed(this);
            Then(onResolved);
            Catch(onFail);
            return this;
        }

        /// <summary>
        /// Handles calling the callbacks and disposing of the object
        /// </summary>
        protected virtual void InvokeResolveInternal()
        {
            for (int index = 0; index < _resolves.Count; index++)
            {
                Action callback = _resolves[index];
                callback.Invoke();
            }
            
            DelayedDispose();
        }

        private void InvokeFailInternal()
        {
            for (int index = 0; index < _fails.Count; index++)
            {
                Action<Exception> callback = _fails[index];
                callback.Invoke(Exception);
            }

            DelayedDispose();
        }

        ///<inheritdoc/>
        public void Resolve()
        {
            PromiseException.ThrowIfDisposed(this);
            SetState(PromiseState.Resolved);
            if (IsInternal)
            {
                InvokeResolveInternal();
                return;
            }
            
            Interface.Oxide.NextTick(_resolve);
        }

        ///<inheritdoc/>
        public void Fail(Exception ex)
        {
            PromiseException.ThrowIfDisposed(this);
            SetState(PromiseState.Failed);
            Exception = ex;
            if (IsInternal)
            {
                InvokeFailInternal();
                return;
            }
            
            Interface.Oxide.NextTick(_fail);
        }

        /// <summary>
        /// Sets the current state of the promise
        /// </summary>
        /// <param name="state"></param>
        protected void SetState(PromiseState state)
        {
            PromiseException.ThrowIfNotPending(this);
            State = state;
        }

        private void DelayedDispose()
        {
            Interface.Oxide.NextTick(_dispose);
        }

        ///<inheritdoc/>
        protected override void LeavePool()
        {
            State = PromiseState.Pending;
        }

        ///<inheritdoc/>
        protected override void EnterPool()
        {
            IsInternal = false;
            _fails.Clear();
            _resolves.Clear();
        }
    }
}