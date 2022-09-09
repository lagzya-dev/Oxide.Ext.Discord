using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    public static class RequestErrorPlaceholders
    {
        public static void HttpCode(StringBuilder builder, PlaceholderState state, RequestError error) => PlaceholderFormatting.Replace(builder, state, error.HttpStatusCode);
        public static void Message(StringBuilder builder, PlaceholderState state, RequestError error) => PlaceholderFormatting.Replace(builder, state, error.DiscordError?.Message ?? error.Message);

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "error", nameof(RequestError));
        }
        
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey)
        {
            DiscordPlaceholders placeholders = DiscordExtension.DiscordPlaceholders;
            placeholders.RegisterPlaceholder<RequestError>(plugin, $"{placeholderPrefix}.code", dataKey, HttpCode);
            placeholders.RegisterPlaceholder<RequestError>(plugin, $"{placeholderPrefix}.message", dataKey, Message);
        }
    }
}