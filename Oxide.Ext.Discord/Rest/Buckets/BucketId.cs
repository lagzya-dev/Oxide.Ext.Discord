using System;

namespace Oxide.Ext.Discord.Rest.Buckets
{
    public struct BucketId : IEquatable<BucketId>
    {
        public readonly string Id;
        public bool IsValid => !string.IsNullOrEmpty(Id);

        public BucketId(string id)
        {
            Id = !string.IsNullOrEmpty(id) ? id : throw new ArgumentNullException(nameof(id));
        }

        public bool Equals(BucketId other) => Id == other.Id;

        public override bool Equals(object obj) => obj is BucketId other && Equals(other);

        public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;

        public override string ToString() => Id;
        
        public static bool operator == (BucketId left, BucketId right) => left.Equals(right);
        
        public static bool operator != (BucketId left, BucketId right) => !(left == right);
    }
}