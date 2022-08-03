using System;
using System.Collections.Generic;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    internal struct TemplateId
    {
        public readonly string TemplateName;
        public readonly string Lang;
        
        public static readonly IEqualityComparer<TemplateId> TemplateIdComparer = new PluginNameTemplateNameLangEqualityComparer();

        public TemplateId(string templateName, string lang)
        {
            TemplateName = templateName ?? throw new ArgumentNullException(nameof(templateName));
            Lang = lang ?? throw new ArgumentNullException(nameof(lang));
        }
        private sealed class PluginNameTemplateNameLangEqualityComparer : IEqualityComparer<TemplateId>
        {
            public bool Equals(TemplateId x, TemplateId y)
            {
                return x.TemplateName == y.TemplateName && x.Lang == y.Lang;
            }
            
            public int GetHashCode(TemplateId obj)
            {
                unchecked
                {
                    int hashCode = obj.TemplateName.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Lang.GetHashCode();
                    return hashCode;
                }
            }
        }
    }
}