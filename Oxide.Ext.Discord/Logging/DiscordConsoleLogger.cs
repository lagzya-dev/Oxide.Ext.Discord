using System;
using System.Text;
using Oxide.Core;
using Oxide.Ext.Discord.Cache;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a Console Logger for Discord
    /// </summary>
    internal class DiscordConsoleLogger : IOutputLogger
    {
        private readonly string _pluginName;
        private readonly object[] _empty = Array.Empty<object>();

        [ThreadStatic]
        private static StringBuilder _builder;
        private static StringBuilder Builder => _builder ?? (_builder = new StringBuilder());

        public DiscordConsoleLogger(string pluginName)
        {
            _pluginName = $"[{pluginName}] ";
        }

        /// <summary>
        /// Adds a message to the server console
        /// </summary>
        /// <param name="level"></param>
        /// <param name="log"></param>
        /// <param name="args"></param>
        /// <param name="ex"></param>
        public void AddMessage(DiscordLogLevel level, string log, object[] args, Exception ex)
        {
            StringBuilder sb = Builder;
            sb.Append(_pluginName);
            sb.Append('[');
            sb.Append(EnumCache<DiscordLogLevel>.Instance.ToString(level));
            sb.Append("]: ");
            if (args.Length != 0)
            {
                sb.AppendFormat(log, args);
            }
            else
            {
                sb.Append(log);
            }

            string message = sb.ToString();

            switch (level)
            {
                case DiscordLogLevel.Debug:
                case DiscordLogLevel.Warning:
                    Interface.Oxide.LogWarning(message, _empty);
                    break;
                case DiscordLogLevel.Error:
                    Interface.Oxide.LogError(message, _empty);
                    break;
                case DiscordLogLevel.Exception:
                    Interface.Oxide.LogException(message, ex);
                    break;
                default:
                    Interface.Oxide.LogInfo(message, _empty);
                    break;
            }
        }

        public void OnShutdown() {}
    }
}