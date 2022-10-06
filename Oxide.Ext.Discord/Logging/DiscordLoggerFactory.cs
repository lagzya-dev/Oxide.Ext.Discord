using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Logging
{
    public static class DiscordLoggerFactory
    {
        private static readonly Hash<string, DiscordLogHandler> Handlers = new Hash<string, DiscordLogHandler>();

        public static DiscordLogger GetLogger(Plugin plugin, DiscordLogLevel logLevel, IDiscordLoggingConfig config)
        {
            DiscordLogHandler handler = Handlers[plugin.Id()];
            if (handler == null)
            {
                handler = new DiscordLogHandler(plugin.Id(), config, false);
                Handlers[plugin.Id()] = handler;
            }

            return new DiscordLogger(logLevel, config, handler);
        }

        internal static DiscordLogger GetExtensionLogger(DiscordLogLevel logLevel)
        {
            DiscordLogHandler handler = Handlers[nameof(DiscordExtension)];
            if (handler == null)
            {
                handler = new DiscordLogHandler(nameof(DiscordExtension), DiscordExtension.DiscordConfig.Logging, true);
                Handlers[nameof(DiscordExtension)] = handler;
            }

            return new DiscordLogger(logLevel,  DiscordExtension.DiscordConfig.Logging, handler);
        }

        internal static void OnPluginUnloaded(Plugin plugin)
        {
            string id = plugin.Id();
            Handlers[id]?.Shutdown();
            Handlers.Remove(id);
        }
        
        internal static void OnServerShutdown()
        {
            foreach (DiscordLogHandler handler in Handlers.Values)
            {
                handler.Shutdown();
            }
            
            Handlers.Clear();
            DiscordFileLogger.OnServerShutdown();
        }
    }
}