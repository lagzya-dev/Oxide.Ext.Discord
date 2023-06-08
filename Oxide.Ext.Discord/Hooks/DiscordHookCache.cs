using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Clients;
using Oxide.Ext.Discord.Connections;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities.Gateway;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Hooks
{
    internal class DiscordHookCache
    {
        private readonly Hash<string, List<Plugin>> _hookCache = new Hash<string, List<Plugin>>();
        private readonly ILogger _logger;
        private readonly ICollection<string> _supportedHooks;

        internal DiscordHookCache(ICollection<string> supportedHooks, ILogger logger)
        {
            _supportedHooks = supportedHooks;
            _logger = logger;
        }

        internal void AddPlugin(DiscordClient client)
        {
            Plugin plugin = client.Plugin;
            Type pluginType = plugin.GetType();
            foreach (MethodInfo method in pluginType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (method.DeclaringType != pluginType)
                {
                    continue;
                }

                HookMethodAttribute hookMethod = method.GetCustomAttribute<HookMethodAttribute>();
                if (!method.IsPrivate && hookMethod == null)
                {
                    continue;
                }
                
                AddPluginHook(client, hookMethod?.Name ?? method.Name);
            }
        }

        private void AddPluginHook(DiscordClient client, string hook)
        {
            if (!_supportedHooks.Contains(hook))
            {
                return;
            }

            BotConnection connection = client.Bot.Connection;
            GatewayIntents intent = DiscordExtHooks.HookGatewayIntent[hook];
            if (intent != GatewayIntents.None && !connection.HasAnyIntent(intent))
            {
                _logger.Warning("{0} is trying to add hook {1} which requires one of the following GatewayIntents \"{2}\", but was not specified. " +
                                "This hook will not work correctly until it is corrected. " +
                                "Please contact the plugin author {3} with this message.", client.PluginName, hook, EnumCache<GatewayIntents>.Instance.ToString(intent), client.Plugin?.Author);
            }

            List<Plugin> hooks = _hookCache[hook];
            if (hooks == null)
            {
                hooks = new List<Plugin>();
                _hookCache[hook] = hooks;
            }

            if (!hooks.Contains(client.Plugin))
            {
                hooks.Add(client.Plugin);
            }
        }

        internal void RemovePlugin(Plugin plugin)
        {
            List<string> hooksToRemove = DiscordPool.Internal.GetList<string>();
            foreach (KeyValuePair<string, List<Plugin>> cache in _hookCache)
            {
                if (cache.Value.Remove(plugin) && cache.Value.Count == 0)
                {
                    hooksToRemove.Add(cache.Key);
                }
            }

            for (int index = 0; index < hooksToRemove.Count; index++)
            {
                _hookCache.Remove(hooksToRemove[index]);
            }
            
            DiscordPool.Internal.FreeList(hooksToRemove);
        }

        internal bool TryGetHook(string hook, out List<Plugin> plugins)
        {
            return _hookCache.TryGetValue(hook, out plugins);
        }
    }
}