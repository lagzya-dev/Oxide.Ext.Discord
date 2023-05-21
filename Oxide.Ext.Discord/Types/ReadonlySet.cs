using System.Collections;
using System.Collections.Generic;
using Oxide.Ext.Discord.Interfaces.Types;

namespace Oxide.Ext.Discord.Types
{
    public class ReadonlySet<T> : IReadonlySet<T>
    {
        public bool IsReadOnly => true;

        public int Count => _set.Count;
        
        private readonly ISet<T> _set;

        public ReadonlySet(ISet<T> set)
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

        public bool Contains(T item) => _set.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _set.CopyTo(array, arrayIndex);
    }
}