using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Command
{
    /// <summary>
    /// Represents a library for discord commands
    /// </summary>
    public class DiscordCommand : Library
    {
        /// <summary>
        /// Available command prefixes used by the extension
        /// </summary>
        public readonly char[] CommandPrefixes;
        
        internal readonly Hash<string, DirectMessageCommand> DirectMessageCommands = new Hash<string, DirectMessageCommand>();
        internal readonly Hash<string, GuildCommand> GuildCommands = new Hash<string, GuildCommand>();

        /// <summary>
        /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L104)
        /// </summary>
        private readonly Hash<Plugin, Event.Callback<Plugin, PluginManager>> _pluginRemovedFromManager = new Hash<Plugin, Event.Callback<Plugin, PluginManager>>();

        /// <summary>
        /// Discord Commands Constructor
        /// </summary>
        /// <param name="prefixes">Command prefixes used by the extension</param>
        public DiscordCommand(char[] prefixes)
        {
            CommandPrefixes = prefixes;
        }
        
        /// <summary>
        /// Returns if there are any guild discord commands are registered
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(HasCommands))]
        public bool HasCommands()
        {
            return HasDirectMessageCommands() || HasGuildCommands();
        }
        
        /// <summary>
        /// Returns if there are any guild discord commands are registered
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(HasDirectMessageCommands))]
        public bool HasDirectMessageCommands()
        {
            return DirectMessageCommands.Count != 0;
        }
        
        /// <summary>
        /// Returns if there are any guild discord commands are registered
        /// </summary>
        /// <returns></returns>
        [LibraryFunction(nameof(HasGuildCommands))]
        public bool HasGuildCommands()
        {
            return GuildCommands.Count != 0;
        }

        /// <summary>
        /// Adds a discord direct message command
        /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L123)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="plugin"></param>
        /// <param name="callback"></param>
        [LibraryFunction(nameof(AddDiscordDirectMessageCommand))]
        public void AddDiscordDirectMessageCommand(string name, Plugin plugin, string callback)
        {
            AddDiscordDirectMessageCommand(name, plugin, (message, command, args) => plugin.CallHook(callback, message, command, args));
        }

        /// <summary>
        /// Adds a discord direct message command
        /// Sourced From Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L134)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="plugin"></param>
        /// <param name="callback"></param>
        public void AddDiscordDirectMessageCommand(string command, Plugin plugin, Action<DiscordMessage, string, string[]> callback)
        {
            string commandName = command.ToLowerInvariant();

            if (DirectMessageCommands.TryGetValue(commandName, out DirectMessageCommand cmd))
            {
                string previousPluginName = cmd.Plugin?.Name ?? "an unknown plugin";
                string newPluginName = plugin?.Name ?? "An unknown plugin";
                DiscordExtension.GlobalLogger.Warning($"{newPluginName} has replaced the '{commandName}' discord direct message command previously registered by {previousPluginName}");
            }

            cmd = new DirectMessageCommand(commandName, plugin, callback);

            // Add the new command to collections
            DirectMessageCommands[commandName] = cmd;

            // Hook the unload event
            if (plugin != null && !_pluginRemovedFromManager.ContainsKey(plugin))
            {
                _pluginRemovedFromManager[plugin] = plugin.OnRemovedFromManager.Add(OnPluginRemovedFromManager);
            }
        }
        
        /// <summary>
        /// Adds a discord guild command
        /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L123)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="plugin"></param>
        /// <param name="allowedChannels"></param>
        /// <param name="callback"></param>
        [LibraryFunction(nameof(AddDiscordGuildCommand))]
        public void AddDiscordGuildCommand(string name, Plugin plugin, List<Snowflake> allowedChannels, string callback)
        {
            AddDiscordGuildCommand(name, plugin, allowedChannels, (message, command, args) => plugin.CallHook(callback, message, command, args));
        }

        /// <summary>
        /// Adds a discord guild command
        /// Sourced From Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L134)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="plugin"></param>
        /// <param name="allowedChannels"></param>
        /// <param name="callback"></param>
        public void AddDiscordGuildCommand(string command, Plugin plugin, List<Snowflake> allowedChannels, Action<DiscordMessage, string, string[]> callback)
        {
            string commandName = command.ToLowerInvariant();

            if (GuildCommands.TryGetValue(commandName, out GuildCommand cmd))
            {
                string previousPluginName = cmd.Plugin?.Name ?? "an unknown plugin";
                string newPluginName = plugin?.Name ?? "An unknown plugin";
                DiscordExtension.GlobalLogger.Warning($"{newPluginName} has replaced the '{commandName}' discord guild command previously registered by {previousPluginName}");
            }

            cmd = new GuildCommand(commandName, plugin, allowedChannels, callback);

            // Add the new command to collections
            GuildCommands[commandName] = cmd;

            // Hook the unload event
            if (plugin != null && !_pluginRemovedFromManager.ContainsKey(plugin))
            {
                _pluginRemovedFromManager[plugin] = plugin.OnRemovedFromManager.Add(OnPluginRemovedFromManager);
            }
        }

        /// <summary>
        /// Removes a previously registered discord command
        /// Sourced From Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L286)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="plugin"></param>
        [LibraryFunction(nameof(RemoveDiscordCommand))]
        public void RemoveDiscordCommand(string command, Plugin plugin)
        {
            BaseCommand cmd = DirectMessageCommands.Values.Where(x => Equals(x.Plugin, plugin)).FirstOrDefault(x => x.Name == command);
            if (command != null)
            {
                RemoveDiscordCommand(cmd);
            }
            
            cmd = GuildCommands.Values.Where(x => Equals(x.Plugin, plugin)).FirstOrDefault(x => x.Name == command);
            if (command != null)
            {
                RemoveDiscordCommand(cmd);
            }
        }

        /// <summary>
        /// Removes a discord command
        /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L314)
        /// </summary>
        /// <param name="command"></param>
        private void RemoveDiscordCommand(BaseCommand command)
        {
            DirectMessageCommands.Remove(command.Name);
            GuildCommands.Remove(command.Name);
        }

        /// <summary>
        /// Called when a plugin has been removed from manager
        /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L377)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="manager"></param>
        private void OnPluginRemovedFromManager(Plugin sender, PluginManager manager)
        {
            // Remove all discord commands which were registered by the plugin
            foreach (DirectMessageCommand cmd in DirectMessageCommands.Values.Where(c => Equals(c.Plugin, sender)).ToArray())
            {
                RemoveDiscordCommand(cmd);
            }
            
            foreach (GuildCommand cmd in GuildCommands.Values.Where(c => Equals(c.Plugin, sender)).ToArray())
            {
                RemoveDiscordCommand(cmd);
            }


            // Unhook the event
            if (_pluginRemovedFromManager.TryGetValue(sender, out Event.Callback<Plugin, PluginManager> callback))
            {
                callback.Remove();
                _pluginRemovedFromManager.Remove(sender);
            }
        }

        /// <summary>
        /// Handles the specified direct message command
        /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L361)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="channel"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <param name="message"></param>
        internal bool HandleDirectMessageCommand(BotClient client, DiscordMessage message, Channel channel, string name, string[] args)
        {
            DirectMessageCommand command = DirectMessageCommands[name];
            if (command == null || !command.CanHandle(message, channel))
            {
                return false;
            }

            if (!client.IsPluginRegistered(command.Plugin))
            {
                return false;
            }

            command.HandleCommand(message, name, args);
            return true;
        }

        /// <summary>
        /// Handles the specified direct message command
        /// Sourced from Command.cs of OxideMod (https://github.com/OxideMod/Oxide.Rust/blob/develop/src/Libraries/Command.cs#L361)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="channel"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <param name="message"></param>
        internal bool HandleGuildCommand(BotClient client, DiscordMessage message, Channel channel, string name, string[] args)
        {
            GuildCommand command = GuildCommands[name];
            if (command == null || !command.CanHandle(message, channel))
            {
                return false;
            }

            if (!client.IsPluginRegistered(command.Plugin))
            {
                return false;
            }

            command.HandleCommand(message, name, args);
            return true;
        }
    }
}