using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Singleton;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Factory for creating DiscordLoggers
    /// </summary>
    public class DiscordLoggerFactory : Singleton<DiscordLoggerFactory>
    {
        private readonly Hash<string, DiscordLogHandler> _handlers = new Hash<string, DiscordLogHandler>();

        private DiscordLoggerFactory() {}
        
        /// <summary>
        /// Returns a newly created <see cref="DiscordLogger"/> for a given plugin
        /// </summary>
        /// <param name="plugin">Plugin the logger is for</param>
        /// <param name="logLevel">The current LogLevel for the logger</param>
        /// <param name="config">The config for the logger</param>
        /// <returns><see cref="DiscordLogger"/></returns>
        public DiscordLogger GetLogger(Plugin plugin, DiscordLogLevel logLevel, IDiscordLoggingConfig config) => GetLoggerInternal(plugin.Id(), logLevel, config, false);
        internal DiscordLogger GetExtensionLogger(DiscordLogLevel logLevel) =>  GetLoggerInternal(nameof(DiscordExtension), logLevel, DiscordExtension.DiscordConfig.Logging, true);

        private DiscordLogger GetLoggerInternal(string pluginName, DiscordLogLevel logLevel, IDiscordLoggingConfig config, bool isExtension)
        {
            DiscordLogHandler handler = _handlers[pluginName];
            if (handler == null)
            {
                handler = new DiscordLogHandler(pluginName, config, isExtension);
                _handlers[pluginName] = handler;
            }

            return new DiscordLogger(logLevel, config, handler);
        }

        internal void OnPluginUnloaded(Plugin plugin)
        {
            string id = plugin.Id();
            _handlers[id]?.Shutdown();
            _handlers.Remove(id);
        }
        
        internal void OnServerShutdown()
        {
            foreach (DiscordLogHandler handler in _handlers.Values)
            {
                handler.Shutdown();
            }
            
            _handlers.Clear();
            DiscordFileLogger.OnServerShutdown();
        }
    }
}