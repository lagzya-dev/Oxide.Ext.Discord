using System;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Messages;

namespace Oxide.Ext.Discord.Libraries.Command
{
    /// <summary>
    /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L76)
    /// </summary>
    internal class BaseCommand
    {
        public readonly string Name;
        public readonly Plugin Plugin;
        private readonly Action<DiscordMessage, string, string[]> _callback;

        protected BaseCommand(string name, Plugin plugin, Action<DiscordMessage, string, string[]> callback)
        {
            Name = name;
            Plugin = plugin;
            _callback = callback;
        }
        
        public void HandleCommand(DiscordMessage message, string name, string[] args)
        {
            Interface.Oxide.NextTick(() =>
            {
                try
                {
                    Plugin.TrackStart();
                    _callback.Invoke(message, name, args);
                    Plugin.TrackEnd();
                }
                catch(Exception ex)
                {
                    DiscordExtension.GlobalLogger.Exception($"An exception occured in discord command {name} for plugin {Plugin?.Name}", ex);   
                }
            });
        }

        public virtual bool CanHandle(DiscordMessage message, DiscordChannel channel) => true;
    }
}