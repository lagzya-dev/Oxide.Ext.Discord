using System;
using System.Collections.Generic;
using System.Text;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal struct AppCommandId
    {
        public readonly string Command;
        public readonly string Group;
        public readonly string SubCommand;
        public readonly string Argument;

        public static IEqualityComparer<AppCommandId> AppCommandIdComparer { get; } = new AppCommandIdEqualityComparer();
        
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

        private sealed class AppCommandIdEqualityComparer : IEqualityComparer<AppCommandId>
        {
            public bool Equals(AppCommandId x, AppCommandId y)
            {
                return string.Equals(x.Command, y.Command, StringComparison.OrdinalIgnoreCase) 
                       && string.Equals(x.Group, y.Group, StringComparison.OrdinalIgnoreCase) 
                       && string.Equals(x.SubCommand, y.SubCommand, StringComparison.OrdinalIgnoreCase)
                       && string.Equals(x.Argument, y.Argument, StringComparison.OrdinalIgnoreCase);
            }
            
            public int GetHashCode(AppCommandId obj)
            {
                unchecked
                {
                    int hashCode = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Command);
                    hashCode = (hashCode * 397) ^ (obj.Group != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Group) : 0);
                    hashCode = (hashCode * 397) ^ (obj.SubCommand != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(obj.SubCommand) : 0);
                    hashCode = (hashCode * 397) ^ (obj.Argument != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Argument) : 0);
                    return hashCode;
                }
            }
        }
    }
}