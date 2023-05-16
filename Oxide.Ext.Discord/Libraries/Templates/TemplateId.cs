using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Plugins;

namespace Oxide.Ext.Discord.Libraries.Templates
{
    internal struct TemplateId : IEquatable<TemplateId>
    {
        internal readonly PluginId PluginId;
        public readonly string TemplateName;
        public readonly string Language;
        
        public bool IsGlobal => string.IsNullOrEmpty(Language);
        
        private TemplateId(Plugin plugin, string templateName, string language)
        {
            PluginId = plugin?.Id() ?? throw new ArgumentNullException(nameof(plugin));
            TemplateName = templateName;
            Language = language;
        }

        private TemplateId(PluginId pluginName, string templateName, string language)
        {
            PluginId = pluginName;
            TemplateName = templateName;
            Language = language;
        }

        public static TemplateId CreateGlobal(Plugin plugin, string templateName) => new TemplateId(plugin, templateName, null);
        public static TemplateId CreateLocalized(Plugin plugin, string templateName, string language) => new TemplateId(plugin, templateName, language);
        public static TemplateId CreateInteraction(Plugin plugin, string templateName, DiscordInteraction interaction) => new TemplateId(plugin, templateName, DiscordLang.Instance.GetOxideLanguage(interaction.Locale));
        public static TemplateId CreatePlayer(Plugin plugin, string templateName, string playerId) => new TemplateId(plugin, templateName, DiscordLang.Instance.GetPlayerLanguage(playerId));

        public string GetPluginName()
        {
            return PluginExt.GetFullName(PluginId);
        }

        public TemplateId WithLanguage(string language)
        {
            return new TemplateId(PluginId, TemplateName, language);
        }

        public string GetLanguageName() => IsGlobal ? "Global" : Language;
        
        public bool Equals(TemplateId other)
        {
            return PluginId == other.PluginId && TemplateName == other.TemplateName && Language == other.Language;
        }
        
        public override bool Equals(object obj)
        {
            return obj is TemplateId other && Equals(other);
        }

        public override string ToString()
        {
            return $"Plugin: {PluginId} Template: {TemplateName} Language: {GetLanguageName()}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = PluginId.GetHashCode();
                hashCode = (hashCode * 397) ^ (TemplateName != null ? TemplateName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Language != null ? Language.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}