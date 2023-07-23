using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Plugins.Core.AppCommands
{
    internal class CommandCache
    {
        public readonly List<DiscordApplicationCommand> Commands;
        private readonly DateTime _cachedDateTime = DateTime.UtcNow;

        public CommandCache(List<DiscordApplicationCommand> commands)
        {
            Commands = commands;
        }
        
        public bool IsExpired => _cachedDateTime + TimeSpan.FromSeconds(15) < DateTime.UtcNow;

        public void RemoveCommand(Snowflake id)
        {
            for (int index = 0; index < Commands.Count; index++)
            {
                DiscordApplicationCommand command = Commands[index];
                if (command.Id == id)
                {
                    Commands.RemoveAt(index);
                    break;
                }
            }
        }
    }
}