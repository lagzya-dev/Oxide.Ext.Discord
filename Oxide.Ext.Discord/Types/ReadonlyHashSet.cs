using System;
using System.Collections;
using System.Collections.Generic;
namespace Oxide.Ext.Discord.Types
{
    public class ReadonlyHashSet<T> : ISet<T>, IReadOnlyCollection<T>
    {
        private readonly HashSet<T> _set;

        private const string ReadonlyError = "Readonly HashSet cannot be modifed";
        
        public bool IsReadOnly => true;

        public int Count => _set.Count;

        public ReadonlyHashSet(HashSet<T> set)
        {
            _set = set;
        }

        public IEnumerator<T> GetEnumerator() => _set.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public bool IsSubsetOf(IEnumerable<T> other) => _set.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) => _set.IsSupersetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => _set.IsProperSupersetOf(other);

        public bool IsProperSubsetOf(IEnumerable<T> other) => _set.IsProperSubsetOf(other);

        public bool Overlaps(IEnumerable<T> other) => _set.Overlaps(other);

        public bool SetEquals(IEnumerable<T> other) => _set.SetEquals(other);

        public void Clear() => throw new NotSupportedException(ReadonlyError);

        public bool Contains(T item) => _set.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _set.CopyTo(array, arrayIndex);

        public bool Add(T item) => throw new NotSupportedException(ReadonlyError);

        void ICollection<T>.Add(T item) => throw new NotSupportedException(ReadonlyError);

        public void UnionWith(IEnumerable<T> other) => throw new NotSupportedException(ReadonlyError);

        public void IntersectWith(IEnumerable<T> other) => throw new NotSupportedException(ReadonlyError);

        public void ExceptWith(IEnumerable<T> other) => throw new NotSupportedException(ReadonlyError);

        public void SymmetricExceptWith(IEnumerable<T> other) => throw new NotSupportedException(ReadonlyError);
        
        public bool Remove(T item) => throw new NotSupportedException(ReadonlyError);
    }
}