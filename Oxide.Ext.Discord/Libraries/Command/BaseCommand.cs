using System;
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
        private readonly Action<Message, string, string[]> _callback;

        protected BaseCommand(string name, Plugin plugin, Action<Message, string, string[]> callback)
        {
            Name = name;
            Plugin = plugin;
            _callback = callback;
        }
        
        public void HandleCommand(Message message, string name, string[] args)
        {
            Plugin?.TrackStart();
            _callback?.Invoke(message, name, args);
            Plugin?.TrackEnd();
        }

        public virtual bool CanHandle(Message message, Channel channel) => true;
    }
}