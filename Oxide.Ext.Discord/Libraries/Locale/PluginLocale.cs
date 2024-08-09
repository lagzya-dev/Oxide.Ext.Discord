using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Plugins;

namespace Oxide.Ext.Discord.Libraries;

internal readonly record struct PluginLocale
{
    internal readonly PluginId PluginId;
    private readonly ServerLocale _language;

    public PluginLocale(Plugin plugin, ServerLocale language)
    {
        if(!language.IsValid) throw new ArgumentNullException(nameof(language));
        PluginId = plugin?.Id() ?? throw new ArgumentNullException(nameof(plugin));
        _language = language;
    }

    public override string ToString()
    {
        return $"Plugin: {PluginId.ToString()} Language: {_language}";
    }
}