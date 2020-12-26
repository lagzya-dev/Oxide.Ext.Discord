using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Helpers.Converters;

namespace Oxide.Ext.Discord.Entities
{
    [JsonConverter(typeof(SnowflakeConverter))]
    public readonly struct Snowflake : IComparable<Snowflake>, IEquatable<Snowflake>, IComparable<ulong>, IEquatable<ulong>
    {
        public readonly ulong Id;

        public Snowflake(ulong id)
        {
            Id = id;
        }

        public Snowflake(DateTimeOffset offset)
        {
            Id = (ulong)(Helpers.Time.DiscordEpoch - offset).TotalMilliseconds << 22;
        }   

        public DateTimeOffset GetCreationTime()
        {
            return Helpers.Time.DiscordEpoch + TimeSpan.FromSeconds(Id >> 22);
        }

        public static bool TryParse(string value, out Snowflake snowflake)
        {
            if(ulong.TryParse(value, out ulong id))
            {
                snowflake = new Snowflake(id);
                return true;
            }

            snowflake = default;
            return false;
        }

        public bool Equals(Snowflake other)
        {
            return Id == other.Id;
        }
        
        public override bool Equals(object obj)
        {
            return obj is Snowflake other && Equals(other);
        }
        
        public bool Equals(ulong other)
        {
            return Id == other;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public int CompareTo(Snowflake num)
        {
            return Id.CompareTo(num.Id);
        }
        
        public int CompareTo(ulong other)
        {
            return Id.CompareTo(other);
        }

        public static bool operator == (Snowflake left, Snowflake right)
        {
            return left.Id == right.Id;
        }

        public static bool operator !=(Snowflake left, Snowflake right)
        {
            return !(left == right);
        }
        
        public static bool operator <(Snowflake left, Snowflake right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Snowflake left, Snowflake right)
        {
            return left.CompareTo(right) > 0;
        }
        
        public static bool operator <=(Snowflake left, Snowflake right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Snowflake left, Snowflake right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static implicit operator ulong(Snowflake snowflake) => snowflake.Id;
        public static explicit operator Snowflake(ulong id) => new Snowflake(id);
    }
}