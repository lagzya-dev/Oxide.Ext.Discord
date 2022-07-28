using System;
using Oxide.Ext.Discord.Entities.Interactions;

namespace Oxide.Ext.Discord.Attributes.ApplicationCommands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DiscordSlashCommandAttribute : BaseApplicationCommandAttribute
    {
        public readonly string Group;
        public readonly string SubCommand;

        public DiscordSlashCommandAttribute(string command, string group = null, string subCommand = null) : base(InteractionType.ApplicationCommand, command)
        {
            Group = group;
            SubCommand = subCommand;
        }
    }
}