using System;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Discord.Libraries.Langs
{
    internal struct LangId : IEquatable<LangId>
    {
        internal readonly string PluginName;
        private readonly string _language;

        public LangId(Plugin plugin, string language)
        {
            if(string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));
            PluginName = plugin?.Name ?? throw new ArgumentNullException(nameof(plugin));
            _language = language;
        }

        public bool Equals(LangId other)
        {
            return PluginName == other.PluginName && _language == other._language;
        }
        
        public override bool Equals(object obj)
        {
            return obj is LangId other && Equals(other);
        }

        public override string ToString()
        {
            return $"Plugin: {PluginName} Language: {_language}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = PluginName.GetHashCode();
                hashCode = (hashCode * 397) ^ _language.GetHashCode();
                return hashCode;
            }
        }
    }
}