using System;

namespace Oxide.Ext.Discord.Rest.Buckets
{
    /// <summary>
    /// Represents an ID for a bucket
    /// </summary>
    public struct BucketId : IEquatable<BucketId>
    {
        /// <summary>
        /// ID of the bucket
        /// </summary>
        public readonly string Id;
        
        /// <summary>
        /// If the bucket ID is valid
        /// </summary>
        public bool IsValid => !string.IsNullOrEmpty(Id);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">ID of the bucket</param>
        /// <exception cref="ArgumentNullException">Thrown if ID is null or empty</exception>
        public BucketId(string id)
        {
            Id = !string.IsNullOrEmpty(id) ? id : throw new ArgumentNullException(nameof(id));
        }

        ///<inheritdoc/>
        public bool Equals(BucketId other) => Id == other.Id;

        ///<inheritdoc/>
        public override bool Equals(object obj) => obj is BucketId other && Equals(other);

        ///<inheritdoc/>
        public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;

        ///<inheritdoc/>
        public override string ToString() => Id;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator == (BucketId left, BucketId right) => left.Equals(right);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator != (BucketId left, BucketId right) => !(left == right);
    }
}