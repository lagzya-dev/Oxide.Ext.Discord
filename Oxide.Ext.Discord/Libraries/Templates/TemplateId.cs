using System;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    internal struct TemplateId : IEquatable<TemplateId>
    {
        public readonly string TemplateName;
        public readonly string Language;

        public TemplateId(string templateName, string lang)
        {
            TemplateName = templateName ?? throw new ArgumentNullException(nameof(templateName));
            Language = lang ?? throw new ArgumentNullException(nameof(lang));
        }
        
        public bool Equals(TemplateId other)
        {
            return TemplateName == other.TemplateName && Language == other.Language;
        }
        
        public override bool Equals(object obj)
        {
            return obj is TemplateId other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return ((TemplateName != null ? TemplateName.GetHashCode() : 0) * 397) ^ (Language != null ? Language.GetHashCode() : 0);
            }
        }
    }
}