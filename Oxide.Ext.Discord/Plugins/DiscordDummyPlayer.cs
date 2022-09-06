using System;
using System.Globalization;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Ext.Discord.Plugins
{
    internal class DiscordDummyPlayer : IPlayer
    {
        private static readonly GenericPosition Default = new GenericPosition();
        
        public string Id { get; }
        
        public string Name 
        {
            get => "Unknown Player"; 
            set { }
        }
        
        public object Object => null;
        public string Address => "0.0.0.0";
        public int Ping => 0;
        public CultureInfo Language => CultureInfo.GetCultureInfo("en");
        public bool IsConnected => false;
        public bool IsSleeping => false;
        public bool IsServer => false;
        public bool IsAdmin => false;
        public bool IsBanned => false;
        public TimeSpan BanTimeRemaining => TimeSpan.Zero;

        public float Health
        {
            get => 0; 
            set {}
        }

        public float MaxHealth 
        {  
            get =>  0; 
            set {}
        }
        
        public CommandType LastCommand
        {
            get => CommandType.Chat; 
            set { }
        }

        public DiscordDummyPlayer(string id)
        {
            Id = id;
        }

        public void Ban(string reason, TimeSpan duration = new TimeSpan()) {}
        public void Heal(float amount) {}
        public void Hurt(float amount) {}
        public void Kick(string reason) {}
        public void Kill() {}
        public void Rename(string name) {}
        public void Teleport(float x, float y, float z) {}
        public void Teleport(GenericPosition pos) {}
        public void Unban() {}
        public void Message(string message, string prefix, params object[] args) {}
        public void Message(string message) {}
        public void Reply(string message, string prefix, params object[] args) {}
        public void Reply(string message) {}
        public void Command(string command, params object[] args) {}
        public bool HasPermission(string perm) => false;
        public void GrantPermission(string perm) {}
        public void RevokePermission(string perm) {}
        public bool BelongsToGroup(string group) => false;
        public void AddToGroup(string group) {}
        public void RemoveFromGroup(string group) {}
        
        public void Position(out float x, out float y, out float z)
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public GenericPosition Position() => Default;
    }
}