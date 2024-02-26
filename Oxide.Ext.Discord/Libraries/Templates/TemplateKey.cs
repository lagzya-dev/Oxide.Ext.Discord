using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Json;

namespace Oxide.Ext.Discord.Libraries
{
    /// <summary>
    /// Represents a Template Key. This is the key for template lookup
    /// </summary>
    [JsonConverter(typeof(TemplateKeyConverter))]
    public struct TemplateKey: IEquatable<TemplateKey>, IDiscordKey
    {
        /// <summary>
        /// Placeholder Key
        /// </summary>
        public readonly string Name;
        
        /// <summary>
        /// If <see cref="Name"/> Is a Valid Key
        /// </summary>
        public bool IsValid => !string.IsNullOrEmpty(Name);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Placeholder Value</param>
        public TemplateKey(string name)
        {
            Name = name;
        }
        
        /// <inheritdoc />
        public bool Equals(TemplateKey other) => Name == other.Name;
        
        /// <inheritdoc />
        public override bool Equals(object obj) => obj is TemplateKey other && Equals(other);
        
        /// <inheritdoc />
        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;

        /// <summary>
        /// Template Key == operator
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(TemplateKey left, TemplateKey right) => left.Name == right.Name;

        /// <summary>
        /// Template Key != operator
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(TemplateKey left, TemplateKey right) => !(left == right);

        /// <summary>
        /// Returns the template name
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;
        
        /// <summary>
        /// Implicitly converts to <see cref="string"/> by calling the <see cref="ToString"/> method.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static implicit operator string(TemplateKey key) => key.ToString();
    }
}