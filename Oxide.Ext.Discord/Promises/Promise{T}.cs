// Originally from: https://github.com/Real-Serious-Games/C-Sharp-Promise
// Modified by: MJSU

using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Ext.Discord.Exceptions.Promise;
using Oxide.Ext.Discord.Interfaces.Promises;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Threading;

namespace Oxide.Ext.Discord.Promises
{
    /// <summary>
    /// Implements a C# promise.
    /// https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise
    /// </summary>
    public sealed class Promise<TPromised> : BasePromise, IPendingPromise<TPromised>
    {
        /// <summary>
        /// The value when the promises is resolved.
        /// </summary>
        private TPromised _resolveValue;

        /// <summary>
        /// Completed handlers that accept a value.
        /// </summary>
        private readonly List<ResolveHandler<TPromised>> _resolves = new List<ResolveHandler<TPromised>>();

        private readonly Action<TPromised> _onResolve;
        private readonly Action _onResolveInternal;

        /// <summary>
        /// Constructor
        /// </summary>
        public Promise()
        {
            _onResolve = Resolve;
            _onResolveInternal = InvokeResolveHandlersInternal;
        }

        /// <summary>
        /// Returns a promise that is currently pending
        /// </summary>
        /// <returns></returns>
        public static IPendingPromise<TPromised> Create() => Create(false);

        internal static Promise<TPromised> Create(bool isInternal) => Create<TPromised>(isInternal);

        internal static Promise<TConvert> Create<TConvert>(bool isInternal)
        {
            Promise<TConvert> promise = DiscordPool.Internal.Get<Promise<TConvert>>();
            promise.IsInternal = isInternal;
            return promise;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        public static IPromise<TPromised> Create(Action<Action<TPromised>, Action<Exception>> resolver)
        {
            Promise<TPromised> promise = Create(false);
            try
            {
                resolver(promise._onResolve, promise.OnReject);
            }
            catch (Exception ex)
            {
                promise.Reject(ex);
            }

            return promise;
        }

        /// <summary>
        /// Convert a simple value directly into a resolved promise.
        /// </summary>
        public static IPromise<TPromised> Resolved(TPromised promisedValue)
        {
            Promise<TPromised> promise = Create(false);
            promise.State = PromiseState.Resolved;
            promise._resolveValue = promisedValue;
            return promise;
        }

        /// <summary>
        /// Convert an exception directly into a rejected promise.
        /// </summary>
        public static IPromise<TPromised> Rejected(Exception ex)
        {
            Promise<TPromised> promise = Create(false);
            promise.State = PromiseState.Rejected;
            promise.Exception = ex;
            return promise;
        }

        ///<inheritdoc/>
        protected override void ClearHandlers()
        {
            _resolves.Clear();
            base.ClearHandlers();
        }

        /// <summary>
        /// Invoke all resolve handlers.
        /// </summary>
        private void InvokeResolveHandlers(TPromised value)
        {
            _resolveValue = value;
            if (ThreadState.IsMain || IsInternal)
            {
                InvokeResolveHandlersInternal();
                return;
            }
            
            Interface.Oxide.NextTick(_onResolveInternal);
        }

        private void InvokeResolveHandlersInternal()
        {
            if (_resolves != null)
            {
                for (int i = 0, maxI = _resolves.Count; i < maxI; i++)
                {
                    _resolves[i].Resolve(_resolveValue);
                }
            }

            ClearHandlers();
        }

        ///<inheritdoc/>
        public void Resolve(TPromised value)
        {
            PromiseException.ThrowIfDisposed(this);
            PromiseException.ThrowIfNotPending(State);

            _resolveValue = value;
            State = PromiseState.Resolved;

            InvokeResolveHandlers(value);
        }

        ///<inheritdoc/>
        public IPromise<TPromised> WithName(string name)
        {
            Name = name;
            return this;
        }

        ///<inheritdoc/>
        public IPromise Catch(Action<Exception> onRejected)
        {
            if (State == PromiseState.Resolved)
            {
                return Promise.Resolved();
            }

            Promise resultPromise = Promise.Create(IsInternal);
            resultPromise.WithName(Name);

            void ResolveHandler(TPromised _) => resultPromise.Resolve();
            void RejectHandler(Exception ex)
            {
                try
                {
                    onRejected(ex);
                    resultPromise.Resolve();
                }
                catch (Exception cbEx)
                {
                    resultPromise.Reject(cbEx);
                }
            }

            AddHandlers(new ResolveHandler<TPromised>(ResolveHandler, resultPromise), new RejectHandler(RejectHandler, resultPromise));

            return resultPromise;
        }

        ///<inheritdoc/>
        public IPromise Catch<TException>(Action<TException> onRejected) where TException : Exception
        {
            return Catch(ex =>
            {
                if (ex is TException exception)
                {
                    onRejected.Invoke(exception);
                }
            });
        }

        ///<inheritdoc/>
        public IPromise<TPromised> Catch(Func<Exception, TPromised> onRejected)
        {
            if (State == PromiseState.Resolved)
            {
                return this;
            }

            Promise<TPromised> resultPromise = Create(IsInternal);
            resultPromise.WithName(Name);
            
            void RejectHandler(Exception ex)
            {
                try
                {
                    resultPromise.Resolve(onRejected(ex));
                }
                catch (Exception cbEx)
                {
                    resultPromise.Reject(cbEx);
                }
            }

            AddHandlers(new ResolveHandler<TPromised>(resultPromise._onResolve, resultPromise), new RejectHandler(RejectHandler, resultPromise));

            return resultPromise;
        }

        ///<inheritdoc/>
        public IPromise<TConvert> Then<TConvert>(Func<TPromised, IPromise<TConvert>> onResolved) => Then(onResolved, null);

        ///<inheritdoc/>
        public IPromise<TPromised> Then(Action<TPromised> onResolved) => Then(onResolved, null);

        ///<inheritdoc/>
        public IPromise<TConvert> Then<TConvert>(Func<TPromised, IPromise<TConvert>> onResolved, Func<Exception, IPromise<TConvert>> onRejected)
        {
            if (State == PromiseState.Resolved)
            {
                try
                {
                    return onResolved(_resolveValue);
                }
                catch (Exception ex)
                {
                    return Promise<TConvert>.Rejected(ex);
                }
            }

            // This version of the function must supply an onResolved.
            // Otherwise there is no way to get the converted value to pass to the resulting promise.
            Promise<TConvert> resultPromise = Create<TConvert>(IsInternal);
            resultPromise.WithName(Name);

            void ResolveHandler(TPromised promised)
            {
                onResolved(promised).Then(resultPromise);
            }

            void RejectHandler(Exception ex)
            {
                if (onRejected == null)
                {
                    resultPromise.Reject(ex);
                    return;
                }

                try
                {
                    onRejected(ex).Then(resultPromise);
                }
                catch (Exception callbackEx)
                {
                    resultPromise.Reject(callbackEx);
                }
            }

            AddHandlers(new ResolveHandler<TPromised>(ResolveHandler, resultPromise), new RejectHandler(RejectHandler, resultPromise));

            return resultPromise;
        }

        ///<inheritdoc/>
        public IPromise Then(Func<TPromised, IPromise> onResolved, Action<Exception> onRejected)
        {
            if (State == PromiseState.Resolved)
            {
                try
                {
                    return onResolved(_resolveValue);
                }
                catch (Exception ex)
                {
                    return Promise.Rejected(ex);
                }
            }

            Promise resultPromise = Promise.Create(IsInternal);
            resultPromise.WithName(Name);

            void ResolveHandler(TPromised promised)
            {
                if (onResolved != null)
                {
                    onResolved(promised).Then(resultPromise);
                }
                else
                {
                    resultPromise.Resolve();
                }
            }

            Action<Exception> rejectHandler;
            if (onRejected != null)
            {
                void RejectHandler(Exception ex)
                {
                    onRejected(ex);
                    resultPromise.Reject(ex);
                }

                rejectHandler = RejectHandler;
            }
            else
            {
                rejectHandler = resultPromise.OnReject;
            }

            AddHandlers(new ResolveHandler<TPromised>(ResolveHandler, resultPromise), new RejectHandler(rejectHandler, resultPromise));

            return resultPromise;
        }

        ///<inheritdoc/>
        public IPromise<TPromised> Then(IPromise<TPromised> promise)
        {
            Promise<TPromised> prom = (Promise<TPromised>)promise;
            return Then(prom._onResolve, prom.OnReject);
        }

        ///<inheritdoc/>
        public IPromise<TPromised> Then(Action<TPromised> onResolved, Action<Exception> onRejected)
        {
            if (State == PromiseState.Resolved)
            {
                try
                {
                    onResolved(_resolveValue);
                    return Resolved(_resolveValue);
                }
                catch (Exception ex)
                {
                    return Rejected(ex);
                }
            }

            Promise<TPromised> resultPromise = Create(IsInternal);
            resultPromise.WithName(Name);

            void ResolveHandler(TPromised value)
            {
                onResolved?.Invoke(value);
                resultPromise.Resolve(value);
            }

            Action<Exception> rejectHandler;
            if (onRejected != null)
            {
                void RejectHandler(Exception ex)
                {
                    onRejected(ex);
                    resultPromise.Reject(ex);
                }

                rejectHandler = RejectHandler;
            }
            else
            {
                rejectHandler = resultPromise.OnReject;
            }

            AddHandlers(new ResolveHandler<TPromised>(ResolveHandler, resultPromise), new RejectHandler(rejectHandler, resultPromise));

            return resultPromise;
        }

        ///<inheritdoc/>
        public IPromise<TConvert> Then<TConvert>(Func<TPromised, TConvert> transform) => Then(value => Promise<TConvert>.Resolved(transform(value)));

        /// <summary>
        /// Helper function to invoke or register resolve/reject handlers.
        /// </summary>
        private void AddHandlers(ResolveHandler<TPromised> resolve, RejectHandler reject)
        {
            if (State == PromiseState.Resolved)
            {
                resolve.Resolve(_resolveValue);
            }
            else if (State == PromiseState.Rejected)
            {
                reject.Reject(Exception);
            }
            else
            {
                _resolves.Add(resolve);
                Rejects.Add(reject);
            }
        }

        ///<inheritdoc/>
        public IPromise<IEnumerable<TConvert>> ThenAll<TConvert>(Func<TPromised, IEnumerable<IPromise<TConvert>>> chain) => Then(value => Promise<TConvert>.All(chain(value)));

        ///<inheritdoc/>
        public IPromise ThenAll(Func<TPromised, IEnumerable<IPromise>> chain) => Then(value => Promise.All(chain(value)), null);

        /// <summary>
        /// Returns a promise that resolves when all of the promises in the enumerable argument have resolved.
        /// Returns a promise of a collection of the resolved results.
        /// </summary>
        public static IPromise<IEnumerable<TPromised>> All(params IPromise<TPromised>[] promises) => All((IEnumerable<IPromise<TPromised>>)promises); // Cast is required to force use of the other All function.

        /// <summary>
        /// Returns a promise that resolves when all of the promises in the enumerable argument have resolved.
        /// Returns a promise of a collection of the resolved results.
        /// </summary>
        public static IPromise<IEnumerable<TPromised>> All(IEnumerable<IPromise<TPromised>> promisesEnumerable)
        {
            List<IPromise<TPromised>> promises = new List<IPromise<TPromised>>();
            promises.AddRange(promisesEnumerable);
            
            if (promises.Count == 0)
            {
                return Promise<IEnumerable<TPromised>>.Resolved(Enumerable.Empty<TPromised>());
            }

            int remainingCount = promises.Count;
            TPromised[] results = new TPromised[remainingCount];
            Promise<IEnumerable<TPromised>> resultPromise = Create<IEnumerable<TPromised>>(false);
            resultPromise.WithName("All");

            for (int index = 0; index < promises.Count; index++)
            {
                int resultIndex = index;
                IPromise<TPromised> promise = promises[index];
                promise
                    .Then(result =>
                    {
                        results[resultIndex] = result;

                        --remainingCount;
                        if (remainingCount <= 0 && resultPromise.State == PromiseState.Pending)
                        {
                            // This will never happen if any of the promises errored.
                            resultPromise.Resolve(results);
                        }
                    })
                    .Catch(ex =>
                    {
                        if (resultPromise.State == PromiseState.Pending)
                        {
                            // If a promise errored and the result promise is still pending, reject it.
                            resultPromise.Reject(ex);
                        }
                    });
            }

            return resultPromise;
        }

        ///<inheritdoc/>
        public IPromise<TPromised> Finally(Action onComplete)
        {
            if (State == PromiseState.Resolved)
            {
                try
                {
                    onComplete();
                    return this;
                }
                catch (Exception ex)
                {
                    return Rejected(ex);
                }
            }

            Promise<TPromised> promise = Create(IsInternal);
            promise.WithName(Name);

            Then(promise._onResolve);
            Catch(e => 
            {
                try 
                {
                    onComplete();
                    promise.Reject(e);
                } 
                catch (Exception ex) 
                {
                    promise.Reject(ex);
                }
            });

            return promise.Then(promised =>
            {
                onComplete();
                return promised;
            });
        }

        ///<inheritdoc/>
        public IPromise ContinueWith(Func<IPromise> onComplete)
        {
            Promise promise = Promise.Create(IsInternal);
            promise.WithName(Name);

            Then(x => promise.Resolve());
            Catch(e => promise.Resolve());

            return promise.Then(onComplete);
        }

        ///<inheritdoc/>
        public IPromise<TConvert> ContinueWith<TConvert>(Func<IPromise<TConvert>> onComplete)
        {
            Promise promise = Promise.Create(IsInternal);
            promise.WithName(Name);

            Then(x => promise.Resolve());
            Catch(e => promise.Resolve());

            return promise.Then(onComplete);
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            _resolveValue = default(TPromised);
            _resolves.Clear();
        }
    }
}