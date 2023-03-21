using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="RequestError"/> placeholders
    /// </summary>
    public static class RequestErrorPlaceholders
    {
        /// <summary>
        /// <see cref="RequestError.HttpStatusCode"/> placeholder
        /// </summary>
        public static void HttpCode(StringBuilder builder, PlaceholderState state, RequestError error) => PlaceholderFormatting.Replace(builder, state, error.HttpStatusCode);
        
        /// <summary>
        /// <see cref="RequestError.Message"/> placeholder
        /// </summary>
        public static void Message(StringBuilder builder, PlaceholderState state, RequestError error) => PlaceholderFormatting.Replace(builder, state, error.DiscordError?.Message ?? error.Message);

        internal static void RegisterPlaceholders()
        {
            RegisterPlaceholders(DiscordExtensionCore.Instance, "error");
        }
        
        /// <summary>
        /// Registers placeholders for the given plugin. 
        /// </summary>
        /// <param name="plugin">Plugin to register placeholders for</param>
        /// <param name="placeholderPrefix">Prefix to use for the placeholders</param>
        /// <param name="dataKey">Data key in <see cref="PlaceholderData"/></param>
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(RequestError))
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<RequestError>(plugin, $"{placeholderPrefix}.code", dataKey, HttpCode);
            placeholders.RegisterPlaceholder<RequestError>(plugin, $"{placeholderPrefix}.message", dataKey, Message);
        }
    }
}