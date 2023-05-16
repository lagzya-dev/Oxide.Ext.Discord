using System.Collections.Generic;

namespace Oxide.Ext.Discord.Interfaces.Types
{
    /// <summary>
    /// Represents a ReadOnly interface for <see cref="ISet{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of the set</typeparam>
    public interface IReadonlySet<T> : IReadOnlyCollection<T>
    {
        ///<inheritdoc cref="HashSet{T}.IsSubsetOf"/>
        bool IsSubsetOf(IEnumerable<T> other);

        ///<inheritdoc cref="HashSet{T}.IsSupersetOf"/>
        bool IsSupersetOf(IEnumerable<T> other);

        ///<inheritdoc cref="HashSet{T}.IsProperSupersetOf"/>
        bool IsProperSupersetOf(IEnumerable<T> other);

        ///<inheritdoc cref="HashSet{T}.IsProperSubsetOf"/>
        bool IsProperSubsetOf(IEnumerable<T> other);

        ///<inheritdoc cref="HashSet{T}.Overlaps"/>
        bool Overlaps(IEnumerable<T> other);

        ///<inheritdoc cref="HashSet{T}.SetEquals"/>
        bool SetEquals(IEnumerable<T> other);

        ///<inheritdoc cref="HashSet{T}.Contains"/>
        bool Contains(T item);

        ///<inheritdoc cref="HashSet{T}.CopyTo(T[],int)"/>
        void CopyTo(T[] array, int arrayIndex);
    }
}