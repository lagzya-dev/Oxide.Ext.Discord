using System;
using System.Text;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal struct AppCommandId : IEquatable<AppCommandId>
    {
        public readonly string Command;
        public readonly string Group;
        public readonly string SubCommand;
        public readonly string Argument;

        public AppCommandId(string command, string group = null, string subCommand = null, string argument = null)
        {
            if(string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            
            Command = command;
            Group = group;
            SubCommand = subCommand;
            Argument = argument;
        }

        public override string ToString()
        {
            StringBuilder sb = DiscordPool.GetStringBuilder();
            sb.Append(Command);
            if (!string.IsNullOrEmpty(Group))
            {
                sb.Append('/');
                sb.Append(Group);
            }
            if (!string.IsNullOrEmpty(SubCommand))
            {
                sb.Append('/');
                sb.Append(SubCommand);
            }
            if (!string.IsNullOrEmpty(Argument))
            {
                sb.Append('#');
                sb.Append(Argument);
            }

            string cmd = sb.ToString();
            DiscordPool.FreeStringBuilder(ref sb);

            return cmd;
        }

        public bool Equals(AppCommandId other)
        {
            return string.Equals(Command, other.Command, StringComparison.OrdinalIgnoreCase) 
                   && string.Equals(Group, other.Group, StringComparison.OrdinalIgnoreCase) 
                   && string.Equals(SubCommand, other.SubCommand, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(Argument, other.Argument, StringComparison.OrdinalIgnoreCase);
        }
        
        public override bool Equals(object obj)
        {
            return obj is AppCommandId other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Command != null ? Command.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Group != null ? Group.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SubCommand != null ? SubCommand.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Argument != null ? Argument.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}