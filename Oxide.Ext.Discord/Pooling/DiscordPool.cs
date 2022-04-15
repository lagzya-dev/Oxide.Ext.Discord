using System;
using System.Collections.Generic;
using System.Text;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Built in pooling for discord entities
    /// </summary>
    public static class DiscordPool
    {
        private static readonly Hash<Type, IPool> Pools = new Hash<Type, IPool>();

        /// <summary>
        /// Returns a pooled object of type T
        /// Must inherit from BasePoolable and have an empty default constructor
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <returns>Pooled object of type T</returns>
        public static T Get<T>() where T : BasePoolable, new()
        {
            IPool<T> pool = GetObjectPool<T>();
            return pool.Get();
        }

        /// <summary>
        /// Returns a BasePoolable back into the pool
        /// </summary>
        /// <param name="value">Object to free</param>
        /// <typeparam name="T">Type of object being freed</typeparam>
        public static void Free<T>(ref T value) where T : BasePoolable, new()
        {
            IPool<T> pool = GetObjectPool<T>();
            pool.Free(ref value);
        }
        
        /// <summary>
        /// Returns a BasePoolable back into the pool
        /// </summary>
        /// <param name="value">Object to free</param>
        /// <typeparam name="T">Type of object being freed</typeparam>
        internal static void Free<T>(T value) where T : BasePoolable, new()
        {
            IPool<T> pool = GetObjectPool<T>();
            pool.Free(ref value);
        }
        
        /// <summary>
        /// Returns a pooled list of type T
        /// </summary>
        /// <typeparam name="T">Type for the list</typeparam>
        /// <returns>Pooled List</returns>
        public static List<T> GetList<T>()
        {
            ListPool<T> pool = GetListPool<T>();
            return pool.Get();
        }
        
        /// <summary>
        /// Returns a pooled hash of type TKey, TValue
        /// </summary>
        /// <typeparam name="TKey">Type for the key</typeparam>
        /// <typeparam name="TValue">Type for the value</typeparam>
        /// <returns>Pooled Hash</returns>
        public static Hash<TKey, TValue> GetHash<TKey, TValue>()
        {
            HashPool<TKey, TValue> pool = GetHashPool<TKey, TValue>();
            return pool.Get();
        }

        /// <summary>
        /// Returns a pooled StringBuilder
        /// </summary>
        /// <returns>Pooled StringBuilder</returns>
        public static StringBuilder GetStringBuilder()
        {
            StringBuilderPool pool = GetStringBuilderPool();
            return pool.Get();
        }

        /// <summary>
        /// Free's a pooled list
        /// </summary>
        /// <param name="list">List to be freed</param>
        /// <typeparam name="T">Type of the list</typeparam>
        public static void FreeList<T>(ref List<T> list)
        {
            ListPool<T> pool = GetListPool<T>();
            pool.Free(ref list);
        }

        /// <summary>
        /// Frees a pooled hash
        /// </summary>
        /// <param name="hash">Hash to be freed</param>
        /// <typeparam name="TKey">Type for key</typeparam>
        /// <typeparam name="TValue">Type for value</typeparam>
        public static void FreeHash<TKey, TValue>(ref Hash<TKey, TValue> hash)
        {
            HashPool<TKey, TValue> pool = GetHashPool<TKey, TValue>();
            pool.Free(ref hash);
        }

        /// <summary>
        /// Frees a StringBuilder back to the pool
        /// </summary>
        /// <param name="sb">StringBuilder being freed</param>
        public static void FreeStringBuilder(ref StringBuilder sb)
        {
            StringBuilderPool pool = GetStringBuilderPool();
            pool.Free(ref sb);
        }
        
        /// <summary>
        /// Frees a StringBuilder back to the pool
        /// </summary>
        /// <param name="sb">StringBuilder being freed</param>
        public static string GetAndFreeStringBuilder(ref StringBuilder sb)
        {
            string result = sb?.ToString();
            StringBuilderPool pool = GetStringBuilderPool();
            pool.Free(ref sb);
            return result;
        }

        private static IPool<T> GetObjectPool<T>() where T : BasePoolable, new()
        {
            Type type = typeof(T);
            IPool<T> pool = (IPool<T>)Pools[type];
            if (pool == null)
            {
                pool = new ObjectPool<T>();
                Pools[type] = pool;
            }

            return pool;
        }
        
        private static ListPool<T> GetListPool<T>()
        {
            Type type = typeof(List<T>);
            ListPool<T> pool = (ListPool<T>)Pools[type];
            if (pool == null)
            {
                pool = new ListPool<T>();
                Pools[type] = pool;
            }

            return pool;
        }

        private static HashPool<TKey, TValue> GetHashPool<TKey, TValue>()
        {
            Type type = typeof(Hash<TKey, TValue>);
            HashPool<TKey, TValue> pool = (HashPool<TKey, TValue>)Pools[type];
            if (pool == null)
            {
                pool = new HashPool<TKey, TValue>();
                Pools[type] = pool;
            }

            return pool;
        }

        private static StringBuilderPool GetStringBuilderPool()
        {
            Type type = typeof(StringBuilder);
            StringBuilderPool pool = (StringBuilderPool)Pools[type];
            if (pool == null)
            {
                pool = new StringBuilderPool();
                Pools[type] = pool;
            }

            return pool;
        }
    }
}