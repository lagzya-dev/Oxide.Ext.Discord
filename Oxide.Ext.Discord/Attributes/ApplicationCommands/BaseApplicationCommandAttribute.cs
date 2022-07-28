using System;
using Oxide.Ext.Discord.Entities.Interactions;

namespace Oxide.Ext.Discord.Attributes.ApplicationCommands
{
    public abstract class BaseApplicationCommandAttribute : Attribute
    {
        public readonly InteractionType Type;
        public readonly string Command;

        public BaseApplicationCommandAttribute(InteractionType type, string command)
        {
            if(string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            
            Type = type;
            Command = command;
        }
    }
}