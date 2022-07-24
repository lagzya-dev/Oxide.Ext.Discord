using System;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Pooling
{
    /// <summary>
    /// Represents a <see cref="DiscordArrayPool{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of the array pool</typeparam>
    public class DiscordArrayPool<T>
    {
        /// <summary>
        /// <see cref="DiscordArrayPool{T}"/> Instance
        /// </summary>
        public static readonly DiscordArrayPool<T> Shared;
        
        private const int DefaultMaxArrayLength = 1024 * 16;
        private const int DefaultMaxNumberOfArraysPerBucket = 50;
        
        private readonly Bucket[] _buckets;

        static DiscordArrayPool()
        {
            Shared = new DiscordArrayPool<T>();
        }
        
        private DiscordArrayPool() : this(DefaultMaxArrayLength, DefaultMaxNumberOfArraysPerBucket) { }

        private DiscordArrayPool(int maxArrayLength, int maxArraysPerBucket)
        {
            if (maxArrayLength <= 0) throw new ArgumentOutOfRangeException(nameof(maxArrayLength));
            if (maxArraysPerBucket <= 0) throw new ArgumentOutOfRangeException(nameof(maxArraysPerBucket));

            maxArrayLength = maxArrayLength.Clamp(16, DefaultMaxArrayLength);

            _buckets = new Bucket[SelectBucketIndex(maxArrayLength) + 1];
            for (int i = 0; i < _buckets.Length; i++)
            {
                _buckets[i] = new Bucket(GetMaxSizeForBucket(i), maxArraysPerBucket);
            }
        }

        /// <summary>
        /// Rents an array from the pool
        /// </summary>
        /// <param name="minimumLength"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T[] Rent(int minimumLength)
        {
            if (minimumLength < 0)  throw new ArgumentOutOfRangeException(nameof(minimumLength));

            if (minimumLength == 0)
            {
                return Array.Empty<T>();
            }
            
            T[] array;
            int bucketIndex = SelectBucketIndex(minimumLength);
            int index = bucketIndex;
            do
            {
                array = _buckets[index].Rent();
                if (array != null)
                {
                    return array;
                }

                index++;
            }
            while (index < _buckets.Length && index != bucketIndex + 2);
            array = new T[_buckets[bucketIndex].BufferLength];
            return array;
        }

        /// <summary>
        /// Returns an array to the pool
        /// </summary>
        /// <param name="array"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Return(T[] array)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (array.Length == 0)
            {
                return;
            }
            
            int num = SelectBucketIndex(array.Length);
            if (num < _buckets.Length)
            {
                _buckets[num].Return(array);
            }
        }
        
        private static int GetMaxSizeForBucket(int binIndex)
        {
            return 16 << (binIndex & 31);
        }
        
        private static int SelectBucketIndex(int bufferSize)
        {
            uint num = (uint)(bufferSize - 1 >> 4);
            int num1 = 0;
            if (num > 65535)
            {
                num >>= 16;
                num1 = 16;
            }
            if (num > 255)
            {
                num >>= 8;
                num1 += 8;
            }
            if (num > 15)
            {
                num >>= 4;
                num1 += 4;
            }
            if (num > 3)
            {
                num >>= 2;
                num1 += 2;
            }
            if (num > 1)
            {
                num >>= 1;
                num1++;
            }
            return (int)(num1 + num);
        }

        private sealed class Bucket
        {
            internal readonly int BufferLength;

            private readonly T[][] _buffers;

            private int _index;

            internal Bucket(int bufferLength, int numberOfBuffers)
            {
                _buffers = new T[numberOfBuffers][];
                BufferLength = bufferLength;
            }

            internal T[] Rent()
            {
                if (_index < _buffers.Length)
                {
                    T[] array = _buffers[_index];
                    _buffers[_index] = null;
                    _index++;
                    if (array != null)
                    {
                        return array;
                    }
                }
                return new T[BufferLength];
            }

            internal void Return(T[] array)
            {
                if (array.Length != BufferLength)  throw new ArgumentException("Buffer not from pool", nameof(array));
                if (_index != 0)
                {
                    _index--;
                    _buffers[_index] = array;
                }
            }
        }
    }
}