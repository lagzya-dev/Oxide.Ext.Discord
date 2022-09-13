using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    internal struct TemplateId : IEquatable<TemplateId>
    {
        internal readonly string PluginName;
        public readonly string TemplateName;
        public readonly string Language;
        
        public bool IsGlobal => string.IsNullOrEmpty(Language);
        
        public TemplateId(Plugin plugin, string templateName, string language)
        {
            PluginName = plugin?.Name ?? throw new ArgumentNullException(nameof(plugin));;
            TemplateName = templateName ?? throw new ArgumentNullException(nameof(templateName));
            Language = language;
        }

        public TemplateId(TemplateId id, string language)
        {
            PluginName = id.PluginName;
            TemplateName = id.TemplateName;
            Language = language;
        }

        public string GetPluginName()
        {
            return PluginExt.GetFullName(PluginName);
        }

        public string GetLanguageName() => IsGlobal ? "Global" : Language;
        
        public bool Equals(TemplateId other)
        {
            return PluginName == other.PluginName && TemplateName == other.TemplateName && Language == other.Language;
        }
        
        public override bool Equals(object obj)
        {
            return obj is TemplateId other && Equals(other);
        }

        public override string ToString()
        {
            return $"{PluginName}/{GetLanguageName()}/{TemplateName}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = PluginName.GetHashCode();
                hashCode = (hashCode * 397) ^ TemplateName.GetHashCode();
                hashCode = (hashCode * 397) ^ (Language != null ? Language.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}