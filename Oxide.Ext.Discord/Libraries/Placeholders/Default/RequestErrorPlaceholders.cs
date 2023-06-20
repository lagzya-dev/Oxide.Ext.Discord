using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    /// <summary>
    /// <see cref="ResponseError"/> placeholders
    /// </summary>
    public static class RequestErrorPlaceholders
    {
        /// <summary>
        /// <see cref="ResponseError.HttpStatusCode"/> placeholder
        /// </summary>
        public static DiscordHttpStatusCode HttpCode(ResponseError error) => error.HttpStatusCode;
        
        /// <summary>
        /// <see cref="ResponseError.ResponseMessage"/> placeholder
        /// </summary>
        public static string Message(ResponseError error) => error.DiscordError?.Message ?? error.ResponseMessage;

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
        public static void RegisterPlaceholders(Plugin plugin, string placeholderPrefix, string dataKey = nameof(ResponseError))
        {
            DiscordPlaceholders placeholders = DiscordPlaceholders.Instance;
            placeholders.RegisterPlaceholder<ResponseError, DiscordHttpStatusCode>(plugin, $"{placeholderPrefix}.code", dataKey, HttpCode);
            placeholders.RegisterPlaceholder<ResponseError, string>(plugin, $"{placeholderPrefix}.message", dataKey, Message);
        }
    }
}