using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins;

namespace Oxide.Ext.Discord.Libraries;

internal readonly record struct TemplateId
{
    internal readonly PluginId PluginId;
    public readonly TemplateKey TemplateName;
    public readonly ServerLocale Language;
        
    public bool IsGlobal => !Language.IsValid;
        
    private TemplateId(Plugin plugin, TemplateKey templateName, ServerLocale language) : this(plugin?.Id() ?? throw new ArgumentNullException(nameof(plugin)), templateName, language) { }

    private TemplateId(PluginId pluginName, TemplateKey templateName, ServerLocale language)
    {
        PluginId = pluginName;
        TemplateName = templateName;
        Language = language;
    }

    public static TemplateId CreateGlobal(Plugin plugin, TemplateKey templateName) => new(plugin, templateName, default);
    public static TemplateId CreateLocalized(Plugin plugin, TemplateKey templateName, ServerLocale language) => new(plugin, templateName, language);
    public static TemplateId CreateInteraction(Plugin plugin, TemplateKey templateName, DiscordInteraction interaction) => new(plugin, templateName, interaction.Locale.GetServerLocale());
    public static TemplateId CreatePlayer(Plugin plugin, TemplateKey templateName, string playerId) => new(plugin, templateName, DiscordLocales.Instance.GetPlayerLanguage(playerId));

    public string GetPluginName()
    {
        return PluginExt.GetFullName(PluginId);
    }

    public TemplateId WithLanguage(ServerLocale language)
    {
        return new TemplateId(PluginId, TemplateName, language);
    }

    public string GetLanguageName() => IsGlobal ? "Global" : Language.Id;

    public override string ToString() => $"Plugin: {PluginId} Template: {TemplateName} Language: {GetLanguageName()}";
}