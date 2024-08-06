using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Json;

internal static class JsonSettings
{
    internal static readonly JsonSerializerSettings Indented = new()
    {
        Formatting = Formatting.Indented
    };
}