using System.Collections.Generic;

namespace Oxide.Ext.Discord.Interfaces.Types
{
    public interface IReadonlySet<T> : IReadOnlyCollection<T>
    {
        bool IsSubsetOf(IEnumerable<T> other);

        bool IsSupersetOf(IEnumerable<T> other);

        bool IsProperSupersetOf(IEnumerable<T> other);

        bool IsProperSubsetOf(IEnumerable<T> other);

        bool Overlaps(IEnumerable<T> other);

        bool SetEquals(IEnumerable<T> other);

        bool Contains(T item);

        void CopyTo(T[] array, int arrayIndex);
    }
}