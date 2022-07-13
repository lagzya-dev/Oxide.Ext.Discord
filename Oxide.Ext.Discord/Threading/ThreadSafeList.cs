using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Oxide.Ext.Discord.Threading
{
    /// <summary>
    /// Thread Safe List
    /// </summary>
    /// <typeparam name="T">Type of T</typeparam>
    public class ThreadSafeList<T> : IList<T>, IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private readonly List<T> _list;

        ///<inheritdoc/>
        public bool IsReadOnly => false;
        
        ///<inheritdoc/>
        public int Count
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _list.Count;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ThreadSafeList()
        {
            _list = new List<T>();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capacity">Starting capacity of the list</param>
        public ThreadSafeList(int capacity)
        {
            _list = new List<T>(capacity);
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">Initial items in the list</param>
        public ThreadSafeList(IEnumerable<T> collection)
        {
            _list = new List<T>(collection);
        }
        
        ///<inheritdoc/>
        public bool Contains(T item)
        {
            _lock.EnterReadLock();
            try
            {
                return _list.Contains(item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        
        ///<inheritdoc/>
        public int IndexOf(T item)
        {
            _lock.EnterReadLock();
            try
            {
                return _list.IndexOf(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        
        ///<inheritdoc/>
        public T this[int index]
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _list[index];
                }
                finally
                {
                    _lock.ExitReadLock();               
                }           
            }
            set
            {
                _lock.EnterUpgradeableReadLock();
                try
                {
                    
                    _lock.EnterWriteLock();
                    try
                    {                       
                        _list[index] = value;
                    }
                    finally
                    {
                        _lock.ExitWriteLock();              
                    }
                }
                finally
                {
                    _lock.ExitUpgradeableReadLock();
                }
            }
        }
        
        ///<inheritdoc/>
        public void Add(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                _list.Add(item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        
        /// <summary>
        /// Adds items into the list
        /// </summary>
        /// <param name="items">Items to be added</param>
        /// <exception cref="ArgumentNullException">Thrown if items is null</exception>
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            
            _lock.EnterWriteLock();
            try
            {
                _list.AddRange(items);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        
        ///<inheritdoc/>
        public void Insert(int index, T item)
        {
            _lock.EnterWriteLock();
            try
            {
                _list.Insert(index, item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        ///<inheritdoc/>
        public bool Remove(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                return _list.Remove(item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        
        ///<inheritdoc/>
        public void RemoveAt(int index)
        {
            _lock.EnterWriteLock();
            try
            {
                _list.RemoveAt(index);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        
        ///<inheritdoc/>
        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                _list.Clear();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        ///<inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            try
            {
                _list.CopyTo(array, arrayIndex);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        ///<inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            _lock.EnterReadLock();

            try
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    yield return _list[i];
                }
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        ///<inheritdoc/>
        public void Dispose()
        {
            _lock.Dispose();
        }
    }
}