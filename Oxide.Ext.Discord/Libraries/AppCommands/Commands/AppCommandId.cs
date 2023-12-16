using System;
using System.Text;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Libraries.Pooling;

namespace Oxide.Ext.Discord.Libraries.AppCommands.Commands
{
    internal struct AppCommandId : IEquatable<AppCommandId>
    {
        public readonly InteractionType Type;
        public readonly string Command;
        public readonly string Group;
        public readonly string SubCommand;
        public readonly string Argument;

        public AppCommandId(InteractionType type, string command, string group = null, string subCommand = null, string argument = null)
        {
            if(string.IsNullOrEmpty(command)) throw new ArgumentNullException(nameof(command));

            Type = type;
            Command = command;
            Group = group;
            SubCommand = subCommand;
            Argument = argument;
        }
        
        public static bool operator == (AppCommandId left, AppCommandId right) => left.Equals(right);
        public static bool operator !=(AppCommandId left, AppCommandId right) => !(left == right);

        public bool Equals(AppCommandId other) => Type == other.Type && Command == other.Command && Group == other.Group && SubCommand == other.SubCommand && Argument == other.Argument;

        public override bool Equals(object obj) => obj is AppCommandId other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int)Type;
                hashCode = (hashCode * 397) ^ (Command != null ? Command.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Group != null ? Group.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SubCommand != null ? SubCommand.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Argument != null ? Argument.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        public override string ToString()
        {
            StringBuilder sb = DiscordPool.Internal.GetStringBuilder();
            sb.Append(EnumCache<InteractionType>.Instance.ToString(Type));
            sb.Append(':');
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

            return DiscordPool.Internal.ToStringAndFree(sb);
        }
    }
}