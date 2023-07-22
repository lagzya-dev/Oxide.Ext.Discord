using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Helpers
{
    public static class ServerFormatting
    {
        public static string Color(string text, DiscordColor color) => $"[{color.ToHex()}]{text}[/#]";
        public static string Color(this PlaceholderKey key, DiscordColor color) => $"[{color.ToHex()}]{key.ToString()}[/#]";
    }
}