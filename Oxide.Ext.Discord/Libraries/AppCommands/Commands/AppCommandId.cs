using System;
using System.Collections.Generic;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal struct AppCommandId
    {
        public readonly string Command;
        public readonly string Group;
        public readonly string SubCommand;

        public static IEqualityComparer<AppCommandId> AppCommandIdComparer { get; } = new AppCommandIdEqualityComparer();
        
        public AppCommandId(string command, string group = null, string subCommand = null)
        {
            if(string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));
            
            Command = command;
            Group = group;
            SubCommand = subCommand;
        }

        private sealed class AppCommandIdEqualityComparer : IEqualityComparer<AppCommandId>
        {
            public bool Equals(AppCommandId x, AppCommandId y)
            {
                return string.Equals(x.Command, y.Command, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Group, y.Group, StringComparison.OrdinalIgnoreCase) && string.Equals(x.SubCommand, y.SubCommand, StringComparison.OrdinalIgnoreCase);
            }
            
            public int GetHashCode(AppCommandId obj)
            {
                unchecked
                {
                    int hashCode = StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Command);
                    hashCode = (hashCode * 397) ^ (obj.Group != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Group) : 0);
                    hashCode = (hashCode * 397) ^ (obj.SubCommand != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(obj.SubCommand) : 0);
                    return hashCode;
                }
            }
        }
    }
}