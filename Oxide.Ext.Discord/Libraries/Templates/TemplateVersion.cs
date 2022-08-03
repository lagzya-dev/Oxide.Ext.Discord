using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Json.Converters;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    /// <summary>
    /// Version of a specific template
    /// </summary>
    [JsonConverter(typeof(TemplateVersionConverter))]
    public struct TemplateVersion : IEquatable<TemplateVersion>, IComparable<TemplateVersion>
    {
        /// <summary>
        /// Major Version
        /// </summary>
        public readonly ushort Major;
        
        /// <summary>
        /// Minor Version
        /// </summary>
        public readonly ushort Minor;
        
        /// <summary>
        /// Revision Version
        /// </summary>
        public readonly ushort Revision;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="major">Major Version</param>
        /// <param name="minor">Minor Version</param>
        /// <param name="revision">Revision Version</param>
        public TemplateVersion(ushort major, ushort minor, ushort revision)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
        }

        /// <summary>
        /// Parses a template version from a string
        /// </summary>
        /// <param name="version">String of a template version</param>
        public TemplateVersion(string version)
        {
            int index = version.IndexOf(".", StringComparison.Ordinal);
            Major = ushort.Parse(version.Substring(0, index));
            int previousIndex = index;
            index = version.IndexOf(".", index + 1, StringComparison.Ordinal);
            Minor = ushort.Parse(version.Substring(previousIndex, index - previousIndex));
            Revision = ushort.Parse(version.Substring(index, version.Length - 1 - index));
        }

        ///<inheritdoc/>
        public bool Equals(TemplateVersion other)
        {
            return Major == other.Major && Minor == other.Minor && Revision == other.Revision;
        }
        
        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is TemplateVersion other && Equals(other);
        }
        
        ///<inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Major.GetHashCode();
                hashCode = (hashCode * 397) ^ Minor.GetHashCode();
                hashCode = (hashCode * 397) ^ Revision.GetHashCode();
                return hashCode;
            }
        }

        ///<inheritdoc/>
        public int CompareTo(TemplateVersion other)
        {
            int majorComparison = Major.CompareTo(other.Major);
            if (majorComparison != 0)
                return majorComparison;
            int minorComparison = Minor.CompareTo(other.Minor);
            if (minorComparison != 0)
                return minorComparison;
            return Revision.CompareTo(other.Revision);
        }

        /// <summary>
        /// Returns if the template versions are equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator == (TemplateVersion left, TemplateVersion right) => left.Equals(right);
        
        /// <summary>
        /// Returns if the template versions are not equal
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator != (TemplateVersion left, TemplateVersion right) => !(left == right);
        
        /// <summary>
        /// Returns if the left template version is less than the right
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(TemplateVersion left, TemplateVersion right) => left.CompareTo(right) < 0;
        
        /// <summary>
        /// Returns if the left template version is less than or equal the right
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(TemplateVersion left, TemplateVersion right) => left.CompareTo(right) <= 0;
        
        /// <summary>
        /// Returns if the right template version is greater than the left
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(TemplateVersion left, TemplateVersion right) => left.CompareTo(right) > 0;
        
        /// <summary>
        /// Returns if the right template version is greater or equal than the left
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(TemplateVersion left, TemplateVersion right) => left.CompareTo(right) >= 0;

        ///<inheritdoc/>
        public override string ToString()
        {
            return $"{Major}.{Minor}.{Revision}";
        }
    }
}