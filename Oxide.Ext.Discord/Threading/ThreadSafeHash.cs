using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Threading
{
    /// <summary>
    /// Represents a ThreadSafe Hash
    /// </summary>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    public class ThreadSafeHash<TKey, TValue> : IDictionary<TKey, TValue>, IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private readonly Hash<TKey, TValue> _hash = new Hash<TKey, TValue>();
        
        ///<inheritdoc/>
        public bool IsReadOnly => false;

        ///<inheritdoc/>
        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();
                foreach (KeyValuePair<TKey,TValue> pair in _hash)
                {
                    keys.Add(pair.Key);
                }

                return keys;
            }
        }

        ///<inheritdoc/>
        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> keys = new List<TValue>();
                foreach (KeyValuePair<TKey, TValue> pair in _hash)
                {
                    keys.Add(pair.Value);
                }

                return keys;
            }
        }
        
        ///<inheritdoc/>
        public int Count
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _hash.Count;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }
        
        ///<inheritdoc/>
        public TValue this[TKey key]
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _hash[key];
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
                        _hash[key] = value;
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
        public bool TryGetValue(TKey key, out TValue value)
        {
            _lock.EnterReadLock();
            try
            {
                return _hash.TryGetValue(key, out value);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        
        ///<inheritdoc/>
        public bool ContainsKey(TKey key)
        {
            _lock.EnterReadLock();
            try
            {
                return _hash.ContainsKey(key);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        
        ///<inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            _lock.EnterReadLock();
            try
            {
                return _hash.Contains(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        
        ///<inheritdoc/>
        public void Add(TKey key, TValue value)
        {
            _lock.EnterWriteLock();
            try
            {
                _hash.Add(key, value);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        
        ///<inheritdoc/>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _lock.EnterWriteLock();
            try
            {
                _hash.Add(item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        
        ///<inheritdoc/>
        public bool Remove(TKey key)
        {
            _lock.EnterWriteLock();
            try
            {
                return _hash.Remove(key);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
        
        ///<inheritdoc/>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            _lock.EnterWriteLock();
            try
            {
                return _hash.Remove(item);
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
                _hash.Clear();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        ///<inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            _lock.EnterReadLock();
            List<TKey> list = DiscordPool.GetList<TKey>();
            
            try
            {
                list.AddRange(_hash.Keys);

                for (int index = 0; index < list.Count; index++)
                {
                    TKey key = list[index];
                    if (_hash.ContainsKey(key))
                    {
                        yield return new KeyValuePair<TKey, TValue>(key, _hash[key]);
                    }
                }
            }
            finally
            {           
                DiscordPool.FreeList(ref list);
                _lock.ExitReadLock();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        ///<inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            try
            {
                _hash.CopyTo(array, arrayIndex);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        ///<inheritdoc/>
        public void Dispose()
        {
            _lock.Dispose();
        }
    }
}