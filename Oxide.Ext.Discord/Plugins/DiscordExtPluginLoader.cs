using System;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Plugins.Core;

namespace Oxide.Ext.Discord.Plugins
{
    internal class DiscordExtPluginLoader : PluginLoader
    {
        public override Type[] CorePlugins => new[] { typeof(DiscordExtensionCore) };
    }
}