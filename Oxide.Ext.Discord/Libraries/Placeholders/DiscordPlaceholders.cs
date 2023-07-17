using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Libraries.Placeholders.Callbacks;
using Oxide.Ext.Discord.Libraries.Placeholders.Default;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Plugins.Core;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Placeholders
{
    /// <summary>
    /// Discord Placeholders Library
    /// </summary>
    public class DiscordPlaceholders : BaseDiscordLibrary<DiscordPlaceholders>
    {
        private readonly Regex _placeholderRegex = new Regex(@"{([^\d][^:{}""]+)(?::([^{}""]+))*?}", RegexOptions.Compiled);
        private readonly Hash<PlaceholderKey, IPlaceholder> _placeholders = new Hash<PlaceholderKey, IPlaceholder>();
        private readonly Hash<PlaceholderKey, IPlaceholder> _internalPlaceholders = new Hash<PlaceholderKey, IPlaceholder>();
        private readonly Covalence _covalence = Interface.Oxide.GetLibrary<Covalence>();
        private readonly ILogger _logger;
        
        internal DiscordPlaceholders(ILogger logger)
        {
            _logger = logger;
        }

        internal void RegisterPlaceholders()
        {
            ChannelPlaceholders.RegisterPlaceholders();
            DateTimePlaceholders.RegisterPlaceholders();
            GuildPlaceholders.RegisterPlaceholders();
            InteractionPlaceholders.RegisterPlaceholders();
            ServerPlaceholders.RegisterPlaceholders();
            MemberPlaceholders.RegisterPlaceholders();
            MessagePlaceholders.RegisterPlaceholders();
            PlayerPlaceholders.RegisterPlaceholders();
            PluginPlaceholders.RegisterPlaceholders();
            RolePlaceholders.RegisterPlaceholders();
            TimestampPlaceholders.RegisterPlaceholders();
            UserPlaceholders.RegisterPlaceholders();
            ApplicationCommandPlaceholders.RegisterPlaceholders();
            ResponseErrorPlaceholders.RegisterPlaceholders();
            SnowflakePlaceholders.RegisterPlaceholders();
            TimeSpanPlaceholders.RegisterPlaceholders();
        }

        internal IEnumerable<KeyValuePair<PlaceholderKey, IPlaceholder>> GetPlaceholders()
        {
            foreach (KeyValuePair<PlaceholderKey, IPlaceholder> placeholder in _placeholders)
            {
                yield return placeholder;
            }
        }
        
        /// <summary>
        /// Returns true if the text contains placeholders
        /// </summary>
        /// <param name="text">Text to check for placeholders</param>
        /// <returns></returns>
        public bool HasPlaceholders(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            
            MatchCollection matches = _placeholderRegex.Matches(text);
            return matches.Count != 0;
        }
        
        /// <summary>
        /// Returns placeholders found in the given text
        /// </summary>
        /// <param name="text">Text to get placeholders for</param>
        /// <returns></returns>
        public IEnumerable<string> GetPlaceholders(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                yield break;
            }
            
            MatchCollection matches = _placeholderRegex.Matches(text);
            foreach (Match match in matches)
            {
                yield return match.Groups[1].Value;
            }
        }

        /// <summary>
        /// Process placeholders for the given text.
        /// </summary>
        /// <param name="text">Text to process placeholders for</param>
        /// <param name="data">Placeholder Data for the placeholders</param>
        /// <param name="autoDispose">Automatically dispose <see cref="PlaceholderData"/> on completion. <see cref="PlaceholderData"/> must also have AutoPool enabled</param>
        /// <returns>string with placeholders replaced. If no placeholders are found the original string is returned</returns>
        public string ProcessPlaceholders(string text, PlaceholderData data, bool autoDispose = true)
        {
            if (data == null)
            {
                return text;
            }
            
            if (string.IsNullOrEmpty(text))
            {
                DisposeData(data, autoDispose);
                return text;
            }
            
            if (data == null) throw new ArgumentNullException(nameof(data));
            
            MatchCollection matches = _placeholderRegex.Matches(text);
            if (matches.Count != 0)
            {
                StringBuilder builder = DiscordPool.Internal.GetStringBuilder();
                builder.Append(text);
                PlaceholderState state = PlaceholderState.Create(data);
                bool hasNonMatchingPlaceholder = false;
                for (int index = matches.Count - 1; index >= 0; index--)
                {
                    Match match = matches[index];
                    state.UpdateState(match);
                    IPlaceholder placeholder = _placeholders[state.Key];
                    if (placeholder == null)
                    {
                        hasNonMatchingPlaceholder = true;
                        continue;
                    }

                    placeholder.Invoke(builder, state);
                }

                if (hasNonMatchingPlaceholder)
                {
                    DiscordExtensionCore.Instance.GetReplacer()?.Invoke(data.Get<IPlayer>(), builder, true);
                }
                
                text = DiscordPool.Internal.FreeStringBuilderToString(builder);
                state.Dispose();
            }

            DisposeData(data, autoDispose);

            return text;
        }
        
        private static void DisposeData(PlaceholderData data, bool autoDispose)
        {
            if (autoDispose && data.AutoPool)
            {
                data.Dispose();
            }
        }

        /// <summary>
        /// Returns a pooled <see cref="PlaceholderData"/> for the given plugin.
        /// If you wish to manually pool call the <see cref="PlaceholderData.ManualPool"/> method.
        /// If you wish to clone <see cref="PlaceholderData"/> call the <see cref="PlaceholderData.Clone"/> method.
        /// </summary>
        /// <returns><see cref="PlaceholderData"/></returns>
        public PlaceholderData CreateData(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            
            PlaceholderData data = DiscordPool.Instance.GetOrCreate(plugin).GetPlaceholderData();
            data.AddServer(_covalence.Server);
            data.AddPlugin(plugin);
            return data;
        }

        /// <summary>
        /// Registers a placeholder static value placeholder.
        /// Static placeholder value can not be changed.
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="key">Placeholder key</param>
        /// <param name="value">Static string value</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder(Plugin plugin, PlaceholderKey key, string value)
        {
            if (!key.IsValid) throw new ArgumentNullException(nameof(key));
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (value == null) throw new ArgumentNullException(nameof(value));
            
            StaticPlaceholder holder = new StaticPlaceholder(plugin, value);
            IPlaceholder existing = _placeholders[key];
            if (existing != null && !existing.IsExtensionPlaceholder && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("Plugin {0} has replaced placeholder '{1}' previously registered by plugin {2}", plugin.FullName(), key.Placeholder, existing.PluginName);
            }
            _placeholders[key] = holder;
        }
        
        /// <summary>
        /// Registers a placeholder that will call the callback function when the placeholder is called.
        /// This function will return TResult data for the placeholder
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="key">Placeholder key</param>
        /// <param name="callback">Callback Method for the placeholder</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder<TResult>(Plugin plugin, PlaceholderKey key, Func<TResult> callback)
        {
            if (!key.IsValid) throw new ArgumentNullException(nameof(key));
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            
            Placeholder<TResult> holder = new Placeholder<TResult>(plugin, callback);
            IPlaceholder existing = _placeholders[key];
            if (existing != null && !existing.IsExtensionPlaceholder && !existing.IsForPlugin(plugin))
            {
                _logger.Warning("Plugin {0} has replaced placeholder '{1}' previously registered by plugin {2}", plugin.FullName(), key.Placeholder, existing.PluginName);
            }
            
            _placeholders[key] = holder;
        }

        /// <summary>
        /// Registers a placeholder that will take a datakey from <see cref="PlaceholderData"/> and use that as the placeholder value
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="key">Placeholder key</param>
        /// <param name="dataKey"></param>
        /// <typeparam name="TData">Type that is registered in the PlaceholderData</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if placeholder or plugin is null</exception>
        public void RegisterPlaceholder<TData>(Plugin plugin, PlaceholderKey key, PlaceholderDataKey dataKey)
        {
            if (!key.IsValid) throw new ArgumentNullException(nameof(key));
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));

            Placeholder<TData, TData> holder = new Placeholder<TData, TData>(dataKey, plugin, data => data);
            if (!holder.IsExtensionPlaceholder && !_internalPlaceholders.ContainsKey(key) && !key.Placeholder.StartsWith(plugin.Name, StringComparison.OrdinalIgnoreCase))
            {
                _logger.Error("Plugin placeholder {0} must be prefixed with the plugin name {1} unless overriding a Discord Extension provided placeholder.", key.Placeholder, plugin.Name.ToLower());
                return;
            }

            _placeholders[key] = holder;
            if (holder.IsExtensionPlaceholder)
            {
                _internalPlaceholders[key] = holder;
            }
        }

        /// <summary>
        /// Registers a placeholder that will pull type T from <see cref="PlaceholderData"/>. The datakey for T will be the T.GetType().Name
        /// Type T will be passed into the callback function and will expect a TResult to be returned from that function.
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="key">Placeholder key</param>
        /// <param name="callback">Callback Method for the placeholder</param>
        /// <typeparam name="TData">Type of the data key</typeparam>
        /// <typeparam name="TResult">The return type of the placeholder callback</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder<TData, TResult>(Plugin plugin, PlaceholderKey key, Func<TData, TResult> callback)
        {
            RegisterPlaceholder(plugin, key, new PlaceholderDataKey(typeof(TData).Name), callback);
        }
        
        /// <summary>
        /// Registers a placeholder that will pull type T from <see cref="PlaceholderData"/>. The datakey for T will come from the datakey argument
        /// Type T will be passed into the callback function and will expect a TResult to be returned from that function.
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="key">Placeholder Key</param>
        /// <param name="dataKey">The name of the data key in PlaceholderData</param>
        /// <param name="callback">Callback Method for the placeholder</param>
        /// <typeparam name="TData">Type of the data key</typeparam>
        /// <typeparam name="TResult">The return type of the placeholder callback</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder<TData, TResult>(Plugin plugin, PlaceholderKey key, PlaceholderDataKey dataKey, Func<TData, TResult> callback)
        {
            if (!key.IsValid) throw new ArgumentNullException(nameof(key));
            if (!dataKey.IsValid) throw new ArgumentNullException(nameof(dataKey));
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            
            Placeholder<TData, TResult> holder = new Placeholder<TData, TResult>(dataKey, plugin, callback);
            if (!holder.IsExtensionPlaceholder && !_internalPlaceholders.ContainsKey(key) && !key.Placeholder.StartsWith(plugin.Name, StringComparison.OrdinalIgnoreCase))
            {
                _logger.Error("Plugin placeholder {0} must be prefixed with the plugin name {1} unless overriding a Discord Extension provided placeholder.", key.Placeholder, plugin.Name.ToLower());
                return;
            }

            _placeholders[key] = holder;
            if (holder.IsExtensionPlaceholder)
            {
                _internalPlaceholders[key] = holder;
            }
        }
        
        /// <summary>
        /// Registers a placeholder that will pull type T from <see cref="PlaceholderData"/>. The datakey for T will be the T.GetType().Name
        /// Type T will be passed into the callback function along with the current <see cref="PlaceholderState"/> and will expect a TResult to be returned from that function.
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="key">Placeholder key</param>
        /// <param name="callback">Callback Method for the placeholder</param>
        /// <typeparam name="TData">Type of the data key</typeparam>
        /// <typeparam name="TResult">The return type of the placeholder callback</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder<TData, TResult>(Plugin plugin, PlaceholderKey key, Func<PlaceholderState, TData, TResult> callback)
        {
            RegisterPlaceholder(plugin, key, new PlaceholderDataKey(typeof(TData).Name), callback);
        }
        
        /// <summary>
        /// Registers a placeholder that will pull type T from <see cref="PlaceholderData"/>. The datakey for T will come from the datakey argument
        /// Type T will be passed into the callback function along with the current <see cref="PlaceholderState"/> and will expect a TResult to be returned from that function.
        /// </summary>
        /// <param name="plugin">Plugin this placeholder is for</param>
        /// <param name="key">Placeholder key</param>
        /// <param name="dataKey">The name of the data key in PlaceholderData</param>
        /// <param name="callback">Callback Method for the placeholder</param>
        /// <typeparam name="TData">Type of the data key</typeparam>
        /// <typeparam name="TResult">The return type of the placeholder callback</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterPlaceholder<TData, TResult>(Plugin plugin, PlaceholderKey key, PlaceholderDataKey dataKey, Func<PlaceholderState, TData, TResult> callback)
        {
            if (!key.IsValid) throw new ArgumentNullException(nameof(key));
            if (!dataKey.IsValid) throw new ArgumentNullException(nameof(dataKey));
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            
            Placeholder<TData, TResult> holder = new Placeholder<TData, TResult>(dataKey, plugin, callback);
            if (!holder.IsExtensionPlaceholder && !_internalPlaceholders.ContainsKey(key) && !key.Placeholder.StartsWith(plugin.Name, StringComparison.OrdinalIgnoreCase))
            {
                _logger.Error("Plugin placeholder {0} must be prefixed with the plugin name {1} unless overriding a Discord Extension provided placeholder.", key.Placeholder, plugin.Name.ToLower());
                return;
            }

            _placeholders[key] = holder;
            if (holder.IsExtensionPlaceholder)
            {
                _internalPlaceholders[key] = holder;
            }
        }

        ///<inheritdoc/>
        protected override void OnPluginUnloaded(Plugin plugin)
        {
            _placeholders.RemoveAll(p => p.IsForPlugin(plugin));
            foreach (KeyValuePair<PlaceholderKey, IPlaceholder> placeholder in _internalPlaceholders)
            {
                if (!_placeholders.ContainsKey(placeholder.Key))
                {
                    _placeholders[placeholder.Key] = placeholder.Value;
                }
            }
        }
    }
}