using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins;

namespace Oxide.Ext.Discord.Libraries.Langs
{
    internal struct LangId : IEquatable<LangId>
    {
        internal readonly PluginId PluginId;
        private readonly string _language;

        public LangId(Plugin plugin, string language)
        {
            if(string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));
            PluginId = plugin?.Id() ?? throw new ArgumentNullException(nameof(plugin));
            _language = language;
        }

        public bool Equals(LangId other)
        {
            return PluginId == other.PluginId && _language == other._language;
        }
        
        public override bool Equals(object obj)
        {
            return obj is LangId other && Equals(other);
        }

        public override string ToString()
        {
            return $"Plugin: {PluginId.ToString()} Language: {_language}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = PluginId.GetHashCode();
                hashCode = (hashCode * 397) ^ _language.GetHashCode();
                return hashCode;
            }
        }
    }
}