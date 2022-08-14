using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Placeholders.Callbacks;
using Oxide.Ext.Discord.Libraries.Placeholders.Default;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins.Core;
using Oxide.Ext.Discord.Pooling;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    /// <summary>
    /// Discord Placeholders Library
    /// </summary>
    public class DiscordPlaceholders : Library
    {
        private readonly Regex _placeholderRegex = new Regex(@"{([^\d][^!:{}""]+)(?::([^!{}""]+))*?}", RegexOptions.Compiled);
        private readonly Hash<string, BasePlaceholder> _placeholders = new Hash<string, BasePlaceholder>();
        private readonly Hash<string, BasePlaceholder> _internalPlaceholders = new Hash<string, BasePlaceholder>();
        private readonly Covalence _covalence = Interface.Oxide.GetLibrary<Covalence>();
        private readonly ILogger _logger;
        
        internal DiscordPlaceholders(ILogger logger)
        {
            _logger = logger;
            ChannelPlaceholders.RegisterPlaceholders(this);
            GuildPlaceholders.RegisterPlaceholders(this);
            ServerPlaceholders.RegisterPlaceholders(this);
            MemberPlaceholders.RegisterPlaceholders(this);
            PlayerPlaceholders.RegisterPlaceholders(this);
            RolePlaceholders.RegisterPlaceholders(this);
            TimestampPlaceholders.RegisterPlaceholders(this);
            UserPlaceholders.RegisterPlaceholders(this);
            ApplicationCommandPlaceholders.RegisterPlaceholders(this);
        }
        
        /// <summary>
        /// Process placeholders for the given Text
        /// </summary>
        /// <param name="text">Text to process placeholders for</param>
        /// <param name="data">Placeholder Data for the placeholders</param>
        /// <returns>string with placeholders replaced. If no placeholders are found the original string is returned</returns>
        public string ProcessPlaceholders(string text, PlaceholderData data)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            
            MatchCollection matches = _placeholderRegex.Matches(text);
            if (matches.Count != 0)
            {
                StringBuilder builder = DiscordPool.GetStringBuilder();
                builder.Append(text);
                bool nonMatchedPlaceholder = false;
                for (int index = matches.Count - 1; index >= 0; index--)
                {
                    Match match = matches[index];
                    PlaceholderMatch placeholderMatch = new PlaceholderMatch(match);
                    BasePlaceholder placeholder = _placeholders[placeholderMatch.Name];
                    if (placeholder == null)
                    {
                        nonMatchedPlaceholder = true;
                        continue;
                    }

                    placeholder.Invoke(builder, placeholderMatch, data);
                }

                if (nonMatchedPlaceholder)
                {
                    DiscordExtensionCore.Instance.GetReplacer()?.Invoke(data.Get<IPlayer>(nameof(IPlayer)), builder, true);
                }

                text = builder.ToString();
                DiscordPool.FreeStringBuilder(ref builder);
            }

            if (data.ShouldPool)
            {
                DiscordPool.FreePlaceholderData(ref data);
            }
            
            return text;
        }

        /// <summary>
        /// Creates Pooled <see cref="PlaceholderData"/>
        /// </summary>
        /// <returns><see cref="PlaceholderData"/></returns>
        public PlaceholderData CreateData()
        {
            PlaceholderData data = DiscordPool.GetPlaceholderData();
            data.AddServer(_covalence.Server);
            return data;
        }

        /// <summary>
        /// Registers a placeholder
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="placeholder">Placeholder string</param>
        /// <param name="callback">Callback Method for the placeholder</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder(Plugin plugin, string placeholder, Action<StringBuilder, PlaceholderMatch> callback)
        {
            if (string.IsNullOrEmpty(placeholder)) throw new ArgumentNullException(nameof(placeholder));
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            Placeholder holder = new Placeholder(plugin, callback);
            BasePlaceholder existing = _placeholders[placeholder];
            if (existing != null && !existing.IsExtensionPlaceholder() && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("{0} Plugin has replaced placeholder '{1}' previously registered by plugin {2}", plugin.FullName(), placeholder, existing.Plugin.FullName());
            }
            _placeholders[placeholder] = holder;
        }
        
        /// <summary>
        /// Registers a placeholder static value placeholder
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="placeholder">Placeholder string</param>
        /// <param name="value">Static string value</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder(Plugin plugin, string placeholder, string value)
        {
            if (string.IsNullOrEmpty(placeholder)) throw new ArgumentNullException(nameof(placeholder));
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            StaticPlaceholder holder = new StaticPlaceholder(plugin, value);
            BasePlaceholder existing = _placeholders[placeholder];
            if (existing != null && !existing.IsExtensionPlaceholder() && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("{0} Plugin has replaced placeholder '{1}' previously registered by plugin {2}", plugin.FullName(), placeholder, existing.Plugin.FullName());
            }
            _placeholders[placeholder] = holder;
        }
        
        /// <summary>
        /// Registers a placeholder of type {T}
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="placeholder">Placeholder string</param>
        /// <param name="callback">Callback Method for the placeholder</param>
        /// <typeparam name="T">Type of the data key</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder<T>(Plugin plugin, string placeholder, Action<StringBuilder, T, PlaceholderMatch> callback)
        {
            if (string.IsNullOrEmpty(placeholder)) throw new ArgumentNullException(nameof(placeholder));
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            Placeholder<T> holder = new Placeholder<T>(typeof(T).Name, plugin, callback);
            BasePlaceholder existing = _placeholders[placeholder];
            if (existing != null && !existing.IsExtensionPlaceholder() && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("{0} Plugin has replaced placeholder '{1}' previously registered by plugin {2}", plugin.FullName(), placeholder, existing.Plugin.FullName());
            }
            _placeholders[placeholder] = holder;
        }
        
        /// <summary>
        /// Registers a placeholder of type {T}
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="placeholder">Placeholder string</param>
        /// <param name="dataKey">The name of the data key in PlaceholderData</param>
        /// <param name="callback">Callback Method for the placeholder</param>
        /// <typeparam name="T">Type of the data key</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder<T>(Plugin plugin, string placeholder, string dataKey, Action<StringBuilder, T, PlaceholderMatch> callback)
        {
            if (string.IsNullOrEmpty(placeholder)) throw new ArgumentNullException(nameof(placeholder));
            if (string.IsNullOrEmpty(dataKey)) throw new ArgumentNullException(nameof(dataKey));
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            Placeholder<T> holder = new Placeholder<T>(dataKey, plugin, callback);
            BasePlaceholder existing = _placeholders[placeholder];
            if (existing != null && !existing.IsExtensionPlaceholder() && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("{0} Plugin has replaced placeholder '{1}' previously registered by plugin {2}", plugin.FullName(), placeholder, existing.Plugin.FullName());
            }
            _placeholders[placeholder] = holder;
        }

        internal void RegisterInternalPlaceholder<T>(string placeholder, Action<StringBuilder, T, PlaceholderMatch> callback)
        {
            if (string.IsNullOrEmpty(placeholder)) throw new ArgumentNullException(nameof(placeholder));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            Placeholder<T> holder = new Placeholder<T>(typeof(T).Name, callback);
            _placeholders[placeholder] = holder;
            _internalPlaceholders[placeholder] = holder;
        }
        
        internal void RegisterInternalPlaceholder<T>(string placeholder, string customDataKey, Action<StringBuilder, T, PlaceholderMatch> callback)
        {
            if (string.IsNullOrEmpty(placeholder)) throw new ArgumentNullException(nameof(placeholder));
            if (string.IsNullOrEmpty(customDataKey)) throw new ArgumentNullException(nameof(customDataKey));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            Placeholder<T> holder = new Placeholder<T>(customDataKey, callback);
            _placeholders[placeholder] = holder;
            _internalPlaceholders[placeholder] = holder;
        }

        internal void OnPluginUnloaded(Plugin plugin)
        {
            _placeholders.RemoveAll(p => p.IsForPlugin(plugin));
            foreach (KeyValuePair<string, BasePlaceholder> placeholder in _internalPlaceholders)
            {
                if (!_placeholders.ContainsKey(placeholder.Key))
                {
                    _placeholders[placeholder.Key] = placeholder.Value;
                }
            }
        }
    }
}